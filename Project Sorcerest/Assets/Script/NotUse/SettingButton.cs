using UnityEngine;
using System.Collections;

public class SettingButton : MonoBehaviour {
	
	public GUITexture clickButton;
	public Texture2D selectButtonTexture;
	public Texture2D deselectButtonTexture;

	private void OnMouseEnter()
	{
		clickButton.texture = selectButtonTexture;
	}
	
	private void OnMouseExit()
	{
		clickButton.texture = deselectButtonTexture;
	}
	
	private void OnMouseDown()
	{
		//Application.LoadLevel("Ball");	
	}
}
