using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==シークバーを管理するクラス
//
//使用方法：シークバーにアタッチ
public class Bar : MonoBehaviour {
	Transform _transform;

	// Use this for initialization
	void Start( ) {
		_transform = GetComponent<Transform>( );
	}

	//=================================================
	//ゲッター
	public Vector3 GetTransform( ) { return _transform.localPosition; }
	//=================================================
	//=================================================


	// Update is called once per frame
	void Update( ) {
		
	}

	//===================================================
	//public関数
	//--シークバーのスケールを変更する関数
	public void MoveScale( Vector3 rate ) {
		_transform.localScale = rate;
	}
	//====================================================
	//====================================================
}
