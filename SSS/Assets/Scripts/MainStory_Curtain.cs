using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStory_Curtain : MonoBehaviour {
	Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			_animator.SetTrigger ( "OpenFlag" );
		}
		if( Input.GetKeyDown (KeyCode.C) ) {
			_animator.SetTrigger ( "CloseFlag" );
		}


	}
}
