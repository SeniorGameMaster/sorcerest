using UnityEngine;
using System.Collections;

public class LegendaryQuest : AbstractQuest{	
	
	
	public LegendaryQuest() {
		_questId = QuestID.GetAllLegendaryItem;
		_questName = "All_Legendary_Item";
		_questProcess = QuestProcess.InProgress;
		_questType = QuestType.FindItem;
		_questDialogue = 0;
		_questDetail = "";
		_questGoal = "Find All Legendary Magic item to get back.";
		//_questItemReward = "";
		_showQuest = false;
		
		_itemGoal = new ItemID[7];
		_itemGoal[0] = ItemID.Boots_of_Intelligent;
		_itemGoal[1] = ItemID.Wand_of_Truth;
		_itemGoal[2] = ItemID.FeatherPen;
		_itemGoal[3] = ItemID.LightCrystal;
		_itemGoal[4] = ItemID.MixColor;
		_itemGoal[5] = ItemID.Gemstone;
		_itemGoal[6] = ItemID.PhilosopherMap;
		
		_maxAmount = new int[7] {1,1,1,1,1,1,1};
		_curAmount = new int[7] {0,0,0,0,0,0,0};
	}
	
	public string LegendaryQuestTalk() {
		switch(_questDialogue) {
			
		case 0:
			//_questDetail = "HEY DUDE !! \nI'm a owl fountain. \n \nI have some problem...Can you help me!?";
			break;
			
		case 1:
			//_questDetail = "WOW !! You are so kindly !! \nLet's talk about my problem. " +
				//"\n\nI drop my old boots in the forest.Can you find it for me.";
			break;
		
		case 2:
			//_questDetail = "THANK YOU !! I wish you find my old boots. \n\nAh...Wait a minute " +
			//	"\nThere are some rabbit around the forest, you can kill it to get rabbit fur";
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
