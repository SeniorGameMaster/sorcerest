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
	// Use this for initialization
	void Start () {
		_toon = new PlayerCharacter();	
		_toon.Awake();
		
		pointsLeft = STARTING_POINTS;
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
			_toon.GetPrimaryAttribute(cnt).BaseValue = STARTING_VALUE;
			pointsLeft -= (STARTING_VALUE - MIN_STARTING_ATTRIBUTE_VALUE);
		}
		
		_toon.StatUpdate();
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
		
		
	}
	
	private void DisplayName() {
		GUI.Label(new Rect(10, 10, 50, 25), "Name:");	
		_toon.Name = GUI.TextField(new Rect(65, 10, 100, 25), _toon.Name);
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
								),_toon.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());
			
			if(GUI.Button(new Rect( OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH, //x
									StatStartingPos + (cnt * BUITTON_HEIGHT),                       //y
									BUITTON_WIDTH,                                     //width
									BUITTON_HEIGHT                                     //height
									), "-")) {
				if(_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE) {
					_toon.GetPrimaryAttribute(cnt).BaseValue--	;	
					pointsLeft++;
					_toon.StatUpdate();
				}
			}
			
			if(GUI.Button(new Rect( OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUITTON_WIDTH , //x
									StatStartingPos + (cnt * BUITTON_HEIGHT),                                        //y
									BUITTON_WIDTH,                                                      //width
									BUITTON_HEIGHT ), "+")) {	                                        //height
				if(pointsLeft > 0) {
					_toon.GetPrimaryAttribute(cnt).BaseValue++;	
					pointsLeft--;
					_toon.StatUpdate();
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
								),_toon.GetVital(cnt).AdjustedBaseValue.ToString());
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
								,_toon.GetSkill(cnt).AdjustedBaseValue.ToString());
		}
	}
	
	private void DisplayPointLeft() {
		GUI.Label(new Rect(250, 10, 100, 25), "Points Left: " + pointsLeft.ToString());	
	}
}
