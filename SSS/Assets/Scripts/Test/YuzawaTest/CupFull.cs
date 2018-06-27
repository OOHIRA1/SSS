using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupFull : MonoBehaviour {
	[SerializeField]GameObject _cupFull = null;
	[SerializeField]GameObject _cupEmpty = null;

	// Use this for initialization
	void Start () {
		
	}

	public void Cup(){
		_cupFull.SetActive (true);
		_cupEmpty.SetActive (false);
	}
}
