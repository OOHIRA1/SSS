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

		Rigidbody2D rb = GetComponent<Rigidbody2D>();

		//int num_1 = 0;

		//_animator.SetBool( "Bool_01", false );

		if( Input.GetKey ( KeyCode.Q )){
			_animator.SetBool( "boolGameOver", true );
		}
		if( Input.GetKey ( KeyCode.W )){
			_animator.SetBool( "bool01", true );
		}
		if( Input.GetKey ( KeyCode.E )){
			_animator.SetBool( "bool01", false );
		}
		if( Input.GetKey ( KeyCode.Z )){
			_animator.SetBool( "Bool02", true );
		}
		if( Input.GetKey ( KeyCode.X )){
			_animator.SetBool( "Bool03", true );
		}
		if( Input.GetKey ( KeyCode.C )){
			_animator.SetBool( "Bool04", true );
		}
		if( Input.GetKey ( KeyCode.A )){
			rb.AddForce ( Vector3.left * 0.5f );
			_animator.SetBool( "bool01", true );
			this.transform.localScale = new Vector3 ( 1, 1, 1 );
		}	
		if( Input.GetKey ( KeyCode.D )){
			rb.AddForce (Vector3.right * 0.5f);
			_animator.SetBool( "bool01", true );
			this.transform.localScale = new Vector3 ( -1, 1, 1 );
		}
		if( Input.GetKey ( KeyCode.S )){
			_animator.SetBool( "bool01", false );
			rb.AddForce (Vector3.right * 0.0f);
		}

		/*if( num_1 == 1 ){
			_animator.SetBool( "boolGameOver", true );
		} 

		if( num_1 == 2 ){
			_animator.SetBool( "bool01", true );
		}


		if( Input.GetKey ( KeyCode.P )){
			_animator.SetBool( "bool01", false );
			_animator.SetBool( "boolGameOver", false );
		}*/

	}
}
