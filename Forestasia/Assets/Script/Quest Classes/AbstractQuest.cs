public abstract class AbstractQuest {	
	protected QuestID _questId;
	protected string _questName;
	protected QuestProcess _questProcess;
	protected QuestType _questType;
	protected int _questDialogue;
	protected string _questDetail;
	protected string _questGoal;
	protected ItemID _questItemReward;
	protected ItemTypes _rewardType;
	protected bool _showQuest;
	protected ItemID[] _itemGoal;
	protected int[] _maxAmount;
	protected int[] _curAmount;
	
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
	
	public ItemID Reward {
		get { return _questItemReward; }	
		set { _questItemReward = value; }	
	}
	
	public ItemTypes TypeReward {
		get { return _rewardType; }	
		set { _rewardType = value; }	
	}
	
	public bool Showing {	
		get { return _showQuest; }	
		set { _showQuest = !_showQuest; }	
	}
		
	public ItemID[] ItemGoal {	
		get { return _itemGoal; }	
		set { _itemGoal = value; }	
	}
	
	public int[] ItemMax {	
		get { return _maxAmount; }	
		set { _maxAmount = value; }	
	}
	
	public int[] ItemCur {	
		get { return _curAmount; }	
		set { _curAmount = value; }	
	}
	
#endregion
	
	public void closeQuest() {
		Messenger.Broadcast("CloseQuest");
	}
	
	public void changeShowing(bool show) {
		_showQuest = show;
	}
	
	public string checkItemGoal(int pos) {
		return _itemGoal[pos].ToString();
	}
	
	public int checkItemMax(int pos) {
		return _maxAmount[pos];
	}
	
	public int checkItemCur(int pos) {
		return _curAmount[pos];
	}
	
	public void addItemCur(int index, int count) {
		_curAmount[index] = count;
	}
	
}

public enum QuestID {
		None = 0,
		GetAllLegendaryItem = 1,
		FindBoots = 2,
		WandOfTruth = 3,
		WaterFromCactus = 4
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