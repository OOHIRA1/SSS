﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamiyaTest : MonoBehaviour {
	Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//if( Input.GetKeyDown (KeyCode.C) ) {
			//_animator.SetTrigger ( "CloseFlag" );
		//}


	}
	public void curatin_close () {

			_animator.SetTrigger ( "CloseFlag" );

	}
}
