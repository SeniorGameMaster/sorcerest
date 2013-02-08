using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour {	
	private QuestID _questId;
	private string _questName;
	private QuestProcess _questProcess;
	private QuestType _questType;
	private int _questDialogue;
	private string _questDetail;
	private string _questGoal;
	private QuestItemReward _questItemReward;
	
	public Quest() {
	
	}
	
	public Quest(int id, string name, int process, string type, 
		int dialogue, string detail, string goal, string itemReward, string moneyReward) {
		
		
	}
	
	
}
/*
public enum QuestID {
		None = 0,
		FindBoots = 1,		
}

public enum QuestProcess {
		Unknown = 0,
		InProgress = 1,
		Complete = 2
}

public enum QuestType {
		KillMonster = 0,
		FindItem = 1,
		SendItem = 2,
		SearchLocation = 3
}

public enum QuestItemReward {
		None = 0,
		Old_Boots = 1,
		RabbitFur = 2
}
*/