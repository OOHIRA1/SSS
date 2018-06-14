using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==シークバーのポイントを管理するクラス
//
//使用方法：シークバーのポイントにアタッチ
public class Point : MonoBehaviour {
	RectTransform _transform;
	[SerializeField] float _leftPos  = 0;			//pointのｘ座標の最小値
	[SerializeField] float _rightPos = 0;			//pointのｘ座標の最大値
	float _maxRange;								//pointの最大移動範囲
	Vector2 _initialPos;
	// Use this for initialization
	void Start( ) {
		_transform = GetComponent< RectTransform >( );
		_initialPos = transform.position;
		_maxRange = _rightPos;
	}

	//==================================================
	//ゲッター
	public Vector2 GetTransform( ) { return _transform.position; } 

	public Vector2 GetInitialPos( ) { return _initialPos; }

	public float GetMaxRange( ) { return _maxRange; }
	//==================================================
	//===================================================

	// Update is called once per frame
	void Update( ) {
	}


	//=============================================================
	//public関数
	//=============================================================
	//--シークバーの位置を動かす関数
	public void MovePosition( Vector2 vector ) {
		
		Vector2 pos = vector;
		if ( vector.x < _leftPos ) {
			pos.x = _leftPos;
		}
		if (vector.x > _rightPos) {
			pos.x = _rightPos;
		}
			
		_transform.position = pos;
	}
	//=============================================================
	//=============================================================
}
