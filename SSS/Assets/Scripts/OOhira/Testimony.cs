using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==証言をする機能を付けるクラス
//
//使用方法：証言をするNPCにアタッチ
[RequireComponent(typeof(BoxCollider2D))]
public class Testimony : MonoBehaviour {
	[SerializeField] GameObject _testimonyBalloon = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//===========================================================
	//コライダー検出時処理関数
	void OnTriggerEnter2D( Collider2D col ) {
		if (col.tag == "Player") {
			_testimonyBalloon.SetActive (true);
		}
	}

	void OnTriggerExit2D( Collider2D col ) {
		if (col.tag == "Player") {
			_testimonyBalloon.SetActive (false);
		}
	}
	//============================================================
	//============================================================

}
