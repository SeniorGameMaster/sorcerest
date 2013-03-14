using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCharacter : BaseCharacter {
	public const int MAX_INVENTORY = 40;
	private static List<Item> _inventory = new List<Item>();
	private static List<Item> _alchemy = new List<Item>();
	private static List<Item> _alchemyResult = new List<Item>();
	
	public bool initialized = false;
	
	private static PlayerCharacter instance = null;
	public static PlayerCharacter Instance {
		get {	
			if(instance == null) {	
				Debug.Log("Instancing a new pc");
				GameObject go = Instantiate( Resources.Load("Player Character Prefab"),new Vector3(0,0,0),Quaternion.identity) as GameObject;
				PlayerCharacter temp = go.GetComponent<PlayerCharacter>();
				
				if(temp == null)
					Debug.LogError("Player Prefab doesn't contain script");
				
				instance = go.GetComponent<PlayerCharacter>();
				go.name = "pc";
				go.tag = "Player";	
			}
			
			return instance;
		}
	}
	
	public void Initialize() {
		if(!initialized)
			LoadCharacter();
	}
	
	new void Awake() {
		base.Awake();
		
		instance = this; 
	}
	
	public void LoadCharacter() {
		
		initialized = true;
	}
	
	public static List<Item> Inventory {
		get{ return _inventory; }
	}
	
	public static List<Item> Alchemy {
		get{ return _alchemy; }
	}
	
	public static List<Item> MixResult {
		get{ return _alchemyResult; }
	}
	
	public static void IncreaseAmount(ItemID itemID)
    {
		//bool noItem = true;
		for(int i=0; i < _inventory.Count; i++)
		{
			if(_inventory[i].Id == itemID && _inventory[i].CurAmount < _inventory[i].MaxAmount) {
				_inventory[i].CurAmount++;
				//noItem = false;
			}
		}
		
	//	return noItem;
    }
	
	/****************************************************/
	/*INVENTORY ITEM*/
	/****************************************************/
	public static bool checkSameItemInventory(ItemID itemID) {
		bool haveItem = false;
		for(int i=0; i < _inventory.Count; i++)
		{
			if(_inventory[i].Id == itemID && _inventory[i].CurAmount < _inventory[i].MaxAmount) {
				haveItem = true;
			}
		}
		return haveItem;
	}
	
	public static int getIndexOfSameItemInventory(ItemID itemID) {
		int index = 0;
		for(int i=0; i < _inventory.Count; i++)
		{
			if(_inventory[i].Id == itemID && _inventory[i].CurAmount < _inventory[i].MaxAmount) {
				index = i;
			}
		}	
		return index;
	}
	
	/****************************************************/
	/*ALCHEMY ITEM*/
	/****************************************************/
	public static bool checkSameItemAlchemy(ItemID itemID) {
		bool haveItem = false;
		for(int i=0; i < _alchemy.Count; i++)
		{
			if(_alchemy[i].Id == itemID && _alchemy[i].CurAmount < _alchemy[i].MaxAmount) {
				haveItem = true;
			}
		}
		return haveItem;
	}
	
	public static int getIndexOfSameItemAlchemy(ItemID itemID) {
		int index = 0;
		for(int i=0; i < _alchemy.Count; i++)
		{
			if(_alchemy[i].Id == itemID && _alchemy[i].CurAmount < _alchemy[i].MaxAmount) {
				index = i;
			}
		}	
		return index;
	}
	
	void Update() {
		Messenger<int, int>.Broadcast("player health update", 80, 100, MessengerMode.DONT_REQUIRE_LISTENER);			
	}
}