using UnityEngine;
using System.Collections;

public class RecipeData {	
	public const int MAX_RECIPE = 2;
	
	public struct RecipeList {
		public RecipeID _recipeId;
		public ItemID[] _itemIngredient;
		public int[] _amountIngredient;
		public ItemID _itemResult;
		public ItemTypes _itemResultType;
		public bool _showRecipe;
		public bool _unlockRecipe;
		
	}
	
	public RecipeList[] _recipeList;
	
	public RecipeData() {
		 _recipeList = new RecipeList[MAX_RECIPE];
		addRecipeList();
	}
	
	private void addRecipeList() {
		/**************************/
		/*Boots of Intelligent*/
		/*************************/
		_recipeList[0]._recipeId = RecipeID.Boot_of_Intelliget_Recipe;
		_recipeList[0]._itemIngredient = new ItemID[2];
		_recipeList[0]._amountIngredient = new int[2];
		_recipeList[0]._itemIngredient[0] = ItemID.OldBoots;
		_recipeList[0]._amountIngredient[0] = 1;
		_recipeList[0]._itemIngredient[1] = ItemID.RabbitFur;
		_recipeList[0]._amountIngredient[1] = 2;
		_recipeList[0]._itemResult = ItemID.Boots_of_Intelligent;
		_recipeList[0]._itemResultType = ItemTypes.Legendary;
		_recipeList[0]._showRecipe = false;
		_recipeList[0]._unlockRecipe = true;

		/**************************/
		/*Boots of Intelligent*/
		/*************************/
		_recipeList[1]._recipeId = RecipeID.FortunePotion_Recipe;
		_recipeList[1]._itemIngredient = new ItemID[2];
		_recipeList[1]._amountIngredient = new int[2];
		_recipeList[1]._itemIngredient[0] = ItemID.CognitiveHerb;
		_recipeList[1]._amountIngredient[0] = 20;
		_recipeList[1]._itemIngredient[1] = ItemID.Water_of_Cactus;
		_recipeList[1]._amountIngredient[1] = 1;
		_recipeList[1]._itemResult = ItemID.FortunePotion;
		_recipeList[1]._itemResultType = ItemTypes.Quest;
		_recipeList[1]._showRecipe = false;
		_recipeList[1]._unlockRecipe = false;
	}
	
	public RecipeID getRecipeID(int index) {
		return _recipeList[index]._recipeId;
	}
	
	public ItemID getItemIngredient(int index, int item) {
		return _recipeList[index]._itemIngredient[item];
	}
	
	public int getIngredientAmount(int index, int item) {
		return _recipeList[index]._amountIngredient[item];
	}
	
	public ItemID getItemResult(int index) {
		return _recipeList[index]._itemResult;	
	}
	
	public ItemTypes getItemResultType(int index) {
		return _recipeList[index]._itemResultType;	
	}
	
	public bool getShowRecipe(int index) {
		return _recipeList[index]._showRecipe;
	}
	
	public void setShowRecipe(int index, bool show) {
		_recipeList[index]._showRecipe = show;	
	}
	
	public bool getUnlockRecipe(int index) {
		return _recipeList[index]._unlockRecipe;	
	}
	
	public void setUnlockRecipe(int index, bool show) {
		_recipeList[index]._unlockRecipe = show;	
	}
		
	public int getRecipeLength() {
		return MAX_RECIPE;
	}
	
	public int getIngredientLegth(int index) {
		return _recipeList[index]._itemIngredient.Length;
	}
	
	public bool checkIngredient(int recipeIndex, ItemID itemID) {
		//_recipeList[recipeIndex]._itemIngredient[.Equals()
		
		bool flag = false;
		for(int i = 0; i < _recipeList[recipeIndex]._itemIngredient.Length; i++) {
			if(_recipeList[recipeIndex]._itemIngredient[i] == itemID)
				flag = true;
		}
		return flag;
		
	}
	
	public int getIndexIngredientAmount(int recipeIndex, ItemID itemID) {
		//_recipeList[recipeIndex]._itemIngredient[.Equals()
		
		int index = 0;
		for(int i = 0; i < _recipeList[recipeIndex]._itemIngredient.Length; i++) {
			if(_recipeList[recipeIndex]._itemIngredient[i] == itemID)
				index = i;
		}
		return index;
		
	}
	/*
	protected RecipeID[] _recipeId;
	protected ItemID[][] _itemIngredient;
	protected ItemID[] _itemResult;
	protected bool[] _showRecipe;
	*/
/*
	protected QuestID _questId;
	protected string _questName;
	protected QuestProcess _questProcess;
	protected QuestType _questType;
	protected int _questDialogue;
	protected string _questDetail;
	protected string _questGoal;
	protected ItemID _questItemReward;
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
*/
}

public enum RecipeID {
		None = 0,
		Boot_of_Intelliget_Recipe = 1,
		FortunePotion_Recipe = 2,
		WandOfTruth = 3
}
	