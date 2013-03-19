using UnityEngine;
using System.Collections;

public class FirePillar : MonoBehaviour {
	
	void Start() {
		particleEmitter.emit = false;	
	}
	
	public void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")) {
			particleEmitter.emit = true;
		}
	}
	
	public void OnTriggerExit(Collider other) {
		if(other.CompareTag("Player")) {
			particleEmitter.emit = false;
		}
	}
}
