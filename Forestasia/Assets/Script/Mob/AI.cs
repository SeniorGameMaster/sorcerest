using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AdvancedMovement))]
[RequireComponent (typeof(SphereCollider))]
public class AI : MonoBehaviour {
	private enum State {
		Idle,			//do nothing
		Init,			//checking the value that we need to use
		Setup,			//assign all value
		Search,			//finding player
		Decide,			//decide what to do with target	
		Walk,			//Make Mob walk forward
		Back,			//Make Mobe back home
		Dead			//Mob doesn't update anything because it dead

	}
	
	public float perceptionRadius = 10;
	public float minHomeDistance = 3;
	public float maxHomeDistance = 15;
	
	//public float baseMeleeRange = 3.5f;
	public Transform target;
	
	public bool randomWalk = false;
	private float _walkTimer = 0;	//timer for counting walking
	private float _walkTime;		//Random value that assign the time length when walk
	private float _rotateTimer = 0;	//timer for counting rotation
	private float _rotateTime = 6;		//Random value that assign the tine length when rotate
	private Quaternion rot;			//Random quaternion value in y-axis in range 0 - 360 degree
	private float _randomTimer = 0;	//timer for counting random behavior
	private float _randomTime = 3;	//Random time in random monster behavior
	private int _randomBehavior;	//The variable that check the monster behavior
	
	private Transform _myTransform;
	
	private const float ROTATION_DAMP = .3f;
	private const float FORWARD_DAMP = .9f;
	
	private Transform _home;
	private State _state;
	private SphereCollider _sphereCollider;
	
	private Mob _me;
	
	void Awake() {
		_me = gameObject.GetComponent<Mob>();
	}
	
	void Start() {
		_state = AI.State.Init;
		StartCoroutine("FSM");
	}
	
	void Update() {
		if(randomWalk) {
			if(_state != State.Search && _state != State.Decide && _state != State.Dead){
				Behavior();
				FarHome();
			}
		}
	}
	
	//Finite state machine
	private IEnumerator FSM() {
		while(_state != AI.State.Idle) {
			switch(_state) {
			case State.Init :
				Init();
				break;
			case State.Setup :
				Setup();
				break;
			case State.Search :
				Search();
				break;	
			case State.Decide :
				Decide();
				break;
			case State.Walk :
				Walk();
				break;
			case State.Back :
				BackHome();
				break;
			}
			
			yield return null;
		}
	}
	
	private void Init() {
		_myTransform = transform;
		_home = transform.parent.transform;

		_sphereCollider = GetComponent<SphereCollider>();
		
		if(_sphereCollider == null) {
			Debug.Log("There is no spherecollider on this mob");
			return;
		}
		
		_state = AI.State.Setup;
	}
	
	private void Setup() {
		_sphereCollider.center = GetComponent<CharacterController>().center;
		_sphereCollider.radius = perceptionRadius;
		_sphereCollider.isTrigger = true;
		
		_state = AI.State.Idle;
		
	}
	
	private void Behavior() {
		if(_state != AI.State.Walk)
			_randomTimer += Time.deltaTime;
		
			if(_randomTimer > _randomTime) {
				_randomTimer = 0;
				_randomBehavior = Random.Range(0,5);
				switch (_randomBehavior) {
				case 0:case 1:	
					//_isWalk = false;	
					_state = AI.State.Walk;
					_walkTime = Random.Range(1,4);
					StartCoroutine("FSM");
				break;
				case 2:case 3:	case 4:	
					_rotateTime = Random.Range(8,12);
					//_isWalk = true;	
				break;
				default:break;
				}
			}
		
		_rotateTimer += Time.deltaTime;
		
		if(_rotateTimer > _rotateTime) {
			_rotateTimer = 0;
			 rot = Quaternion.Euler( 0, Random.Range(0, 360), 0);
			
		}
		
		_myTransform.rotation = Quaternion.Slerp(_myTransform.rotation, rot, Time.deltaTime * 3.0f);
	}
	
	private void FarHome() {
		if(Vector3.Distance(_myTransform.position, _home.position) > maxHomeDistance) {
			target = _home;
			_state = State.Back;
			StartCoroutine("FSM");
		}
	}
	
	private void BackHome() {
		Move();
		
		if(Vector3.Distance(_myTransform.position, _home.position) < minHomeDistance) {
			target = null;
			_state = State.Idle;
		}

	}
	
	private void Walk() {
		
		_walkTimer += Time.deltaTime;
		SendMessage("MoveMeForward", AdvancedMovement.Forward.forward);	
		
		if(_walkTimer > _walkTime) {
			_walkTimer = 0;
			_state = AI.State.Idle;
			SendMessage("MoveMeForward", AdvancedMovement.Forward.none);	
		}
	}
	
	private void Search() {
		if(target == null) {
			Debug.Log("No target that monster select");
			
			if(_me.InCombat)
				_me.InCombat = false;
			
			
			_state = AI.State.Idle;
			
		}
		else {
			_state = AI.State.Decide;
			
			if(!_me.InCombat)
				_me.InCombat = true;
		}	
		
	}
	
