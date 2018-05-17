using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveOfficeScriptButler : MonoBehaviour {

    Animator _animator;

	// Use this for initialization
	void Start () {
        _animator = GetComponentInChildren<Animator>( );
	}
	
	// Update is called once per frame
	void Update () {

        if ( Input.GetKey( KeyCode.Z ) ) {
            _animator.SetBool( "ButlerShockStart", true );
        }
        if ( Input.GetKey( KeyCode.X ) ) {
            _animator.SetBool( "ButlerShockStart", false );
        }
        if ( Input.GetKey( KeyCode.C ) ) {
            _animator.SetBool( "ButlerWalkStart", true );
        }
        if ( Input.GetKey( KeyCode.V ) ) {
            _animator.SetBool( "ButlerWalkStart", false );
        }
        if ( Input.GetKey( KeyCode.D ) ) {
            _animator.SetBool( "ButlerTalk", true );
        }
        if ( Input.GetKey( KeyCode.F ) ) {
            _animator.SetBool( "ButlerTalk", false );
        }
    }
     public void ButlerShock01() {
            _animator.SetBool( "ButlerShockStart", true );
        }
     public void ButlerShock02() {
            _animator.SetBool( "ButlerShockStart", false );
        }
     public void ButlerWalk01() {
            _animator.SetBool( "ButlerWalkStart", true );
        }
     public void ButlerWalk02() {
            _animator.SetBool( "ButlerWalkStart", false );
        }
     public void ButlerTalk01() {
            _animator.SetBool( "ButlerTalk", true );
        }
     public void ButlerTalk02() {
            _animator.SetBool( "ButlerTalk", false );
        }
}
