using UnityEngine;

public class Clothing : BuffItem {
	private ArmorSlot _slot;
	
	public Clothing() {
		_slot = ArmorSlot.Head;	
	}
	
	public Clothing(ArmorSlot slot) {
		_slot = _slot;	
	}
	
	public ArmorSlot Slot {
		get { return _slot; }	
		set { _slot = value; }
	}
}

public enum ArmorSlot {
	Head,
	Shoulders,
	UpperBody,
	Torso,
	Legs,
	Hands,
	Feet,
	Back
}
