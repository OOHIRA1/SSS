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
	[SerializeField] GameObject _detectiveTalkingUI = null;			//探偵によるテキストで使用するUI
	[SerializeField] GameObject _criminalChoiseUI = null;			//犯人指摘で使用するUI
	[SerializeField] GameObject _dangerousWeaponChoiseUI = null;	//凶器選択で使用するUI


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
		//デバッグ用------------------------------------
		if (Input.GetKeyDown(KeyCode.Q)) {
			_state = State.INVESTIGATE;
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			_state = State.DETECTIVE_TALKING;
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			_state = State.CRIMINAL_CHOISE;
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			_state = State.DANGEROUS_WEAPON_CHOISE;
		}
		//-----------------------------------------------

		switch( _state ) {
		case State.INVESTIGATE:
			break;
		case State.DETECTIVE_TALKING:
			break;
		case State.CRIMINAL_CHOISE:
			break;
		case State.DANGEROUS_WEAPON_CHOISE:
			break;
		default :
			break;
		}
		ChangeActive (_detectiveTalkingUI, State.DETECTIVE_TALKING);
		ChangeActive (_criminalChoiseUI, State.CRIMINAL_CHOISE);
		ChangeActive (_dangerousWeaponChoiseUI, State.DANGEROUS_WEAPON_CHOISE);
		Debug.Log (_state);
	}


	//--uiのアクティブ・非アクティブを現在のステートがstateかどうかで変える関数
	void ChangeActive( GameObject ui, State state ) {
		if (ui.activeInHierarchy) {
			if (_state != state) {
				ui.SetActive (false);
			}
		} else {
			if (_state == state) {
				ui.SetActive (true);
			}
		}
	}
}
