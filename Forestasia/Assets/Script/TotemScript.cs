using UnityEngine;
using System.Collections;

public class TotemScript : MonoBehaviour {
	public int type;
	public float minimumspeed;
	public float maximumspeed;
	public int maximumRange;
	float distance;
	Vector3 firstLocation;
	bool reverse;
	float speed;
	// Use this for initialization
	void Start () {
		distance =0f;
		firstLocation = transform.localPosition;
		reverse = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		speed = Random.Range(minimumspeed,maximumspeed);
		switch(type)
		{
			case 1:distance =transform.localPosition.x - firstLocation.x;break;
			case 2:distance =transform.localPosition.y - firstLocation.y;break;
			
		}
		
		
		if(!reverse)
		{
			if(distance >= maximumRange)
				reverse = true;
		}
		if(reverse)
		{
			
			if(distance < 0)
				reverse = false;
		}
		setType (type,reverse,speed);
	}
	void setType(int type, bool reverse, float speed)
	{
		if(reverse)
		{
			speed = speed * -1f;
		}
			switch(type)
			{
				case 1:transform.Translate(speed*Time.deltaTime,0f,0f);break;
				case 2:transform.Translate(0f,speed*Time.deltaTime,0f);break;
			}

	}
}
