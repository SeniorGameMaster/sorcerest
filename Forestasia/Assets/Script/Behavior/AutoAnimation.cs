using UnityEngine;
using System.Collections;

public class AutoAnimation : MonoBehaviour {
	
	public AnimationClip autoAnim;
	public AnimationClip talkAnim;
	
	// Use this for initialization
	void Start () {
		//for(int i = 0; i < runAnim.Length; i++)
		animation[autoAnim.name].wrapMode = WrapMode.Loop;	
		animation[talkAnim.name].wrapMode = WrapMode.Once;	
		
		animation.CrossFade(autoAnim.name);
	}
	
	// Update is called once per frame
	void Update () {
		if(!animation.IsPlaying(talkAnim.name))
			animation.CrossFade(autoAnim.name);
	}
	
	void OnMouseDown() {
		animation.CrossFade(talkAnim.name);
	}
	
	void OnMouseUp() {
		//animation.CrossFade(talkAnim.name);
	}
}
