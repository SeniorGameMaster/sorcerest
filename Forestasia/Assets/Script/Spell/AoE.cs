using UnityEngine;
using System.Collections;

public class AoE : Bolt, IAoE {
	#region IAoE implementation
	public int MaxTargets { get; set; }
	public float AoERange { get; set; }
	public float AoEDamage { get; set; }
	public float AoEDamageVariance { get; set; }
	#endregion
	
	public AoE() {
		MaxTargets = 0;
		AoERange = 0;
		AoEDamage = 0;
		AoEDamageVariance = 0;
	}
}
