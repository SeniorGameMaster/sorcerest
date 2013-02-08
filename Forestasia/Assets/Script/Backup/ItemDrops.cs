using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[RequireComponent (typeof(BoxCollider))]
[RequireComponent (typeof(AudioSource))]

public class ItemDrops : MonoBehaviour {
	public enum State {
		open,
		close,
		inbetween
	}
	
	//public AudioClip openSound;  		//Open sound
	//public AudioClip closeSound; 		//Close sound
	 
	public GameObject particleEffect; 	//Particle effect when open
	
	public GameObject[] parts; 			//Highlight part of chest by color
	private Color[] _defautlColors; 	//default color of chest
	
	public State state; 				//Current state of chest
	
	public float maxDistance = 5;		//The max distance the player can open chest
	
	private GameObject _player;
	private Transform _myTransform;
	public bool inUse = false;
	private bool _used = false;			//tracking the chest is use or not
	
	public List<Item> loot = new List<Item>();
	
	// Use this for initialization
	void Start () {
		_myTransform = transform;
		
		state = ItemDrops.State.close;
		
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
			MyGUI.chest.ForceClose();
		//	Messenger.Broadcast("CloseChest");
	}
	
	public void OnMouseEnter() {

		HighLight(true);
	}
	
	public void OnMouseExit() {

		HighLight(false);
	}
	
	public void OnMouseUp() {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		
		if(go == null)
			return;
		
		if(Vector3.Distance(_myTransform.position, go.transform.position) > maxDistance && !inUse)
			return;
		
		if(_myTransform.GetComponent<EnemyHealth>().isDead) {
		
			switch(state) {
			case State.open :
				state = ItemDrops.State.inbetween;
				//StartCoroutine("Close");
				ForceClose();
				break;
			case State.close :
				if(MyGUI.chest != null) {
					MyGUI.itemdrops.ForceClose();
				}
			
				state = ItemDrops.State.inbetween;
				StartCoroutine("Open");
				break;	
			}
		}
	}
	
	private IEnumerator Open() {
		MyGUI.itemdrops = this;
		
		_player = GameObject.FindGameObjectWithTag("Player");
		inUse = true;
		
		//animation.Play("GoodOpen");
		particleEffect.SetActive(true);
		//audio.PlayOneShot(openSound);
		
		if(!_used)
			PopulateItem(1);
		
		yield return new WaitForSeconds(5);
		//yield return new WaitForSeconds(animation["GoodOpen"].length);
		state = ItemDrops.State.open;
		
		//Messenger<int>.Broadcast("PopulateChest", 5, MessengerMode.DONT_REQUIRE_LISTENER);
		Messenger.Broadcast("DisplayLoot");
	}
	
	private void PopulateItem(int x) {
		
		for(int cnt = 0; cnt < x; cnt++){
			loot.Add(new Item());
			loot[cnt].Name = "I:" + Random.Range(0,100);
		}
		_used = true;
	}
	
	private IEnumerator Close() {
		_player = null;
		inUse = false;
			
		//animation.Play("GoodClose");
		particleEffect.SetActive(false);
		//audio.PlayOneShot(closeSound);
		
		yield return new WaitForSeconds(5);
		//yield return new WaitForSeconds(animation["GoodClose"].length);
		state = ItemDrops.State.close;
		
		if(loot.Count == 0)
			Destroy(gameObject);
	}
	
	//Close the chest in any situation
	public void ForceClose() {
		Messenger.Broadcast("CloseLootItem");
		
		StopCoroutine("Open");
		StartCoroutine("Close");
	}
	
	//HighLight and deHighLight the selected chest
	private void HighLight(bool glow) {
		if(glow){
			if(parts.Length > 0)
			for (int cnt = 0; cnt < _defautlColors.Length; cnt++)
				parts[cnt].renderer.material.SetColor("_Color", Color.yellow);
		}
		else {
			if(parts.Length > 0)
			for (int cnt = 0; cnt < _defautlColors.Length; cnt++)
				parts[cnt].renderer.material.SetColor("_Color", _defautlColors[cnt]);
		}
	}
}
