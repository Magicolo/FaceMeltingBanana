using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Threading;
using System.Diagnostics;

public abstract class TiledMapLoader {
	
	protected int mapWidth;
	protected int mapHeight;
	
	private XDocument document;
	
	public void loadFromFile(string fileName){
		string text = System.IO.File.ReadAllText(fileName);
        loadLevel(text);
	}
	
	private void loadLevel(string levelFileContent){
		document = XDocument.Parse(levelFileContent);
		
		loadMapAttributes();
        loadMapProperties();
        loadLevelsLayers();
        loadLevelsObjectGroup();
	}

	
	void loadMapAttributes(){
		XElement mapElement = document.Elements().First();
		this.mapWidth  = this.parseInt(mapElement.Attribute("width").Value);
		this.mapHeight = this.parseInt(mapElement.Attribute("height").Value);
		afterMapAttributesLoaded();
	}
	protected abstract void afterMapAttributesLoaded();

	
	
	void loadLevelsObjectGroup(){
		 var objGroups = document.Elements().Descendants().Where(e => e.Name == "objectgroup");
		 foreach (var objGroup in objGroups) {
		 	string name = objGroup.Attribute("name").Value;
		 	foreach (var obj in objGroup.Descendants().Where(e => e.Name == "object")) {
		 		loadObject(obj,name);
		 	}	
		 }
	}
	
	void loadObject(XElement obj, string objectGroupName){
		int x = Int32.Parse(obj.Attribute("x").Value);
		int y = Int32.Parse(obj.Attribute("y").Value);
		var propertiesElemens = obj.Descendants().First(e => e.Name == "properties").Descendants();
		Dictionary<string, string> properties = new Dictionary<string, string>();
		
		foreach (var property in propertiesElemens) {
			string name = property.Attribute("name").Value;
			string value = property.Attribute("value").Value;
			properties.Add(name,value);
		}
		
		addObject(objectGroupName,x,y,properties);
	}
	
	protected abstract void addObject(string objectGroupName, int x, int y, Dictionary<string, string> properties);
	
	#region loadMapLayerRegion
	
	void loadLevelsLayers(){
		 var layers = document.Elements().Descendants().Where(e => e.Name == "layer");
		 foreach (var layer in layers) {
		 	loadLayer(layer);
		 }
	}

	void loadLayer(XElement layer){
		int width = Int32.Parse(layer.Attribute("width").Value);
		int height = Int32.Parse(layer.Attribute("height").Value);
		
		createNewLayer(layer, width, height);
		loadLayerTiles(layer, height);
	}

	void createNewLayer(XElement layer, int width, int height){
		Dictionary<string, string> properties = new Dictionary<string, string>();
		
		if(layer.Elements("properties").Any() ){
			var propertiesElements = layer.Element("properties").Descendants();
			foreach (var property in propertiesElements) {
				string pname = property.Attribute("name").Value;
				string value = property.Attribute("value").Value;
				properties.Add(pname, value);
			}
		}
		
		string name = layer.Attribute("name").Value;
		addLayer(name, width, height, properties);
	}
	
	protected abstract void addLayer(string layerName, int width, int height, Dictionary<string, string> properties);

	void loadLayerTiles(XElement layer, int height){
		string tilesCSV = layer.Element("data").Value;
		string[] tilesLines = tilesCSV.Split(new string[] { "\n\r", "\r\n", "\n", "\r" }, StringSplitOptions.None);
		int y = height;
		for (int i = 1; i <= height; i++) {
			y--;
			loadLayerLine( y, tilesLines[i]);
		}
	}
	
	void loadLayerLine(int y, string tileLine){
		string[] tiles = tileLine.Split(new char[] { ',' }, StringSplitOptions.None);
		int x = 0;
		foreach (string tileId in tiles) {
			if(!tileId.Equals("0") && !tileId.Equals("") && tileId != null){
				int id = parseInt(tileId) - 1;
				addTile(x,y,id);
			}
			x++;
		}
	}

	protected abstract void addTile(int x, int y, int id);
	
	#endregion
	
	
	
	#region loadMapProperties
	
	void loadMapProperties(){
		XElement propertiesElement = document.Elements().Descendants().First(e => e.Name == "properties");
		var properties = propertiesElement.Descendants();
		foreach (var property in properties) {
			string name = property.Attribute("name").Value;
			string value = property.Attribute("value").Value;
			loadMapProperty(name,value);
		}

    	afterMapPropertiesLoaded();
	}
	
	protected abstract void loadMapProperty(string name, string value);
	
	protected abstract void afterMapPropertiesLoaded();
	#endregion
	
	
	
	
	
	protected int parseInt(string intStr){
		try{
			int id = Int32.Parse(intStr);
			return id;
		}catch (OverflowException){
			UnityEngine.Debug.LogError(intStr + " overflow the memory :(");
		}
		return -1;
	}
	
	private void debugLog(string log){
		UnityEngine.Debug.Log(log);
	}
}
