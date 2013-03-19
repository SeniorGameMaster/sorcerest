using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerGUI : MonoBehaviour {
	
	public GUISkin mySkin;

	public float buttonWidth = 40;
	public float buttonHeight = 40;
	public float buttonquestWidth = 90;
	public float buttonquestHeight = 90;
	public float closeButtonWidth = 20;
	public float closeButtonHeight = 20;
	
	public float QuestWindowHeight = 400;
	public float QuestWindowWidth = 100;
	
	private float _offset = 10;
	private int _alchemyButtonWidth = 80;
	private int _alchemyButtonHeight = 35;
	private int _amountButtonWidth = 50;
	private int _amountButtonHeight = 30;
	
	/****************************************************/
	/* Loot window variable */
	/***************************************************/
	private int _lootWindowWidth = 250;
	private int _lootWindowHeight = 300;
	private bool _displayLootWindow = false;
	private const int LOOT_WINDOW_ID = 0;
	private Rect _lootWindowRect = new Rect(Screen.width/2 - 125, Screen.height/2 - 150, 250, 300);
	private Vector2 _lootWindowSlider = Vector2.zero;
	
	public static Chest chest;//Not use
	public static ItemDrops itemdrops;//Not use
	public static LootingItem lootingItem;
	private string _toolTip = "";
	private int _toolPosX, _toolPosY;
	
	/****************************************************/
	/* Inventory window variables */
	/***************************************************/
	private bool _displayInventoryWindow = true;
	private const int INVENTORY_WINDOW_ID = 1;
	private Rect _inventoryWindowRect = new Rect(10, Screen.height/2, 345, 140);
	private int _inventoryRows = 2;
	private int _inventoryCols = 6;
	
	/***/
	public enum selectItemState {
		None = 0,
		InventoryToAlchemy = 1,
		AlchemyToInventory = 2,
		LootToInventory = 3
	}
	
	
	private bool _displaySelectItemAmount = false;
	private const int AMOUNT_WINDOW_ID = 6;
	private int _amountPosX, _amountPosY;
	private Rect _amountWindowRect;
	private int _amountWindowWidth = 150;
	private int _amountWindowHeight = 100;
	
	private int _maxSelectAmount;
	private int _minSelectAmount = 1;
	private int _curSelecAmount;
	private bool _submitAmount = false;
	private int _itemIndex;
	private selectItemState _selectState;
	
	/****************************************************/
	/* Quest window variable */
	/***************************************************/
	//public static BaseQuest baseQuest;
	private QuestID currentQuestID = QuestID.None;
	private bool _displayQuestWindow = false;
	private const int QUEST_WINDOW_ID = 2;
	private Rect _questWindowRect = new Rect(10, 60, 300, 300);
	
	/****************************************************/
	/* QuestLog window variables */
	/***************************************************/
	private bool _displayQuestlogWindow = false;
	private const int QUESTLOG_WINDOW_ID = 3;
	private Rect _questlogWindowRect = new Rect(Screen.width - 360, 10, 350, 300);
	private Vector2 questlogScroll;
	
	private int questCount = 0;
	private List<QuestID> questList = new List<QuestID>();
	private List<string> itemquestList = new List<string>();
	
	public static LegendaryQuest legendaryQuest = new LegendaryQuest();
	public static BootsQuest bootsQuest = new BootsQuest();
	public static WandQuest wandQuest = new WandQuest();
	public static WaterQuest waterQuest = new WaterQuest();
	
	/****************************************************/
	/* Encycopedia window variables */
	/***************************************************/
	private bool _displayEncycopediaWindow = false;
	private const int ENCYCOPEDIA_WINDOW_ID = 4;
	private Rect _encycopediaWindowRect = new Rect(Screen.width - 300, Screen.height/2, 250, 350);	
	private Vector2 _encycopediaScroll;
	private int _displayData = 0; //0 is recipe, 1 is monsterData
	RecipeData recipeData = new RecipeData();
	EncycopediaData encycopediaData = new EncycopediaData();
	
	/****************************************************/
	/* Alchemy Ingredient window variables */
	/***************************************************/
	private bool _displayAlchemyWindow = false;
	private const int ALCHEMY_WINDOW_ID = 5;
	private Rect _alchemyWindowRect = new Rect(Screen.width/2 - 100, Screen.height/2 - 200, 300, 400);	
	public Texture testTexture;
	private Rect[] ingredientPos = new Rect[7];
	string mixDescription;
	
	// Use this for initialization
	void Start () {
		AddQuest(QuestID.GetAllLegendaryItem);
		ingredientPos[0] = new Rect(130, 30, buttonWidth, 	buttonHeight);
		ingredientPos[1] = new Rect(40,  30 + buttonHeight, buttonWidth, buttonHeight);
		ingredientPos[2] = new Rect(220, 30 + buttonHeight, buttonWidth, buttonHeight);
		ingredientPos[3] = new Rect(40, 45 + buttonHeight*3, buttonWidth, buttonHeight);
		ingredientPos[4] = new Rect(220 ,45+buttonHeight*3, buttonWidth, buttonHeight);
		ingredientPos[5] = new Rect(130, 45+buttonHeight*4, buttonWidth, buttonHeight);
		ingredientPos[6] = new Rect(130, 40+buttonHeight*2, buttonWidth, buttonHeight);
		//_lootItems = new List<Item>();
	}
	
	private void OnEnable() {
		Messenger.AddListener("CloseChest", ClearWindow);
		
		Messenger.AddListener("DisplayLoot", DisplayLoot);	
		Messenger.AddListener("CloseLoot", ClearWindow);
		
		Messenger.AddListener("ToggleInventory", ToggleInventoryWindow);
		Messenger.AddListener("ToggleQuestlog", ToggleQuestlogWindow);
		Messenger.AddListener("ToggleRecipe", ToggleRecipeWindow);	
		
		/****************************************************/
		/* Quest Listener */
		/***************************************************/
		Messenger.AddListener("AddQuestID1", AddQuestID1);
		Messenger.AddListener("AddQuestID2", AddQuestID2);
		Messenger.AddListener("AddQuestID3", AddQuestID3);
		Messenger.AddListener("AddQuestID4", AddQuestID4);
		Messenger.AddListener("CloseQuest", ClearQuest);
		/****************************************************/
		/* Mixing Listener */
		/***************************************************/
		Messenger.AddListener("DisplayAlchemy", DisplayAlchemy);
		Messenger.AddListener("CloseAlchemy", CloseAlchemy);
		
	}
	
	private void OnDisable() {
		Messenger.RemoveListener("CloseChest", ClearWindow);
		
		Messenger.RemoveListener("DisplayLoot", DisplayLoot);
		Messenger.RemoveListener("CloseLoot", ClearWindow);
		
		Messenger.AddListener("ToggleInventory", ToggleInventoryWindow);
		Messenger.AddListener("TogglelQuestlog", ToggleQuestlogWindow);
		Messenger.AddListener("ToggleRecipe", ToggleRecipeWindow);
		
		/****************************************************/
		/* Quest Remove Listener */
		/***************************************************/
		Messenger.RemoveListener("AddQuestID1", AddQuestID1);
		Messenger.RemoveListener("AddQuestID2", AddQuestID2);
		Messenger.RemoveListener("AddQuestID3", AddQuestID3);
		Messenger.RemoveListener("AddQuestID4", AddQuestID4);
		Messenger.RemoveListener("CloseQuest", ClearQuest);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		GUI.skin = mySkin;
		
		
		/*
		if(_displayLootWindow)
			_lootWindowRect = GUI.Window(LOOT_WINDOW_ID, 
									new Rect(Screen.width - (_offset + _lootWindowWidth),Screen.height - (_offset + lootWindowHeight),
									Screen.width - (_offset * 2), lootWindowHeight), LootWindow, "Loot Window");
		*/
		if(_displayLootWindow)
			_lootWindowRect = GUI.Window(LOOT_WINDOW_ID, _lootWindowRect, LootWindow, "Loot Window");
		
		if(_displayInventoryWindow)
			_inventoryWindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventoryWindowRect, InventoryWindow, "Inventory");
		
		if(_displaySelectItemAmount) {
			_amountWindowRect = GUI.Window(AMOUNT_WINDOW_ID, _amountWindowRect,AmountWindow,"Select Amount");
			
		}
		
		if(_displayQuestWindow)
			_questWindowRect = GUI.Window(QUEST_WINDOW_ID, 
									_questWindowRect, QuestWindow, "Quest Window");
		
		if(_displayQuestlogWindow)
			_questlogWindowRect = GUI.Window(QUESTLOG_WINDOW_ID, _questlogWindowRect, QuestlogWindow, "Quest Log");
		
		if(_displayEncycopediaWindow)
			_encycopediaWindowRect = GUI.Window(ENCYCOPEDIA_WINDOW_ID, _encycopediaWindowRect, RecipeWindow, "Encycopedia");
		
		if(_displayAlchemyWindow)
				_alchemyWindowRect = GUI.Window(ALCHEMY_WINDOW_ID, _alchemyWindowRect, AlchemyWindow, "Alchemy");
		
		
		
		//DisplaySelectItemAmount();
		DisplayToolTip();
	}
			
	/****************************************************/
	/*LOOT FUNCTION*/
	/****************************************************/
	private void LootWindow(int id)
	{
		GUI.skin = mySkin;
		/*		
		if(GUI.Button(new Rect(_lootWindowRect.width - 20, 0, closeButtonWidth, closeButtonHeight), "x")) {
			ClearWindow();
		}*/
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
		
		
		
		if(GUI.Button(new Rect(_offset * 3, (_lootWindowHeight - _alchemyButtonHeight) - 15, _alchemyButtonWidth, _alchemyButtonHeight),"GET ALL")) {
			getAllItemFromLootToInventory();
			CheckQuestItem();
		}
		
		
		
		if(GUI.Button(new Rect(_offset * 6 + _alchemyButtonWidth, (_lootWindowHeight - _alchemyButtonHeight) - 15, _alchemyButtonWidth, _alchemyButtonHeight),"CANCEL"))
			ClearWindow();
		
		
		int lootCount = lootingItem.loot.Count;
		
		_lootWindowSlider = GUI.BeginScrollView(new Rect(_offset * 1, _offset * 2, _lootWindowRect.width - _offset * 2, _lootWindowRect.height - _offset * 8),
												_lootWindowSlider, 
												new Rect(0, 0, (buttonWidth) + _offset, (lootCount * buttonHeight) + _offset));
		
		for(int cnt = 0; cnt < lootCount; cnt++){
			
			if(lootingItem == null) //make this return because close windowloot will make lootingItem value be null
				return;
			
			setAmountWindowPosition();		
			if(GUI.Button(new Rect(5 ,5 + (5 * cnt) + (buttonHeight * cnt), buttonWidth, buttonHeight), new GUIContent(lootingItem.loot[cnt].Icon,lootingItem.loot[cnt].ToolTip()))){
	
				setShowWindowSelectAmount(true);
				setAmountVariable(lootingItem.loot[cnt].CurAmount, cnt, selectItemState.LootToInventory);	
					
				CheckQuestItem();
			}
			GUI.Box(new Rect(5 + _offset + buttonWidth , 5 + (5 * cnt) + (buttonHeight * cnt), _lootWindowWidth - (5 + _offset * 6 + buttonWidth),buttonHeight), lootingItem.loot[cnt].Name + " * " + lootingItem.loot[cnt].CurAmount );
		}	
					
		GUI.EndScrollView();
		
		SetToolTip();
		
	}
	
	private void DisplayLoot() {
		_displayLootWindow = true;
	}
		
	private void ClearWindow() {
		_displayLootWindow = false;	
		lootingItem.OnMouseUp();
		lootingItem = null;
	}
	
	/****************************************************/
	/*INVENTORY FUNCTION*/
	/****************************************************/
	public void ToggleInventoryWindow() {
		_displayInventoryWindow = !_displayInventoryWindow;	
	}
	
	private void InventoryWindow(int id) {
		int cnt = 0;
		for(int y = 0; y < _inventoryRows; y++) {
			for(int x = 0; x < _inventoryCols; x++) {	
				if(cnt < PlayerCharacter.Inventory.Count) {
					
					setAmountWindowPosition();			
					if(GUI.Button(new Rect(5 + (x * buttonWidth) + (5 * (x+1)), 20 + (y * buttonHeight) + (5 * (y+1)), buttonWidth, buttonHeight), new GUIContent(PlayerCharacter.Inventory[cnt].Icon, PlayerCharacter.Inventory[cnt].ToolTip()))) {
						
						//set mode Enum of the select amount such as delete, alchemy, drp etc.
						if(_displayAlchemyWindow) {
						setShowWindowSelectAmount(true);
						setAmountVariable(PlayerCharacter.Inventory[cnt].CurAmount, cnt, selectItemState.InventoryToAlchemy);
						}
						switch (PlayerCharacter.Inventory[cnt].Type) {
							
							case ItemTypes.Usable :
								itemUsing(PlayerCharacter.Inventory[cnt], cnt);
								break;
							
						default:break;
						}				
					}
				}
				else {		
						GUI.Label(new Rect(5 + (x * buttonWidth) + (5 * (x+1)), 20 + (y * buttonHeight) + (5 * (y+1))	, buttonWidth, buttonHeight),(x + y * _inventoryCols).ToString(), "box");		
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
	
	private void SetToolTip() {
		if(Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip) {
			if(_toolTip != "")
				_toolTip = "";
			
			if(GUI.tooltip != "") 
				_toolTip = GUI.tooltip;
		}
	}
	
	private void DisplayToolTip() {
		
		if(!_displaySelectItemAmount) {
			if(_toolTip != "") {
				_toolPosX = (int)Input.mousePosition.x;
				_toolPosY = (int)Input.mousePosition.y;
				_toolPosY = Screen.height - _toolPosY;
				GUI.depth = 0;
				GUI.Box (new Rect(_toolPosX, _toolPosY, 200, 100), _toolTip);
				//GUI.Box (new Rect(Screen.width / 2 - 100, 10, 200, 100), _toolTip);
			}
		}
		
	}
	
	private void addItemFromInventoryToAlchemy(int index) {
		if(_displayAlchemyWindow){
			if(PlayerCharacter.Alchemy.Count < 6) {
				if(_submitAmount) {
						PlayerCharacter.Inventory[index].CurAmount -= _curSelecAmount;
						
						if(PlayerCharacter.checkSameItemAlchemy(PlayerCharacter.Inventory[index].Id))
							PlayerCharacter.Alchemy[PlayerCharacter.getIndexOfSameItemAlchemy(PlayerCharacter.Inventory[index].Id)].CurAmount += _curSelecAmount;
						else
							PlayerCharacter.Alchemy.Add(ItemGenerator.addItemAmount(PlayerCharacter.Inventory[index],_curSelecAmount));	
					
						if(PlayerCharacter.Inventory[index].CurAmount == 0)
							PlayerCharacter.Inventory.RemoveAt(index);	
						}		
				}
		}
		setSubmitAmount(false);
	}
	
	private void addItemFromAlchemyToInventory(int index) {
		if(_displayAlchemyWindow){
			if(PlayerCharacter.Inventory.Count < PlayerCharacter.MAX_INVENTORY) {
				if(_submitAmount) {
						PlayerCharacter.Alchemy[index].CurAmount -= _curSelecAmount;
						
						if(PlayerCharacter.checkSameItemInventory(PlayerCharacter.Alchemy[index].Id))
							PlayerCharacter.Inventory[PlayerCharacter.getIndexOfSameItemInventory(PlayerCharacter.Alchemy[index].Id)].CurAmount += _curSelecAmount;
						else
							PlayerCharacter.Inventory.Add(ItemGenerator.addItemAmount(PlayerCharacter.Alchemy[index],_curSelecAmount));		
										
						if(PlayerCharacter.Alchemy[index].CurAmount == 0)
							PlayerCharacter.Alchemy.RemoveAt(index);	
					}		
				}
		}
		setSubmitAmount(false);
	}
	
	private void addItemFromLootToInventory(int index) {
		if(PlayerCharacter.Inventory.Count < PlayerCharacter.MAX_INVENTORY) {
				if(_submitAmount) {
						lootingItem.loot[index].CurAmount -= _curSelecAmount;
						
						if(PlayerCharacter.checkSameItemInventory(lootingItem.loot[index].Id))
							PlayerCharacter.Inventory[PlayerCharacter.getIndexOfSameItemInventory(lootingItem.loot[index].Id)].CurAmount += _curSelecAmount;
						else
							PlayerCharacter.Inventory.Add(ItemGenerator.addItemAmount(lootingItem.loot[index],_curSelecAmount));		
										
						if(lootingItem.loot[index].CurAmount == 0)
							lootingItem.loot.RemoveAt(index);	
					}		
				}
		setSubmitAmount(false);
	}
	
	private void getAllItemFromLootToInventory() {
		if(lootingItem == null)
			return;
		
		int count = lootingItem.loot.Count-1;
		for(int index = count; index >= 0; index--) {
			if(PlayerCharacter.Inventory.Count < PlayerCharacter.MAX_INVENTORY) {
				
				if(PlayerCharacter.checkSameItemInventory(lootingItem.loot[index].Id))
					PlayerCharacter.Inventory[PlayerCharacter.getIndexOfSameItemInventory(lootingItem.loot[index].Id)].CurAmount += lootingItem.loot[index].CurAmount;
				else
					PlayerCharacter.Inventory.Add(ItemGenerator.addItemAmount(lootingItem.loot[index],lootingItem.loot[index].CurAmount));		
				
				lootingItem.loot.RemoveAt(index);
			}
		}
	}
	
	/****************************************************/
	/*SELECTED AMOUNT FUNCTION*/
	/****************************************************/
	private void setAmountWindowPosition() {
		if (Input.GetMouseButton(0) && !_displaySelectItemAmount){
			_amountPosX = (int)Input.mousePosition.x;
			_amountPosY = (int)Input.mousePosition.y;
			_amountPosY = Screen.height - _amountPosY;
			
			_amountWindowRect = new Rect(_amountPosX, _amountPosY, _amountWindowWidth, _amountWindowHeight);
			setSubmitAmount(false);
		}
	}
	
	private void setShowWindowSelectAmount(bool flag) {
		_displaySelectItemAmount = flag;
	}
	
	private void setAmountVariable(int max, int index, selectItemState state) {
		_curSelecAmount = _minSelectAmount;	
		_maxSelectAmount = max;
		_itemIndex = index;
		_selectState = state;
	}
	
	private void setSubmitAmount(bool flag) {
		_submitAmount = flag;	
	}
	
	private void AmountWindow(int id) {
		GUI.skin = mySkin;
		GUI.BringWindowToFront(AMOUNT_WINDOW_ID);
		
		if(GUI.Button(new Rect(10,20,30,30),"-")) {
			if(_minSelectAmount < _curSelecAmount)
				_curSelecAmount--;
		}
		
		string _temp = GUI.TextArea(new Rect(60,22,30,25),_curSelecAmount.ToString(),2);
		
		int number;
		bool result = Int32.TryParse(_temp, out number);
			if(result) 
				_curSelecAmount = number;
			else 
				_curSelecAmount = 0;
		
		
		//GUI.Label(new Rect(70,20,30,30),_curSelecAmount.ToString(),GUI.skin.customStyles[0].fontSize(15));
		
		if(GUI.Button(new Rect(110,20,30,30),"+")) {
			if(_curSelecAmount < _maxSelectAmount)
				_curSelecAmount++;
		}
		
		
		if(_curSelecAmount != 0 &&(GUI.Button(new Rect(20,60,_amountButtonWidth,_amountButtonHeight),"Ok"))) {
			setSubmitAmount(true);
			setShowWindowSelectAmount(false);
			switch(_selectState) {
			case selectItemState.InventoryToAlchemy :
				addItemFromInventoryToAlchemy(_itemIndex);	
				break;
			case selectItemState.AlchemyToInventory :
				addItemFromAlchemyToInventory(_itemIndex);	
				break;
			case selectItemState.LootToInventory :
				addItemFromLootToInventory(_itemIndex);
				break;
			
			}		
		}
		
		if(GUI.Button(new Rect(80,60,_amountButtonWidth,_amountButtonHeight),"Cancel")) {
			setSubmitAmount(false);
			setShowWindowSelectAmount(false);
		}
		
		GUI.DragWindow();
	}
	
	/****************************************************/
	/*QUEST FUNCTION*/
	/****************************************************/
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
		case QuestID.WaterFromCactus:
			WaterQuest();
			break;
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
			if(bootsQuest.Process == QuestProcess.Complete) {
				if(GUI.Button(new Rect(100, 150, buttonquestWidth, buttonquestHeight), "THANK")) 
				{
					bootsQuest.NextDialogue();
					for (int i = 0; i < bootsQuest.ItemGoal.Length; i++) {
						for(int cnt = 0; cnt < PlayerCharacter.Inventory.Count; cnt++) {
							if(PlayerCharacter.Inventory[cnt].Name == bootsQuest.checkItemGoal(i))
								PlayerCharacter.Inventory[cnt].CurAmount -= bootsQuest.checkItemMax(i);
							
							if(PlayerCharacter.Inventory[cnt].CurAmount <= 0)
								PlayerCharacter.Inventory.RemoveAt(cnt);			
						}
					}
					
					
					PlayerCharacter.Inventory.Add(ItemGenerator.CreatingProcess(bootsQuest.TypeReward, bootsQuest.Reward, 1));
					bootsQuest.NextDialogue();
					questList.Remove(bootsQuest.Id);
				}
			}
			else {
				if(GUI.Button(new Rect(100, 150, buttonquestWidth, buttonquestHeight), "LET'S SEE")) 
				{
					bootsQuest.BackDialogue();
				}
			}
			break;
		case 4:
			if(GUI.Button(new Rect(30, 150, buttonquestWidth, buttonquestHeight), "LET'S GO")) {
				
				GameObject owlDesert = Helpers.Find("WarpAltar_Desert1",typeof(GameObject)) as GameObject;
				Vector3 dropPosition = new Vector3(owlDesert.transform.position.x, owlDesert.transform.position.y, owlDesert.transform.position.z);
				//GameObject owlDesert = Helpers.Find("OwlFountainSand",typeof(GameObject)) as GameObject;
				//Vector3 dropPosition = new Vector3(owlDesert.transform.position.x - 60, owlDesert.transform.position.y - 70, owlDesert.transform.position.z +70);
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				
				player.transform.position = dropPosition;
				/*
				dropPosition = new Vector3(destination.transform.position.x, destination.transform.position.y + 3, destination.transform.position.z - 7);
				if(other.transform.CompareTag("Player")) 
			other.transform.position = dropPosition;*/
			
			}
				
			if(GUI.Button(new Rect(20 + buttonquestWidth + 60, 150, buttonquestWidth, buttonquestHeight), "NO"))
				bootsQuest.BackDialogue();
			
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
	
	private void WaterQuest() {
		
		GUILayout.Label(waterQuest.WaterQuestTalk());
		switch(waterQuest.Dialogue) {
		case 0:
			if(GUI.Button(new Rect(30, 150, buttonquestWidth, buttonquestHeight), "YES"))
				waterQuest.NextDialogue();
				
			if(GUI.Button(new Rect(20 + buttonquestWidth + 60, 150, buttonquestWidth, buttonquestHeight), "NO"))
				waterQuest.BackDialogue();
			break;
		case 1:
			if(GUI.Button(new Rect(30, 150, buttonquestWidth, buttonquestHeight), "WHERE"))
				waterQuest.NextDialogue();
				
			if(GUI.Button(new Rect(20 + buttonquestWidth + 60, 150, buttonquestWidth, buttonquestHeight), "MAYBE")) 
				waterQuest.BackDialogue();
			break;
		case 2:				
			if(GUI.Button(new Rect(100, 150, buttonquestWidth, buttonquestHeight), "OK")) 
			{		
				waterQuest.NextDialogue();
				ClearQuest();
				AddQuest(QuestID.WaterFromCactus);
				ToggleQuestlogWindow();
			}
			break;
		case 3:
			if(waterQuest.Process == QuestProcess.Complete) {
				if(GUI.Button(new Rect(30, 150, buttonquestWidth, buttonquestHeight), "YES"))
				{
					GameObject owlSand = Helpers.Find("WaterSandFountain",typeof(GameObject)) as GameObject;
					
					owlSand.SetActive(true);
					//owlSand.gameObject.
					//owlSand.GetComponent<Water Fountain>
					
					waterQuest.NextDialogue();
				}
				
				if(GUI.Button(new Rect(20 + buttonquestWidth + 60, 150, buttonquestWidth, buttonquestHeight), "NO")) 
					waterQuest.BackDialogue();
				
					/*
					for (int i = 0; i < bootsQuest.ItemGoal.Length; i++) {
						for(int cnt = 0; cnt < PlayerCharacter.Inventory.Count; cnt++) {
							if(PlayerCharacter.Inventory[cnt].Name == bootsQuest.checkItemGoal(i))
								PlayerCharacter.Inventory[cnt].CurAmount -= bootsQuest.checkItemMax(i);
							
							if(PlayerCharacter.Inventory[cnt].CurAmount <= 0)
								PlayerCharacter.Inventory.RemoveAt(cnt);			
						}
					}
					*/
					
			}
			else {
				if(GUI.Button(new Rect(100, 150, buttonquestWidth, buttonquestHeight), "Hmm")) 
				{
					waterQuest.BackDialogue();
				}
			}
			break;
		case 4:
			if(GUI.Button(new Rect(100, 150, buttonquestWidth, buttonquestHeight), "THANK")) {
				PlayerCharacter.Inventory.Add(ItemGenerator.CreatingProcess(waterQuest.TypeReward, waterQuest.Reward, 1));
				questList.Remove(waterQuest.Id);
				waterQuest.NextDialogue();
			}

			break;
		case 5:
			if(GUI.Button(new Rect(30, 150, buttonquestWidth, buttonquestHeight), "GO")) {
				GameObject owlForest = Helpers.Find("WarpAltar_Forest2",typeof(GameObject)) as GameObject;
				Vector3 dropPosition = new Vector3(owlForest.transform.position.x, owlForest.transform.position.y + 5, owlForest.transform.position.z - 5);
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				
				player.transform.position = dropPosition;	
			}
				
			if(GUI.Button(new Rect(20 + buttonquestWidth + 60, 150, buttonquestWidth, buttonquestHeight), "THANK")) 
				waterQuest.BackDialogue();
			
			break;
			
		default:break;
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
				
			case QuestID.WaterFromCactus:
				for (int i = 0; i < waterQuest.ItemGoal.Length; i++) {
						itemquestList.Add(waterQuest.checkItemGoal(i));
					}
				break;
				
			default:break;
			}
		}
	}
	
	private void AddQuestID1() {
		//currentQuestID = QuestID.FindBoots;
		//DisplayQuest();
	}
	
	private void AddQuestID2() {
		currentQuestID = QuestID.FindBoots;
		DisplayQuest();
	}
	
	private void AddQuestID3() {
		currentQuestID = QuestID.WandOfTruth;
		DisplayQuest();
	}
	
	private void AddQuestID4() {
		currentQuestID = QuestID.WaterFromCactus;
		DisplayQuest();
	}
	
	private void DisplayQuest() {
		_displayQuestWindow = true;
	}

	private void ClearQuest() {
		_displayQuestWindow = false;		
		currentQuestID = 0;
	}
	
	/****************************************************/
	/*QUESTLOG FUNCTION*/
	/****************************************************/
	public void ToggleQuestlogWindow() {
		CheckAllQuestLog();
		CheckQuestItem();
		_displayQuestlogWindow = !_displayQuestlogWindow;	
	}
	
	private void QuestlogWindow(int id) {
			
		if(questCount == 0) {
			GUILayout.Label("There is no quest.");
		}
		else {
				int questNumber = 1;
				questlogScroll = GUILayout.BeginScrollView(questlogScroll, GUILayout.Width(330), GUILayout.Height(270));
				
				
			foreach (QuestID questID in questList) {
				switch (questID) {
					
				case QuestID.GetAllLegendaryItem:
					
					bool showQ1 = legendaryQuest.Showing;
					showQ1 = GUILayout.Toggle(showQ1," "+questNumber+". "+legendaryQuest.Goal+" [" + legendaryQuest.Process + "]",GUILayout.Width(400.0f));
					legendaryQuest.changeShowing(showQ1);
					
					
					if(legendaryQuest.Showing) {
						for (int i = 0; i < legendaryQuest.ItemGoal.Length; i++) {							
							GUILayout.Toggle(legendaryQuest.checkItemCur(i) >= legendaryQuest.checkItemMax(i) ? true : false, " " + legendaryQuest.checkItemGoal(i) + " ( " + legendaryQuest.checkItemCur(i) + " / " + legendaryQuest.checkItemMax(i) + " )");
						}
					}
					
					break;
				
				case QuestID.FindBoots:
					
					bool showQ2 = bootsQuest.Showing;
					showQ2 = GUILayout.Toggle(showQ2," "+questNumber+". "+bootsQuest.Goal+" [" + bootsQuest.Process + "]",GUILayout.Width(400.0f));
					bootsQuest.changeShowing(showQ2);
					
					
					if(bootsQuest.Showing) {
						for (int i = 0; i < bootsQuest.ItemGoal.Length; i++) {
							GUILayout.Toggle(bootsQuest.checkItemCur(i) >= bootsQuest.checkItemMax(i) ? true : false, " " + bootsQuest.checkItemGoal(i) + " ( " + bootsQuest.checkItemCur(i) + " / " + bootsQuest.checkItemMax(i) + " )");
						}
					}
				
					
					break;
					
				case QuestID.WandOfTruth:
					
					bool showQ3 = wandQuest.Showing;
					showQ3 = GUILayout.Toggle(showQ3," "+questNumber+". "+wandQuest.Goal+" [" + wandQuest.Process + "]",GUILayout.Width(400.0f));
					wandQuest.changeShowing(showQ3);
					
					if(wandQuest.Showing) {
						for (int i = 0; i < wandQuest.ItemGoal.Length; i++) {
							GUILayout.Toggle(wandQuest.checkItemCur(i) >= wandQuest.checkItemMax(i) ? true : false, " " + wandQuest.checkItemGoal(i) + " ( " + wandQuest.checkItemCur(i) + " / " + wandQuest.checkItemMax(i) + " )");
						}
					}
		
					break;	
				
				case QuestID.WaterFromCactus:
					
					bool showQ4 = waterQuest.Showing;
					showQ4 = GUILayout.Toggle(showQ4," "+questNumber+". "+waterQuest.Goal+" [" + waterQuest.Process + "]",GUILayout.Width(400.0f));
					waterQuest.changeShowing(showQ4);
					
					if(waterQuest.Showing) {
						for (int i = 0; i < waterQuest.ItemGoal.Length; i++) {
							GUILayout.Toggle(waterQuest.checkItemCur(i) >= waterQuest.checkItemMax(i) ? true : false, " " + waterQuest.checkItemGoal(i) + " ( " + waterQuest.checkItemCur(i) + " / " + waterQuest.checkItemMax(i) + " )");
						}
					}
		
					break;	
					
				default:break;
				}
				
				questNumber++;
				}
	
		GUILayout.EndScrollView();
		
		}
		
		GUI.DragWindow();
		
	}
	
	private void CheckQuestItem() {
		int count = 0;
		for(int j = 0; j < itemquestList.Count; j++) {
			
			for(int i = 0; i < PlayerCharacter.Inventory.Count; i++) {	
				if(PlayerCharacter.Inventory[i].Name.Contains(itemquestList[j])) {	
					count = PlayerCharacter.Inventory[i].CurAmount;
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
			
			for (int ic = 0; ic <  waterQuest.ItemGoal.Length; ic++) {
				if(waterQuest.checkItemGoal(ic).Contains(itemquestList[j])) {
					waterQuest.addItemCur(ic,count);
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
				
				case QuestID.WaterFromCactus: 
					for (int all = 0; all < waterQuest.ItemGoal.Length; all++) {
						if(waterQuest.checkItemCur(all) >= waterQuest.checkItemMax(all))
							haveAllItem += 1;
					}
					if(haveAllItem.Equals(waterQuest.ItemGoal.Length))
						waterQuest.Process = QuestProcess.Complete;
						
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
	
	/****************************************************/
	/*RECIPE FUNCTION*/
	/****************************************************/
	public void ToggleRecipeWindow() {
		
		_displayEncycopediaWindow = !_displayEncycopediaWindow;	
	}
	
	private string recipeParseStar(string word) {
		string starString = "";
		for(int star = 0; star < word.Length; star++) {
			starString += "x";
		}
		return starString;
	}
	
	private void RecipeWindow(int id) {
		
		if(_displayData != 0) {
			if(GUI.Button(new Rect(20,25,90,20),"Recipe"))
				_displayData = 0;
		}
		else { 
			GUI.Label(new Rect(20,25,90,20),"Recipe","Box");
		}
		
		if(_displayData != 1){
			if(GUI.Button(new Rect(140,25,90,20),"MonsterData"))
				_displayData = 1;
		}
		else {
			GUI.Label(new Rect(140,25,90,20),"MonsterData","Box");
		}

		GUILayout.Label("");
	
		_encycopediaScroll = GUILayout.BeginScrollView(_encycopediaScroll, GUILayout.Width(230), GUILayout.Height(300));
		
		if(_displayData == 0) {
			int RecipeNumber = 1;
			bool showRecipe;
			string unlockRecipe;
			
			for(int recipeIndex = 0; recipeIndex < recipeData.getRecipeLength(); recipeIndex++) {
				
				if(recipeData.getUnlockRecipe(recipeIndex))
					unlockRecipe = recipeData.getItemResult(recipeIndex).ToString();
				else
					unlockRecipe = "Unknown";
						
				showRecipe = recipeData.getShowRecipe(recipeIndex);
				showRecipe = GUILayout.Toggle(showRecipe, " " + RecipeNumber + ". " + unlockRecipe,GUILayout.Width(300.0f));
				recipeData.setShowRecipe(recipeIndex,showRecipe);		
					
				if(recipeData.getShowRecipe(recipeIndex)) {
					for (int ingreIndex = 0; ingreIndex < recipeData.getIngredientLegth(recipeIndex); ingreIndex++) {
						if(recipeData.getUnlockRecipe(recipeIndex))
							GUILayout.Label("         "+recipeData.getItemIngredient(recipeIndex,ingreIndex).ToString() + " * " + recipeData.getIngredientAmount(recipeIndex,ingreIndex));
						else
							GUILayout.Label("         "+recipeParseStar(recipeData.getItemIngredient(recipeIndex,ingreIndex).ToString()));
					}				
				
				if(_displayAlchemyWindow) {
					GUILayout.BeginHorizontal();
						GUILayout.Space(35);
						
					if(!recipeData.getUnlockRecipe(recipeIndex))
						GUILayout.Button("HINT", GUILayout.Width(70), GUILayout.Height(20));
						
						GUILayout.Space(30);		
					GUILayout.EndHorizontal();	
					}
				}
				
				RecipeNumber++;
			}	
		}
		else if(_displayData == 1) {
				int EncycopediaNumber = 1;
				bool showEncycopedia;
				string unlockEncycopedia;
			
				for(int encycopediaIndex = 0; encycopediaIndex < encycopediaData.getEncycopediaLength(); encycopediaIndex++) {
				
					if(encycopediaData.getUnlockEncycopedia(encycopediaIndex))
						unlockEncycopedia = encycopediaData.getEncycopediaID(encycopediaIndex).ToString();
					else
						unlockEncycopedia = "Unknown";
							
					showEncycopedia = encycopediaData.getShowDetail(encycopediaIndex);
					showEncycopedia = GUILayout.Toggle(showEncycopedia, " " + EncycopediaNumber + ". " + unlockEncycopedia,GUILayout.Width(300.0f));
					encycopediaData.setShowDetail(encycopediaIndex,showEncycopedia);
				
					if(encycopediaData.getShowDetail(encycopediaIndex)) {
						if(encycopediaData.getUnlockEncycopedia(encycopediaIndex))
							GUILayout.Label("" + encycopediaData.getDataDescription(encycopediaIndex));
						else 
							GUILayout.Label("" + recipeParseStar(encycopediaData.getDataDescription(encycopediaIndex).ToString()));
					}
				
				EncycopediaNumber++;
			}
		}
		
		GUILayout.EndScrollView();
		GUI.DragWindow();		
	}
	
	/****************************************************/
	/*ALCHEMY FUNCTION*/
	/****************************************************/
	private void DisplayAlchemy() {
		_displayAlchemyWindow = true;
		if(!_displayEncycopediaWindow)
			_displayEncycopediaWindow = !_displayEncycopediaWindow;
	}
	
	private void CloseAlchemy() {
		_displayAlchemyWindow = false;
		getAllIngredientBack();
	}
	
	private void setUnlockRecipe(int index) {
		recipeData.setUnlockRecipe(index, true);
	}
		
	private void AlchemyWindow(int id) {	
		mixDescription = "Unknown";
		//TEXTURE GUI
		GUI.DrawTexture(new Rect(3, 0, 300, 350), testTexture, ScaleMode.ScaleToFit, true, 0.0F);
	
		for(int mixIndex = 0; mixIndex < 6; mixIndex++) {	
			if(PlayerCharacter.Alchemy.Count > mixIndex) {
				if(PlayerCharacter.Alchemy[mixIndex].Name != null ) {	
					
					setAmountWindowPosition();		
					if(GUI.Button(ingredientPos[mixIndex], new GUIContent(PlayerCharacter.Alchemy[mixIndex].Icon, PlayerCharacter.Alchemy[mixIndex].ToolTip()))) {
					//set mode Enum of the select amount such as delete, alchemy, drp etc.
					setShowWindowSelectAmount(true);
					setAmountVariable(PlayerCharacter.Alchemy[mixIndex].CurAmount, mixIndex, selectItemState.AlchemyToInventory);	
					
					}
				}
			}
			else
				GUI.Label(ingredientPos[mixIndex],"", "box");	
		}
		
		if(PlayerCharacter.MixResult.Count > 0) {
			if(PlayerCharacter.MixResult[0].Name.Length > 0) {
				if(GUI.Button(ingredientPos[6], new GUIContent(PlayerCharacter.MixResult[0].Icon, PlayerCharacter.MixResult[0].ToolTip()))) {
					PlayerCharacter.Inventory.Add(PlayerCharacter.MixResult[0]);
					PlayerCharacter.MixResult.RemoveRange(0,PlayerCharacter.MixResult.Count);
				}
			}
		}
		else 
			GUI.Label(ingredientPos[6],"", "box");	
		
		//Process Alchemy get description
		alchemyItem(0);
		
		GUI.Label(new Rect(20,270,260,50),mixDescription, "box");	
		
		//Combine the ingredient
		if(GUI.Button(new Rect(50,_alchemyWindowRect.height - 60,_alchemyButtonWidth,_alchemyButtonHeight),"COMBINE")) {
			alchemyItem(1);
//			GUI.Button(ingredientPos[6], new GUIContent(PlayerCharacter.Alchemy[7].Icon, PlayerCharacter.Alchemy[7].ToolTip()));
		}
		
		//Close Alchemy Window and Get All item back from Alchemy Window
		if(GUI.Button(new Rect(170,_alchemyWindowRect.height - 60,_alchemyButtonWidth,_alchemyButtonHeight),"CANCEL")) {
			CloseAlchemy();
		}
		
		SetToolTip();
		GUI.DragWindow();
	}
	
	//Fix it
	private void getAllIngredientBack() {
		int ingreLength = PlayerCharacter.Alchemy.Count-1;
		for(int getBack = ingreLength; getBack >= 0; getBack--) {
				PlayerCharacter.Inventory.Add(PlayerCharacter.Alchemy[getBack]);
				PlayerCharacter.Alchemy.RemoveAt(getBack);	
		}
	}
	
	private void removeAllIngredient() {
		int ingreLength = PlayerCharacter.Alchemy.Count-1;
		for(int getBack = ingreLength; getBack >= 0; getBack--) {
				PlayerCharacter.Alchemy.RemoveAt(getBack);	
		}
	}
	
	private void remove2Ingredient(ItemID slot1, ItemID slot2) {
		for(int index = 0; index < recipeData.getRecipeLength(); index++) {
			
			if(recipeData.checkIngredient(index,slot1) && recipeData.checkIngredient(index,slot2)) {
				PlayerCharacter.Alchemy[0].CurAmount -= recipeData.getIngredientAmount(index,recipeData.getIndexIngredientAmount(index,slot1));
				
				PlayerCharacter.Alchemy[1].CurAmount -= recipeData.getIngredientAmount(index,recipeData.getIndexIngredientAmount(index,slot2));
				
				if(PlayerCharacter.Alchemy[1].CurAmount <= 0)
					PlayerCharacter.Alchemy.RemoveAt(1);
				
				if(PlayerCharacter.Alchemy[0].CurAmount <= 0)
					PlayerCharacter.Alchemy.RemoveAt(0);
				
			}
			//recipeData.getIngredientIndex(index, slot1);
		}
	}
	
	private void alchemyItem(int type) {
		switch(PlayerCharacter.Alchemy.Count) {
		case 0:
		case 1:
			break;
			
		case 2:
			for(int index = 0; index < recipeData.getRecipeLength(); index++) {
				if(
					(
					(PlayerCharacter.Alchemy[0].Id == recipeData.getItemIngredient(index,0) && PlayerCharacter.Alchemy[0].CurAmount >= recipeData.getIngredientAmount(index,0)) || (PlayerCharacter.Alchemy[0].Id == recipeData.getItemIngredient(index,1) && PlayerCharacter.Alchemy[0].CurAmount >= recipeData.getIngredientAmount(index,1))
					)
					&& 
					(
					(PlayerCharacter.Alchemy[1].Id == recipeData.getItemIngredient(index,0) && PlayerCharacter.Alchemy[1].CurAmount >= recipeData.getIngredientAmount(index,0)) || (PlayerCharacter.Alchemy[1].Id == recipeData.getItemIngredient(index,1) && PlayerCharacter.Alchemy[1].CurAmount >= recipeData.getIngredientAmount(index,1))) 
					) {
						switch(type) {
						case 0:
						if(recipeData.getUnlockRecipe(index))
							mixDescription = recipeData.getItemResult(index).ToString();
							break;
						case 1:
							PlayerCharacter.MixResult.Add(ItemGenerator.CreatingProcess(recipeData.getItemResultType(index), recipeData.getItemResult(index), 1));					
							remove2Ingredient(PlayerCharacter.Alchemy[0].Id, PlayerCharacter.Alchemy[1].Id);
						
							if(!recipeData.getUnlockRecipe(index))
								setUnlockRecipe(index);
							break;
						}
				}
			}
			break;
		}
	}
	

	
	
	
	
	
	
	

	
	

	
	
}
