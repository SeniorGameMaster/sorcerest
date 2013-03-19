using UnityEngine;
using System.Collections;

public class Bolt : Spell, IBolt {
	#region IBolt implementation
	public float MaxDamageValue { get; set; }
	public float DamageVariance { get; set; }
	public float SpellRange { get; set; }
	#endregion\

	public Bolt() {
		MaxDamageValue = 0;
		DamageVariance = 0.2f;
		SpellRange = 10f;
	}
}
