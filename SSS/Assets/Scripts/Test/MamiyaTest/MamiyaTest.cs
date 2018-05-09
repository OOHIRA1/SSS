using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamiyaTest : MonoBehaviour {
	Animator _animator;
	ScenesManager SceneTest;
	// Use this for initialization
	void Start () { 
		_animator = GetComponent<Animator> ();
		SceneTest = GetComponent<ScenesManager>();
	}
	
	// Update is called once per frame
	void Update () {
		//if( Input.GetKeyDown (KeyCode.C) ) {
			//_animator.SetTrigger ( "CloseFlag" );
		//}
		if ( Input.GetKeyDown (KeyCode.T) ) {

			SceneTest.ScenesTransition ( "Title" );
			
		}

	}
	public void curatin_close () {

			_animator.SetTrigger ( "CloseFlag" );

	}

}
