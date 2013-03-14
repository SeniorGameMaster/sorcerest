using UnityEngine;
using System.Collections;
using System;

public class GameSettings : MonoBehaviour {
	public const string PLAYER_POSITION = "Player Position"; //This is the name of the gameobject that the player will spawn on at the start of the level
	
	//value of attack type
	public const float BASE_MELEE_ATTACK_TIMER = 2.0f;
	public const float BASE_MELEE_ATTACK_SPEED = 2.0f;
	
	public const float BASE_MELEE_RANGE = 2.0f;
	public const float BASE_MAGIC_RANGE = 7.5f;
	
	public static Vector3 startingPos = new Vector3(0, 0, 0);
	
	void Awake() {
		DontDestroyOnLoad(this);
	}
	
	public void SaveCharacterData() {
		GameObject pc = GameObject.Find("pc");
			
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();
		
	//	PlayerPrefs.DeleteAll();
		
		PlayerPrefs.SetString("Player Name", pcClass.name);
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString()+ " - Base Value", pcClass.GetPrimaryAttribute(cnt).BaseValue);			
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " - Exp To Level", pcClass.GetPrimaryAttribute(cnt).ExpToLevel);			
		}
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) {
			PlayerPrefs.SetInt(((VitalName)cnt).ToString()+ " - Base Value", pcClass.GetVital(cnt).BaseValue);			
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Exp To Level", pcClass.GetVital(cnt).ExpToLevel);			
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Cur Value", pcClass.GetVital(cnt).CurValue);	
	
			//PlayerPrefs.SetString(((VitalName)cnt).ToString()+ "Mods", 	pcClass.GetVital(cnt).GetModifyingAttributesString());
		}
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++) {
			PlayerPrefs.SetInt(((SkillName)cnt).ToString()+ " - Base Value", pcClass.GetSkill(cnt).BaseValue);			
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + " - Exp To Level", pcClass.GetSkill(cnt).ExpToLevel);		
			
			//PlayerPrefs.SetString(((SkillName)cnt).ToString()+ "Mods", pcClass.GetSkill(cnt).GetModifyingAttributesString());	
		}
		
		PlayerPrefs.Save();
		Debug.Log("COMPLETE SAVE");
	}
	
	public void LoadCharacterData() {
		GameObject pc = GameObject.Find("pc");
			
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();
		
		pcClass.name = PlayerPrefs.GetString("Player Name", "Name Me");
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) { 
			pcClass.GetPrimaryAttribute(cnt).BaseValue = PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + " - Base Value",0);
			pcClass.GetPrimaryAttribute(cnt).ExpToLevel = PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + " - Exp To Level", Attribute.STARTING_EXP_COST);
		}
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) { 
			pcClass.GetVital(cnt).BaseValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Base Value",0);
			pcClass.GetVital(cnt).ExpToLevel = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Exp To Level",0);
		
			pcClass.GetVital(cnt).Update();
			
			pcClass.GetVital(cnt).CurValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Cur Value", 1);	
		}
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++) {
			pcClass.GetSkill(cnt).BaseValue = PlayerPrefs.GetInt(((SkillName)cnt).ToString()+ " - Base Value", 0);			
			pcClass.GetSkill(cnt).ExpToLevel = PlayerPrefs.GetInt(((SkillName)cnt).ToString()+ " - Exp To Level", 0);		
		}
		
	}
	
	public static void SavePlayerPosition(Vector3 pos) {
		PlayerPrefs.SetInt(PLAYER_POSITION + "x", (int)pos.x);
		PlayerPrefs.SetInt(PLAYER_POSITION + "y", (int)pos.y);
		PlayerPrefs.SetInt(PLAYER_POSITION + "z", (int)pos.z);
		
		Debug.Log(pos.x.ToString()+" "+ pos.y.ToString()+" "+ pos.z.ToString());
	}
	
	public static Vector3 LoadPlayerPosition() {
		
		Vector3 temp = new Vector3(	PlayerPrefs.GetInt(PLAYER_POSITION + "x"),
								 	PlayerPrefs.GetInt(PLAYER_POSITION + "y"),
									PlayerPrefs.GetInt(PLAYER_POSITION + "z")
			);
		return temp;
	}
	
}
	