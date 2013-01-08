using UnityEngine;
using System.Collections.Generic;

public class TreeManager : MonoBehaviour {
	
	public Transform prefab;
	public int numberOfObjects;
	public Vector3 minPosition, maxPosition;
	
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	
	void Start () {
		nextPosition = transform.localPosition;
		nextPosition.x += 10f;
		objectQueue = new Queue<Transform>(numberOfObjects);
		for(int i = 0; i < numberOfObjects; i++){
			
			
			Transform o = (Transform)Instantiate(prefab);
			o.localPosition = new Vector3(
			Random.Range(minPosition.x, maxPosition.x)
				,Random.Range(minPosition.y, maxPosition.y)
				,Random.Range(minPosition.z, maxPosition.z));//nextPosition;
			//nextPosition.x += 7f;
			objectQueue.Enqueue(o);//((Transform)Instantiate(prefab, new Vector3(-8f, -13f, 34f), Quaternion.identity));
		}
	}
	
	void Update () {
	}
}
