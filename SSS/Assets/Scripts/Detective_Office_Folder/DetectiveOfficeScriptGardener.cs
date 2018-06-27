using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveOfficeScriptGardener : MonoBehaviour {
 Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
	}
	

    public void GardenerTalkStarat() {
        _animator.SetBool( "GardenerTalk", true );
    }

    public void GardenerTalkFin() {
        _animator.SetBool( "GardenerTalk", false );
    }



}