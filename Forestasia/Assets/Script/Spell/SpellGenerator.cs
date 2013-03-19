using UnityEngine;
using System.Collections;

public class SpellGenerator : MonoBehaviour {

	void Start () {
		Spell spell = CreateSpell();
		
	}
	
	public void update() {
		
	}
	
	public Spell CreateSpell () {
		Spell spell = new Spell();
		/*
		spell.Name = "Spell One";
		spell.LineOfSight = true;
		spell.Description = "This is the desc";
		spell.BaseCoolDownTime = 2.5f;
		spell.CoolDownVariance = Random.Range(-.2f,0.2f);
		*/
		if(spell is Buff) {
			Debug.Log("Buff");
		}
		else if(spell is AoE) {
			Debug.Log("AoE");
		}
		else if(spell is Bolt) {
			Debug.Log("Bolt");
		}
			
		
		return spell;
		
	}
}
