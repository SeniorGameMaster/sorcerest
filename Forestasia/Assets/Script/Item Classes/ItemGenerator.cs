using UnityEngine;

public static class ItemGenerator {
	public const int BASE_MELEE_RANGE = 1;
	public const int BASE_RANGED_RANGE = 5;
	
	private const string MELEE_WEAPON_PATH = "";
	public static Item CreateItem() {
		//decide what type of item to make
		//Item temp = new Item();
		
		
		//call the method to create that base item type
		Item item = CreateWeapon();
		
		item.Value = Random.Range(1,101);
		item.Rarity = RarityTypes.Common;
		item.MaxDurability = Random.Range(50,61);
		item.CurDurability = item.MaxDurability;
		//return the new Item

		return item;
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
		meleeWeapon.Name = weapnNames[Random.Range(0,weapnNames.Length)];
		
		
		meleeWeapon.MaxDamage = Random.Range(5, 11);
		meleeWeapon.DamageVariance = Random.Range(.2f, .76f);
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

public enum ItemType {
	Armor,
	Weapon,
	Potion,
	Scroll
}