using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int maxHealth = 100;
	public int currentHealth = 100;
	public bool isSelected = false;
	public float healthBarLegnth;
	public bool isDead = false;
	
	void Start () {
		healthBarLegnth = Screen.width / 2;
	}
	
	void Update () {
		AdjustCurrentHealth(0);
	}
	
	void OnGUI()
	{
		if(isSelected)
			GUI.Box(new Rect(10, 40, healthBarLegnth, 20), currentHealth + "/" + maxHealth);	
	}
	
	public void AdjustCurrentHealth(int adjust)
	{
		currentHealth += adjust;
		
		if(currentHealth <= 0) {
			currentHealth = 0;
			isDead = true;
		}
		
		if(currentHealth > maxHealth)
			currentHealth = maxHealth;
		
		if(maxHealth < 1)
			maxHealth = 1;
		
		healthBarLegnth = (Screen.width / 2) * (currentHealth / (float)maxHealth);
	}
}
