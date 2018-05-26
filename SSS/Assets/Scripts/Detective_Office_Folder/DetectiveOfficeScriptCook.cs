using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveOfficeScriptCook : MonoBehaviour {

	Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void CookBaking01(){
		_animator.SetBool( "CoolBaking", true );
	}
	public void CookBaking02(){
		_animator.SetBool( "CoolBaking", false );
	}
	public void CookHold01(){
		_animator.SetBool( "CookHold", true );
	}
	public void CookHold02(){
		_animator.SetBool( "CookHold", false );
	}
	public void CookTalk01(){
		_animator.SetBool( "CookTalk", true );
	}
	public void CookTalk02(){
		_animator.SetBool( "CookTalk", false );
	}
	public void CookTransp01(){
		_animator.SetBool( "CookTransp", true );
	}
	public void CookTransp02(){
		_animator.SetBool( "CookTransp", false );
	}
	public void CookWalkStart01(){
		_animator.SetBool( "CookWalkStart", true );
	}
	public void CookWalkStart02(){
		_animator.SetBool( "CookWalkStart", false );
	}
}

