using UnityEngine;
using System.Collections;

public class GUISystemButton : MonoBehaviour {

	public GUISkin mySkin;
	
	public int MapLevel;
	
    Rect newGameWindow = new Rect(500,140,350,510);
	GUI.WindowFunction windowFunction;

	void Start()
    {

    }
	
    void OnGUI()
	{
		GUI.skin = mySkin;
		
		GUILayout.BeginArea (new Rect (0 , Screen.height-200, 300, 500));
		
		if(MapLevel != 3)
		{
			if(GUILayout.Button("Next Level", "MainmenuButton",GUILayout.Width(200),GUILayout.Height(50)))
				if(MapLevel == 1)
					Application.LoadLevel("Level2");
				else if(MapLevel == 2)
					Application.LoadLevel("SecretRoom");
		}
		
		GUILayout.Space(10);
		
		if(GUILayout.Button("Back", "MainmenuButton",GUILayout.Width(200),GUILayout.Height(50)))
			if(MapLevel == 1)
				Application.LoadLevel("Mainmenu");
			else if(MapLevel == 2)
				Application.LoadLevel("Level1");
			else if(MapLevel == 3)
				Application.LoadLevel("Level2");
		
		GUILayout.Space(10);
		
		if(GUILayout.Button("Exit", "MainmenuButton",GUILayout.Width(200),GUILayout.Height(50)))
			Application.Quit();

		GUILayout.EndArea ();
	}
}