	private void Decide() {
		Move();
		
		int opt = 0;
		
		if(target != null && target.CompareTag("Player")) {
			if(Vector3.Distance(_myTransform.position, target.position) < GameSettings.BASE_MAGIC_RANGE && _me.meeleeResetTimer <= 0) {
				opt = Random.Range(0,3);
			}
			else {
				
				if(_me.meeleeResetTimer > 0)
					_me.meeleeResetTimer -= Time.deltaTime;
				
				opt = Random.Range(1,3);	
			}
			
			switch (opt) {
			case 0:
				MeleeAttack();
				break;
			case 1:
				MagicAttack();
				break;
			case 2: 
				RangeAttack();
				break;
			default:
				Debug.Log("Option:" + opt + " is not defined."); 
				break;
				
			//add cases for:
				//retreat - run to neareast mob
				//flee - just run away from the player
				
			}
			
			_state = AI.State.Search;
		}
	}
	
	private void MeleeAttack() {
		//set attacktimer to the meleeattacktimer
		_me.meeleeResetTimer = _me.meeleeAttackTimer;
		
		SendMessage("PlayMeleeAttack");
		
		if(true) {
		
		}
	}
	
	private void MagicAttack() {
		
	}
	
	private void RangeAttack() {
		
	}
	/*
	private void Attack() {
		Move();
		_state = AI.State.Retreat;
	}
	
	private void Retreat() {
		_myTransform.LookAt(target);
		Move(); 
		_state = AI.State.Search;
	}
	
	private void Flee() {
		Move();
		_state = AI.State.Search;
	}
	*/
	
	private void Move() {
		/*	
						myTarget = target.position;
						myTransform = curTransform.position;
						myTarget.y = 0;
						myTransform.y = 0;
						curTransform.rotation = Quaternion.Slerp(curTransform.rotation, Quaternion.LookRotation(myTarget - myTransform),rotationSpeed);
					
						//Calculate direction to target
						targetDirection = (target.position - curTransform.position);
						movement = targetDirection.normalized * moveSpeed + new Vector3 (0, verticalSpeed, 0);
		*/
		
		
		if(target) {	
			float dist = Vector3.Distance(target.position,_myTransform.position);	
			
			if(target.name == "Spawn Point") {
				//Debug.LogWarning("Returning Home: " + dist);
				
				if(dist < GameSettings.BASE_MELEE_RANGE) {
					target = null;
					_state = AI.State.Idle;	
						
					SendMessage("MoveMeForward", AdvancedMovement.Forward.none);
					SendMessage("RotateMe", AdvancedMovement.Turn.none);
					
					return;
				}
			}
			Vector3 myTarget, myTransform;
			myTarget = target.position;
			myTransform = _myTransform.position;
			myTarget.y = 0;
			myTransform.y = 0;	
			
			Quaternion rot = Quaternion.LookRotation(myTarget - myTransform);
			_myTransform.rotation = Quaternion.Slerp(_myTransform.rotation, rot, Time.deltaTime * 6.0f);
			
			Vector3 dir = (target.position - _myTransform.position).normalized;
			float direction = Vector3.Dot(dir, transform.forward);
		 	

		
			if(direction > FORWARD_DAMP && dist > GameSettings.BASE_MELEE_RANGE) {
				SendMessage("MoveMeForward", AdvancedMovement.Forward.forward);	
			}
			else {
				SendMessage("MoveMeForward", AdvancedMovement.Forward.none);	
			}
			
			/*
			dir = (target.position - _myTransform.position).normalized;
			direction = Vector3.Dot(dir, transform.right);
			
			if(direction > ROTATION_DAMP) {
				SendMessage("RotateMe", AdvancedMovement.Turn.right);	
				}
			else if(direction < -ROTATION_DAMP) {
				SendMessage("RotateMe", AdvancedMovement.Turn.left);	
			}
			else {
				SendMessage("RotateMe", AdvancedMovement.Turn.none);	
			}*/
		}
		else {
			SendMessage("MoveMeForward", AdvancedMovement.Forward.none);
			SendMessage("RotateMe", AdvancedMovement.Turn.none);
		}	
	}
	
	public void setAIDead() {
		_state = AI.State.Dead;
		//StartCoroutine("FSM");
		_myTransform.GetComponent<LootingItem>().OnMouseExit();
		Debug.Log("DEAD");
	}
	
	public bool isAIDead() {
		if(_state == AI.State.Dead)
			return true;
		else
			return false;
	}
	public void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player") && _state != AI.State.Dead) {
			target = other.transform;
			PlayerCharacter.Instance.InCombat = true;
			_state = AI.State.Search;
			StartCoroutine("FSM");
		}
			//_state = AI.State.Dead;
		
		
	}
	
	public void OnTriggerExit(Collider other) {
		if(other.CompareTag("Player") && _state != AI.State.Dead) {
			target = _home;
			PlayerCharacter.Instance.InCombat = false;
			
			if(_me.InCombat)
				_me.InCombat = false;
		}
	}
	
}
