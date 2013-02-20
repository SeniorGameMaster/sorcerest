using UnityEngine;
using System;

public static class ItemGenerator {
	public const int BASE_MELEE_RANGE = 1;
	public const int BASE_RANGED_RANGE = 5;
	
	private const string MELEE_WEAPON_PATH = "Quest/";
	private const string QUEST_ITEM_PATH = "Quest/";
	
	public static Item CreatingProcess(ItemTypes itemTypes, ItemID itemID) {
		//decide what type of item to make
		//Item temp = new Item();
		//call the method to create that base item type
		
		Item item = CreatItemType(itemTypes, itemID);
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
		 */
		
		
	}
	
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
		
	}
	
	private static Item CreateItemQuest(ItemID itemID) {
		Item questItem = new Item();
		
		questItem.Name = Enum.GetName(typeof(ItemID), itemID);
		questItem.Id = itemID;
		questItem.Value = 100;
		questItem.Detail = EnumClass.GetEnumDescription(itemID);	
		
		switch(itemID) {
		case ItemID.OldBoots :/*
			questItem.Name = Enum.GetName(typeof(ItemID), itemID);
			questItem.Value = 100;
			questItem.Detail = "Very oldboots look like \n it use for a long time";*/
			questItem.Icon = Resources.Load(QUEST_ITEM_PATH + questItem.Name) as Texture2D;
			break;
		case ItemID.RabbitFur :/*
			questItem.Name = Enum.GetName(typeof(ItemID), itemID);
			questItem.Value = 10;
			questItem.Detail = "Fur from rabbit \n so fluffy";*/
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