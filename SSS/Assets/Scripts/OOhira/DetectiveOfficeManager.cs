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
		FINAL_JUDGE					//最終確認
	}

	[SerializeField] State _state;									//現在の探偵ラボのStateを格納する変数
	[SerializeField] GameObject _detectiveTalkingUI = null;			//探偵によるテキストで使用するUI
	[SerializeField] GameObject _criminalChoiseUI = null;			//犯人指摘で使用するUI
	[SerializeField] GameObject _dangerousWeaponChoiseUI = null;	//凶器選択で使用するUI
	[SerializeField] DetectiveTalk[] _detectiveTalk = new DetectiveTalk[3];			//探偵によるテキスト
	int _detectiveTalkIndex;														//探偵によるテキストの配列番号
	GameDataManager _gameDataManager;
	EvidenceManager _evidenceManager;
	[SerializeField] Cursor _cursorForCriminalChoise = null;						//犯人指摘用のカーソル
	[SerializeField] Cursor _cursorForDangerousWeaponChoise = null;					//凶器選択用のカーソル
	[SerializeField] LaboUIManager _laboUIManager = null;
	[SerializeField] Curtain _curtain = null;										//カーテン
	[SerializeField] Detective _detective = null;									//探偵
	[SerializeField] GameObject[] _npcCharacters = null;							//Npcキャラクター
	bool _curtainClosedStateCriminalChose;											//犯人指摘ステート中にカーテンを閉じたかどうかのフラグ
	bool _curtainOpenedStateCriminalChose;											//犯人指摘ステート中にカーテンを開いたかどうかのフラグ
	//[SerializeField] Evidence _evidence3 = null;
	[SerializeField] BoxCollider2D _cookBoxCollider2D = null;


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
		_curtainClosedStateCriminalChose = false;
		_curtainOpenedStateCriminalChose = false;
		_cookBoxCollider2D.enabled = false;
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
		if (Input.GetKeyDown (KeyCode.T)) {
			_state = State.FINAL_JUDGE;
		}
		if (Input.GetKeyDown (KeyCode.U)) {
			_curtain.Close ();
		}
		//-----------------------------------------------

		switch( _state ) {
		case State.INVESTIGATE://調査
			break;
		case State.DETECTIVE_TALKING://探偵によるテキスト表示
			if (!_detectiveTalk [_detectiveTalkIndex].gameObject.activeInHierarchy) {
				_detectiveTalk [_detectiveTalkIndex].gameObject.SetActive (true);
			}
			if (Input.GetMouseButtonDown (0)) {
				//次の文を表示するか次のStateに移動する処理-------------------------------
				if (_detectiveTalk[_detectiveTalkIndex].GetTalkFinishedFlag ()) {
					_detectiveTalk [_detectiveTalkIndex].gameObject.SetActive (false);
					switch (_detectiveTalkIndex) {
					case 1://証拠品3取得後の発言
						_state = State.INVESTIGATE;
						_cookBoxCollider2D.enabled = true;
						break;
					case 2://犯人指摘前の発言
						_state = State.CRIMINAL_CHOISE;
						break;
					case 3://凶器選択前の発音
						_state = State.DANGEROUS_WEAPON_CHOISE;
						break;
					case 4://最終確認前の発言
						_laboUIManager.DisplayJudgeUI ();
						_state = State.FINAL_JUDGE;
						break;
					default :
						_state = State.INVESTIGATE;
						break;
					}
				} else {
					_detectiveTalk[_detectiveTalkIndex].Talk ();
				}
				//-----------------------------------------------------------------------	
			}
			break;
		case State.CRIMINAL_CHOISE://犯人指摘
			if (_cursorForCriminalChoise.GetSelectedFlag ()) {
				if (!_curtain.IsStateClose () && !_curtainClosedStateCriminalChose) {
					_curtain.Close ();
					_curtainClosedStateCriminalChose = true;
				}
				if (_curtain.IsStateClose () && _curtain.ResearchStatePlayTime () >= 1f) {
					for (int i = 0; i < _npcCharacters.Length; i++) {
						if (_npcCharacters [i] != _cursorForCriminalChoise.GetSelectedGameObject ()) {
							_npcCharacters [i].SetActive (false);
						}
					}
					_curtain.Open ();
					_curtainOpenedStateCriminalChose = true;
				}
				if (_curtain.IsStateOpen () && _curtain.ResearchStatePlayTime () >= 1f && _curtainOpenedStateCriminalChose) {
					_detectiveTalkIndex = 3;
					_state = State.DETECTIVE_TALKING;
					_curtainClosedStateCriminalChose = false;
					_curtainOpenedStateCriminalChose = false;
					_cursorForCriminalChoise.SetSelectedFlag (false);
				}
			}
			break;
		case State.DANGEROUS_WEAPON_CHOISE://凶器選択
			if (_cursorForDangerousWeaponChoise.GetSelectedFlag ()) {
				_detectiveTalkIndex = 4;
				_state = State.DETECTIVE_TALKING;
				_cursorForDangerousWeaponChoise.gameObject.SetActive (false);	//「これでいいんだな？」で「いいえ」を選んだらカーソルをアクティブに戻す！
			}
			break;
		case State.FINAL_JUDGE://最終確認
			switch (_laboUIManager.GetJudge ()) {
			case LaboUIManager.Judge.YES:
				_curtain.Close ();
				break;
			case LaboUIManager.Judge.NO:
				if (!_curtain.IsStateClose () && !_curtainClosedStateCriminalChose) {
					_cursorForCriminalChoise.SetSelectedFlag (false);
					_curtain.Close ();
					_curtainClosedStateCriminalChose = true;
				}
				if (_curtain.IsStateClose () && _curtain.ResearchStatePlayTime () >= 1f) {
					for (int i = 0; i < _npcCharacters.Length; i++) {
						if (_npcCharacters [i] != _cursorForCriminalChoise.GetSelectedGameObject ()) {
							_npcCharacters [i].SetActive (true);
						}
					}
					_cursorForDangerousWeaponChoise.SetSelectedFlag (false);
					_curtain.Open ();
					_curtainOpenedStateCriminalChose = true;
				}
				if (_curtain.IsStateOpen () && _curtain.ResearchStatePlayTime () >= 1f && _curtainOpenedStateCriminalChose) {
					_curtainClosedStateCriminalChose = false;
					_curtainOpenedStateCriminalChose = false;
					_cursorForDangerousWeaponChoise.gameObject.SetActive (true);
					_laboUIManager.SetJudgeFlag ((int)LaboUIManager.Judge.OTHERWISE);
					_state = State.INVESTIGATE;
				}
				break;
			default :
				break;
			}

			break;
		default :
			break;
		}

		//各UIのオンオフを処理---------------------------------------------------------------------------------------------------------------
		ChangeActive (_detectiveTalkingUI, State.DETECTIVE_TALKING);
		ChangeActive (_criminalChoiseUI, State.CRIMINAL_CHOISE, _curtainClosedStateCriminalChose);
		ChangeActive (_dangerousWeaponChoiseUI, State.DANGEROUS_WEAPON_CHOISE, false, _cursorForDangerousWeaponChoise.GetSelectedFlag());
		//----------------------------------------------------------------------------------------------------------------------------------
		Debug.Log (_state);


		//初めて探偵ラボに来た時--------------------------------------------------------------------------------------------------
		if (!_gameDataManager.CheckAdvancedData (GameDataManager.CheckPoint.FIRST_COME_TO_DETECTIVE_OFFICE)) {
			//カーテンが開ききったら行う処理---------------------------------------------------------------------------
			if (_curtain.IsStateOpen () && _curtain.ResearchStatePlayTime() >= 1) {
				_detectiveTalkIndex = 0;
				_state = State.DETECTIVE_TALKING;
				_gameDataManager.UpdateAdvancedData (GameDataManager.CheckPoint.FIRST_COME_TO_DETECTIVE_OFFICE);
			}
			//-------------------------------------------------------------------------------------------------------
		}
		//------------------------------------------------------------------------------------------------------------------------

		//証拠品3を入手した時---------------------------------------------------------------------------------------------------------------------------------------------------
		if (_evidenceManager.CheckEvidence(EvidenceManager.Evidence.STORY1_EVIDENCE3) && !_gameDataManager.CheckAdvancedData(GameDataManager.CheckPoint.GET_EVIDENCE3)) {
			_detectiveTalkIndex = 1;
			_state = State.DETECTIVE_TALKING;
			_gameDataManager.UpdateAdvancedData (GameDataManager.CheckPoint.GET_EVIDENCE3);
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------

		//証拠品を全て揃えて探偵ラボに来た時-------------------------------------------------------------------------------------------------------------------------------------
		if (_evidenceManager.CheckEvidence(EvidenceManager.Evidence.STORY1_EVIDENCE6) && !_gameDataManager.CheckAdvancedData(GameDataManager.CheckPoint.GET_EVIDENCE6)) {
			//カーテンが開ききったら行う処理---------------------------------------------------------------------------
			if (_curtain.IsStateOpen () && _curtain.ResearchStatePlayTime () >= 1) {
				_detectiveTalkIndex = 2;
				_state = State.DETECTIVE_TALKING;
				_gameDataManager.UpdateAdvancedData (GameDataManager.CheckPoint.GET_EVIDENCE6);
			}
			//------------------------------------------------------------------------------------------------------
		}
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------

		//調査Stateでカーテンが開ききっている時のみ探偵の操作を受け付ける処理----------------------------------------
		if (_state == State.INVESTIGATE && _curtain.IsStateOpen() && _curtain.ResearchStatePlayTime() >= 1f) {
			_detective.enabled = true;
		} else {
			_detective.enabled = false;
		}
		//--------------------------------------------------------------------------------------------------------

		Debug.Log (_laboUIManager.GetJudge());
	}




	//--uiのアクティブ・非アクティブを現在のステートがstateかどうかで変える関数
	void ChangeActive( GameObject ui, State state, bool forcedNonActiveFlag = false, bool forcedActieFlag = false ) {
		if (forcedNonActiveFlag) {	//stateによらずforcedNonActiveFlagがtrueなら強制的に非アクティブにする
			ui.SetActive (false);
			return;
		}
		if (forcedActieFlag) {		//stateによらずforcedActiveFlagがtrueなら強制的にアクティブにする
			ui.SetActive (true);
			return;
		}
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
