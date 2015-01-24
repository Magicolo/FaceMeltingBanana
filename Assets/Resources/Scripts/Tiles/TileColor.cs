using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TileColor : StateLayer {

	public Color color;
	public TileColor nextTile;
	public bool isNextTile;
	public bool debug;
	
	MeshRenderer _cubeMeshRenderer;
	public MeshRenderer cubeMeshRenderer { get { return _cubeMeshRenderer ? _cubeMeshRenderer : (_cubeMeshRenderer = GetComponentInChildren<MeshRenderer>()); } }
	
}

