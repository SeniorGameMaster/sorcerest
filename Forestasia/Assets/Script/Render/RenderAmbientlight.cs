using UnityEngine;
using System.Collections;

public class RenderAmbientlight : MonoBehaviour {
	
	public Color setAmbientLight;
	private Color _defaultAmbientColor;
	void Start() {
		_defaultAmbientColor = RenderSettings.ambientLight;
	}
	
	public void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")) {
			RenderSettings.ambientLight = setAmbientLight;
		}
		
	}
	
	public void OnTriggerExit(Collider other) {
		if(other.CompareTag("Player")) {
			RenderSettings.ambientLight = _defaultAmbientColor;
		}
	}
}
