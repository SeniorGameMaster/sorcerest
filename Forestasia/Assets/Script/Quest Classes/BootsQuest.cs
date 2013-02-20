using UnityEngine;
using System.Collections;

public class BootsQuest : AbstractQuest{	
	
	
	public BootsQuest() {
		_questId = QuestID.FindBoots;
		_questName = "Boots_of_Intelligent";
		_questProcess = QuestProcess.Unknown;
		_questType = QuestType.FindItem;
		_questDialogue = 0;
		_questDetail = "";
		_questGoal = "Help owl fountain find some items.";
		_questItemReward = ItemID.Boots_of_Intelligent;
		_showQuest = false;
		
		_itemGoal = new ItemID[2];
		_itemGoal[0] = ItemID.OldBoots;
		_itemGoal[1] = ItemID.RabbitFur;
		_maxAmount = new int[2] {1,5};
		_curAmount = new int[2] {0,0};
	}
	
	public string BootsQuestTalk() {
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

}
