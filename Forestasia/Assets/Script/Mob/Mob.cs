#define DEBUGGER

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
	public int minDamage;
	public int maxDamage;
	public int minConstitution;
	public int maxConstitution;
	
	static public GameObject camera;
	private Transform displayName;
	private Transform _myTransform;
	
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
		_myTransform = transform;
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
			/*
			for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
				GUI.Label(new Rect(10, 10 + (cnt * lh), 300, lh), ((AttributeName)cnt).ToString() +	": " + GetPrimaryAttribute(cnt).BaseValue);
			}
			
			for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) {
				GUI.Label(new Rect(10, 10 + (cnt * lh) + Enum.GetValues(typeof(AttributeName)).Length * lh , 140, lh), ((VitalName)cnt).ToString() +	": " + GetVital(cnt).CurValue + "/" + GetVital(cnt).AdjustedBaseValue);
			}
			
			for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++) {
				GUI.Label(new Rect(150, 10 + (cnt * lh) , 140, lh), ((SkillName)cnt).ToString() +	": " + GetSkill(cnt).AdjustedBaseValue);
			}*/
			
			for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
				GUI.Label(new Rect(10, 10 + (cnt * lh), 300, lh), ((AttributeName)cnt).ToString() +	": " + GetPrimaryAttribute(cnt).BaseValue);
			}
			
			for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) {
				GUI.Label(new Rect(10, 10 + (cnt * lh) + Enum.GetValues(typeof(AttributeName)).Length * lh , 140, lh), ((VitalName)cnt).ToString() +	": " + GetVital(cnt).CurValue + "/" + GetVital(cnt).AdjustedBaseValue);
			}
		}
	}
#endif
	
	private void Spawn() {
		//setup attribute and skills
		//setup gear
		SetupStats();
	}
	
	private void SetupStats() {
		/*
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
				GetPrimaryAttribute(cnt).BaseValue = UnityEngine.Random.Range(50,100);
			}
			
			
		*/
		//GetPrimaryAttribute(AttributeName.)
		GetPrimaryAttribute((int)AttributeName.Might).BaseValue = UnityEngine.Random.Range(minDamage,maxDamage);
		GetPrimaryAttribute((int)AttributeName.Constitution).BaseValue = UnityEngine.Random.Range(minConstitution,maxConstitution);
		
		StatUpdate();
		
		for( int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) {
			GetVital(cnt).CurValue = GetVital(cnt).AdjustedBaseValue;
		}
		//GetVital((int)VitalName.Health).Update();
	}
	
		
	public void OnTriggerEnter(Collider other) {
		//AI ai = (AI)gameObject.GetComponent("AI");
		//AdvancedMovement ad = (AdvancedMovement)gameObject.GetComponent("AdvancedMovement");
		if(other.CompareTag("Spell") && !_myTransform.GetComponent<AI>().isAIDead()) {
			GameObject go = GameObject.FindGameObjectWithTag("Player");
			Spellcaster magic = (Spellcaster)go.GetComponent("Spellcaster");
			GetVital(0).CurValue =  GetVital(0).CurValue - magic.getMagicDamage();
		
			GameObject sp = GameObject.FindGameObjectWithTag("Spell");
			DestroyObject(sp);
			
			if(GetVital(0).CurValue <= 0) {
				//ai.setAIDead();
				//ad.setDead();
				_myTransform.GetComponent<AI>().setAIDead();
				_myTransform.GetComponent<AdvancedMovement>().setDead();
				
				Messenger<int, int>.Broadcast("mob health update", 0, GetVital(0).AdjustedBaseValue, MessengerMode.DONT_REQUIRE_LISTENER);
			}
			else 
				Messenger<int, int>.Broadcast("mob health update", GetVital(0).CurValue, GetVital(0).AdjustedBaseValue, MessengerMode.DONT_REQUIRE_LISTENER);						
		}
	}
	
	public void OnMouseUp() {
		
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		TargetMob tm = (TargetMob)go.GetComponent("TargetMob");
		tm.DeselectTarget();
		/*
		if(tm.selectedTarget == null)
			Debug.Log("nulltarget");
		*/
		
		tm.selectedTarget = _myTransform;
		tm.SelectTarget();
		
	}
	

	/*
	private void SelectTarget() {
		Transform mobName = selectedTarget.FindChild("Name");
		
		if(mobName == null) {
			Debug.Log("Can't find mob name on" + selectedTarget.name);
			return;
		}
		
		mobName.GetComponent<TextMesh>().text = selectedTarget.GetComponent<Mob>().name;
		mobName.GetComponent<MeshRenderer>().enabled = true;
		selectedTarget.GetComponent<Mob>().DisplayHealth();
		
		Messenger<bool>.Broadcast("show mob vitalbars", true);
	}
	
	private void DeselectTarget() {
		selectedTarget.FindChild("Name").GetComponent<MeshRenderer>().enabled = false;

		selectedTarget = null;
		Messenger<bool>.Broadcast("show mob vitalbars", false);
	}*/
}
