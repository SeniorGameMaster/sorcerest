//#define DEBUGGER

using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(SphereCollider))]
[RequireComponent (typeof(AI))]
[RequireComponent (typeof(AdvancedMovement))]
public class Mob : BaseCharacter {
//add all of the debugging variable here
#if DEBUGGER
	public bool debugger = true;
#endif
	static public GameObject camera;
	private Transform displayName;
	
	new void Awake() {
		base.Awake();
		Spawn();
	}
	
	// Use this for initialization
	void Start () {
		//find the player camera
		camera = GameObject.Find("Player Camera");
		//find the name component
	    displayName = transform.FindChild("Name");

		//change display name when assign
		displayName.GetComponent<TextMesh>().text = name;
	}
	
	public void DisplayHealth() {
		//Messenger<int, int>.Broadcast("mob health update", curHealth, maxHealth);
	}
	
	void Update() {
		//if it does not exit, warn and return
		if(displayName == null) {
			Debug.LogWarning("Please Add a JOText to the mob.");
			return;	
		}	
		
		if(camera == null) {
			Debug.Log("Can't find player camera");
			return;
		}
		
		displayName.LookAt(camera.transform);
		displayName.Rotate(new Vector3(0, 180, 0));
	}
	
#if DEBUGGER
	void OnGUI() {
		if(debugger) {
			int lh = 20;
			
			for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
				GUI.Label(new Rect(10, 10 + (cnt * lh), 300, lh), ((AttributeName)cnt).ToString() +	": " + GetPrimaryAttribute(cnt).BaseValue);
			}
			
			for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) {
				GUI.Label(new Rect(10, 10 + (cnt * lh) + Enum.GetValues(typeof(AttributeName)).Length * lh , 140, lh), ((VitalName)cnt).ToString() +	": " + GetVital(cnt).CurValue + "/" + GetVital(cnt).AdjustedBaseValue);
			}
			
			for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++) {
				GUI.Label(new Rect(150, 10 + (cnt * lh) , 140, lh), ((SkillName)cnt).ToString() +	": " + GetSkill(cnt).AdjustedBaseValue);
			
			}
		}
	}
#endif
	
	private void Spawn() {
		//setup attribute and skills
		//setup gear
		SetupStats();
		SetupGear();
	}
	
	private void SetupStats() {
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
				GetPrimaryAttribute(cnt).BaseValue = UnityEngine.Random.Range(50,100);
			}
		
		StatUpdate();
		
		for( int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) {
			GetVital(cnt).CurValue = GetVital(cnt).AdjustedBaseValue;
		}
		//GetVital((int)VitalName.Health).Update();
	}
	
	private void SetupGear() {
		
	}
}
