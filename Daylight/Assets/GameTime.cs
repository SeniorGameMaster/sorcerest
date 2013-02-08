using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	public enum timeOfDay{
		Idle,
		SunRise,
		SunSet
	} ;
	public Transform[] sun;
	private Sun[] _sunScript;
	public float DayCycleInMinute = 1;
	public float sunRise;
	public float sunSet;
	public float skyboxBlendModifier;
	
	private const float SECOND = 1;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;
	
	private const float DEGREE_PER_SECOND = 360/DAY;
	
	private float _degreeRotation;
	
	private float _timeOfDay;
	
	private float dayCycleInSeconds;
	private timeOfDay _tod;
	// Use this for initialization
	void Start () {
		_tod = timeOfDay.Idle;
		dayCycleInSeconds = DayCycleInMinute * MINUTE;
		RenderSettings.skybox.SetFloat("_Blend",0);
		_sunScript = new Sun[sun.Length];
		
		for(int cnt =0;cnt<sun.Length;cnt++)
		{
			Sun temp = sun[cnt].GetComponent<Sun>(); 
			
			if(temp == null)
			{
				Debug.Log("Sun script not found");
				sun[cnt].gameObject.AddComponent<Sun>();
			}
			_sunScript[cnt] = temp;
		}
		_timeOfDay = 0;
		_degreeRotation = DEGREE_PER_SECOND * DAY/(dayCycleInSeconds);
		
		sunRise *= dayCycleInSeconds;
		sunSet *= dayCycleInSeconds;
		
	}
	
	// Update is called once per frame
	void Update () {
		for(int cnt = 0;cnt <sun.Length;cnt++)
			sun[cnt].Rotate(new Vector3(_degreeRotation,0,0)*Time.deltaTime);
		
		_timeOfDay += Time.deltaTime;
		
		if(_timeOfDay > dayCycleInSeconds)
			_timeOfDay -= dayCycleInSeconds;
//		Debug.Log(_timeOfDay);
		
		if(_timeOfDay>sunRise && _timeOfDay<sunSet && RenderSettings.skybox.GetFloat("_Blend")<1)
		{
			_tod = timeOfDay.SunRise;
			BlendSkybox();
		}
		else if(_timeOfDay > sunSet && RenderSettings.skybox.GetFloat("_Blend")>0)
		{
			_tod = GameTime.timeOfDay.SunSet;
			BlendSkybox();
		}
		else
		{
			_tod = GameTime.timeOfDay.Idle;
		}
		
	}
	
	private void BlendSkybox()
	{
		float temp = 0;
			
		switch(_tod)
		{
			case timeOfDay.SunRise:
				temp =(_timeOfDay - sunRise) / dayCycleInSeconds * skyboxBlendModifier;
				break;
			case timeOfDay.SunSet:
				temp =(_timeOfDay - sunSet) / dayCycleInSeconds * skyboxBlendModifier;
				temp = 1 - temp;
				break;
		}
		
//		Debug.Log(temp);
		Debug.Log(_tod);
		
		RenderSettings.skybox.SetFloat("_Blend",temp);

	}
}
