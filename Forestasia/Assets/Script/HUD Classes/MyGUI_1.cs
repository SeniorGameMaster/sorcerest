using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MyGUI_1 : MonoBehaviour {
	
	public GUISkin mySkin;
	
	public float lootWindowHeight = 90;
	public float buttonWidth = 40;
	public float buttonHeight = 40;
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
	private List<int> questList = new List<int>();
	public static BaseQuest baseQuest;
	TestQuest testQuest = new TestQuest();
	private int currentQuestID = 0;
	//public static OwlQuest owlQuest;
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
	private List<string> itemquestList = new List<string>();
	
	private bool toggleQuest = false;
	private bool foundOldbooots = false;
	private bool foundRabbitfur = false;
	private string questProgress = "In progress";
	private int questBootAmount = 1;
	private int questFurAmount = 2;
	private int bootInventory = 0;
	private int furInventory = 0;
	
	//private Vector2 _inventoryWindowSlider = Vector2.zero;
	
	// Use this for initialization
	void Start () {
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
					checkQuestItem();
				}
			}	
		
		GUI.EndScrollView();
		
		SetToolTip();
	}
	
	private void checkQuestItem() {
		int count = 0;
		
		
		for(int j = 0; j < itemquestList.Count; j++) {
			
			
			for(int i = 0; i < PlayerCharacter.Inventory.Count; i++) {	
				Debug.Log(PlayerCharacter.Inventory[i].Name);
				Debug.Log(itemquestList[j]);
				if(PlayerCharacter.Inventory[i].Name.Contains(itemquestList[j])) {	
					count++;
					
				}	
			}
			testQuest.addItemCur(j,count);
			count = 0;
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
						/*
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
	
	private void QuestlogWindow(int id) {
			
		if(questCount == 0) {
			GUILayout.Label("There is no quest.");
		}
		else {
				int questNumber = 1;
				testScroll = GUILayout.BeginScrollView(testScroll, GUILayout.Width(350), GUILayout.Height(400));
				
				
				foreach (int questID in questList) {
				switch (questID) {
				case 1:
					
					bool showQ1 = testQuest.Showing;
					showQ1 = GUILayout.Toggle(showQ1," "+questNumber+". "+testQuest.Goal+" [" + testQuest.Process + "]",GUILayout.Width(600.0f));
					testQuest.changeShowing(showQ1);
					
					if(testQuest.Showing) {
						for (int i = 0; i < testQuest.ItemGoal.Length; i++) {
							GUILayout.Toggle(testQuest.checkItemCur(i) >= testQuest.checkItemMax(i) ? true : false, " " + testQuest.checkItemGoal(i) + " ( " + testQuest.checkItemCur(i) + " / " + testQuest.checkItemMax(i) + " )");
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
		case 0:
			ClearQuest();
			break;
		case 1:
			OwlQuest();
			break;
		}
	
		
			
		/*
		switch(dialogueOwl) {
			
		case 0:
			GUILayout.Label("HEY DUDE !! \nI'm a owl fountain. \n \nI have some problem...Can you help me!?");
			
			if(GUI.Button(new Rect(30, 150, buttonWidth, buttonHeight), "OK"))
				dialogueOwl = 1;
				
			if(GUI.Button(new Rect(20 + buttonWidth + 60, 150, buttonWidth, buttonHeight), "NO"))
				ClearQuest();
			
			break;
		case 1:
			GUILayout.Label("WOW !! You are so kindly !! \nLet's talk about my problem. " +
				"\n\nI drop my old boots in the forest.Can you find it for me.");
			
			if(GUI.Button(new Rect(30, 150, buttonWidth, buttonHeight), "YES"))
				dialogueOwl = 2;
				
			if(GUI.Button(new Rect(20 + buttonWidth + 60, 150, buttonWidth, buttonHeight), "MAYBE")) {
				ClearQuest();
				dialogueOwl = 0;
			}
			break;
		
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
					
					
				}
				if(GUI.Button(new Rect(20 + buttonWidth + 60, 150, buttonWidth, buttonHeight), "MAYBE"))
					ClearQuest();
			
			break;
		case 5:
				GUILayout.Label("Put some ingredients and then BOOOOOM !!");
			break;
		default:break;
		}
		*/
		
	}
	
	private void AddQuest(int questID) {
		questList.Add(questID);	
		
		itemquestList.Clear();
		foreach (int questItem in questList) {
				switch (questItem) {
				case 1:
					for (int i = 0; i < testQuest.ItemGoal.Length; i++) {
						itemquestList.Add(testQuest.checkItemGoal(i));
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
	
	private void OwlQuest() {
		
		GUILayout.Label(testQuest.TestQuestTalk());
		switch(testQuest.Dialogue) {
		case 0:
			if(GUI.Button(new Rect(30, 150, buttonWidth, buttonHeight), "OK"))
				testQuest.NextDialogue();
				
			if(GUI.Button(new Rect(20 + buttonWidth + 60, 150, buttonWidth, buttonHeight), "NO"))
				testQuest.BackDialogue();
			break;
		case 1:
			if(GUI.Button(new Rect(30, 150, buttonWidth, buttonHeight), "YES"))
				testQuest.NextDialogue();
				
			if(GUI.Button(new Rect(20 + buttonWidth + 60, 150, buttonWidth, buttonHeight), "MAYBE")) 
				testQuest.BackDialogue();
			break;
		case 2:
			if(GUI.Button(new Rect(100, 150, buttonWidth, buttonHeight), "YUP")) 
			{		
				testQuest.NextDialogue();
				ClearQuest();
				AddQuest((int)testQuest.Id);
				
				ToggleQuestlogWindow();
				
				
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
		currentQuestID = (int)QuestID.FindBoots;
		DisplayQuest();
	}
	
	private void AddQuestID2() {
	//	currentQuestID = (int)QuestID.FindBoots;
	}
	
	private void AddQuestID3() {
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
