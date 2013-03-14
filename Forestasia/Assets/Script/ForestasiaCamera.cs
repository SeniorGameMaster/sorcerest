/*
	Rotate Camera Button 				: press to allow to rotate camera
	Mouse X								: rotate camera horizontally with mouse
	Mouse Y								: rotate camera vertically with mouse
	Rotate Camera Horizontal Button		: keyboards buttons to rotate camera in x axis
	Rotate Camera Vertical Button		: keyboards buttons to rotate camera in y axis
	Reset Camera						: reset the camera to default position
 */

using UnityEngine;
using System.Collections;

public class ForestasiaCamera : MonoBehaviour {
	public Transform target;
	public string playerTagName = "Player";
	public float walkDistance;
	public float runDistance;
	public float height;
	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	
	private Transform _myTransform;
	private float _x;
	private float _y;
	private bool _camButtonDown = false;
	private bool _rotateCameraKeyPressed = false;
	
	void Awake() {
		_myTransform = transform;
	}
	// Use this for initialization
	void Start () {
		if(target == null)
			Debug.LogWarning ("We do not have a target for the camera");
		else 
			CameraSetUp();	
	}
	
	void Update () {
		//Use input manager to make the user selectable button
		if(Input.GetButtonDown("Rotate Camera Button")) { 
			_camButtonDown = true;	
		}
		if(Input.GetButtonUp("Rotate Camera Button")) {
			_x = 0;		//reset the x value
			_y = 0;		//reset the y value
			_camButtonDown = false;	
		}
		
		if(Input.GetButtonDown("Rotate Camera Horizontal Buttons") || Input.GetButtonDown("Rotate Camera Vertical Buttons"))
			_rotateCameraKeyPressed = true;
		
		if(Input.GetButtonUp("Rotate Camera Horizontal Buttons") || Input.GetButtonUp("Rotate Camera Vertical Buttons")) {
			_x = 0;		//reset the x value
			_y = 0;		//reset the y value
			_rotateCameraKeyPressed = false;
		}	
	}
	
	void LateUpdate() {

		if(target != null){			//have target then can move camera
			if(_rotateCameraKeyPressed) {
				 _x += Input.GetAxis("Rotate Camera Horizontal Buttons") * xSpeed * 0.02f;
		        _y -= Input.GetAxis("Rotate Camera Vertical Buttons") * ySpeed * 0.02f;
		 		
		 		RotateCamera();
			}
			else if(_camButtonDown) {	//Use the Input Manager	to make this user selectable button		
		        _x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
		        _y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
		 		
		 		RotateCamera();
			}
			else {
				//_myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
				//_myTransform.LookAt(target);
				_x = 0;		//reset the x value
				_y = 0;		//reset the y value
				
				// Calculate the current rotation angles
				float wantedRotationAngle = target.eulerAngles.y;
				float wantedHeight = target.position.y + height;
					
				float currentRotationAngle = _myTransform.eulerAngles.y;
				float currentHeight = _myTransform.position.y;
				
				// Damp the rotation around the y-axis
				currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
				// Damp the height
				currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
				// Convert the angle into a rotation
				Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
				
				// Set the position of the camera on the x-z plane to:
				// distance meters behind the target
				_myTransform.position = target.position;
				_myTransform.position -= currentRotation * Vector3.forward * walkDistance;
			
				// Set the height of the camera
				_myTransform.position = new Vector3(_myTransform.position.x, currentHeight, _myTransform.position.z);
				
				// Always look at the target
				_myTransform.LookAt (target);			
			}
		}	
		else {
			GameObject go = GameObject.FindGameObjectWithTag(playerTagName);
			
			if(go == null)
				return;
			
			target = go.transform;
		}
	}
	
	private void RotateCamera() {
		 Quaternion rotation = Quaternion.Euler(_y, _x,0);       
		 Vector3 position = rotation * new Vector3(0.0f, 0.0f, -walkDistance) + target.position;
	
		 _myTransform.rotation = rotation;	//set the rotation of the camera
		 _myTransform.position = position;	//set the position of the camera	
	}
	
	//set the camera to default position behind the player and facing them
	private void CameraSetUp() {
		_myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
		_myTransform.LookAt(target);
	}
}
