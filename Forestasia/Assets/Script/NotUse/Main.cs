using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    public Texture2D cursorImage;

    void Start()
    {
        Screen.showCursor = false;
    }

    void OnGUI()
    {       
        GUI.DrawTexture(new Rect(Input.mousePosition.x - 64, Screen.height - Input.mousePosition.y - 64, 128, 128), cursorImage);
    }
}