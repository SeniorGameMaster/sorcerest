using UnityEngine;
using System.Collections;

public class RotatingWarp : MonoBehaviour {
	//public enum RotationAxis;
	public float xAxis;
	public float yAxis;
	public float zAxis;
	public float speed;
	private Transform _myTransform;
	// Use this for initialization
	void Start () {
		_myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		_myTransform.RotateAroundLocal(new Vector3(xAxis,yAxis,zAxis),speed * Time.deltaTime);
	}
}
