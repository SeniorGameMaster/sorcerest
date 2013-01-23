using UnityEngine;
using System.Collections;

public class RabbitBehaviourScript : MonoBehaviour {
	public Transform target;
	public float speed;
	public int maxDistance;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = target.localPosition;
		Vector3 monsterPosition = transform.localPosition;
		if(targetPosition.x>monsterPosition.x)
			transform.Translate(speed*Time.deltaTime,0f,0f);
		if(targetPosition.x<monsterPosition.x)
			transform.Translate(speed*-Time.deltaTime,0f,0f);
		if(targetPosition.z>monsterPosition.z)
			transform.Translate(0f,0f,speed*Time.deltaTime);
		if(targetPosition.z<monsterPosition.z)
			transform.Translate(0f,0f,speed*-Time.deltaTime);
	}
}
