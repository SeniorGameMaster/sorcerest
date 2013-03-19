using UnityEngine;
using System.Collections;

public class WaterQuest : AbstractQuest{	
	
	
	public WaterQuest() {
		_questId = QuestID.WaterFromCactus;
		_questName = "Water from Cactus";
		_questProcess = QuestProcess.Unknown;
		_questType = QuestType.FindItem;
		_questDialogue = 0;
		_questDetail = "";
		_questGoal = "Find water aroudn desert.";
		_questItemReward = ItemID.TruthScroll;
		_rewardType = ItemTypes.Quest;
		_showQuest = false;
		
		_itemGoal = new ItemID[1];
		_itemGoal[0] = ItemID.WaterCactus;
		_maxAmount = new int[1] {10};
		_curAmount = new int[1] {0};
	}
	
	public string WaterQuestTalk() {
		switch(_questDialogue) {
			
		case 0:
			_questDetail = "OHHH!! \nMy friend send you to help me?.";
			break;
			
		case 1:
			_questDetail = "I'm so thirsty. \n\nCan you find some water around here? ";
			break;
		
		case 2:
			_questDetail = "In the Desert, there are many cactus \nthat maybe give some water." +
				"\nBut you must use spike from headhog \naround the desert to stab it.";
			break;
		case 3:
			if(_questProcess == QuestProcess.Complete) {
				_questDetail = "OWWWWW !! Cactus water !! \n\nYou can find it!!" +
					"\nPlease give me some of it.";
			}
			else {
				_questDetail = "*cough* Gimme sum wuturrrrrrr *cough* *cough*";
			}
			break;
		case 4:	
			_questDetail = "Fu haaaa !! I'm so freshhhhh. \n\nThank for your helping\n" +
				"I will give you some treasure that I find around the desert";
			break;
		case 5:
			_questDetail = "Are you want to go back to see my old friend?\nI will send you like him. Hu Hu Hu" +
				"\n\n From my treasure that I gave you.\nYou can open to read some description and hint from clicking it.";
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
			_questDialogue = 4;
			break;
		case 4:
			_questDialogue = 5;
			break;	
		default:break;
		}
	}
	
	public void BackDialogue() {
		switch(_questDialogue) {
			
		case 0:
		case 1:
			closeQuest();
			break;
		
		
			break;
		default:closeQuest();break;
		}
	}

}
