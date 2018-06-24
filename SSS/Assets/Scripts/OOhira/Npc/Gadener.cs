using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gadener : MonoBehaviour {
    DetectiveOfficeScriptGardener _animManager;


	// Use this for initialization
	void Start () {
		_animManager = GetComponent<DetectiveOfficeScriptGardener> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //=================================================================
    //コライダー検出系
    void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.scene.name != "DetectiveOffice" ) return;
        if(col.tag == "Player") {
            Detective detective = col.gameObject.GetComponent<Detective>();
            if(!detective.GetIsAnimWalk()) {
                _animManager.GardenerTalkStarat();
            }
        }
    }
    void OnTriggerExit2D( Collider2D col ) {
        if(col.gameObject.scene.name != "DetectiveOffice")return;
        if(col.tag == "Player") {
            _animManager.GardenerTalkFin();
        }
    }

    //=================================================================
    //=================================================================
}
