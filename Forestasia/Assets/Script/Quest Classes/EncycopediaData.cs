using UnityEngine;
using System.Collections;

public class EncycopediaData {	
	private const int MAX_ENCYCOPEDIA = 3;
	
	public struct EncycopediaList {
		public EncycopediaID _encycopediaId;
		public string _dataDescription;
		public bool _showDetail;
		public bool _unlockEncycopedia;
		
	}
	
	public EncycopediaList[] _encycopediaList;
	
	public EncycopediaData() {
		 _encycopediaList = new EncycopediaList[MAX_ENCYCOPEDIA];
		addEncycopediaList();
	}
	
	private void addEncycopediaList() {
		/**************************/
		/*Rabbit*/
		/*************************/
		_encycopediaList[0]._encycopediaId = EncycopediaID.Rabbit;
		_encycopediaList[0]._dataDescription = "The fluffy animal live in hole.";
		_encycopediaList[0]._showDetail = false;
		_encycopediaList[0]._unlockEncycopedia = true;

		/**************************/
		/*Hedgehog*/
		/*************************/
		_encycopediaList[1]._encycopediaId = EncycopediaID.Hedgehog;
		_encycopediaList[1]._dataDescription = "The mammal animal have many spike in the back.";
		_encycopediaList[1]._showDetail = false;
		_encycopediaList[1]._unlockEncycopedia = true;
		
		/**************************/
		/*Herb*/
		/*************************/
		_encycopediaList[2]._encycopediaId = EncycopediaID.Hedgehog;
		_encycopediaList[2]._dataDescription = "The secret herb that have ability to cure wound when extract.";
		_encycopediaList[2]._showDetail = false;
		_encycopediaList[2]._unlockEncycopedia = false;
	}
	
	public EncycopediaID getEncycopediaID(int index) {
		return _encycopediaList[index]._encycopediaId;
	}
	
	public string getDataDescription(int index) {
		return _encycopediaList[index]._dataDescription;
	}
	
	public bool getShowDetail(int index) {
		return _encycopediaList[index]._showDetail;
	}
	
	public void setShowDetail(int index, bool show) {
		_encycopediaList[index]._showDetail = show;	
	}
	
	public bool getUnlockEncycopedia(int index) {
		return _encycopediaList[index]._unlockEncycopedia;	
	}
	
	public void setUnlockEncycopedia(int index, bool show) {
		_encycopediaList[index]._unlockEncycopedia = show;	
	}
		
	public int getEncycopediaLength() {
		return MAX_ENCYCOPEDIA;
	}
}

public enum EncycopediaID {
		None = 0,
		Rabbit = 1,
		Hedgehog = 2,
		Herb = 3
}
	