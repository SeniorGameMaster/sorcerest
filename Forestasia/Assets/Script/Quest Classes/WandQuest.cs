using UnityEngine;
using System.Collections;

public class WandQuest : AbstractQuest{	

	public WandQuest() {
		_questId = QuestID.WandOfTruth;
		_questName = "Wand_of_Truth";
		_questProcess = QuestProcess.Unknown;
		_questType = QuestType.FindItem;
		_questDialogue = 0;
		_questDetail = "";
		_questGoal = "Mixing all ingredient to create wand.";
		_questItemReward = ItemID.WandOfTruth;
		_rewardType = ItemTypes.Legendary;
		_showQuest = false;
		
		_itemGoal = new ItemID[2];
		_itemGoal[0] = ItemID.MysticalBranch;
		_itemGoal[1] = ItemID.FortunePotion;
		
		_maxAmount = new int[2] {1,1};
		_curAmount = new int[2] {0,0};
	}
	
	public string WandQuestTalk() {
		switch(_questDialogue) {
			
		case 0:
			_questDetail = "This old recipe scroll was wrote by \nthe legendary wizard of the resonance.";
			break;
			
		case 1:
			_questDetail = "This recipe was a result of my whole life \nfrom researching the unique wand.";
			break;
		
		case 2:
			_questDetail = "Find the uncommon branch from the uncommon tree and pour in a extract liquid." +
					"\nExtract liquid was compound by alchemy a rarely herb with dessert water.";
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
			_questProcess = QuestProcess.InProgress;
			break;
		case 2:
			
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
