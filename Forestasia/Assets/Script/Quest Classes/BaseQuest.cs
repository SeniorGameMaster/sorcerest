using UnityEngine;
using System.Collections;

public class BaseQuest : MonoBehaviour {
	
	public enum State {
		talk,
		end,
		inbetween
	}
	
	public int questID = 0;
	public State state;
	public float maxDistance = 10;
	public bool inUse = false;
	public GameObject[] parts;
	
	private Color[] _defautlColors; 	//default color
	private GameObject _player;
	private Transform _myTransform;
	
	// Use this for initialization
	void Start () {
		
		state = BaseQuest.State.end;
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
			
			//state = BaseQuest.State.inbetween;
			//ForceClose();
				
			break;
		case State.end :
			
			state = BaseQuest.State.inbetween;
			StartCoroutine("Talk");
			
			break;	
		default:break;
		}
		
	}
	
	private IEnumerator Talk() {
		//BaseQuest.owlQuest = this;
		
		_player = GameObject.FindGameObjectWithTag("Player");
		inUse = true;
		
		//audio.PlayOneShot(openSound);
		
		yield return new WaitForSeconds(.1f);
		state = BaseQuest.State.end;
		
		Messenger.Broadcast("AddQuestID"+questID.ToString());
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
		state = BaseQuest.State.end;
	}
	
	//Close the quest in any situation
	public void ForceClose() {
		Messenger.Broadcast("CloseQuest");
		
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
