using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==探偵ラボの容疑者説明時のカーソル操作機能を管理するクラス
//
//使用方法：探偵ラボの常にアクティブなゲームオブジェクトにアタッチ
public class DetectiveTalkCursorControll : MonoBehaviour {
	[SerializeField] DetectiveTalk _detectiveTalk = null;
	[SerializeField] GameObject _cursor = null;
	Vector3 _firstPosition;					//カーソルの初期位置
	[SerializeField] float _moveRange = 0;	//アニメーションで動く範囲
	[SerializeField] float _moveSpeed = 0;	//アニメーションの速さ(unit/second)
	bool _downFlag;							//アニメーションで下に動いているかのフラグ
	[SerializeField] Transform[] _npcTransform = null;	//npcの座標(0:庭師、1:夫人、2:執事、3:メイド、4：料理長)


	// Use this for initialization
	void Start () {
		_cursor.SetActive (false);
		_firstPosition = _cursor.transform.position;
		_downFlag = true;
	}
	
	// Update is called once per frame
	void Update () {
		//カーソルのアニメーション----------------------------------------------------------
		if (_cursor.activeInHierarchy) {
			float posY = _cursor.transform.position.y;
			if (_downFlag) {
				_cursor.transform.Translate (0, -_moveSpeed * Time.deltaTime, 0, Space.World);
				if (posY < _firstPosition.y - _moveRange / 2) {
					_downFlag = false;
				}
			} else {
				_cursor.transform.Translate (0, _moveSpeed * Time.deltaTime, 0, Space.World);
				if (posY > _firstPosition.y + _moveRange / 2) {
					_downFlag = true;
				}
			}
		}
		//----------------------------------------------------------------------------------

		//カーソルの位置調整と表示・非表示----------------------------------------------------------------------------
		if (_detectiveTalk.gameObject.activeInHierarchy) {
			if (Input.GetMouseButtonDown (0)) {
				int stateMentNum = _detectiveTalk.GetStateMentNumber ();
				if (stateMentNum >= 1 && stateMentNum <= 5) {
					Vector3 pos = _npcTransform [stateMentNum - 1].position;
					_cursor.transform.position = new Vector3 (pos.x, _cursor.transform.position.y, pos.z);
					_cursor.SetActive (true);
				} else {
					_cursor.SetActive (false);
				}
			}
		}
		//-----------------------------------------------------------------------------------------------------------
	}

}
