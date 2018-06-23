using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveOfficeMcsScript : MonoBehaviour {

    Animator _animator;

	// Use this for initialization
	void Start () {
        _animator = GetComponentInChildren<Animator>( );
	}
	
	// Update is called once per frame
	void Update () {

        if ( Input.GetKey( KeyCode.A ) ) {
            _animator.SetBool( "MrsTalk", true );
        }
        if ( Input.GetKey( KeyCode.S ) ) {
            _animator.SetBool( "MrsTalk", false );
        }
        if ( Input.GetKey( KeyCode.B ) ) {
            _animator.SetBool( "MrsWalk", true );
        }
        if ( Input.GetKey( KeyCode.N ) ) {
            _animator.SetBool( "MrsWalk", false );
        }
    }


	public void Talk01() {
		_animator.SetBool( "MrsTalk", true );
	}
	public void Talk02() {
		_animator.SetBool( "MrsTalk", false );
	}
}
