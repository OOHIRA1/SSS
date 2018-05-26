using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveOfficeScript : MonoBehaviour {

	Animator _animator;

	// Use this for initialization
	void Start () {
		
		_animator = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
    }

	public void DetectiveWalk01(){
		_animator.SetBool( "DetectiveWalk", true );
	}
	public void DetectiveWalk02(){
		_animator.SetBool( "DetectiveWalk", false );
	}
	public void DetectiveThink01(){
		_animator.SetBool( "DetectiveThink", true );
	}
	public void DetectiveThink02(){
		_animator.SetBool( "DetectiveThink", false );
	}
	public void DetectiveBow01(){
		_animator.SetBool( "DetectiveBow", true );
	}
	public void DetectiveBow02(){
		_animator.SetBool( "DetectiveBow", false );
	}
	public void DetectiveConcetrate01(){
		_animator.SetBool( "DetectiveConcetrate", true );
	}
	public void DetectiveConcetrate02(){
		_animator.SetBool( "DetectiveConcetrate", false );
	}
	public void DetectiveDown01(){
		_animator.SetBool( "DetectiveDown", true );
	}
	public void DetectiveDown02(){
		_animator.SetBool( "DetectiveDown", false );
	}



}