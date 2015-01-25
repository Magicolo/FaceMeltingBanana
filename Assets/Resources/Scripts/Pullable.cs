using UnityEngine;
using System.Collections;

public class Pullable : MonoBehaviour {

	private GameObject player;
	
	private Vector3 pullDirection;
	
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	void OnMouseDown() {
		Vector3 diff = player.transform.position - this.transform.position;
		diff.Normalize();
		if(Mathf.Abs(diff.x) > Mathf.Abs(diff.z)){
			pullDirection = new Vector3(Mathf.Sign(diff.x),0,0);
		}else{
			pullDirection = new Vector3(0,0,Mathf.Sign(diff.z));
		}
		
		if(isInDistance() && canBePulled()){
			pullToPlayer();
		}
	}

	bool isInDistance(){
		Vector3 diff = this.transform.position - player.transform.position;
		return Mathf.Abs(diff.magnitude) <= 4 && Mathf.Abs(diff.magnitude) >= 1.5f;
	}

	bool canBePulled(){
		float x = this.transform.position.x;
		float z = this.transform.position.z;
		float dx = pullDirection.x;
		float dz = pullDirection.z;
		return x + dx < 11 && x + dx > 1 && z + dz < 11 && z + dz > 1 ;
	}
	
	void pullToPlayer(){
		this.transform.position += pullDirection;
	}
}

