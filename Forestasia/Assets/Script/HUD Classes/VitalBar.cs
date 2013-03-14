using UnityEngine;
using System.Collections;

public class VitalBar : MonoBehaviour {
	public bool _isPlayerHealthBar;	//This bool use for indentify player or mob health bar
	
	private int _maxBarLength;			//the maximum length of health bar in %
	private int _curBarLength;			//the current length of health bar in %
	private GUITexture _display;
	
	void Awake() {
		_display = gameObject.GetComponent<GUITexture>();
		
	}
	
	// Use this for initialization
	void Start () {		
		_maxBarLength = (int)_display.pixelInset.width;
		
		_curBarLength = _maxBarLength;
		_display.pixelInset = CalculatePosition();
		
		OnEnable();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//This method is called when the gameobject is enabled
	public void OnEnable() {
		if(_isPlayerHealthBar)
			Messenger<int, int>.AddListener("player health update", OnChangeHealthBarSize);
		else {
			ToggleDisplay(false);
			Messenger<int, int>.AddListener("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.AddListener("show mob vitalbars", ToggleDisplay);
		}
	}
	
	//This method is called when the gameobject is disabled
	public void OnDisable() {
		if(_isPlayerHealthBar)
			Messenger<int, int>.RemoveListener("player health update", OnChangeHealthBarSize);
		else {
			Messenger<int, int>.RemoveListener("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.RemoveListener("show mob vitalbars", ToggleDisplay);
		}
	}
	 
	//This method will calculate the total size of the healthbar in relation to the % of health the target hasleft
	public void OnChangeHealthBarSize(int curHealth, int maxHealth) {
		
		_curBarLength = (int)((curHealth / (float)maxHealth) * _maxBarLength);	//calculate current bar length based on the player %	
		
		//_display.pixelInset = new Rect(_display.pixelInset.x, _display.pixelInset.y, _curBarLength, _display.pixelInset.height);
		_display.pixelInset = CalculatePosition();
	}
	
	//setting the healthbar to the player or mob
	public void SetPlayerHealth(bool b) {
		_isPlayerHealthBar = b;	
	}
	
	private Rect CalculatePosition() {
		float yPos = _display.pixelInset.y / 2 - 10;
		
		if(!_isPlayerHealthBar) {
			float xPos = (_maxBarLength - _curBarLength) - (_maxBarLength / 4 + 10);
			return new Rect(xPos, yPos, _curBarLength, _display.pixelInset.height);
		}
		
		return new Rect(_display.pixelInset.x, yPos, _curBarLength, _display.pixelInset.height);
	}
	
	private void ToggleDisplay(bool show) {
		_display.enabled = show;
	}
}
