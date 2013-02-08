using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//[RequireComponent (typeof(BoxCollider))]
[RequireComponent (typeof(AudioSource))]

public class LootingItem : MonoBehaviour {
	public enum State {
		open,
		close,
		inbetween
	}
	
	public enum Type {
		chest,
		monster,
		tree
	}
	
	//public AudioClip openSound;  		//Open sound
	//public AudioClip closeSound; 		//Close sound
	 
	public GameObject particleEffect; 	//Particle effect when loot
	
	public GameObject[] parts; 			//Highlight part of object by color
	private Color[] _defautlColors; 	//default color of object
	
	public State state; 				//Current state of object
	public Type type;					//Type of looting item
	
	public float maxDistance = 5;		//The max distance the player can loot object
	
	private GameObject _player;
	private Transform _myTransform;
	public bool inUse = false;
	private bool _used = false;			//tracking the object is use or not
	
	public List<Item> loot = new List<Item>();
	
	// Use this for initialization
	void Start () {
		_myTransform = transform;
		
		state = LootingItem.State.close;
		
		particleEffect.SetActive(false);
			
		_defautlColors = new Color[parts.Length];
		
		if(parts.Length > 0)
			for (int cnt = 0; cnt < _defautlColors.Length; cnt++)
				_defautlColors[cnt] = parts[cnt].renderer.material.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () {
		if(!inUse)
			return;
		
		if(_player == null)
			return;
		
		if(Vector3.Distance(transform.position, _player.transform.position) > maxDistance)
			MyGUI_1.lootingItem.ForceClose();
		//	Messenger.Broadcast("CloseChest");
	}
	
	public void OnMouseEnter() {
		
		switch(type) {
		case Type.chest:
				HighLight(true);
			break;
		case Type.monster:
			if(_myTransform.GetComponent<EnemyHealth>().isDead) {
				HighLight(true);
			}
			break;
		case Type.tree:
			
			break;
		default:break;
		}
		
	}
	
	public void OnMouseExit() {
		
		switch(type) {
		case Type.chest:
				HighLight(false);
			break;
		case Type.monster:
			if(_myTransform.GetComponent<EnemyHealth>().isDead) {
				HighLight(false);
			}
			break;
		case Type.tree:
			
			break;
		default:break;
		}
	}
	
	public void OnMouseUp() {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		
		if(go == null)
			return;
		
		if(Vector3.Distance(_myTransform.position, go.transform.position) > maxDistance && !inUse)
			return;
		
		switch(type) {
		case Type.chest:
			OnLooting();
			break;
		case Type.monster:
			if(_myTransform.GetComponent<EnemyHealth>().isDead) {
				OnLooting();
			}
			break;
		case Type.tree:	
			
			break;
		default:break;
		}		
	}
	
	private void OnLooting(){
		switch(state) {
			case State.open :
				state = LootingItem.State.inbetween;
				//StartCoroutine("Close");
				ForceClose();
				break;
			case State.close :
				if(MyGUI_1.lootingItem != null) {
					MyGUI_1.lootingItem.ForceClose();
				}
			
				state = LootingItem.State.inbetween;
				StartCoroutine("Open");
				break;	
			default:break;
			}
	}
	
	private IEnumerator Open() {
		MyGUI_1.lootingItem = this;
		
		_player = GameObject.FindGameObjectWithTag("Player");
		inUse = true;
		
		switch(type) {
		case Type.chest:
			animation.Play("GoodOpen");
			yield return new WaitForSeconds(animation["GoodOpen"].length);
			break;
		case Type.monster:
			yield return new WaitForSeconds(1);
			break;
		case Type.tree:
			
			break;
		default:break;
		}
		
		particleEffect.SetActive(true);
		//audio.PlayOneShot(openSound);
		
		if(!_used) {
			switch(type) {
			case Type.chest:
				PopulateItem();
				break;
			case Type.monster:
				//PopulateItem(1, "Rabbit Fur");
			//	if(UnityEngine.Random.Range(0,4) <= 2)
					//PopulateItem(2, "Rabbit Ear");
				break;
			case Type.tree:
				
				break;
			default:break;
			}
		
		}

		state = LootingItem.State.open;
		
		Messenger.Broadcast("DisplayLoot");
	}
	
	private void PopulateItem() {
		//(string name, int value, RarityTypes rare, int maxDur, int curDur)
		
		//Item sumitem = new Item(name, 0, RarityTypes.Common, 0, 0);
		//loot.Add(sumitem);
		
		loot.Add(ItemGenerator.CreateItem());
		//loot[x-1].Name = name;
		/*
		for(int cnt = 0; cnt < x; cnt++){
			loot.Add(new Item());
			//loot[cnt].Name = "I:" + Random.Range(0,100);
			loot[cnt].Name = cnt + 1  + ". " + name;
		}*/
		
		_used = true;
	}
	
	private IEnumerator Close() {
		_player = null;
		inUse = false;
		
		switch(type) {
		case Type.chest:
			animation.Play("GoodClose");
			yield return new WaitForSeconds(animation["GoodClose"].length);
			break;
		case Type.monster:
			yield return new WaitForSeconds(1);
			break;
		case Type.tree:
			
			break;
		default:break;
		}
	
		particleEffect.SetActive(false);
		//audio.PlayOneShot(closeSound);
		
		
		
		state = LootingItem.State.close;
		
		if(loot.Count == 0)
			Destroy(gameObject);
	}
	
	//Close the object in any situation
	public void ForceClose() {
		Messenger.Broadcast("CloseLoot");
		
		StopCoroutine("Open");
		StartCoroutine("Close");
	}
	
	//HighLight and deHighLight the selected object
	private void HighLight(bool glow) {
		if(glow){
			if(parts.Length > 0)
			for (int cnt = 0; cnt < _defautlColors.Length; cnt++)
				parts[cnt].renderer.material.SetColor("_Color", Color.blue);
		}
		else {
			if(parts.Length > 0) {
				switch(type) {
				case Type.chest:
					for (int cnt = 0; cnt < _defautlColors.Length; cnt++)
						parts[cnt].renderer.material.SetColor("_Color", _defautlColors[cnt]);
					break;
				case Type.monster:
					for (int cnt = 0; cnt < _defautlColors.Length; cnt++)
						parts[cnt].renderer.material.SetColor("_Color", Color.black);
					break;
				case Type.tree:
				
					break;
				default:break;
				}
			}
			
		}
	}
}
