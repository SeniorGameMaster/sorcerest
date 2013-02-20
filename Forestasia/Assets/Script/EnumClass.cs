using UnityEngine;
using System;
using System.Reflection;
using System.ComponentModel;

public class EnumClass {

	public static string GetEnumDescription(Enum value)
	{
	    FieldInfo fi = value.GetType().GetField(value.ToString());
		
		    DescriptionAttribute[] attributes = 
		        (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
		
		    if (attributes != null && attributes.Length > 0)
		        return attributes[0].Description;
		    else
		        return value.ToString();
	}
	/*
	public struct MonsterObj {
		public MonsterID monID;
		public ItemID[] itemID;
		public ItemTypes[] typeID;
		public int[] dropChance;
	}
	
	public MonsterObj[] monObj;
	
	public EnumClass() {
		monObj[0].monID = MonsterID.Hedgehog;
		monObj[0].itemID[0] = ItemID.Boots_of_Intelligent;
		monObj[0].itemID[1] = ItemID.FeatherPen;
		monObj[0].typeID[0] = ItemTypes.Quest;
		monObj[0].typeID[1] = ItemTypes.Quest;
		monObj[0].dropChance[0] = 100;
		monObj[0].dropChance[1] =70;
			
	}
	*/
}
