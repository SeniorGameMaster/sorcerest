using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {
	private const string DEFAULT_WARP_POINT = "Player Position";
	public GameObject destination;
	private Vector3 dropPosition;
	// Use this for initialization
	void Start () {
		if(destination == null)
			destination = GameObject.Find(DEFAULT_WARP_POINT);
		
		dropPosition = new Vector3(destination.transform.position.x, destination.transform.position.y + 3, destination.transform.position.z - 7);
	}
	
	public void OnTriggerEnter(Collider other) {
		if(other.transform.CompareTag("Player")) 
			other.transform.position = dropPosition;
	}
}
