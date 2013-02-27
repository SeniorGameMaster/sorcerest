using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class Item : MonoBehaviour {
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
	
	public Item() {
		_name = "Name";
		_id = ItemID.None;
		_point = 0;
		_type = ItemTypes.Misc;
		_detail = "Detail";
		_maxDur = 50;
		_curDur = _maxDur;
	}
	
	public Item (string name, ItemID id, int curAmount, int maxAmount, int point, ItemTypes rare, string detail, int maxDur, int curDur) {
		_name = name;
		_id = id;
		_curAmount = curAmount;
		_maxAmount = maxAmount;
		_point = point;
		_type = rare;
		_detail = detail;
		_maxDur = maxDur;
		_curDur = curDur;
	}
	
	public string Name {
		get { return _name; }	
		set { _name = value; }	
	}
	
	public ItemID Id {
		get { return _id; }	
		set { _id = value; }	
	}
	
	public int CurAmount {
		get { return _curAmount; }	
		set { _curAmount = value; }	
	}
	
	public int MaxAmount {
		get { return _maxAmount; }	
		set { _maxAmount = value; }	
	}
	
	public int Point {
		get { return _point; }	
		set { _point = value; }	
	}
	
	public ItemTypes Type {
		get { return _type; }	
		set { _type = value; }	
	}
	
	public string Detail {
		get { return _detail; }	
		set { _detail = value; }	
	}
	
	public int MaxDurability {
		get { return _maxDur; }	
		set { _maxDur = value; }	
	}
	
	public int CurDurability {
		get { return _curDur; }	
		set { _curDur = value; }	
	}
	
	public Texture2D Icon {
		get { return _icon; }
		set { _icon = value; }
	}
	
	public virtual string ToolTip() {
		return Name + "\n" +
				"Amounts : " + CurAmount + "\n" +
				"Points : " + Point + "\n" +
				"Type : " + Type + "\n" +
				"Detail :" + Detail + "\n";
				
	}
}

public enum ItemTypes {
		Misc,
		Ingredient,
		Quest,
		Unknown,
		Legendary,
		Usable
}

public enum ItemID {
	/*
		None = 0,
		Boots_of_Intelligent = 1,
		Wand_of_Truth = 2,
		FeatherPen = 3,
		LightCrystal = 4,
		MixColor = 5,
		Gemstone = 6,
		PhilosopherMap = 7,
		OldBoots = 8,
		RabbitFur = 9
	*/
	[Description("Unknown Item")]
		None = 0,
	[Description("The boots that improve  \n owned intelligent.")]
		Boots_of_Intelligent = 1,
	[Description("The legendary wand using \n for see reality.")]
		Wand_of_Truth = 2,
	[Description("Everythings will answer by \n this pen.")]
		FeatherPen = 3,
	[Description("Shining and brilliant \n rare crystal.")]
		LightCrystal = 4,
	[Description("Mixing type of color.")]
		MixColor = 5,
	[Description("Legendary jewel using \n in  sacrifice magic.")]
		Gemstone = 6,
	[Description("Show every things in \n the area.")]
		PhilosopherMap = 7,
	[Description("Very oldboots look like \n it use for a long time.")]
		OldBoots = 8,
	[Description("Fur from rabbit \n so fluffy.")]
		RabbitFur = 9,
	[Description("Branch from thounsand \n year olds tree.")]
		MysticalBranch = 10,
	[Description("old branch from tree.")]
		OldBranch = 11,
	[Description("rare herb grow by \n magic power from forest.")]
		CognitiveHerb = 12,
	[Description("sharp spike from \n hedgehog use to pierce smth.")]
		Spike_of_Hedgehog = 13,
	[Description("stored water from \n cactus around dessert")]
		Water_of_Cactus = 14,
	[Description("mystical liquid extract by \n alchemy ")]
		FortunePotion = 15,
	[Description("old scroll contain \n some description ")]
		TruthScroll = 16
}

	

