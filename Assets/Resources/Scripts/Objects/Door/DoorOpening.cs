using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class DoorOpening : State {

    Door Layer {
		get { return ((Door)layer); }
	}
	
	public AudioSource doorSource;
	
	private float t = 0;
	private Color alphaColor;
	
	public override void OnEnter()
	{
		doorSource.Play();
	}
	
	public override void OnStart() {
		alphaColor = new Color(Layer.baseColor.r, Layer.baseColor.g, Layer.baseColor.b, 0);
	}
	
	
	public override void OnUpdate() {
		t += Time.deltaTime;
		if(t >= 1 ){
			Layer.cubeMeshRenderer.material.color = new Color(0,0,0,0);
			Layer.cubeMeshRenderer.enabled = false;
			Layer.cube.GetComponent<BoxCollider>().enabled = false;
			SwitchState<DoorOpen>();
		}else{
			Layer.cubeMeshRenderer.material.color = Color.Lerp(Layer.baseColor, alphaColor, t);
		}
	}
}

