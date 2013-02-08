using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyGUI : MonoBehaviour {
	
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
	
	/****************************************************/
	/* Quest window variable */
	/***************************************************/
	public static OwlQuest owlQuest;
	private bool _displayQuestWindow = false;
	private int dialogueOwl = 0;
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
	private Rect _questlogWindowRect = new Rect(Screen.width - 280, 10, 270, 400);
	private int questCount = 0;
	private bool toggleQuest = false;
	private bool foundOldbooots = false;
	private bool foundRabbitfur = false;
	private string questProgress = "In progress";
	private int questBootAmount = 1;
	private int questFurAmount = 2;
	private int bootInventory = 0;
	private int furInventory = 0;
	
	
	/****************************************************/
	/* Mixing window variables */
	/***************************************************/
	private bool _displayMixingWindow = false;
	private const int MIXING_WINDOW_ID = 4;
	private Rect _mixingWindowRect = new Rect(400, 80, 370, 300);
	
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
		
		Messenger.AddListener("DisplayQuest", DisplayQuest);
		Messenger.AddListener("CloseQuest", ClearQuest);
	}
	
	private void OnDisable() {
		//Messenger<int>.RemoveListener("PopulateChest", PopulateChest);
		Messenger.RemoveListener("CloseChest", ClearWindow);
		
		Messenger.RemoveListener("DisplayLoot", DisplayLoot);
		Messenger.RemoveListener("CloseLoot", ClearWindow);
		
		Messenger.AddListener("ToggleInventory", ToggleInventoryWindow);
		Messenger.AddListener("TogglelQuestlog", ToggleQuestlogWindow);
		
		Messenger.RemoveListener("DisplayQuest", DisplayQuest);
		Messenger.RemoveListener("CloseQuest", ClearQuest);
	}
	
	// Update is called once per frame
	void Update () {
		checkQuestItem();
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
		
		if(_displayMixingWindow)
			_mixingWindowRect = GUI.Window(MIXING_WINDOW_ID, _mixingWindowRect, MixingWindow, "Mixing Item");
		
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
				if(GUI.Button(new Rect(5 + (buttonWidth * cnt), 5, buttonWidth, buttonHeight), lootingItem.loot[cnt].Name)){
					PlayerCharacter.Inventory.Add(lootingItem.loot[cnt]);
					lootingItem.loot.RemoveAt(cnt);
				}
			}	
		
		GUI.EndScrollView();
	}
	
	private void checkQuestItem() {
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
	}
	
	private void InventoryWindow(int id) {
		int cnt = 0;
		
		for(int y = 0; y < _inventoryRows; y++) {
			for(int x = 0; x < _inventoryCols; x++) {	
				if(cnt < PlayerCharacter.Inventory.Count) {
					if(GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), PlayerCharacter.Inventory[cnt].Name)) {
						if(_displayMixingWindow){
							PlayerCharacter.Mixing.Add(PlayerCharacter.Inventory[cnt]);
							PlayerCharacter.Inventory.RemoveAt(cnt);		
						}
					}
				}
				else {		
					GUI.Label(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight),(x + y * _inventoryCols).ToString(), "box");	
				}
				cnt++;
			}
		}
		
		
		GUI.DragWindow();
	}
	
	private void QuestlogWindow(int id) {

			if(questCount == 0) {
				GUILayout.Label("There is no quest.");
			}
			else if(questCount == 1) {
			toggleQuest = GUILayout.Toggle(toggleQuest," 1. Help owl fountain find some items. \n  [" + questProgress + "]");
			//GUILayout.Label(" 1. Help owl fountain find some items. [" + questProgress + "]");
			
			if(toggleQuest) {
				GUI.Toggle(new Rect(40,55,120,20),foundOldbooots, " Old boots ( " + bootInventory + " / " + questBootAmount + " )");
				GUI.Toggle(new Rect(40,75,120,20),foundRabbitfur, " Rabbit fur ( " + furInventory + " / " + questFurAmount + " )");
				}
			}
		GUI.DragWindow();
	}
	public void GetAllMixingToInventory() {
		for(int x = 0; x < PlayerCharacter.Mixing.Count; x++) {
			PlayerCharacter.Inventory.Add(PlayerCharacter.Mixing[x]);
		}

		PlayerCharacter.Mixing.RemoveRange(0,PlayerCharacter.Mixing.Count);		
	
		
	}
	
	private void MixingWindow(int id) {
		int count = 0;
		
		for(int y = 0; y < 3; y++) {
			for(int x = 0; x < 4; x++) {	
				if(count < PlayerCharacter.Mixing.Count) {
					if(GUI.Button(new Rect(5 + (x * buttonWidth), _offset*2 + (y * buttonHeight), buttonWidth, buttonHeight), PlayerCharacter.Mixing[count].Name)) {				
							PlayerCharacter.Inventory.Add(PlayerCharacter.Mixing[count]);
							PlayerCharacter.Mixing.RemoveAt(count);						
					}
				}
				count++;
				
				if(GUI.Button(new Rect(70, 250, buttonWidth, buttonHeight), "MIX"))
					PlayerCharacter.Mixing.RemoveRange(0,PlayerCharacter.Mixing.Count);		
				
				if(GUI.Button(new Rect(200, 250, buttonWidth, buttonHeight), "CANCEL"))
					ClearQuest();
			}
		}
		
		
		GUI.DragWindow();
	}
	
	public void ToggleInventoryWindow() {
		_displayInventoryWindow = !_displayInventoryWindow;	
	}
	
	public void ToggleQuestlogWindow() {
		_displayQuestlogWindow = !_displayQuestlogWindow;	
	}
	
	public void ToggleMixingWindow() {
		_displayMixingWindow = !_displayMixingWindow;
	}
	
	private void QuestWindow(int id) {
		GUI.skin = mySkin;
				
		if(GUI.Button(new Rect(_questWindowRect.width - 20, 0, closeButtonWidth, closeButtonHeight), "x")) {
			ClearQuest();
		}
		
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
					ToggleMixingWindow();
					
				}
				if(GUI.Button(new Rect(20 + buttonWidth + 60, 150, buttonWidth, buttonHeight), "MAYBE"))
					ClearQuest();
			
			break;
		case 5:
				GUILayout.Label("Put some ingredients and then BOOOOOM !!");
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
	
	private void DisplayQuest() {
		_displayQuestWindow = true;
	}

	private void ClearQuest() {
		_displayQuestWindow = false;		
		
		owlQuest.OnMouseUp();
		
		owlQuest = null;
		
		if(_displayMixingWindow) {
			dialogueOwl = 4;
			ToggleMixingWindow();
			GetAllMixingToInventory();
		}
	}
}
