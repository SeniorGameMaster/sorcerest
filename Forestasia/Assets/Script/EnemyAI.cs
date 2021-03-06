using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public enum EnemyState {
		Idle = 0,
		Walk = 1,
		Attract = 2,
		Attack = 3,
		Dead = 4,	
	}
	
	public enum EnemyBehavior {
		Basic,
		Attack
	}
	
	public EnemyState enemyState = EnemyState.Idle;
	public EnemyBehavior enemyBehavior;
	public Transform target;
	public float walkSpeed = 1.0f;
	public float runSpeed = 3.0f;
	public int rotationSpeed = 1;
	public int maxDistance = 2;
	public int minSeeing = 15;
	public int maxSeeing = 30;
	public float gravity = 20.0f;
	public float speedSmoothing = 10.0f;
	public int randomTimeWalking = 6;
	public int randomTimeDirection = 3;
	
	private Transform curTransform;
	private Vector3 myTarget,myTransform,targetDirection,movement;
	private CollisionFlags collisionFlags; 
	private float moveSpeed = 0.0f;
	private float verticalSpeed;
	private float curSmooth;
	private float curSpeed;
	private float waitTimeWalking;
	private float waitTimeDirection;
	private int randomState;
	private int randomDirection;
	private int rotateEnemy;
	
	void CalculateGravity(){
		if (IsGrounded ()) 
			verticalSpeed = 0.0f;
		else
			verticalSpeed -= gravity * Time.deltaTime;
	}
	
	bool IsGrounded () {
		return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
	}
	
	
	void Awake() {
		curTransform = transform;	
	}
	
	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.FindGameObjectWithTag("Player");
		target = obj.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(target.position, curTransform.position, Color.yellow);
		
		CalculateGravity();
		
		//Check Enemy Die or not

		if(curTransform.GetComponent<EnemyHealth>().isDead && enemyState != EnemyState.Dead) {
			enemyState = EnemyAI.EnemyState.Dead;
			renderer.material.SetColor("_Color", Color.black);
		}
		
			switch(enemyState)
			{
				case EnemyState.Idle :
				case EnemyState.Walk :
					
					curSpeed = walkSpeed;
						
					waitTimeWalking += Time.deltaTime;
					waitTimeDirection += Time.deltaTime;
					//Random state Idle or Walk
					if(waitTimeWalking > randomTimeWalking) {
						waitTimeWalking = 0;
						randomState = Random.Range(0,4);
						
						switch (randomState)
						{
						case 0:
						case 1:	
							enemyState = EnemyState.Idle;
							break;
						case 2:
						case 3:	
						case 4:	
							enemyState = EnemyState.Walk;	
							break;
						default:break;
						}
					}
					
					//Random direction
					if(waitTimeDirection > randomTimeDirection) {
						waitTimeDirection = 0;
						randomDirection = Random.Range(0,7);
			
						switch (randomDirection)
						{
						case 0:
							curTransform.rotation = Quaternion.Slerp(curTransform.rotation, Quaternion.LookRotation(new Vector3(1,0,0)),rotationSpeed);
							break;
						case 1:
							curTransform.rotation = Quaternion.Slerp(curTransform.rotation, Quaternion.LookRotation(new Vector3(-1,0,0)),rotationSpeed);
							break;
						case 2:
							curTransform.rotation = Quaternion.Slerp(curTransform.rotation, Quaternion.LookRotation(new Vector3(0,0,1)),rotationSpeed);
							break;
						case 3:
							curTransform.rotation = Quaternion.Slerp(curTransform.rotation, Quaternion.LookRotation(new Vector3(0,0,-1)),rotationSpeed);
							break;		
						case 4:
							curTransform.rotation = Quaternion.Slerp(curTransform.rotation, Quaternion.LookRotation(new Vector3(1,0,1)),rotationSpeed);
							break;
						case 5:
							curTransform.rotation = Quaternion.Slerp(curTransform.rotation, Quaternion.LookRotation(new Vector3(-1,0,-1)),rotationSpeed);
							break;
						case 6:
							curTransform.rotation = Quaternion.Slerp(curTransform.rotation, Quaternion.LookRotation(new Vector3(-1,0,1)),rotationSpeed);
							break;
						case 7:
							curTransform.rotation = Quaternion.Slerp(curTransform.rotation, Quaternion.LookRotation(new Vector3(1,0,-1)),rotationSpeed);
							break;	
							
						default:break;
						}
					}
					
					//Behavior Checking
					switch(enemyBehavior)
					{
					case EnemyBehavior.Basic :
						if(curTransform.GetComponent<EnemyHealth>().currentHealth < curTransform.GetComponent<EnemyHealth>().maxHealth)
						{
							enemyState = EnemyState.Attract;
						}
						break;
					case EnemyBehavior.Attack ://Monster see player and change state to attract
						if(Vector3.Distance(target.position, curTransform.position) < minSeeing) {
						enemyState = EnemyState.Attract;
						}
						break;
					default:break;
					}
					
					
					
					//Calculate moster movement
					if(enemyState == EnemyState.Idle) {
						movement = moveSpeed * new Vector3 (0, verticalSpeed, 0);
					}
					else if(enemyState == EnemyState.Walk) {
						movement = curTransform.forward * moveSpeed + new Vector3 (0, verticalSpeed, 0);
					}					
				break;
				
			case EnemyState.Attract :
				//if(IsGrounded() ) {
			
					curSpeed = runSpeed;
					
					if(Vector3.Distance(target.position, curTransform.position) > maxSeeing) {
						enemyState = EnemyState.Idle;
					}
					
					//Calculate moster movement
					//Enemy walk to Player and stop when the distance of enemy to player is more than maxDistance
					if(Vector3.Distance(target.position, curTransform.position) > maxDistance) {
						//Rotation to target
						myTarget = target.position;
						myTransform = curTransform.position;
						myTarget.y = 0;
						myTransform.y = 0;
						curTransform.rotation = Quaternion.Slerp(curTransform.rotation, Quaternion.LookRotation(myTarget - myTransform),rotationSpeed);
					
						//Calculate direction to target
						targetDirection = (target.position - curTransform.position);
						movement = targetDirection.normalized * moveSpeed + new Vector3 (0, verticalSpeed, 0);
					}
			//	}
				break;	
			case EnemyState.Dead :
				curSpeed = 0;
				
				if(enemyState == EnemyState.Dead) {
					movement = moveSpeed * new Vector3 (0, 0, 0);
				}
				
				break;	
				
			default:break;
			}
			
		//Moving
		curSmooth = speedSmoothing * Time.deltaTime;
		moveSpeed = Mathf.Lerp(moveSpeed, curSpeed, curSmooth);	
		movement *= Time.deltaTime;
		CharacterController controller = GetComponent<CharacterController>();
		collisionFlags = controller.Move(movement);
	}
}
