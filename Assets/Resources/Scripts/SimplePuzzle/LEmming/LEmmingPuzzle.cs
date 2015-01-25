using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LEmmingPuzzle : SimplePuzzle {

	public GameObject LEmmingPrefab;
	public float spawnRate;
	public float t;
	
	private List<GameObject> lemmings;
	private List<GameObject> lemmingsToRemove;
	public Vector3 startingDirection;
	
	protected override void puzzleInit(){
		lemmings = new List<GameObject>();
		lemmingsToRemove = new List<GameObject>();
	}
	
	void Update(){
		if(t <= 0 ){
			t = spawnRate;
			spawn();
		}
		t-= Time.deltaTime;
	}

	void spawn(){
		GameObjectExtend.createClone(LEmmingPrefab, "Lemming", this.transform, new Vector3(2,0.5f,3));
	}
	
	public override void handleMessage(string source, string message) {
		if(!puzzleActive) return;
		if(source == "btn"){
			foreach (var door in this.doors) {
				if(door.keyword == message){
					door.Open();
				}
			}
		}
	}
}
