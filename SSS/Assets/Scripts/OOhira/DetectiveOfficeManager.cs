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
	[SerializeField] DetectiveTalk[] _detectiveTalk = new DetectiveTalk[3];			//探偵によるテキスト
	int _detectiveTalkIndex;														//探偵によるテキストの配列番号
	GameDataManager _gameDataManager = null;
	EvidenceManager _evidenceManager = null;
	[SerializeField] Cursor _cursorForCriminalChoise = null;
	[SerializeField] Cursor _cursorForDangerousWeaponChoise = null;
	[SerializeField] LaboUIManager _laboUIManager = null;

	//===================================================================================
	//ゲッター
	public State GetState() { return _state; }
	//===================================================================================
	//===================================================================================

	// Use this for initialization
	void Start () {
		_state = State.INVESTIGATE;
		_detectiveTalkIndex = 0;
		_gameDataManager = GameObject.FindWithTag ("GameDataManager").GetComponent<GameDataManager>();
		_evidenceManager = GameObject.FindWithTag ("EvidenceManager").GetComponent<EvidenceManager> ();
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
			if (!_detectiveTalk [_detectiveTalkIndex].gameObject.activeInHierarchy) {
				_detectiveTalk [_detectiveTalkIndex].gameObject.SetActive (true);
			}
			if (Input.GetMouseButtonDown (0)) {
				if (_detectiveTalk[_detectiveTalkIndex].GetTalkFinishedFlag ()) {
					_detectiveTalk [_detectiveTalkIndex].gameObject.SetActive (false);
					if (_detectiveTalkIndex == 2) {
						_state = State.CRIMINAL_CHOISE;
					} else if (_detectiveTalkIndex == 3) {
						_state = State.DANGEROUS_WEAPON_CHOISE;
					} else {
						_state = State.INVESTIGATE;
					}
				} else {
					_detectiveTalk[_detectiveTalkIndex].Talk ();
				}
					
			}
			break;
		case State.CRIMINAL_CHOISE:
			if (_cursorForCriminalChoise.GetSelectedFlag ()) {
				_detectiveTalkIndex = 3;
				_state = State.DETECTIVE_TALKING;
			}
			break;
		case State.DANGEROUS_WEAPON_CHOISE:
			if (_cursorForDangerousWeaponChoise.GetSelectedFlag ()) {
				_detectiveTalkIndex = 4;
				_state = State.DETECTIVE_TALKING;
			}
			break;
		default :
			break;
		}
		ChangeActive (_detectiveTalkingUI, State.DETECTIVE_TALKING);
		ChangeActive (_criminalChoiseUI, State.CRIMINAL_CHOISE);
		ChangeActive (_dangerousWeaponChoiseUI, State.DANGEROUS_WEAPON_CHOISE);
		Debug.Log (_state);

		if (!_gameDataManager.CheckAdvancedData (GameDataManager.CheckPoint.FIRST_COME_TO_DETECTIVE_OFFICE)) {	//初めて探偵ラボに来た時
			_detectiveTalkIndex = 0;
			_state = State.DETECTIVE_TALKING;
			_gameDataManager.UpdateAdvancedData (GameDataManager.CheckPoint.FIRST_COME_TO_DETECTIVE_OFFICE);
		}
		if (_evidenceManager.CheckEvidence(EvidenceManager.Evidence.STORY1_EVIDENCE3) && !_gameDataManager.CheckAdvancedData(GameDataManager.CheckPoint.GET_EVIDENCE3)) {	//証拠品3を入手した時
			_detectiveTalkIndex = 1;
			_state = State.DETECTIVE_TALKING;
			_gameDataManager.UpdateAdvancedData (GameDataManager.CheckPoint.GET_EVIDENCE3);
		}
		if (_evidenceManager.CheckEvidence(EvidenceManager.Evidence.STORY1_EVIDENCE6) && !_gameDataManager.CheckAdvancedData(GameDataManager.CheckPoint.GET_EVIDENCE6)) {	//証拠品を全て揃えて探偵ラボに来た時
			_detectiveTalkIndex = 2;
			_state = State.DETECTIVE_TALKING;
			_gameDataManager.UpdateAdvancedData (GameDataManager.CheckPoint.GET_EVIDENCE6);
		}

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
