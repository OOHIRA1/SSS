using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_select_cutain_scripts : MonoBehaviour {
	Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
        
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetKeyDown(KeyCode.A)) {
			_animator.SetBool( "Bool_X",true );
    }

		if (Input.GetKeyDown (KeyCode.S)) {
			_animator.SetBool( "Bool_X",false);
		}

	}
}
