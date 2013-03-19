using UnityEngine;
using System.Collections;

public class LightMyFire : MonoBehaviour {
	
	void Start() {
		light.enabled = false;
	}
	
	public void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")) {
			light.enabled = true;
		}
	}
	
	public void OnTriggerExit(Collider other) {
		if(other.CompareTag("Player")) {
			light.enabled = false;
		}
	}
}
