using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider))]
public class RenderedAlgorithm : MonoBehaviour {
	public GameObject[] renderSceneObject;
	private GameObject _go;
	
	void Awake() {
		for(int i = 0; i < renderSceneObject.Length; i++) 
			setRendering(renderSceneObject[i], false);
	}
	
	void Update() {
	}
	
	public void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")) {
			for(int i = 0; i < renderSceneObject.Length; i++) 
				setRendering(renderSceneObject[i], true);
		}
		
	}
	
	public void OnTriggerExit(Collider other) {
		if(other.CompareTag("Player")) {
			for(int i = 0; i < renderSceneObject.Length; i++) 
				setRendering(renderSceneObject[i], false);
		}
	}
	
	private void setRendering(GameObject tag,bool flag) {
		_go = Helpers.Find(tag.name,typeof(GameObject)) as GameObject;

		if(_go == null)
			return;

		_go.SetActive(flag);
	}
}
