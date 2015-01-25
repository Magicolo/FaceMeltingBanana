using UnityEngine;
using System.Collections;

public class LEmming : MonoBehaviour {

	public Vector3 direction;
	public float speed = 3;
	private float timeIgnore = 0 ;
	
	void Start () {
	
	}
	
	
	void Update () {
		this.transform.position += direction * speed * Time.deltaTime;
		if(this.transform.position.y <= -2){
			Object.Destroy(this.gameObject);
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if(timeIgnore >= 0 ){
			timeIgnore -= Time.deltaTime;
			return;
		}
		
        foreach (ContactPoint contact in collision.contacts) {
			if(contact.otherCollider.name == "Pillar"){
				timeIgnore = 1;
				if(direction.x != 0){
					this.transform.position += new Vector3( -Mathf.Sign(direction.x) * 0.5f, 0, 0);
					direction = new Vector3(0,0,1);
				}else{
					this.transform.position += new Vector3( 0, 0, -Mathf.Sign(direction.z) * 0.5f);
					direction = new Vector3(1,0,0);
				}
				break;
			}
			
        }
        
    }
}
