using UnityEngine;
using System.Collections;

public class BehaviorButterfly : MonoBehaviour {
	public enum Flying {
		xAxis,
		yAxis,
		zAxis,
		xyAxis,
		xzAxis,
		yzAxis,
	}
	
	public AnimationClip flyAnimation;
	public Flying flyType;
	
	public float flyTime = 6;
	public float minFlyRange = 1;
	public float maxFlyRange = 20;
	private int _randomDirection;
	private float _flyTimer;
	private Transform _myTransform;
	// Use this for initialization
	void Start () {
		_myTransform = transform;
		animation[flyAnimation.name].wrapMode = WrapMode.Loop;
	}
	// Update is called once per frame
	void Update () {		
		animation.CrossFade(flyAnimation.name);
		
		_flyTimer += Time.deltaTime;
		
		if(flyType == Flying.xAxis) {
			if(_flyTimer > flyTime) {
				_flyTimer = 0;
				_randomDirection = Random.Range(0,2);
				switch (_randomDirection) {
				case 0: 
					//_myTransform.transform.position = new Vector3(_myTransform.transform.position.x + maxFlyRange, _myTransform.transform.position.y, _myTransform.transform.position.z);
					
					
					
//					_myTransform.transform.position.x = go.transform.position.x + 10;
					//SendMessage("MoveMeForward", AdvancedMovement.Forward.forward);	
					Debug.Log("Move");
					//transform.position = 
					break;
				case 1:
					
					break;
				default:break;
				}
			}
		}
		
		//_myTransform.transform.position = new Vector3(minFlyRange * _flyTimer, _myTransform.position.y, _myTransform.position.z);
	}
}


