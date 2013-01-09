using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {
	
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
		Debug.Log("Quit");
		Application.Quit();
	}
}