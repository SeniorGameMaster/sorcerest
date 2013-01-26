using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	// Use this for initialization
	public float height;
	public float speed;
	float length;
	int levelController;
	void Start () {
		length = 0;
		levelController = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(length < height)
		{
			transform.Translate(0f,levelController*speed*Time.deltaTime,0f);
			length+=Time.deltaTime;
		}
		else
		{
			length = 0;
			levelController*=-1;
		}
		
	}
}
