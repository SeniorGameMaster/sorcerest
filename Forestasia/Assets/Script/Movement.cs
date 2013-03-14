using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour {
	public float moveSpeed = 5;				//the speed our character walk
	public float runMultiplier = 2;			//the multiply of walk speed to make character run
	public float strafeSpeed = 2.5f;		//the strafe speed of character
	public float rotateSpeed = 250;			//the speed when rotate character
	
	private Transform _myTransform;
	private CharacterController _controller;
	
	public void Awake() {
		_myTransform = transform;
		_controller = GetComponent<CharacterController>();
	}

	void Start () {
		animation.wrapMode = WrapMode.Loop;
	}
	
	
	void Update () {
		if(!_controller.isGrounded) {
			_controller.Move(Vector3.down * Time.deltaTime);	
		}
		
		Turn();
		Walk();
		Strafe();
			
	}
	
	private void Turn() {
		if(Mathf.Abs(Input.GetAxis("Rotate Player")) > 0) {
			_myTransform.Rotate(0,Input.GetAxis("Rotate Player") * Time.deltaTime * rotateSpeed, 0);
		}
	}
	
	private void Walk() {
		if(Mathf.Abs(Input.GetAxis("Move Forward")) > 0) {
			if(Input.GetButton("Run")) {
				animation.CrossFade("PenguinSpining"); //run
				_controller.SimpleMove(_myTransform.TransformDirection(Vector3.forward) * Input.GetAxis("Move Forward") * moveSpeed * runMultiplier);		
			}
			else {
				animation.CrossFade("PenguinSpin"); //walk
				_controller.SimpleMove(_myTransform.TransformDirection(Vector3.forward) * Input.GetAxis("Move Forward") * moveSpeed);
			}
		}
		else {
			//animation.CrossFade("PenguinSpin"); idle
		}
	}
	
	private void Strafe() {
		if(Mathf.Abs(Input.GetAxis("Strafe")) > 0) {
			_controller.SimpleMove(_myTransform.TransformDirection(Vector3.right) * Input.GetAxis("Strafe") * strafeSpeed);
		}
	}
}
