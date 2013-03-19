using UnityEngine;
using System.Collections;

public class RenderSkybox : MonoBehaviour {
	public Material renderSkybox;
	// Use this for initialization
	void Start () {
		//RenderSettings.skybox = renderSkybox;
		//Debug.Log("Rendering Skybox" + renderSkybox.ToString());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")) {
			RenderSettings.skybox = renderSkybox;
		}
		
	}
	
	public void OnTriggerExit(Collider other) {
		//if(other.CompareTag("Player")) {
			
		//}
	}
}
