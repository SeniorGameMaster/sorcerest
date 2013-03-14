using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
	
	public bool avaliable = true;  //value currently not being use
	public void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 2);
	}
}
