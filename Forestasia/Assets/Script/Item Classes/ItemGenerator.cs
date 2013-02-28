using UnityEngine;
using System;

public static class ItemGenerator {
	public const int BASE_MELEE_RANGE = 1;
	public const int BASE_RANGED_RANGE = 5;
	
	private const int MAX_AMOUNT_INGREDIENT = 99;
	private const int MAX_AMOUNT_QUEST = 99;
	private const int MAX_AMOUNT_USABLE = 10;
	private const int MAX_AMOUNT_LEGENDARY = 1;
	
	private const string MELEE_WEAPON_PATH = "Quest/";
	private const string QUEST_ITEM_PATH = "Quest/";
	
	public static Item CreatingProcess(ItemTypes itemTypes, ItemID itemID, int amount) {
		//decide what type of item to make
		//Item temp = new Item();
		//call the method to create that base item type
		
		Item item = CreatItemType(itemTypes, itemID, amount);
		return item;
		/*
		switch (itemTypes) {
			
		case ItemTypes.Misc :
			
			break;
			
		case ItemTypes.Ingredient :
			
			break;
			
		case ItemTypes.Quest :
			Item item = CreateItemQuest(itemID);
			item.Type = itemTypes;
			returnItem = item;
			
			break;
			
		case ItemTypes.Legendary :
			
			break;
			
		case ItemTypes.Unknown :
			
			break;
			
		default:break;
		}
		*/

		/*
		Item item = CreateWeapon();
		
		item.Value = Random.Range(1,101);
		item.Rarity = RarityTypes.Common;
		item.MaxDurability = Random.Range(50,61);
		item.CurDurability = item.MaxDurability;
		//return the new Item
		
			private string _name;
	private ItemID _id;
	private int _curAmount;
	private int _maxAmount;
	private int _point;
	private ItemTypes _type;
	private string _detail;
	private int _curDur;
	private int _maxDur;
	private Texture2D _icon;
	
		 */
		
		
	}
	
	public static Item addItemAmount(Item addItem, int amount) {
		Item alchemyItem = new Item(addItem.Name, addItem.Id, amount,addItem.MaxAmount,
			addItem.Point, addItem.Type, addItem.Detail, addItem.MaxDurability, addItem.CurDurability);
		return alchemyItem;
		/*
		alchemyItem.Name = addItem.Name;
		alchemyItem.Id = addItem.Id;
		alchemyItem.CurAmount = amount;
		alchemyItem.MaxAmount = addItem.MaxAmount;
		alchemyItem.Point = addItem.Point;
		alchemyItem.Type = addItem.Type;
		alchemyItem.Detail = addItem.Detail;
		alchemyItem.CurDurability = addItem.CurDurability;
		alchemyItem.MaxDurability = addItem.MaxDurability;
		*/
		
	}
	
	/*
	private static Item CreatItemType(ItemTypes itemTypes, ItemID itemID) {
	
		switch (itemTypes) {
			
		case ItemTypes.Misc :
			Item itemM = CreateItemQuest(itemID);
			itemM.Type = itemTypes;
			return itemM;
			
		case ItemTypes.Ingredient :
			Item itemI = CreateItemQuest(itemID);
			itemI.Type = itemTypes;
			return itemI;
			
		case ItemTypes.Quest :
			Item item = CreateItemQuest(itemID);
			item.Type = itemTypes;
			return item;
			
		case ItemTypes.Legendary :
			Item itemL = CreateItemQuest(itemID);
			itemL.Type = itemTypes;
			return itemL;
		
		case ItemTypes.Usable :
			Item itemU = CreateItemQuest(itemID);
			itemU.Type = itemTypes;
			return itemU;
			
		case ItemTypes.Unknown :
			
			return null;
			
		default: return null;
		}
		
	}*/
	
	private static Item CreatItemType(ItemTypes itemTypes, ItemID itemID, int amount) {
		Item itemType = new Item();
		
		
		switch (itemTypes) {
			
		case ItemTypes.Misc :
			itemType = CreateItemQuest(itemID, amount);
			break;
		
		case ItemTypes.Ingredient :
			itemType = CreatItemIngredient(itemID, amount);
			break;
			
		case ItemTypes.Quest :
			itemType = CreateItemQuest(itemID, amount);
			break;
			
		case ItemTypes.Legendary :
			itemType = CreateItemQuest(itemID, amount);	
			break;
		
		case ItemTypes.Usable :
			itemType = CreateItemQuest(itemID, amount);		
			break;
			
		case ItemTypes.Unknown :
			break;
			
		default:break;
		}
		
		itemType.Name = Enum.GetName(typeof(ItemID), itemID);
		itemType.Id = itemID;
		itemType.Detail = EnumClass.GetEnumDescription(itemID);	
		itemType.Type = itemTypes;
		
		return itemType;
	}
	
	private static Item CreatItemIngredient(ItemID itemID, int amount) {
		Item ingredientItem = new Item();
		ingredientItem.CurAmount = UnityEngine.Random.Range(1,amount);
		ingredientItem.MaxAmount = MAX_AMOUNT_INGREDIENT;
		
		switch(itemID) {
		case ItemID.CognitiveHerb :
			ingredientItem.Point = 5;
			break;
		case ItemID.Water_of_Cactus :
			ingredientItem.Point = 55;
			break;
			
		default:break;
		}
		
		return ingredientItem;
	}
	
	private static Item CreateItemQuest(ItemID itemID, int amount) {
		Item questItem = new Item();
		questItem.CurAmount = UnityEngine.Random.Range(1,amount);
		questItem.MaxAmount = MAX_AMOUNT_QUEST;

		switch(itemID) {
		case ItemID.OldBoots :
			questItem.Point = 100;
			questItem.Icon = Resources.Load(QUEST_ITEM_PATH + questItem.Name) as Texture2D;
			break;
		case ItemID.RabbitFur :
			questItem.Point = 15;
			//questItem.Icon = Resources.Load(QUEST_ITEM_PATH + questItem.Name) as Texture2D;
			break;

		default:break;
		}
		
		return questItem;
	}
	
	private static Weapon CreateWeapon() {
		//decide if we make a melee or ranged weapon
		Weapon weapon = CreateMeleeWeapon();
	
		return weapon;
	}
	
	public static Weapon CreateMeleeWeapon() {
		Weapon meleeWeapon = new Weapon();
		
		string[] weapnNames = new string[3];
		weapnNames[0] = "OldBoots";
		weapnNames[1] = "OldBoots";
		weapnNames[2] = "OldBoots";
		//fill in all off values for that item type
		//meleeWeapon.Name = weapnNames[Random.Range(0,weapnNames.Length)];
		
		
		//meleeWeapon.MaxDamage = Random.Range(5, 11);
		//meleeWeapon.DamageVariance = Random.Range(.2f, .76f);
		meleeWeapon.TypeOfDamage = DamageType.Slash;
		
		//assign the max range of this weapon
		meleeWeapon.MaxRange = BASE_MELEE_RANGE;

		//assign the icon for the weapon
		meleeWeapon.Icon = Resources.Load(MELEE_WEAPON_PATH + meleeWeapon.Name) as Texture2D;
		
//		Debug.Log(MELEE_WEAPON_PATH + meleeWeapon.Name);
		//return the melee weapon
		return meleeWeapon;
	}
}

public enum ItemClass {
	Armor,
	Weapon,
	Potion,
	Scroll
}