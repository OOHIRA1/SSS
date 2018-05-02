using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==シークバーのポイントを管理するクラス
//
//使用方法：シークバーのポイントにアタッチ
public class Point : MonoBehaviour {
	Transform _transform;
	float _leftPos;
	float _rightPos;

	// Use this for initialization
	void Start( ) {
		_transform = GetComponent<Transform>( );
	}
	
	// Update is called once per frame
	void Update( ) {
	}


	//=============================================================
	//public関数
	//=============================================================
	//--シークバーの位置を動かす関数
	public void MovePosition( Vector3 vector ) {
		
		Vector3 pos = vector;
		if (vector.x < _leftPos) {
			pos.x = _leftPos;
		}
		if (vector.x > _rightPos) {
			pos.x = _rightPos;
		}

		_transform.position = pos;
	}


	//--シークバーの移動範囲を設定する関数
	public void SetMoveRange( float left, float right ) {
		_leftPos = left;
		_rightPos = right; 
	}

	//=============================================================
	//=============================================================
}
