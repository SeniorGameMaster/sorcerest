//Check if game have savedata in the playerpref
//Check version of save data
//if not current version
//do something
//else if current version
//check top see if they have a charater saved - check for a character name
//if they do not have a character saved, load character generation scene
//esle if they do have a character saved
//if they want to load the character, load the character and go to level 1
//if they want to delete the charactert, delete the character and go to character generation scene

using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public enum MenuState {
		Playgame,
		Checksave,
		Creating,
		About,
		
	}
	
	public bool clearPrefs = false;
	public const float VERSION = .1f;
	
	private string _levelToLoad = "";
	private string _characterGeneration = "CharacterGenerator";
	private string _firstlevel ="Level1";
	
	private bool _hasCharacter = false;
	private float _percentLoad = 0;
	private bool _displayOptions = true;
	
	private MenuState _menuState;
	// Use this for initialization
	void Start () {
		_menuState = MenuState.Playgame;
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(_menuState) {
			
		case MenuState.Playgame :
					
					break;
		case MenuState.Checksave :
			if(clearPrefs)
				PlayerPrefs.DeleteAll();
			
			if(PlayerPrefs.HasKey("ver")) {
				Debug.Log("There is a ver key");
				if(PlayerPrefs.GetFloat("ver") != VERSION) {
					Debug.Log("Saved version is not the same as current version");	
					/* Upgrade playerpref here */
				}
				else {
					Debug.Log("Saved version is the same as current version");	
					if(PlayerPrefs.HasKey("Player Name")) {
						Debug.Log("There is a Player Name tag");
						if(PlayerPrefs.GetString("Player Name") == "") {
							Debug.Log("The Player Name key does not have anything in it");
							PlayerPrefs.DeleteAll();
							_levelToLoad = _characterGeneration;	
						}
						else {
							Debug.Log("The Player Name key has value");
							_hasCharacter = true;
							_menuState = MenuState.Creating;
							//_levelToLoad = _firstlevel;	
						}
					}
					else {
						Debug.Log("There is no Player Name key");
						PlayerPrefs.DeleteAll();
						PlayerPrefs.SetFloat("ver", VERSION);
						_levelToLoad = _characterGeneration;	
						_menuState = MenuState.Creating;
					}
				}		
			}
			else {
				Debug.Log("There is not ver key");
				PlayerPrefs.DeleteAll();
				PlayerPrefs.SetFloat("ver", VERSION);
				_levelToLoad = _characterGeneration;
				_menuState = MenuState.Creating;
			}
			
			
			break;
			
		default:break;
		}
	
		if(_levelToLoad == "")
			return;
		
		if(Application.GetStreamProgressForLevel(_levelToLoad) == 1) {
			Debug.Log("Level Ready");	
			_percentLoad = 1;
			
			if(Application.CanStreamedLevelBeLoaded(_levelToLoad)) {
				Debug.Log("Level can be stramed, so lets load it up!");
				Application.LoadLevel(_levelToLoad);
			}
		}
		else {
			_percentLoad = Application.GetStreamProgressForLevel(_levelToLoad);	
		}
	}
	
	void OnGUI() {
		
		switch(_menuState) {
		case MenuState.Playgame :
			
			GUIStyle myStyle = new GUIStyle(GUI.skin.button);
			myStyle.fontSize = 40;
			Font myFont = (Font)Resources.Load("Fonts/COOPBL", typeof(Font));
			myStyle.font = myFont;
			myStyle.normal.textColor = Color.yellow;
				
			if(GUI.Button(new Rect(Screen.width/2 - 125 , Screen.height/2 - 250, 300, 50), "Play",myStyle)) {	
				_menuState = MenuState.Checksave;
			}
			
			if(GUI.Button(new Rect(Screen.width/2 - 125 , Screen.height/2 - 150, 300, 50), "About",myStyle)) {	
				_menuState = MenuState.About;
			}
			
			if(GUI.Button(new Rect(Screen.width/2 - 125 , Screen.height/2 - 50, 300, 50), "Exit",myStyle)) {	
				Application.Quit();
			}
			
			break;
		case MenuState.Creating :
			if(_displayOptions) {
				if(_hasCharacter) {
					if(GUI.Button(new Rect(10, 10, 110,25), "Load Characyter")) {	
						_levelToLoad = _firstlevel;
						_displayOptions = false;
					}
					if(GUI.Button(new Rect(10, 40, 110,25), "Delete Characyter")) {	
						PlayerPrefs.DeleteAll();
						PlayerPrefs.SetFloat("ver", VERSION);
						_levelToLoad = _characterGeneration;
						_displayOptions = false;
					}
				}
				
				if(_levelToLoad == "")
					return;
					
				GUI.Label(new Rect(Screen.width /2 - 50, Screen.height - 45, 100, 25), (_percentLoad * 100) + "%");
				GUI.Box(new Rect(0, Screen.height - 20, Screen.width * _percentLoad, 15), "");
			}
			break;
		default:break;
		}
	}
}
