using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerGUI : MonoBehaviour {
	
	public GUISkin mySkin;
	
	public float lootWindowHeight = 90;
	public float buttonWidth = 40;
	public float buttonHeight = 40;
	public float buttonquestWidth = 90;
	public float buttonquestHeight = 90;
	public float closeButtonWidth = 20;
	public float closeButtonHeight = 20;
	
	public float QuestWindowHeight = 400;
	public float QuestWindowWidth = 100;
	//private List<Item> _lootItems;
	
	
	private float _offset = 10;

	/****************************************************/
	/* Loot window variable */
	/***************************************************/
	private bool _displayLootWindow = false;
	private const int LOOT_WINDOW_ID = 0;
	private Rect _lootWindowRect = new Rect(0, 0, 0, 0);
	private Vector2 _lootWindowSlider = Vector2.zero;
	
	public static Chest chest;//Not use
	public static ItemDrops itemdrops;//Not use
	public static LootingItem lootingItem;
	private string _toolTip = "";
	/****************************************************/
	/* Quest window variable */
	/***************************************************/
	public static BaseQuest baseQuest;
	private QuestID currentQuestID = QuestID.None;
	private bool _displayQuestWindow = false;
	private const int QUEST_WINDOW_ID = 2;
	private Rect _questWindowRect = new Rect(0, 0, 0, 0);
	
	/****************************************************/
	/* Inventory window variables */
	/***************************************************/
	private bool _displayInventoryWindow = true;
	private const int INVENTORY_WINDOW_ID = 1;
	private Rect _inventoryWindowRect = new Rect(10, 600, 370, 265);
	private int _inventoryRows = 6;
	private int _inventoryCols = 4;
	
	/****************************************************/
	/* QuestLog window variables */
	/***************************************************/
	private bool _displayQuestlogWindow = false;
	private const int QUESTLOG_WINDOW_ID = 3;
	private Rect _questlogWindowRect = new Rect(Screen.width - 360, 10, 350, 400);
	private Vector2 testScroll;
	
	private int questCount = 0;
	private List<QuestID> questList = new List<QuestID>();
	private List<string> itemquestList = new List<string>();
	
	LegendaryQuest legendaryQuest = new LegendaryQuest();
	BootsQuest bootsQuest = new BootsQuest();
	WandQuest wandQuest = new WandQuest();
	//private Vector2 _inventoryWindowSlider = Vector2.zero;
	
	// Use this for initialization
	void Start () {
		AddQuest(QuestID.GetAllLegendaryItem);
		//_lootItems = new List<Item>();
	}
	
	private void OnEnable() {
		//Messenger<int>.AddListener("PopulateChest", PopulateChest);
		Messenger.AddListener("CloseChest", ClearWindow);
		
		Messenger.AddListener("DisplayLoot", DisplayLoot);	
		Messenger.AddListener("CloseLoot", ClearWindow);
		
		Messenger.AddListener("ToggleInventory", ToggleInventoryWindow);
		Messenger.AddListener("ToggleQuestlog", ToggleQuestlogWindow);
		
		
		Messenger.AddListener("CloseQuest", ClearQuest);
		/****************************************************/
		/* Quest Listener */
		/***************************************************/
		Messenger.AddListener("AddQuestID1", AddQuestID1);
		Messenger.AddListener("AddQuestID2", AddQuestID2);
		Messenger.AddListener("AddQuestID3", AddQuestID3);
	}
	
	private void OnDisable() {
		//Messenger<int>.RemoveListener("PopulateChest", PopulateChest);
		Messenger.RemoveListener("CloseChest", ClearWindow);
		
		Messenger.RemoveListener("DisplayLoot", DisplayLoot);
		Messenger.RemoveListener("CloseLoot", ClearWindow);
		
		Messenger.AddListener("ToggleInventory", ToggleInventoryWindow);
		Messenger.AddListener("TogglelQuestlog", ToggleQuestlogWindow);
		
		Messenger.RemoveListener("CloseQuest", ClearQuest);
		/****************************************************/
		/* Quest Remove Listener */
		/***************************************************/
		Messenger.RemoveListener("AddQuestID1", AddQuestID1);
		Messenger.RemoveListener("AddQuestID2", AddQuestID2);
		Messenger.RemoveListener("AddQuestID3", AddQuestID3);
	}
	
	// Update is called once per frame
	void Update () {
		//checkQuestItem();
	}
	
	void OnGUI() {
		GUI.skin = mySkin;
		
		if(_displayInventoryWindow)
			_inventoryWindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventoryWindowRect, InventoryWindow, "Inventory");
		
		if(_displayQuestlogWindow)
			_questlogWindowRect = GUI.Window(QUESTLOG_WINDOW_ID, _questlogWindowRect, QuestlogWindow, "Quest Log");
		
		
		if(_displayLootWindow)
			_lootWindowRect = GUI.Window(LOOT_WINDOW_ID, 
									new Rect(_offset,Screen.height - (_offset + lootWindowHeight),
									Screen.width - (_offset * 2), lootWindowHeight), LootWindow, "Loot Window");
			
		
		if(_displayQuestWindow)
			_questWindowRect = GUI.Window(QUEST_WINDOW_ID, 
									new Rect(_offset, 0 + (_offset + QuestWindowHeight/8),
									QuestWindowWidth, QuestWindowHeight), QuestWindow, "Quest Window");
		
		DisplayToolTip();
	}
			
	private void LootWindow(int id) {
		GUI.skin = mySkin;
				
		if(GUI.Button(new Rect(_lootWindowRect.width - 20, 0, closeButtonWidth, closeButtonHeight), "x")) {
			ClearWindow();
		}
		/*
		if(chest == null)
			return;
		*/
		
		if(lootingItem == null)
			return;
		
		if(lootingItem.loot.Count == 0) {
			ClearWindow();
			return;
		}
		
		/*
		_lootWindowSlider = GUI.BeginScrollView(new Rect(_offset * .5f, 15, _lootWindowRect.width - 10, 70),
												_lootWindowSlider, 
												new Rect(0, 0, (_lootItems.Count * buttonWidth) + _offset, buttonHeight + _offset));
												*/
		_lootWindowSlider = GUI.BeginScrollView(new Rect(_offset * .5f, 15, _lootWindowRect.width - 10, 70),
												_lootWindowSlider, 
												new Rect(0, 0, (lootingItem.loot.Count * buttonWidth) + _offset, buttonHeight + _offset));
		

		
			for(int cnt = 0; cnt < lootingItem.loot.Count; cnt++){
				if(GUI.Button(new Rect(5 + (buttonWidth * cnt), 5, buttonWidth, buttonHeight), new GUIContent(lootingItem.loot[cnt].Icon,lootingItem.loot[cnt].ToolTip()))){
					PlayerCharacter.Inventory.Add(lootingItem.loot[cnt]);
					lootingItem.loot.RemoveAt(cnt);
					CheckQuestItem();
				}
			}	
		
		GUI.EndScrollView();
		
		SetToolTip();
	}
	
	private void CheckQuestItem() {
		int count = 0;
		//fix here
		
		for(int j = 0; j < itemquestList.Count; j++) {
			
			for(int i = 0; i < PlayerCharacter.Inventory.Count; i++) {	
				//Debug.Log(PlayerCharacter.Inventory[i].Name);
				//Debug.Log(itemquestList[j]);
				if(PlayerCharacter.Inventory[i].Name.Contains(itemquestList[j])) {	
					count++;
				}	
			}
			
			for (int ic = 0; ic <  legendaryQuest.ItemGoal.Length; ic++) {
				if(legendaryQuest.checkItemGoal(ic).Contains(itemquestList[j]))
					legendaryQuest.addItemCur(ic,count);
			}
			
			for (int ic = 0; ic <  bootsQuest.ItemGoal.Length; ic++) {
				if(bootsQuest.checkItemGoal(ic).Contains(itemquestList[j])) {
					bootsQuest.addItemCur(ic,count);
				}
			}
			
			for (int ic = 0; ic <  wandQuest.ItemGoal.Length; ic++) {
				if(wandQuest.checkItemGoal(ic).Contains(itemquestList[j])) {
					wandQuest.addItemCur(ic,count);
				}
			}
			
			count = 0;
		}
		
		for(int list = 0; list < questList.Count; list++) {
			int haveAllItem = 0;
			
			switch(questList[list]) {
								
				case QuestID.FindBoots: 
					
					for (int all = 0; all < bootsQuest.ItemGoal.Length; all++) {
						if(bootsQuest.checkItemCur(all) >= bootsQuest.checkItemMax(all))
							haveAllItem += 1;
					}
					//Debug.Log(haveAllItem + " " + bootsQuest.ItemGoal.Length);
					if(haveAllItem.Equals(bootsQuest.ItemGoal.Length))
						bootsQuest.Process = QuestProcess.Complete;
						
					break;
				
				case QuestID.WandOfTruth: 
					for (int all = 0; all < wandQuest.ItemGoal.Length; all++) {
						if(wandQuest.checkItemCur(all) >= wandQuest.checkItemMax(all))
							haveAllItem += 1;
					}
					if(haveAllItem.Equals(wandQuest.ItemGoal.Length))
						wandQuest.Process = QuestProcess.Complete;
						
					break;
					
				default:break;
				}
		}
		
		
		/*
		int furcheck = 0;
		for(int i = 0; i < PlayerCharacter.Inventory.Count; i++) {	
			if(PlayerCharacter.Inventory[i].Name.Contains("Old Boots") && foundOldbooots == false) {	
				foundOldbooots = true;
				bootInventory = 1;
			}
			
			if(PlayerCharacter.Inventory[i].Name.Contains("Rabbit Fur")) {	
				furcheck++;
				if(furInventory == 2) {
					foundRabbitfur = true;	
				}
			}
						
			
		}
		furInventory = furcheck;
		
		if(foundOldbooots && foundRabbitfur && questProgress.Contains("In progress")) {
			questProgress = "Complete";
		}		
		*/
	}
	
	private void InventoryWindow(int id) {
		int cnt = 0;
		
		for(int y = 0; y < _inventoryRows; y++) {
			for(int x = 0; x < _inventoryCols; x++) {	
				if(cnt < PlayerCharacter.Inventory.Count) {
					if(GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), new GUIContent(PlayerCharacter.Inventory[cnt].Icon, PlayerCharacter.Inventory[cnt].ToolTip()))) {
												
						switch (PlayerCharacter.Inventory[cnt].Type) {
							
							case ItemTypes.Usable :
								itemUsing(PlayerCharacter.Inventory[cnt], cnt);
								break;
							
						default:break;
						}
						
						
						/*
						if(PlayerCharacter.Inventory[cnt].Type.Equals(ItemTypes.Usable))//Enum.GetName(typeof(ItemTypes), 
						{
							if(itemUsing(PlayerCharacter.Inventory[cnt]))
								PlayerCharacter.Inventory.RemoveAt(cnt);		
						}
						
						if(_displayMixingWindow){
							PlayerCharacter.Mixing.Add(PlayerCharacter.Inventory[cnt]);
							PlayerCharacter.Inventory.RemoveAt(cnt);		
						}
						*/
					}
				}
				else {		
					GUI.Label(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight),(x + y * _inventoryCols).ToString(), "box");	
				}
				cnt++;
			}
		}
		
		SetToolTip();
		GUI.DragWindow();
	}
	
	private void itemUsing(Item usableItem, int index) {
		switch(usableItem.Id) {
			
		case ItemID.TruthScroll :
			AddQuestID3();
			
			break;
			
		default:break;
		}
	}
	
	private void QuestlogWindow(int id) {
			
		if(questCount == 0) {
			GUILayout.Label("There is no quest.");
		}
		else {
				int questNumber = 1;
				testScroll = GUILayout.BeginScrollView(testScroll, GUILayout.Width(350), GUILayout.Height(400));
				
				
			foreach (QuestID questID in questList) {
				switch (questID) {
					
				case QuestID.GetAllLegendaryItem:
					
					bool showQ1 = legendaryQuest.Showing;
					showQ1 = GUILayout.Toggle(showQ1," "+questNumber+". "+legendaryQuest.Goal+" [" + legendaryQuest.Process + "]",GUILayout.Width(600.0f));
					legendaryQuest.changeShowing(showQ1);
					
					if(legendaryQuest.Showing) {
						for (int i = 0; i < legendaryQuest.ItemGoal.Length; i++) {
							GUILayout.Toggle(legendaryQuest.checkItemCur(i) >= legendaryQuest.checkItemMax(i) ? true : false, " " + legendaryQuest.checkItemGoal(i) + " ( " + legendaryQuest.checkItemCur(i) + " / " + legendaryQuest.checkItemMax(i) + " )");
						}
					}
					
					break;
				
				case QuestID.FindBoots:
					
					bool showQ2 = bootsQuest.Showing;
					showQ2 = GUILayout.Toggle(showQ2," "+questNumber+". "+bootsQuest.Goal+" [" + bootsQuest.Process + "]",GUILayout.Width(600.0f));
					bootsQuest.changeShowing(showQ2);
					
					if(bootsQuest.Showing) {
						for (int i = 0; i < bootsQuest.ItemGoal.Length; i++) {
							GUILayout.Toggle(bootsQuest.checkItemCur(i) >= bootsQuest.checkItemMax(i) ? true : false, " " + bootsQuest.checkItemGoal(i) + " ( " + bootsQuest.checkItemCur(i) + " / " + bootsQuest.checkItemMax(i) + " )");
						}
					}
		
					break;
					
				case QuestID.WandOfTruth:
					
					bool showQ3 = wandQuest.Showing;
					showQ3 = GUILayout.Toggle(showQ3," "+questNumber+". "+wandQuest.Goal+" [" + wandQuest.Process + "]",GUILayout.Width(600.0f));
					wandQuest.changeShowing(showQ3);
					
					if(wandQuest.Showing) {
						for (int i = 0; i < wandQuest.ItemGoal.Length; i++) {
							GUILayout.Toggle(wandQuest.checkItemCur(i) >= wandQuest.checkItemMax(i) ? true : false, " " + wandQuest.checkItemGoal(i) + " ( " + wandQuest.checkItemCur(i) + " / " + wandQuest.checkItemMax(i) + " )");
						}
					}
		
					break;	
					}
				
				questNumber++;
				}
			
		GUILayout.EndScrollView();
		
		}
		
		GUI.DragWindow();
		
	}
	
	private void GenerateQuestDetail() {
	
		
	}

	public void ToggleInventoryWindow() {
		_displayInventoryWindow = !_displayInventoryWindow;	
	}

	public void ToggleQuestlogWindow() {
		CheckAllQuestLog();
		CheckQuestItem();
		_displayQuestlogWindow = !_displayQuestlogWindow;	
	}
	
	private void SetToolTip() {
		if(Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip) {
			if(_toolTip != "")
				_toolTip = "";
			
			if(GUI.tooltip != "")
				_toolTip = GUI.tooltip;
		}
	}
	
	private void DisplayToolTip() {
		if(_toolTip != "")
			GUI.Box (new Rect(Screen.width / 2 - 100, 10, 200, 100), _toolTip);
	}
	
	private void QuestWindow(int id) {
		GUI.skin = mySkin;
				
		if(GUI.Button(new Rect(_questWindowRect.width - 20, 0, closeButtonWidth, closeButtonHeight), "x")) {
			ClearQuest();
		}
		
		switch (currentQuestID) {
		case QuestID.None:
			ClearQuest();
			break;
		case QuestID.FindBoots:
			BootsQuest();
			break;
		case QuestID.WandOfTruth:
			WandQuest();
			break;
		}
	}
	
	private void AddQuest(QuestID questID) {
		questList.Add(questID);	
		
		itemquestList.Clear();
		foreach (QuestID questItem in questList) {
				switch (questItem) {
				
				case QuestID.GetAllLegendaryItem:
					for (int i = 0; i < legendaryQuest.ItemGoal.Length; i++) {
						itemquestList.Add(legendaryQuest.checkItemGoal(i));
					}
				break;
				
				case QuestID.FindBoots:
					for (int i = 0; i < bootsQuest.ItemGoal.Length; i++) {
						itemquestList.Add(bootsQuest.checkItemGoal(i));
					}
				break;
				
				case QuestID.WandOfTruth:
					for (int i = 0; i < wandQuest.ItemGoal.Length; i++) {
						itemquestList.Add(wandQuest.checkItemGoal(i));
					}
				break;
				
			default:break;
			}
		}
	}
	
	private void CheckAllQuestLog() {
		questCount = 0;
		for(int i = 0; i < questList.Count; i++) {
			questCount++;			
		}
		
	}
	
	private void BootsQuest() {
		
		GUILayout.Label(bootsQuest.BootsQuestTalk());
		switch(bootsQuest.Dialogue) {
		case 0:
			if(GUI.Button(new Rect(30, 150, buttonquestWidth, buttonquestHeight), "OK"))
				bootsQuest.NextDialogue();
				
			if(GUI.Button(new Rect(20 + buttonquestWidth + 60, 150, buttonquestWidth, buttonquestHeight), "NO"))
				bootsQuest.BackDialogue();
			break;
		case 1:
			if(GUI.Button(new Rect(30, 150, buttonquestWidth, buttonquestHeight), "YES"))
				bootsQuest.NextDialogue();
				
			if(GUI.Button(new Rect(20 + buttonquestWidth + 60, 150, buttonquestWidth, buttonquestHeight), "MAYBE")) 
				bootsQuest.BackDialogue();
			break;
		case 2:
			if(GUI.Button(new Rect(100, 150, buttonquestWidth, buttonquestHeight), "YUP")) 
			{		
				bootsQuest.NextDialogue();
				ClearQuest();
				AddQuest(QuestID.FindBoots);
				ToggleQuestlogWindow();
			}
			break;
		case 3:
			
			break;
		default:break;
		}
		
	}
	
	private void WandQuest() {
		GUILayout.Label(wandQuest.WandQuestTalk());
		switch(wandQuest.Dialogue) {
		case 0:
			if(GUI.Button(new Rect(30, 150, buttonquestWidth, buttonquestHeight), "READ"))
				wandQuest.NextDialogue();
				
			if(GUI.Button(new Rect(20 + buttonquestWidth + 60, 150, buttonquestWidth, buttonquestHeight), "BACK"))
				wandQuest.BackDialogue();
			break;
		case 1:
			if(GUI.Button(new Rect(30, 150, buttonquestWidth, buttonquestHeight), "GET")) {
				wandQuest.NextDialogue();
				AddQuest(QuestID.WandOfTruth);
				ToggleQuestlogWindow();
			}
			
			if(GUI.Button(new Rect(20 + buttonquestWidth + 60, 150, buttonquestWidth, buttonquestHeight), "NO")) 
				wandQuest.BackDialogue();
			break;
		case 2:
			if(GUI.Button(new Rect(30, 150, buttonquestWidth, buttonquestHeight), "CLOSE")) {
				ClearQuest();
			}		
			break;
			
		case 3:
			
			break;
		default:break;
		}
	}
	
	private void DisplayLoot() {
		_displayLootWindow = true;
	}
	
	/*
	private void PopulateChest(int x) {
		
		for(int cnt = 0; cnt < x; cnt++)
			_lootItems.Add(new Item());
		
		_displayLootWindow = true;
	}
	*/
	
	private void ClearWindow() {
		_displayLootWindow = false;		
		//_lootItems.Clear();
		
		lootingItem.OnMouseUp();
		
		lootingItem = null;
	}
	
	private void AddQuestID1() {
		currentQuestID = QuestID.FindBoots;
		DisplayQuest();
	}
	
	private void AddQuestID2() {
	//	currentQuestID = (int)QuestID.FindBoots;
	}
	
	private void AddQuestID3() {
		currentQuestID = QuestID.WandOfTruth;
		DisplayQuest();
		//currentQuestID = (int)QuestID.FindBoots;
	}
	
	
	private void DisplayQuest() {
		_displayQuestWindow = true;
	}

	private void ClearQuest() {
		_displayQuestWindow = false;		
		currentQuestID = 0;

		/*
		owlQuest.OnMouseUp();
		
		owlQuest = null;
		*/
	
	}
}
