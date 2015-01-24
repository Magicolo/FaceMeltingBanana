using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class KeySnake : StateLayer {

	public int index;
	public bool debug;
	
	[Disable] public KeySnake nextKey;
	[Disable] public bool isNextKey;
	[Disable] public PuzzleSnake puzzle;
}

