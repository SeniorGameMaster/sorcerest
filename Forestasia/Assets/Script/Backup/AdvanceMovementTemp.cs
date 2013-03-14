using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class AdvanceMovementTemp : MonoBehaviour {
	public float walkSpeed = 5;				//the speed our character walk
	public float runMultiplier = 2;			//the multiply of walk speed to make character run
	public float strafeSpeed = 2.5f;		//the strafe speed of character
	public float rotateSpeed = 250;			//the speed when rotate character
	public float gravity = 20;				//the setting for gravity
	public float airTime = 0;				//how long have we been in the air sinc the last time we touch the ground
	public float fallTime = .5f;			//the length of time we have to be falling before the system knows its a fall
	public float jumpHeight = 8;			//the height of character when jumping
	public float jumpTime = 1.5f;			//the total time when jumping
	
	public CollisionFlags _collisionFlags;	//the collisionFlags we have from the last frame
	private Vector3 _moveDirection;			//The direction our character is moving
	private Transform _myTransform;
	private CharacterController _controller;

	
	public void Awake() {
		_myTransform = transform;							
		_controller = GetComponent<CharacterController>();
	}
	
	void Start () {
		_moveDirection = Vector3.zero;				//zero vector3 use for moving player
		animation.Stop();							//stop animation if there have automatically play 
		animation.wrapMode = WrapMode.Loop;			//set all animation to loop by default
		
		//animation["jump].layer = 1;
		//animation["jump].wrapMode = WrapMode.Once;
		//animation.Play("idle");	
	}
	
	void Update () {
		PlayerAction();
	}
	
	private void PlayerAction() {
		//allow the player to turn left and right
		if(Input.GetButtonDown("Move forward") )
			_myTransform.Rotate(0,Input.GetAxis("Move forward") * Time.deltaTime * rotateSpeed, 0);

		
		//if we are on the ground, let us move
		if(_controller.isGrounded)  {
			airTime = 0;
			
			_moveDirection = new Vector3(Input.GetAxis("Strafe"), 0, Input.GetAxis("Move Forward"));
			_moveDirection = _myTransform.TransformDirection(_moveDirection).normalized;
			_moveDirection *= walkSpeed;
			
			if(Input.GetButton("Move Forward")) {
				if(Input.GetButton("Run")) {
					_moveDirection *= runMultiplier;
					Run();	
				}
				else {
					Walk();
				}
			}
			else if(Input.GetButton("Strafe")) {
				Strafe();
			}
			else {
				Idle();	
			}
				
			if(Input.GetButton("Jump")) {			//if the user press jump key
				if(airTime < jumpTime) {			//if we have not already been in the air so long
					_moveDirection.y += jumpHeight;	
					Jump();
				}
			}
		} 
		else {
			//if we have a collisionFlag and it is Collide Below
			if((_collisionFlags & CollisionFlags.CollidedBelow) == 0) {
				airTime += Time.deltaTime;			//increase the air time
				
				if(airTime > fallTime) {			//if we have been in the air to long
					Fall();	
				}
			}
		}
		
		_moveDirection.y -= gravity * Time.deltaTime;		//apply gravity
		
		//move the character and store any new CollisionFlags we get
		_collisionFlags = _controller.Move(_moveDirection * Time.deltaTime);	
	}
		
	/* ANIMATION CALLING FUNCTION */	
	
	public void Idle() {
		//animation.CrossFade("idle");	
	}
	
	public void Walk() {
		animation.CrossFade("PenguinSpin");	
	}
	
	public void Run() {
		animation.CrossFade("PenguinSpining");	
		//animation["run"].speed = 1.5f
		//animation.CrossFade("run");	
	}
	
	public void Strafe() {
		//animation.CrossFade("Strafe");	
	}
	
	public void Jump() {
		//animation.CrossFade("jump");	
	}
	
	public void Fall() {
		//animation.CrossFade("fall");	
	}
}
