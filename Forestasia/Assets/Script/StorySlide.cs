using UnityEngine;
using System.Collections;

public class StorySlide : MonoBehaviour {
	
	public float speed;
	float time2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(-speed*Time.deltaTime,0f,0f);
		time2 += Time.deltaTime;
		Debug.Log("TIME: "+time2);
		
		if(time2 > 79)
			Application.LoadLevel("CharacterGenerator");
		
				//if(transform.position.x < -0.788)
	}
}
