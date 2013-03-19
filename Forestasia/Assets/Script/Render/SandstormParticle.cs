using UnityEngine;
using System.Collections;

public class SandstormParticle : MonoBehaviour {

	public int minSpeedX;
	public int maxSpeedX;
	public int minSpeedZ;
	public int maxSpeedZ;
	public float minStormTime;
	public float maxStormTime;
	public bool stormOpen = true;
	
	private float _stormTime;
	private float _stormTimer;
	private int _randomSpeed;

	void Start () {
		_stormTime = minStormTime;

	}
	
	void Update () {
		if(stormOpen)
			stormBehavior();
	}
	
	private void stormBehavior() {
		_stormTimer += Time.deltaTime;
		
		if(_stormTimer > _stormTime) {
			_stormTimer = 0;
			_stormTime = Random.Range(minStormTime,maxStormTime);
			particleEmitter.localVelocity = new Vector3(
				(Random.Range(minSpeedX,maxSpeedX) * ((Random.Range(0,2) == 0) ? 0 : 1 )) * ((Random.Range(0,2) == 0) ? 1 : -1),
				0,
				(Random.Range(minSpeedZ,maxSpeedZ) * ((Random.Range(0,2) == 0) ? 0 : 1 ))  * ((Random.Range(0,2) == 0) ? 1 : -1));
		}
	}
	
	public void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")) {
			particleEmitter.emit = true;
		}
	}
	
	public void OnTriggerExit(Collider other) {
		if(other.CompareTag("Player")) {
			particleEmitter.emit = false;
		}
	}
	
}
