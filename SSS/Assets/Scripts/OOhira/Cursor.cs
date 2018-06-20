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
	[SerializeField] GameObject _selectedGameObject =null;	//選んだGameObject
	[SerializeField] bool _selectedFlag = false;			//選び終わったかどうかのフラグ


	public AudioClip cursor_SE;
    AudioSource audioSource;

	//=========================================================================
	//ゲッター
	public bool GetSelectedFlag() { return _selectedFlag; }

	public GameObject GetSelectedGameObject() { return _selectedGameObject;	}
	//=========================================================================
	//=========================================================================


	//=========================================================================
	//セッター
	public void SetSelectedFlag(bool x) { _selectedFlag = x; }
	//=========================================================================
	//=========================================================================


	// Use this for initialization
	void Start () {
		_firstPosition = transform.position;
		_downFlag = true;
		_selectedFlag = false;
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		//カーソルのアニメーション------------------------------------------------------
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
		//-----------------------------------------------------------------------------

		//_selectedFlagが立っていたら常に_selectedGameObjectを指し続ける--------
		if ( _selectedFlag ) {
			Vector3 pos = _selectedGameObject.transform.position;
			transform.position = new Vector3 (pos.x, _firstPosition.y, pos.z);
			return;
		}
		//---------------------------------------------------------------------

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = _rayShooter.Shoot (Input.mousePosition);
			if (hit) {
				if (_selectedGameObject == hit.collider.gameObject) {
					_selectedFlag = true;
				}

				//犯人指摘での処理-----------------------------------------------------
				if (hit.collider.tag == "Npc") {
					Vector3 pos = hit.transform.position;
					transform.position = new Vector3 (pos.x, _firstPosition.y, pos.z);
					_selectedGameObject = hit.collider.gameObject;
					audioSource.PlayOneShot(cursor_SE, 0.7F);
				}
				//--------------------------------------------------------------------

				//凶器カーソルでの処理-------------------------------------------------
				if (hit.collider.tag == "DangerousWepon") {
					Vector3 pos = hit.transform.position;
					transform.position = new Vector3 (pos.x, pos.y + 2, pos.z);
					_firstPosition.y = pos.y + 2;
					_selectedGameObject = hit.collider.gameObject;
					audioSource.PlayOneShot(cursor_SE, 0.7F);
				}
				//--------------------------------------------------------------------
			}
		}
	}

}
