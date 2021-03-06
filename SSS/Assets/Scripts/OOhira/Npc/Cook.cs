﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour {
   DetectiveOfficeScriptCook _animManager;

	// Use this for initialization
	void Start () {
		_animManager = GetComponent<DetectiveOfficeScriptCook> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //===================================================================
    //コライダー検出系
    void OnTriggerStay2D( Collider2D col ) {
        if( col.gameObject.scene.name != "DetectiveOffice" ) return;//ラボシーン以外受け付けない
        if( col.tag == "Player" ) {
            Detective detective = col.gameObject.GetComponent<Detective>();
            if(!detective.GetIsAnimWalk ()) {
                _animManager.CookTalk01();//話すアニメーション
            }
        }
    }

    void OnTriggerExit2D( Collider2D col ) {
        if(col.gameObject.scene.name != "DetectiveOffice") return;//ラボシーン以外受け付けない
        if(col.tag == "Player") {
            _animManager.CookTalk02();//話すアニメーション終了
        }
    }
    //===================================================================
    //===================================================================
}