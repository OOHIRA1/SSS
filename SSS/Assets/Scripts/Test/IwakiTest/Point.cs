using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==シークバーのポイントを管理するクラス
//
//使用方法：シークバーのポイントにアタッチ
public class Point : MonoBehaviour {
	Transform _transform;
	[SerializeField] float _leftPos  = 0;			//pointのｘ座標の最小値
	[SerializeField] float _rightPos = 0;			//pointのｘ座標の最大値
	float _maxRange;									//pointの最大移動範囲

	// Use this for initialization
	void Start( ) {
		_transform = GetComponent<Transform>( );
		_maxRange = _rightPos - _leftPos;
	}

	//==================================================
	//ゲッター
	public Transform GetTransform( ) { return _transform; } 
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
	public void MovePosition( Vector3 vector ) {
		
		Vector3 pos = vector;
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
