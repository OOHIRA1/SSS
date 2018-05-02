using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detective_Office_Script : MonoBehaviour {

	Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		int num_1 = 0;

		//_animator.SetBool( "Bool_01", false );

		if( Input.GetKey ( KeyCode.Q )){
			num_1 = 1;
		}

		if( Input.GetKey ( KeyCode.W )){
			num_1 = 2;
		}


		if( num_1 == 1 ){
			_animator.SetBool( "boolGameOver", true );
		} 

		if( num_1 == 2 ){
			_animator.SetBool( "bool01", true );
		}


		if( Input.GetKey ( KeyCode.P )){
			_animator.SetBool( "bool01", false );
			_animator.SetBool( "boolGameOver", false );
		}

	}
}
