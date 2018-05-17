using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveOfficeScriptMaid : MonoBehaviour {

    Animator _animator;

	// Use this for initialization
	void Start () {
        _animator = GetComponentInChildren<Animator>( );
	}
	
	// Update is called once per frame
	void Update () {
        if ( Input.GetKey(KeyCode.E )) {
            _animator.SetBool( "MaidWalk", true );
        }
        if ( Input.GetKey( KeyCode.R ) ) {
            _animator.SetBool( "MaidWalk", false );
        }
        if ( Input.GetKey( KeyCode.T ) ) {
            _animator.SetBool( "MaidTalk", true );
        }
        if ( Input.GetKey( KeyCode.Y ) ) {
            _animator.SetBool( "MaidTalk", false );
        }
        if ( Input.GetKey( KeyCode.U ) ) {
            _animator.SetBool( "MaidTransport", true );
        }
        if ( Input.GetKey( KeyCode.I ) ) {
            _animator.SetBool( "MaidTransport", false );
        }
        if ( Input.GetKey( KeyCode.N ) ) {
            _animator.SetBool( "MaidHold", true );
        }
        if ( Input.GetKey( KeyCode.M ) ) {
            _animator.SetBool( "MaidHold", false );
        }
    }
    public void MaidWalk01() {
            _animator.SetBool( "MaidWalk", true );
        }
    public void MaidWalk02() {
            _animator.SetBool( "MaidWalk", false );
        }
    public void MaidTalk01() {
            _animator.SetBool( "MaidTalk", true );
        }
    public void MaidTalk02(){
            _animator.SetBool( "MaidTalk", false );
        }
    public void MaidTransport01() {
            _animator.SetBool( "MaidTransport", true );
        }
    public void MaidTransport02() {
            _animator.SetBool( "MaidTransport", false );
        }
    public void MaidHold01() {
            _animator.SetBool( "MaidHold", true );
        }
    public void MaidHold02() {
            _animator.SetBool( "MaidHold", false );
        }
}
