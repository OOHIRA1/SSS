using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==カーソルのアニメーションを管理するクラス
//
//使用方法：カーソルにアタッチ
public class CursorAnimationControll : MonoBehaviour {
	Vector3 _firstPosition;					//カーソルの初期位置
	[SerializeField] float _moveRange = 0;	//アニメーションで動く範囲
	[SerializeField] float _moveSpeed = 0;	//アニメーションの速さ(unit/second)
	bool _downFlag;							//アニメーションで下に動いているかのフラグ

	// Use this for initialization
	void Start () {
		_firstPosition = transform.position;
		_downFlag = true;
	}
	
	// Update is called once per frame
	void Update () {
		float posY = transform.position.y;
		if (_downFlag) {
			transform.Translate (0, -_moveSpeed * Time.deltaTime, 0, Space.World);
			if (posY < _firstPosition.y - _moveRange / 2) {
				_downFlag = false;
			}
		} else {
			transform.Translate (0, _moveSpeed * Time.deltaTime, 0, Space.World);
			if (posY > _firstPosition.y + _moveRange / 2) {
				_downFlag = true;
			}
		}
	}

}
