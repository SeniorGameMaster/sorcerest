using UnityEngine;
using System;

public static class ItemGenerator {
	private const int MAX_AMOUNT_LEGENDARY = 1;
	private const int MAX_AMOUNT_QUEST = 99;
	private const int MAX_AMOUNT_INGREDIENT = 99;
	private const int MAX_AMOUNT_MISC = 10;
	private const int MAX_AMOUNT_USABLE = 10;

	private const string LEGENDARY_ITEM_PATH = "ItemPic/Legendary/";
	private const string QUEST_ITEM_PATH = "ItemPic/Quest/";
	private const string INGREDIENT_ITEM_PATH = "ItemPic/Ingredient/";
	private const string MISC_ITEM_PATH = "ItemPic/Misc/";
	
	public static Item CreatingProcess(ItemTypes itemTypes, ItemID itemID, int amount) {
		
		Item item = CreatItemType(itemTypes, itemID, amount);
		return item;		
	}
	
	public static Item addItemAmount(Item addItem, int amount) {
		Item alchemyItem = new Item(addItem.Name, addItem.Id, amount,addItem.MaxAmount,
			addItem.Point, addItem.Type, addItem.Detail, addItem.MaxDurability, addItem.CurDurability, addItem.Icon);
		return alchemyItem;
	}
		
	private static Item CreatItemType(ItemTypes itemTypes, ItemID itemID, int amount) {
		Item itemType = new Item();
		
		switch (itemTypes) {
				
		case ItemTypes.Legendary :
			itemType = CreatItemLegendary(itemID, amount);	
			itemType.Icon = Resources.Load(LEGENDARY_ITEM_PATH + itemType.Name) as Texture2D;
			break;
			
		case ItemTypes.Quest :
			itemType = CreateItemQuest(itemID, amount);
			itemType.Icon = Resources.Load(QUEST_ITEM_PATH + itemType.Name) as Texture2D;
			break;
			
		case ItemTypes.Ingredient :
			itemType = CreatItemIngredient(itemID, amount);
			itemType.Icon = Resources.Load(INGREDIENT_ITEM_PATH + itemType.Name) as Texture2D;
			break;
			
		case ItemTypes.Usable :
			itemType = CreateItemQuest(itemID, amount);		
			break;
			
		case ItemTypes.Misc :
			itemType = CreatItemMisc(itemID, amount);
			itemType.Icon = Resources.Load(MISC_ITEM_PATH + itemType.Name) as Texture2D;
			break;
			
		case ItemTypes.Unknown :
			break;
			
		default:break;
		}
		
		itemType.Id = itemID;
		itemType.Detail = EnumClass.GetEnumDescription(itemID);	
		itemType.Type = itemTypes;
		
		
		return itemType;
	}
	
	private static Item CreatItemLegendary(ItemID itemID, int amount) {
		Item legendaryItem = new Item();
		legendaryItem.Name = Enum.GetName(typeof(ItemID), itemID);
		legendaryItem.CurAmount = amount;
		legendaryItem.MaxAmount = MAX_AMOUNT_LEGENDARY;
		
		switch(itemID) {
		case ItemID.WingsBoots :
			legendaryItem.Point = 500;
			break;
		case ItemID.WandOfTruth :
			legendaryItem.Point = 700;
			break;
		case ItemID.FeatherPen :
			legendaryItem.Point = 900;
			break;	
		case ItemID.LightCrystal :
			legendaryItem.Point = 1500;
			break;		
		case ItemID.MixColor :
			legendaryItem.Point = 300;
			break;		
		case ItemID.Gemstone :
			legendaryItem.Point = 2000;
			break;
		case ItemID.PhilosopherMap :
			legendaryItem.Point = 1000;
			break;	
		default:break;
		}
		
		return legendaryItem;
	}
	
	private static Item CreateItemQuest(ItemID itemID, int amount) {
		Item questItem = new Item();
		questItem.Name = Enum.GetName(typeof(ItemID), itemID);
		questItem.CurAmount = UnityEngine.Random.Range(1,amount);
		questItem.MaxAmount = MAX_AMOUNT_QUEST;

		switch(itemID) {
		case ItemID.OldBoots :
			questItem.Point = 50;	
			break;
		case ItemID.RabbitFur :
			questItem.Point = 15;
			break;

		default:break;
		}
		
		return questItem;
	}
	
	private static Item CreatItemIngredient(ItemID itemID, int amount) {
		Item ingredientItem = new Item();
		ingredientItem.Name = Enum.GetName(typeof(ItemID), itemID);
		ingredientItem.CurAmount = UnityEngine.Random.Range(1,amount);
		ingredientItem.MaxAmount = MAX_AMOUNT_INGREDIENT;
		
		switch(itemID) {
		case ItemID.CognitiveHerb :
			ingredientItem.Point = 5;
			break;
		case ItemID.WaterCactus :
			ingredientItem.Point = 55;
			break;
			
		default:break;
		}
		
		return ingredientItem;
	}
	
	private static Item CreatItemMisc(ItemID itemID, int amount) {
		Item miscItem = new Item();
		miscItem.Name = Enum.GetName(typeof(ItemID), itemID);
		miscItem.CurAmount = UnityEngine.Random.Range(1,amount);
		miscItem.MaxAmount = MAX_AMOUNT_MISC;
		
		switch(itemID) {
		case ItemID.Spike :
			miscItem.Point = 3;
			break;
			
		default:break;
		}
		
		return miscItem;
	}
	
	
}

public enum ItemClass {
	Armor,
	Weapon,
	Potion,
	Scroll
}