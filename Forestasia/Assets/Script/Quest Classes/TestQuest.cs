using UnityEngine;
using System.Collections;

public class TestQuest : AbstractQuest{	
	
	
	public TestQuest() {
		_questId = QuestID.FindBoots;
		_questName = "TestOwlQuest";
		_questProcess = QuestProcess.Unknown;
		_questType = QuestType.FindItem;
		_questDialogue = 0;
		_questDetail = "";
		_questGoal = "Help owl fountain find some items.";
		_questItemReward = QuestItemReward.Old_Boots;
		_showQuest = false;
		
		_itemGoal = new ItemID[2];
		_itemGoal[0] = ItemID.OldBoots;
		_itemGoal[1] = ItemID.RabbitFur;
		_maxAmount = new int[2] {1,5};
		_curAmount = new int[2] {0,0};
	}
	
	public string TestQuestTalk() {
		switch(_questDialogue) {
			
		case 0:
			_questDetail = "HEY DUDE !! \nI'm a owl fountain. \n \nI have some problem...Can you help me!?";
			break;
			
		case 1:
			_questDetail = "WOW !! You are so kindly !! \nLet's talk about my problem. " +
				"\n\nI drop my old boots in the forest.Can you find it for me.";
			break;
		
		case 2:
			_questDetail = "THANK YOU !! I wish you find my old boots. \n\nAh...Wait a minute " +
				"\nThere are some rabbit around the forest, you can kill it to get rabbit fur";
			break;
			
			
		default:break;
		}
		return _questDetail;
	}
	
	public void NextDialogue() {
		switch(_questDialogue) {
			
		case 0:
			_questDialogue = 1;
			break;
		case 1:
			_questDialogue = 2;
			break;
		case 2:
			_questDialogue = 3;
			_questProcess = QuestProcess.InProgress;
			break;
		case 3:
		default:break;
		}
	}
	
	public void BackDialogue() {
		switch(_questDialogue) {
			
		case 0:
		case 1:
			_questDialogue = 0;
			closeQuest();
			break;
		default:break;
		}
	}
	
	
	/*
		case 2:
			GUILayout.Label("THANK YOU !! I wish you find my old boots. \n\nAh...Wait a minute " +
				"\nThere are some rabbit around the forest, you can kill it to get rabbit fur");
			
			if(GUI.Button(new Rect(100, 150, buttonWidth, buttonHeight), "YUP")) {
				ClearQuest();
				dialogueOwl = 3;
				questCount = 1;
				_displayQuestlogWindow = true;
			}
			break;
		case 3: 
			if(foundOldbooots && foundRabbitfur) {
				GUILayout.Label("OWWWWWW !! You can find my old boots. \nThank you very much. " +
				"\n\nHmm...I'dont have some reward to give you but I will give you my old boots and help you to mixing some item.");
				
				if(GUI.Button(new Rect(100, 150, buttonWidth, buttonHeight), "THANK")) {
				dialogueOwl = 4;
				questCount = 0;
				}
			}
			else {
			GUILayout.Label("RUUUUU !! I think my boots is in this forest.");
			}

			break;
		case 4:
				GUILayout.Label("Run run ruuuuu~*. \nMay I help you? ");
			
				if(GUI.Button(new Rect(30, 150, buttonWidth, buttonHeight), "MIXING")) {
					dialogueOwl = 5;
					ToggleMixingWindow();
					
				}
				if(GUI.Button(new Rect(20 + buttonWidth + 60, 150, buttonWidth, buttonHeight), "MAYBE"))
					ClearQuest();
			
			break;
		case 5:
				GUILayout.Label("Put some ingredients and then BOOOOOM !!");
			break;*/
		
	/*
	protected QuestID _questId;
	protected string _questName;
	protected QuestProcess _questProcess;
	protected QuestType _questType;
	protected int _questDialogue;
	protected string _questDetail;
	protected string _questGoal;
	protected QuestItemReward _questItemReward;

#region Basic Setters and Getters	
	//Basic Setters and Getters
	public QuestID Id {
		get { return _questId; }	
		set { _questId = value; }	
	}
	
	public string Name {
		get { return _questName; }	
		set { _questName = value; }	
	}
	
	public QuestProcess Process {
		get { return _questProcess; }	
		set { _questProcess = value; }	
	}
	
	public int Dialogue {
		get { return _questDialogue; }	
		set { _questDialogue = value; }	
	}
	
	public string Detail {
		get { return _questDetail; }	
		set { _questDetail = value; }	
	}
	
	public string Goal {
		get { return _questGoal; }	
		set { _questGoal = value; }	
	}
	
	public QuestItemReward Reward {
		get { return _questItemReward; }	
		set { _questItemReward = value; }	
	}
#endregion
		*/
	
}
