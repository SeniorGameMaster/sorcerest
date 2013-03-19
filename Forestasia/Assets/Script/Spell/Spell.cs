using UnityEngine;
using System.Collections;

public class Spell : ISpell {
	private float _coolDownTimer;
	private bool _ready;
	
	public string Name { get; set; }
	public GameObject Effect { get; set; }
	public bool LineOfSight { get; set; }
	public string Description { get; set; }
	public float BaseCoolDownTime { get; set; }
	public float CoolDownVariance { get; set; }
	
	public Spell() {
		
		Name = "Fire Bolt";
		LineOfSight = true;
		Description = "Fire from sky";
		
		BaseCoolDownTime = 2.0f;
		CoolDownVariance = 0.25f;
		CoolDownTimer = 0;
		OnCoolDown = true;
		
	}
	
	
	
	
	#region ISpell implmentation
	public void Update() {
		//throw new System.NotImplementedException();	

	}
	
	public void Cast() {
		//	throw new System.NotImplementedException();	
		

	}
	
	public float CoolDownTimer {
		get {
			return _coolDownTimer;
		}
		private set {
			_coolDownTimer = value;
		}
	}
	
	public bool OnCoolDown {
		get {
			return _ready;
		}
		private set {
			_ready = value;	
		}

	}
	#endregion
}

