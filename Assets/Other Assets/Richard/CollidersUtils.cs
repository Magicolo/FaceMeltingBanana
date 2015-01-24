using UnityEngine;
using System.Collections;

public class CollidersUtils  {

	public static Collider2D getCollider(Vector3 rayFrom, Vector3 rayDirection, float maxDistance){
		rayFrom.z = 0;
		RaycastHit2D hit = Physics2D.Raycast(rayFrom, rayDirection);
		if (hit.collider != null) {
			float realDistance = Vector3.Distance(rayFrom,hit.collider.transform.position);
			if(realDistance <= maxDistance){
				return hit.collider;
			}
		}
		return null;
	}
}
