using UnityEngine;
using System.Collections;

public class Spellcaster : MonoBehaviour {
	public GameObject[] spellPrefab;
	public int[] attackDamage;
	public float[] magicCooltime;
	public float[] magicSpeed;

	private int _curIndex;
	private float _spellTimer = 0;
	
	private bool _isBolt = false;
	private Transform _myTransform;
	private Transform _playerTransform;
	
	private GameObject _playerObj;
	private GameObject _spellObj;
	private GameObject _instantSpell;
	/*
	private void OnEnable() {
		
		Messenger.AddListener("Casting1", Casting1);
		Messenger.AddListener("Casting2", Casting2);
		Messenger.AddListener("Casting3", Casting3);
		
		
	}
	
	private void OnDisable() {
		Messenger.RemoveListener("Casting1", Casting1);
		Messenger.RemoveListener("Casting2", Casting2);
		Messenger.RemoveListener("Casting2", Casting3);
	}
	*/
	void Start () {
		
	}
	
	void Update () {
		
		
		
		if(_playerObj == null)
			return;
		
		if(_spellObj == null)
			return;
			
		if(_spellTimer > 0)
			_spellTimer -= Time.deltaTime;
		
		if(_spellTimer < 0) {
			_spellTimer = 0;
			_isBolt = false;	
		}
		
		if(_instantSpell == null)
			return;	
		
		if(_isBolt) {
			switch(_curIndex) {
			case 0:
				_instantSpell.rigidbody.AddForce(_instantSpell.transform.forward * magicSpeed[_curIndex]);
				break;
			case 1:
			if(_spellTimer < 4.5f) {
				//_spellTimer = 0;
				_isBolt = false;	
			}
			//_instantSpell.rigidbody.AddForce(_instantSpell.transform.forward * magicSpeed[_curIndex]);
				
				//* magicSpeed[spell]
			//_instantSpell.rigidbody.AddForce(_instantSpell.transform.forward * magicSpeed[_curIndex]);	
				//float distance = _instantSpell.transform.position - new Vector3(_instantSpell.transform.forward * magicSpeed[_curIndex] );
			//_instantSpell.transform.position = new Vector3(_instantSpell.transform.position.x, _instantSpell.transform.position.y, _instantSpell.transform.position.z);
					//Vector3.Dot(_instantSpell.transform.position, (_instantSpell.transform.forward));
				break;
			}
		}
		else {
				DestroyObject(_instantSpell);
		}	
	}
	
	public int getMagicDamage() {
		return attackDamage[_curIndex];
	}
	
	public void AddSpell(int spell) {
		_playerObj = GameObject.FindGameObjectWithTag("Player");
		_spellObj = Helpers.Find(spellPrefab[spell].name,typeof(GameObject)) as GameObject;
		//sp = GameObject.FindGameObjectWithTag("Spell");
			
		_playerTransform = _playerObj.transform;
	
		_myTransform = _playerTransform;
					

		 _instantSpell = Instantiate(spellPrefab[spell],_playerObj.transform.position,_playerObj.transform.rotation) as GameObject;
		
		_curIndex = spell;
		_isBolt = true;
		
		/*
		if(_curIndex == 1)
			_instantSpell.transform.position = new Vector3(_instantSpell.transform.position.x, _instantSpell.transform.position.y, _instantSpell.transform.position.z * magicSpeed[_curIndex]);
		*/	
	}
	
	public void Casting1() {		
		if(_spellTimer == 0) {
			_spellTimer = magicCooltime[0];
			SendMessage("PlayMeleeAttack");
			AddSpell(0);
		}
	}
	
	public void Casting2() {		
		if(_spellTimer == 0) {
			_spellTimer = magicCooltime[1];
			SendMessage("PlayMeleeAttack");
			AddSpell(1);
		}
	}
	
	public void Casting3() {		
		if(_spellTimer == 0) {
			_spellTimer = magicCooltime[2];
			SendMessage("PlayMeleeAttack");
			AddSpell(2);
		}
	}
}
