using UnityEngine;
using System.Collections;

public class CheckingQuest : MonoBehaviour {
	public QuestID questCheck;
	public GameObject questionText;
	
	// Use this for initialization
	void Start () {
		questionText.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
		switch(questCheck) {
		case QuestID.FindBoots:
			if(PlayerGUI.bootsQuest.Process == QuestProcess.Unknown)
				questionText.SetActive(true);
			else
				questionText.SetActive(false);
			break;
		}
	}
}
