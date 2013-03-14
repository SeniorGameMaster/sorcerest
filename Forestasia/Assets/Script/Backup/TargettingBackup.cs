using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TargettingBackup : MonoBehaviour {
	public Texture2D selectTexture2D,deselectTexture2D;
	public List<Transform> targets;
	public Transform selectedTarget;
	//public bool lockTarget;
	public int maxTargetDistance;
	public float rotationSpeed = 0.5f;
	
	private Transform myTransform;
	private Vector3 curTarget,curTransform;
	
	// Use this for initialization
	void Start () {
		targets = new List<Transform>();
		selectedTarget = null;
		myTransform = transform;
		
		AddAllEnemies();
	}
	
	public void AddAllEnemies() {
		GameObject[] go  = GameObject.FindGameObjectsWithTag("Enemy");
		
		foreach(GameObject enemy in go)
			AddTarget(enemy.transform);
	}
	
	public void AddTarget(Transform enemy) {
		targets.Add(enemy);
	}
	
	public void ClearTarget() {
		 
		targets.RemoveRange(0,targets.Count);	
		
	}
	
	private void SortTargetByDistance() {
		targets.Sort(delegate(Transform t1, Transform t2) { 
			return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position)); 
				});
	}
	
	private void TargetEnemy() {
		if(selectedTarget == null) {
			SortTargetByDistance();
			
			selectedTarget = targets[0];
		}
		else {
			int index = targets.IndexOf(selectedTarget);
			
			if(index < targets.Count - 1) {
				index++;
			}
			else {
				index = 0;	
			}
			DeselectTarget();
			selectedTarget = targets[index];			
		}
		SelectTarget();
	}
	
	private void SelectTarget() {
		selectedTarget.GetComponent<EnemyHealth>().isSelected = true;
		
		selectedTarget.renderer.material.SetTexture("_LightMap",selectTexture2D);
		
		PlayerAttack pa = (PlayerAttack)GetComponent("PlayerAttack");
		
		pa.target = selectedTarget.gameObject;
	}
	
	private void DeselectTarget() {
		selectedTarget.GetComponent<EnemyHealth>().isSelected = false;
		selectedTarget.renderer.material.SetTexture("_LightMap",deselectTexture2D);
		selectedTarget = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab)){
			ClearTarget();
			AddAllEnemies();
			TargetEnemy();		
		}
		/*
		if(Input.GetKeyDown(KeyCode.G)){
			lockTarget = !lockTarget;
		}
		
		if(lockTarget)
			LockTarget();
			*/
	}
	
	private void LockTarget() {
		if(Vector3.Distance(selectedTarget.position, myTransform.position) < maxTargetDistance) {
				Debug.Log("lOCKING");
				//Rotation to target
				curTarget = selectedTarget.position;
				curTransform = myTransform.position;
				curTarget.z = 0;
				curTransform.z = 0;
				curTarget.y= 0;
				curTransform.y = 0;
				//myTransform.LookAt(selectedTarget);
				myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(curTarget - curTransform),rotationSpeed);
		}
	}
}
