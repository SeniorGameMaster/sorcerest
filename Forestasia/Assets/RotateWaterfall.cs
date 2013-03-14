using UnityEngine;
using System.Collections;

public class RotateWaterfall : MonoBehaviour {
	public float yAxis = 1;
	public int ySpeed = 1;
	private Transform _myTransform;
	// Use this for initialization
	void Start () {
		_myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		//_myTransform.RotateAround(new Vector3(0,yAxis,0),ySpeed * Time.deltaTime);
		
		_myTransform.RotateAroundLocal(new Vector3(0,yAxis,0),ySpeed * Time.deltaTime);
		//_myTransform.rotation = Quaternion.Slerp(_myTransform.rotation, Quaternion.LookRotation(_myTransform.position + new Vector3(0,yAxis,0)),ySpeed * Time.deltaTime);
	}
}
