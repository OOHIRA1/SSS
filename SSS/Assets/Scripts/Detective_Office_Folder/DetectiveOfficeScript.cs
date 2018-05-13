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
	

	public void Pipe () {
		_animator.SetBool( "pipe", true );
	}
	public void Voldemort () {
		_animator.SetBool( "koziki", true );
	}
	public void Grass () {
		_animator.SetBool( "grass", true );
	}
	public void WalkLft () {
		//rb.AddForce ( Vector3.left * 0.5f );
           rb.velocity = new Vector2(-2f, 0);
           _animator.SetBool( "fin", true );
           _animator.SetBool( "pipe", false );
           _animator.SetBool( "koziki", false );
           _animator.SetBool( "grass", false );
		_animator.SetBool( "walk", true );
		this.transform.localScale = new Vector3 ( 1, 1, 1 );
	}
      public void WalkLft0 () {
           rb.velocity = new Vector2(0, 0);
           _animator.SetBool( "fin", false );
           _animator.SetBool( "walk", false );
       }

	public void walk () {
           //rb.AddForce (Vector3.right * 0.5f);
           rb.velocity = new Vector2(2f, 0);
           _animator.SetBool( "fin", true );
           
           _animator.SetBool( "pipe", false );
           _animator.SetBool( "koziki", false );
           _animator.SetBool( "grass", false );
           _animator.SetBool( "walk", true );
		this.transform.localScale = new Vector3 ( -1, 1, 1 );
	}
       public void walk0 () {
           rb.velocity = new Vector2(0, 0);
           _animator.SetBool( "walk", false );
           _animator.SetBool( "fin", false );
        }

    public void Disappear () {
        _animator.SetBool( "Disappear", true );
    }
    public void appear () {
        _animator.SetBool( "Disappear", false );
    }
}