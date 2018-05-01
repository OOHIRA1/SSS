using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr : MonoBehaviour {

	Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = GetComponentInChildren<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		int x = scr_02.num;

		/*if( x >= 1 ){
			_animator.SetBool( "Bool1",true );
		}*/

		if( Input.GetKey ( KeyCode.A )){
			_animator.SetBool( "Bool1",true );
		}

		if( Input.GetKey ( KeyCode.S) ){
			_animator.SetBool ("Bool1", false);
		}

	}
}
