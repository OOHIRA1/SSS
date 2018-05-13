using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==カーソルを管理するクラス
//
//使用方法：カーソルにアタッチ
public class Cursor : MonoBehaviour {
	Vector3 _firstPosition;					//カーソルの初期位置
	[SerializeField] float _moveRange = 0;	//アニメーションで動く範囲
	[SerializeField] float _moveSpeed = 0;	//アニメーションの速さ(unit/second)
	bool _downFlag;							//アニメーションで下に動いているかのフラグ
	[SerializeField] RayShooter _rayShooter = null;			//Rayを発射するものを格納する変数

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

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = _rayShooter.Shoot (Input.mousePosition);
			if (hit) {
				//犯人指摘での処理-----------------------------------------------------
				if (hit.collider.tag == "Npc") {
					Vector3 pos = hit.transform.position;
					transform.position = new Vector3 (pos.x, _firstPosition.y, pos.z);
				}
				//--------------------------------------------------------------------

				//凶器カーソルでの処理-------------------------------------------------
				if (hit.collider.tag == "DangerousWepon") {
					Vector3 pos = hit.transform.position;
					transform.position = new Vector3 (pos.x, pos.y + 2, pos.z);
					_firstPosition.y = pos.y + 2;
				}
				//--------------------------------------------------------------------
			}
		}
	}

}
