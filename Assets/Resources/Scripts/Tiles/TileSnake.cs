﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TileSnake : StateLayer {

	public bool debug;
	
	MeshRenderer _cubeMeshRenderer;
	public MeshRenderer cubeMeshRenderer { get { return _cubeMeshRenderer ? _cubeMeshRenderer : (_cubeMeshRenderer = GetComponentInChildren<MeshRenderer>()); } }

	BoxCollider[] _cubeBoxColliders;
	public BoxCollider[] cubeBoxColliders { get { return _cubeBoxColliders ?? (_cubeBoxColliders = GetComponentsInChildren<BoxCollider>()); } }
	
}