using UnityEngine;
using System.Collections;
using System;

public class CharacterGenerator : MonoBehaviour {
	private PlayerCharacter _toon;
	private const int STARTING_POINTS = 350;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 10;
	private const int STARTING_VALUE = 50;
	private int pointsLeft;
	
	private const int OFFSET = 5;
	private const int LINE_HEIGHT = 20;
	
	private const int STAT_LABEL_WIDTH = 100;
	private const int BASEVALUE_LABEL_WIDTH= 30;
	private const int BUITTON_WIDTH = 20;
	private const int BUITTON_HEIGHT = 20;
	
	private int StatStartingPos = 40;
	
	public GUIStyle myStyle;
	//_toon ==> PlayerCharacter.Instance
	//public GameObject playerPrefab;
	
	public float delayTimer = .1f;
	private float _lastClick = 0;
	
	void Awake() {
		
		PlayerCharacter.Instance.Initialize();
	}
	
	// Use this for initialization
	void Start () {
		
		//GameObject pc = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		
	//	pc.name = "pc";
	
		//_toon = pc.GetComponent<PlayerCharacter>();
		
		pointsLeft = STARTING_POINTS;
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
			//_toon.GetPrimaryAttribute(cnt).BaseValue = STARTING_VALUE;
			PlayerCharacter.Instance.GetPrimaryAttribute(cnt).BaseValue = STARTING_VALUE;
			pointsLeft -= (STARTING_VALUE - MIN_STARTING_ATTRIBUTE_VALUE);
		}
		
		PlayerCharacter.Instance.StatUpdate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		
		DisplayName();
		DisplayPointLeft();
		DisplayAttributes();
		DisplayVitals();
		DisplaySkills();
		
		if(PlayerCharacter.Instance.name == "" || pointsLeft > 0)
			DisplayCreateLabel();
		else
			DisplayCreateButton();
		
	}
	
	private void DisplayName() {
		GUI.Label(new Rect(10, 10, 50, 25), "Name:");	
		//_toon.name = GUI.TextField(new Rect(65, 10, 100, 25), _toon.name);
		PlayerCharacter.Instance.name = GUI.TextField(new Rect(65, 10, 100, 25), PlayerCharacter.Instance.name);
	}
	
	private void DisplayAttributes() {
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
			GUI.Label(new Rect( OFFSET, 									//x
								StatStartingPos + (cnt * LINE_HEIGHT), 		//y
								STAT_LABEL_WIDTH, LINE_HEIGHT				//width
								),((AttributeName)cnt).ToString());			//height
			
			GUI.Label(new Rect( STAT_LABEL_WIDTH + OFFSET, 					//x
								StatStartingPos + (cnt * LINE_HEIGHT),  	 //y
								BASEVALUE_LABEL_WIDTH,                 		 //width
								LINE_HEIGHT                             	 //height
								),PlayerCharacter.Instance.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());
			
			if(GUI.RepeatButton(new Rect( OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH, //x
									StatStartingPos + (cnt * BUITTON_HEIGHT),                       //y
									BUITTON_WIDTH,                                     //width
									BUITTON_HEIGHT                                     //height
									), "-")) {
				if(Time.time - _lastClick > delayTimer) {
					if(PlayerCharacter.Instance.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE) {
						PlayerCharacter.Instance.GetPrimaryAttribute(cnt).BaseValue--	;	
						pointsLeft++;
						PlayerCharacter.Instance.StatUpdate();
					}
					_lastClick = Time.time;
				}
			}
			
			if(GUI.RepeatButton(new Rect( OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUITTON_WIDTH , //x
									StatStartingPos + (cnt * BUITTON_HEIGHT),                                        //y
									BUITTON_WIDTH,                                                      //width
									BUITTON_HEIGHT ), "+")) {	                                        //height
				if(Time.time - _lastClick > delayTimer) {
					if(pointsLeft > 0) {
						PlayerCharacter.Instance.GetPrimaryAttribute(cnt).BaseValue++;	
						pointsLeft--;
						PlayerCharacter.Instance.StatUpdate();
					}
				_lastClick = Time.time;
				}
			}
		}
	}
	
	private void DisplayVitals() {
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) {
			GUI.Label(new Rect( OFFSET, 
								StatStartingPos + ((cnt + 7) * LINE_HEIGHT),  	    	//x
								STAT_LABEL_WIDTH,                            			//y
								LINE_HEIGHT                                   			//width
								),((VitalName)cnt).ToString());              			 //height
			
			GUI.Label(new Rect( OFFSET + STAT_LABEL_WIDTH, 								//x
								StatStartingPos + ((cnt + 7) * LINE_HEIGHT), 			//y
								BASEVALUE_LABEL_WIDTH,                             		//width
								LINE_HEIGHT                                          	//height
								),PlayerCharacter.Instance.GetVital(cnt).AdjustedBaseValue.ToString());
		}
	}
	
	private void DisplaySkills() {
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++) {
			GUI.Label(new Rect( OFFSET * 3 + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH * 2 + BUITTON_WIDTH,   //x
								StatStartingPos + (cnt * LINE_HEIGHT),                                       //y
								STAT_LABEL_WIDTH,                                                            //width
								LINE_HEIGHT                                                                   //height
								),((SkillName)cnt).ToString());
			
			GUI.Label(new Rect( OFFSET * 3 + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH * 2 + BUITTON_WIDTH + STAT_LABEL_WIDTH,	//x 
								StatStartingPos + (cnt * LINE_HEIGHT),                                                          //y
								BASEVALUE_LABEL_WIDTH,                                                                                             //width
								LINE_HEIGHT)                                                                                    //height
								,PlayerCharacter.Instance.GetSkill(cnt).AdjustedBaseValue.ToString());
		}
	}
	
	private void DisplayPointLeft() {
		GUI.Label(new Rect(250, 10, 100, 25), "Points Left: " + pointsLeft.ToString());	
	}
	
	private void DisplayCreateLabel() {
		GUI.Label(new Rect( Screen.width/2 - 50,
							 StatStartingPos + (10 * LINE_HEIGHT),
							 100, 
							 LINE_HEIGHT
					), "Fill Name", "Button");
	}
	
	private void DisplayCreateButton() {
		if(GUI.Button(new Rect( Screen.width/2 - 50,
							 StatStartingPos + (10 * LINE_HEIGHT),
							 100, 
							 LINE_HEIGHT
					), "Create"))
		{
			//Saving Character
			GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>(); 			
			//change the cur value of vitals to the max modified value of that vital
			UpdateCurVitalValues();
			
			gsScript.SaveCharacterData();
			
			Application.LoadLevel("testScene"); //can be index of scene
			
		}
	} 
	
	private void UpdateCurVitalValues() {
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) {
			PlayerCharacter.Instance.GetVital(cnt).CurValue = PlayerCharacter.Instance.GetVital(cnt).AdjustedBaseValue;
			
		}
	}
}
