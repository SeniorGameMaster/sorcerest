using UnityEngine;
using System.Collections;
using System;

public class ObjectInteraction : MonoBehaviour {
	
	public enum State {
		talk,
		end,
		inbetween
	}
	
	public enum ObjectType {
		Quest,
		Mixing
	}
	
	public ObjectType objType;
	public QuestID questID = 0;
	public State state;
	public float maxDistance = 10;
	public bool inUse = false;
	public GameObject[] parts;
	
	private Color[] _defautlColors; 	//default color
	private GameObject _player;
	private Transform _myTransform;
	
	// Use this for initialization
	void Start () {
		
		state = ObjectInteraction.State.end;
		_myTransform = transform;
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
			ForceClose();
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
		
		switch(state) {
		case State.talk :
			
			//state = ObjectInteraction.State.inbetween;
			//ForceClose();
				
			break;
		case State.end :
			
			state = ObjectInteraction.State.inbetween;
			StartCoroutine("Talk");
			
			break;	
		default:break;
		}
		
	}
	
	private IEnumerator Talk() {
		//ObjectInteraction.owlQuest = this;
		
		_player = GameObject.FindGameObjectWithTag("Player");
		inUse = true;
		
		//audio.PlayOneShot(openSound);
		
		yield return new WaitForSeconds(.1f);
		state = ObjectInteraction.State.end;
		
		switch (objType) {
		case ObjectType.Quest :
				Messenger.Broadcast("AddQuestID" + (int)questID);
			break;
		case ObjectType.Mixing :
				Messenger.Broadcast("DisplayAlchemy");		
			break;
		}

	}
	/*
	private void PopulateChest(int x) {
		
		for(int cnt = 0; cnt < x; cnt++){
			loot.Add(new Item());
			loot[cnt].Name = "I:" + Random.Range(0,100);
		}
		_used = true;
	}*/
	
	private IEnumerator End() {
		_player = null;
		inUse = false;
			
		//audio.PlayOneShot(closeSound);
		
		yield return new WaitForSeconds(.1f);
		state = ObjectInteraction.State.end;
	}
	
	//Close the quest in any situation
	public void ForceClose() {
		switch (objType) {
		case ObjectType.Quest :
				Messenger.Broadcast("CloseQuest");
			break;
		case ObjectType.Mixing :
				Messenger.Broadcast("CloseAlchemy");	
			break;
		}
		
		
		StopCoroutine("Talk");
		StartCoroutine("End");
	}
	
	private void HighLight(bool glow) {
		if(glow){
			if(parts.Length > 0)
			for (int cnt = 0; cnt < _defautlColors.Length; cnt++)
				parts[cnt].renderer.material.SetColor("_Color", Color.white);
		}
		else {
			if(parts.Length > 0)
			for (int cnt = 0; cnt < _defautlColors.Length; cnt++)
				parts[cnt].renderer.material.SetColor("_Color", _defautlColors[cnt]);
		}
	}
}
