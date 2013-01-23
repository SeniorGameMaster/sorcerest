using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Mainmenu : MonoBehaviour {
	
	public Rect button = new Rect(15, 15, 200, 110);
	public string buttonLabel = "Load Level";
	public  string levelToLoad = "Ball";
	
	private void OnGUI()
	{
		if(GUI.Button(button, buttonLabel))
			Application.LoadLevel(levelToLoad);
	}
}
