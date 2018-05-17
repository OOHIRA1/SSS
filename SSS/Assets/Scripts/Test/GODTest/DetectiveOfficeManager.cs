using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==探偵ラボを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class DetectiveOfficeManager : MonoBehaviour {
	public enum State {
		INVESTIGATE,				//調査
		DETECTIVE_TALKING,			//探偵によるテキスト表示
		CRIMINAL_CHOISE,			//犯人指摘
		DANGEROUS_WEAPON_CHOISE,	//凶器選択
	}

	[SerializeField] State _state;								//現在の探偵ラボのStateを格納する変数
	[SerializeField] GameObject _dangerousWeaponPoster = null;	//凶器UI
	[SerializeField] GameObject _cursor = null;					//カーソル
	[SerializeField] GameObject _spotlightType2 = null;			//スポットライトタイプ２


	//===================================================================================
	//ゲッター
	public State GetState() { return _state; }
	//===================================================================================
	//===================================================================================

	// Use this for initialization
	void Start () {
		_state = State.INVESTIGATE;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			_state = State.DANGEROUS_WEAPON_CHOISE;
		}
		switch( _state ) {
		case State.INVESTIGATE:
			break;
		case State.DETECTIVE_TALKING:
			break;
		case State.CRIMINAL_CHOISE:
			break;
		case State.DANGEROUS_WEAPON_CHOISE:
			_dangerousWeaponPoster.SetActive (true);
			_cursor.SetActive (true);
			_spotlightType2.SetActive (true);
			break;
		default :
			break;
		}
		Debug.Log (_state);
	}
}
