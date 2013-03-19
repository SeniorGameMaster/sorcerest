using UnityEngine;
using System.Collections;

public class Buff : Spell,IBuff {

	#region IBuff implementation
	public int MaxBuffValue { get; set; }
	public float BuffValueVariance { get; set; }
	public float BaseBuffDuration { get; set; }
	public float BuffTimeLeft { get; private set; }
	#endregion
	
	public Buff() {
		MaxBuffValue = 0;
		BuffValueVariance = 0.2f;
		BaseBuffDuration = 120f;
		BuffTimeLeft = 0;
	}
}
