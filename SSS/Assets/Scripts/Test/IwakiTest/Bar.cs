using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==シークバーを管理するクラス
//
//使用方法：シークバーにアタッチ
public class Bar : MonoBehaviour {
	RectTransform _rectTransform;

	// Use this for initialization
	void Start( ) {
		_rectTransform = GetComponent<RectTransform>( );
        _rectTransform.localScale = new Vector2( 0, 1 );
	}

	//=================================================
	//ゲッター
	public Vector2 GetTransform( ) { return _rectTransform.position; }
	//=================================================
	//=================================================


	// Update is called once per frame
	void Update( ) {
		
	}

	//===================================================
	//public関数
	//--シークバーのスケールを変更する関数
	public void MoveScale( Vector2 rate ) {
		_rectTransform.localScale = rate;
	}

	//====================================================
	//====================================================

	public Vector2 GetBarScale( ) {
		return _rectTransform.localScale;
    }

}
