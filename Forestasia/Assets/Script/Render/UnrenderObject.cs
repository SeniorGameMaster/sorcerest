using UnityEngine;
using System.Collections;

public class UnrenderObject : MonoBehaviour {
	public GameObject[] renderSceneObject;
	private GameObject _go;
		
	void Awake() {
		
	}
	
	void Start() {
	
		/*
		for(int i = 0; i < renderSceneObject.Length; i++) 
			setRendering(renderSceneObject[i], false);*/
		
	}
	
	void Update() {
		
	}
	
	public void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")) {
			for(int i = 0; i < renderSceneObject.Length; i++) {
				setRendering(renderSceneObject[i], false);
			
			
			}
		}	
	}
	
	public void OnTriggerExit(Collider other) {
		if(other.CompareTag("Player")) {
			for(int i = 0; i < renderSceneObject.Length; i++) 
				setRendering(renderSceneObject[i], true);
		}
	}
	
	private void setRendering(GameObject tag,bool flag) {
		_go = Helpers.Find(tag.name,typeof(GameObject)) as GameObject;

		if(_go == null)
			return;

		_go.SetActive(flag);
		Debug.Log("Unrender" + tag.name);
	}
}
