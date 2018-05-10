using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//==カーテンの機能を管理するクラス
//
//使い方：Curtainにアタッチ
public class Curtain : MonoBehaviour {
	Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = GetComponentInChildren<Animator> ( );
	}
	
	// Update is called once per frame
	void Update () {
    }


	//----------------------------------------------
	//public関数
	//----------------------------------------------

	//--カーテンを開ける関数
	public void Open( ) {
		_animator.SetTrigger ( "OpenFlag" );
	}


	//--カーテンを閉める関数
	public void Close( ) {
		_animator.SetTrigger ( "CloseFlag" );
	}

    //----------------------------------------------
    //----------------------------------------------

}
