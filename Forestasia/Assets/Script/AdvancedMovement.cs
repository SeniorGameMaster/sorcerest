using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class AdvancedMovement : MonoBehaviour {
	public enum State {
		Idle,
		Init,
		Setup,
		Run
	}
	public enum Turn {
		left = -1,
		none = 0,
		right = 1
	}
	public enum Forward {
		back = -1,
		none = 0,
		forward = 1
	}
	
	public string walkAnimName;
	public string runAnimName;
	public string jumpAnimName;
	public string idleAnimName;
	
	public AnimationClip meleeAttack;
	/*
	public string strafeAnimName;
	public string fallAnimName;
	*/
	
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
	
	private Turn _turn;
	private Forward _forward;
	private Turn _strafe;
	private bool _run;
	private bool _jump;
	
	private State _state;
	private BaseCharacter pc;
	
	public void Awake() {
		_myTransform = transform;							
		_controller = GetComponent<CharacterController>();
		_state = AdvancedMovement.State.Init;	
	 	pc = GetComponent<BaseCharacter>();
	}
	
	IEnumerator Start () {
		while(true) {
			switch(_state) {
			case State.Init :
				Init();
				break;
			case State.Setup :
				SetUp();
				break;
			case State.Run :
				PlayerAction();
				break;
			}
			yield return null;
		}	
	}
	
	private void Init() {
		if(!GetComponent<CharacterController>())	return;
		//if(!GetComponent<Animation>())	return;
		
		_state = AdvancedMovement.State.Setup;
	}
	
	private void SetUp() {
		_moveDirection = Vector3.zero;				//zero vector3 use for moving player
		animation.Stop();							//stop animation if there have automatically play 
		animation.wrapMode = WrapMode.Loop;			//set all animation to loop by default
		
		animation[jumpAnimName].layer = 1;
		animation[jumpAnimName].wrapMode = WrapMode.Once;
		animation.Play(idleAnimName);
		
		_turn = AdvancedMovement.Turn.none;
		_forward = AdvancedMovement.Forward.none;
		_strafe = Turn.none;
		_run = false;
		_jump = false;
		
		_state = AdvancedMovement.State.Run;
	}
	
	private void PlayerAction() {
		//allow the player to turn left and right
		_myTransform.Rotate(0,(int)_turn * Time.deltaTime * rotateSpeed, 0);

		
		//if we are on the ground, let us move
		if(_controller.isGrounded)  
		{
			//reset the air time if we are on the ground
			airTime = 0;
			
			//get the user input if we shoud be moving forward or side ways
			//we will calculate a new vector3 for where the player needs to be
			_moveDirection = new Vector3((int)_strafe, 0, (int)_forward);
			_moveDirection = _myTransform.TransformDirection(_moveDirection).normalized;
			_moveDirection *= walkSpeed;
			
			if(_forward != Forward.none) {
				if(_run) {
					_moveDirection *= runMultiplier;
					Run();	
				}
				else {
					Walk();
				}
			}
			else if(_strafe != AdvancedMovement.Turn.none) {
				Side();
			}
			else {
				Idle();	
			}
				
			if(_jump) {			//if the user press jump key
				if(airTime < jumpTime) {			//if we have not already been in the air so long
					_moveDirection.y += jumpHeight;	
					Jump();
					_jump = false;
				}
			}
		} 
		else 
		{
			
				/*		
			//if we have a collisionFlag and it is Collide Below
			if((_collisionFlags & CollisionFlags.CollidedBelow) == 0) {
				airTime += Time.deltaTime;			//increase the air time
				
				if(airTime > fallTime) {			//if we have been in the air to long
					Fall();	
				}
			}
			*/
		}
		
		_moveDirection.y -= gravity * Time.deltaTime;		//apply gravity
		
		
		//move the character and store any new CollisionFlags we get
		_collisionFlags = _controller.Move(_moveDirection * Time.deltaTime);	
	}
	
	public void MoveMeForward(Forward z) {
		_forward = z;	
	}
	
	public void ToggleRun() {
		_run = !_run;
	}
	
	public void RotateMe(Turn y) {
		_turn = y;	
	}
	
	public void Strafe(Turn x) {
		_strafe = x;	
	}
	
	public void JumpUp() {
		_jump = true;	
	}
	/* ANIMATION CALLING FUNCTION */	
	
	public void Idle() {
		if(idleAnimName == "")
			return;
		/*
		
		if(!pc._inCombat) {
			animation.CrossFade(idleAnimName);
		}
		*/
		if(!animation[meleeAttack.name].enabled)
			animation.CrossFade(idleAnimName);		
	}
	
	public void Walk() {
		if(walkAnimName == "")
			return;
		
		animation.CrossFade(walkAnimName);	
	}
	
	public void Run() {
		if(runAnimName == "")
			return;
		
		animation.CrossFade(runAnimName);
	//	animation.CrossFade("PenguinSpining");	
		//animation["run"].speed = 1.5f
		//animation.CrossFade("run");	
	}
	
	public void Side() {

		//animation.CrossFade("side");	
	}
	
	public void Jump() {
		if(jumpAnimName == "")
			return;
		
		animation.CrossFade(jumpAnimName);
		//animation.CrossFade("jump");	
	}
	
	public void Fall() {
		//animation.CrossFade(fallAnimName);	
	}
	
	public void PlayMeleeAttack() {
		
		animation[meleeAttack.name].wrapMode = WrapMode.Once;
		
		if(meleeAttack == null) {
			Debug.LogWarning("Don't have attack animation");
			return;
		}
		
		//animation[meleeAttack.name].speed = animation[meleeAttack.name].length / 2f;
		
		animation.Play(meleeAttack.name);
		
		//animation[meleeAttack.name].speed = animation[meleeAttack.name].speed * 2f;
	}
}
