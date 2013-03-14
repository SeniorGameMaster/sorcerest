using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//[RequireComponent (typeof(BoxCollider))]
//s[RequireComponent (typeof(AudioSource))]

public class LootingItem : MonoBehaviour {
	public enum State {
		open = 0,
		close = 1,
		inbetween = 2
	}
	
	public enum Type {
		treasurebox = 0,
		monster = 1,
		tree = 2,
		require = 3
	}
	
	public enum TreasureID {
		None = 0,
		OldbootsBox = 1,
		FeatherpenBox = 2,
		ItemBox = 3,
		LegendaryBox = 4
	}
	
	public enum MonsterID {
		None = 0,
		Rabbit = 1,
		Hedgehog = 2
	}
	
	public enum TreeID {
		None = 0,
		Special = 1,
		Herb = 2,
		Cactus = 3
	}
	
	//public AudioClip openSound;  		//Open sound
	//public AudioClip closeSound; 		//Close sound
	 
	public GameObject particleEffect; 	//Particle effect when loot
	public GameObject[] parts; 			//Highlight part of object by color
	public float maxDistance = 5;		//The max distance the player can loot object
	public bool inUse = false;
	public List<Item> loot = new List<Item>();
	public bool setDestroy = false;
	public static float defaultLifeTimer = 120;
	private float _lifeTimer = 0;
	
	public ItemID[] dropID;
	public ItemTypes[] dropType;
	public int[] dropChance;
	public int[] dropAmount;

	public State state; 				//Current state of object
	public Type type;					//Type of looting item
	public ItemID[] itemRequire;
	
	public TreasureID box;				//TreasureID of Type
	public MonsterID monster;			//MonsterID of Type
	public TreeID tree;					//TreeID of Type
	
	private Color[] _defautlColors; 	//default color of object
	private GameObject _player;
	private Transform _myTransform;
	private bool _used = false;			//tracking the object is use or not
	
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
		_lifeTimer += Time.deltaTime;
		
		if(setDestroy && (_lifeTimer > defaultLifeTimer) && state == State.close)
			DestroyLoot();
		
		if(!inUse)
			return;
		
		if(_player == null)
			return;
		
		if(Vector3.Distance(transform.position, _player.transform.position) > maxDistance)
			PlayerGUI.lootingItem.ForceClose();
		//	Messenger.Broadcast("CloseChest");
	}
	
	public void OnMouseEnter() {
		
		switch(type) {
		case Type.treasurebox:
				HighLight(true);
			break;
		case Type.monster:
			if(_myTransform.GetComponent<EnemyHealth>().isDead) {
				HighLight(true);
			}
			break;
		case Type.tree:
				HighLight(true);
			break;
		case Type.require:
			if(checkRequireItem())
				HighLight(true);
			break;
		default:break;
		}
		
	}
	
	public bool checkRequireItem() {
		int haveAllItem = 0;
		
		for(int itm = 0; itm < PlayerCharacter.Inventory.Count; itm++) {	
			for(int req = 0; req < itemRequire.Length; req++) { 
				if(PlayerCharacter.Inventory[itm].Name.Contains(itemRequire[req].ToString()))
						haveAllItem += 1;
			}
		}
		
		if(haveAllItem.Equals(itemRequire.Length))
			return true;
		else
			return false;
						
	}
	
	public void OnMouseExit() {
		
		switch(type) {
		case Type.treasurebox:
				HighLight(false);
			break;
		case Type.monster:
			if(_myTransform.GetComponent<EnemyHealth>().isDead) {
				HighLight(false);
			}
			break;
		case Type.tree:
				HighLight(false);
			break;
		case Type.require:
			if(checkRequireItem())
				HighLight(false);
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
		case Type.treasurebox:
			OnLooting();
			break;
		case Type.monster:
			if(_myTransform.GetComponent<EnemyHealth>().isDead) {
				OnLooting();
			}
			break;
		case Type.tree:	
			OnLooting();
			break;
		case Type.require:
			if(checkRequireItem())
				OnLooting();		
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
				if(PlayerGUI.lootingItem != null) {
					PlayerGUI.lootingItem.ForceClose();
				}
			
				state = LootingItem.State.inbetween;
				StartCoroutine("Open");
				break;	
			default:break;
			}
	}
	
	private IEnumerator Open() {
		PlayerGUI.lootingItem = this;
		
		_player = GameObject.FindGameObjectWithTag("Player");
		inUse = true;
		
		switch(type) {
		case Type.treasurebox:
			animation.Play("GoodOpen");
			yield return new WaitForSeconds(animation["GoodOpen"].length);
			break;
		case Type.monster:
			yield return new WaitForSeconds(1);
			break;
		case Type.tree:
			yield return new WaitForSeconds(1);
			break;
		case Type.require:
			yield return new WaitForSeconds(1);
			break;
		default:break;
		}
		
		particleEffect.SetActive(true);
		//audio.PlayOneShot(openSound);
		
		if(!_used) {
			//ItemTypeClassify();

			for (int i = 0; i < dropID.Length; i++) {
				
				if(UnityEngine.Random.Range(0,100) <= dropChance[i])
					PopulateItem(dropType[i],dropID[i],dropAmount[i]);
			}
				
		}

		state = LootingItem.State.open;
		
		Messenger.Broadcast("DisplayLoot");
	}
	
	private void ItemTypeClassify() {
		switch(type) {
			case Type.treasurebox:
				
				switch (box) {
					case TreasureID.OldbootsBox :
						//PopulateItem(ItemTypes.Quest,ItemID.OldBoots);
					break;
				default:break;
				}
				
				break;
			
			case Type.monster:
				
				switch (monster) {
					case MonsterID.Rabbit :
						//PopulateItem(ItemTypes.Quest,ItemID.RabbitFur);
					break;
				default:break;
				}
			
				break;
			case Type.tree:
				
				switch (tree) {
				case TreeID.Special :
						//PopulateItem(ItemTypes.Quest,ItemID);
					break;
				default:break;
				}
			
				break;
		default:break;
		}
	}
	
	private void PopulateItem(ItemTypes itemTypes, ItemID itemID, int amount) {
		
		loot.Add(ItemGenerator.CreatingProcess(itemTypes, itemID, amount));
	
		_used = true;
		
	}
	
	private IEnumerator Close() {
		_player = null;
		inUse = false;
		
		switch(type) {
		case Type.treasurebox:
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
			DestroyLoot();
	}
	
	private void DestroyLoot() {
		loot = null;
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
				case Type.treasurebox:
					for (int cnt = 0; cnt < _defautlColors.Length; cnt++)
						parts[cnt].renderer.material.SetColor("_Color", _defautlColors[cnt]);
					break;
				case Type.monster:
					for (int cnt = 0; cnt < _defautlColors.Length; cnt++)
						parts[cnt].renderer.material.SetColor("_Color", Color.black);
					break;
				case Type.tree:
					for (int cnt = 0; cnt < _defautlColors.Length; cnt++)
						parts[cnt].renderer.material.SetColor("_Color", _defautlColors[cnt]);
					break;
				case Type.require:
					for (int cnt = 0; cnt < _defautlColors.Length; cnt++)
						parts[cnt].renderer.material.SetColor("_Color", _defautlColors[cnt]);
					break;
				default:break;
				}
			}
			
		}
	}
}
