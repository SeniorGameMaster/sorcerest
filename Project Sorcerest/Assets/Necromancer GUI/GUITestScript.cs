using UnityEngine;
using System.Collections;

public class GUITestScript : MonoBehaviour {

	public GUISkin mySkin;
	public bool newGameButton = true;	
	public bool settingButton = true;	
	public bool exitButton = true;	
	
    Rect newGameWindow = new Rect(500,140,350,510);
	GUI.WindowFunction windowFunction;

	
private int leafOffset;
private int frameOffset;
private int skullOffset;

private int RibbonOffsetX;
private int FrameOffsetX;
private int SkullOffsetX;
private int RibbonOffsetY;
private int FrameOffsetY;
private int SkullOffsetY;

private int WSwaxOffsetX;
private int WSwaxOffsetY;
private int WSribbonOffsetX;
private int WSribbonOffsetY;
	
private int spikeCount;
	
// This script will only work with the Necromancer skin


	
//if you're using the spikes you'll need to find sizes that work well with them these are a few...
private Rect windowRect0 = new Rect (500, 140, 350, 510);
private Rect windowRect1 = new Rect (380, 40, 262, 420);
private Rect windowRect2 = new Rect (700, 40, 306, 480);
private Rect windowRect3 = new Rect (0, 40, 350, 500);

private Vector2 scrollPosition;
private float HroizSliderValue = 0.5f;
private float VertSliderValue = 0.5f;
private bool ToggleBTN = false;

//skin info
private string NecroText ="This started as a question... How flexible is the built in GUI in unity? The answer... pretty damn flexible! At first I wasnt so sure; it seemed no one ever used it to make a non OS style GUI at least not a publicly available one. So I decided I couldnt be sure until I tried to develop a full GUI, Long story short Necromancer was the result and is now available to the general public, free for comercial and non-comercial use. I only ask that if you add something Share it.   Credits to Kevin King for the fonts.";
	
	void Start()
    {
        windowFunction = setNewgameButton;
    }

	public void FancyTop(int topX)
	{
		leafOffset = (topX/2)-64;
		frameOffset = (topX/2)-27;
		skullOffset = (topX/2)-20;
		GUI.Label(new Rect(leafOffset, 18, 0, 0), "", "GoldLeaf");//-------------------------------- custom	
		GUI.Label(new Rect(frameOffset, 3, 0, 0), "", "IconFrame");//-------------------------------- custom	
		GUI.Label(new Rect(skullOffset, 12, 0, 0), "", "Skull");//-------------------------------- custom	
	}

	public void WaxSeal(int x, int y)
	{
		WSwaxOffsetX = x - 120;
		WSwaxOffsetY = y - 115;
		WSribbonOffsetX = x - 114;
		WSribbonOffsetY = y - 83;
		
		GUI.Label(new Rect(WSribbonOffsetX, WSribbonOffsetY, 0, 0), "", "RibbonBlue");//-------------------------------- custom	
		GUI.Label(new Rect(WSwaxOffsetX, WSwaxOffsetY, 0, 0), "", "WaxSeal");//-------------------------------- custom	
	}

	public void DeathBadge(int x,int y)
	{
		RibbonOffsetX = x;
		FrameOffsetX = x+3;
		SkullOffsetX = x+10;
		RibbonOffsetY = y+22;
		FrameOffsetY = y;
		SkullOffsetY = y+9;
		
		GUI.Label(new Rect(RibbonOffsetX, RibbonOffsetY, 0, 0), "", "RibbonRed");//-------------------------------- custom	
		GUI.Label(new Rect(FrameOffsetX, FrameOffsetY, 0, 0), "", "IconFrame");//-------------------------------- custom	
		GUI.Label(new Rect(SkullOffsetX, SkullOffsetY, 0, 0), "", "Skull");//-------------------------------- custom	
	}
	
    void OnGUI()
	{
			GUI.skin = mySkin;

		//GUI.BeginGroup (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 50, 200, 50));
		GUILayout.BeginArea (new Rect (Screen.width/2 - 125 , Screen.height/2 - 250, 300, 500));
		
		if(GUILayout.Button("New Game", "MainmenuButton",GUILayout.Width(300),GUILayout.Height(75)))
			Application.LoadLevel("NewScene");
		
		GUILayout.Space(50);
		
		if(GUILayout.Button("Setting", "MainmenuButton",GUILayout.Width(300),GUILayout.Height(75)))
			Debug.Log("Go to Setting");  
		
		GUILayout.Space(50);
		
		if(GUILayout.Button("Exit", "MainmenuButton",GUILayout.Width(300),GUILayout.Height(75)))
			Application.Quit();

		GUILayout.EndArea ();
		
	
		
		//GUI.EndGroup();
		/*
			if (newGameButton)
				newGameWindow = GUI.Window (0, newGameWindow, windowFunction, "");
				//now adjust to the group. (0,0) is the topleft corner of the group.
				GUI.BeginGroup (new Rect (0,0,100,100));
				// End the group we started above. This is very important to remember!
				GUI.EndGroup ();
			*/
	}
	
    void setNewgameButton(int windowID)
	{
		//GUI.Button(new Rect(10,20,100,20), "New Game");
		//GUILayout.Button("Standard Button");
		//FancyTop((int)newGameWindow.width);
		//GUILayout.BeginVertical();
		//GUILayout.BeginHorizontal();
		 
		//GUILayout.EndHorizontal();
		//GUILayout.EndVertical();
		//GUI.DragWindow (new Rect (0,0,10000,10000));
	}
	
}
