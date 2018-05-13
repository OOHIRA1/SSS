using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveOfficeScript : MonoBehaviour {

    Rigidbody2D rb;

	Animator _animator;



	// Use this for initialization
	void Start () {
		
		_animator = GetComponentInChildren<Animator> ();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
    }
	


	public void GameOver () {
		_animator.SetBool( "boolGameOver", true );
	}
	

	public void pp () {
		_animator.SetBool( "pipe", true );
	}
	public void koziki () {
		_animator.SetBool( "koziki", true );
	}
	public void mg () {
		_animator.SetBool( "grass", true );
	}
	public void Walk () {
		//rb.AddForce ( Vector3.left * 0.5f );
           rb.velocity = new Vector2(-2f, 0);
           _animator.SetBool( "bool05", true );
           _animator.SetBool( "pipe", false );
           _animator.SetBool( "koziki", false );
           _animator.SetBool( "grass", false );
		_animator.SetBool( "wslk", true );
		this.transform.localScale = new Vector3 ( 1, 1, 1 );
	}
      public void Walk0 () {
           rb.velocity = new Vector2(0, 0);
           _animator.SetBool( "bool05", false );
           _animator.SetBool( "wslk", false );
       }

	public void walk () {
           //rb.AddForce (Vector3.right * 0.5f);
           rb.velocity = new Vector2(2f, 0);
           _animator.SetBool( "Bool05", true );
           
           _animator.SetBool( "pipe", false );
           _animator.SetBool( "koziki", false );
           _animator.SetBool( "grass", false );
           _animator.SetBool( "wslk", true );
		this.transform.localScale = new Vector3 ( -1, 1, 1 );
	}
       public void walk0 () {
           rb.velocity = new Vector2(0, 0);
           _animator.SetBool( "wslk", false );
           _animator.SetBool( "bool05", false );
        }
}