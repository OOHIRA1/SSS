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
	[SerializeField] DetectiveTalk[] _detectiveTalk = null;			//探偵によるテキスト
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
	Vector3 _selectedNpcPosition;													//選択した犯人の座標を格納する変数
	[SerializeField] DarkingControll _darkingControll = null;
	[SerializeField] CutinControll _cutinControll = null;
	bool _cutinPlayedFlag;															//カットインをしたかどうかのフラグ
	bool _curtainClosedStateFinalJudge;											//最終確認ステート中にカーテンを閉じたかどうかのフラグ
	[SerializeField] ScenesManager _scenesManager = null;
	[SerializeField] Vector3 _exitPos = new Vector3(0, 0, 0);					//探偵が退場後の座標
	[SerializeField] GameObject[] _clockUIs = null;								//時計UI
	[SerializeField] BoxCollider2D[] _dangerousWeaponCollider = null;			//凶器UIのコライダー
	BGMManager _bgmManager;


	//===================================================================================
	//ゲッター
	public State GetState() { return _state; }
	public int GetDetectiveTalkIndex() { return _detectiveTalkIndex;}//※BGMManagerで使用
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
		if (!_evidenceManager.CheckEvidence (EvidenceManager.Evidence.STORY1_EVIDENCE3)) {//証拠品3を取っていなかったらコライダーを外す
			_cookBoxCollider2D.enabled = false;
		} else {
			_cookBoxCollider2D.enabled = true;
		}
		_cutinPlayedFlag = false;
		_curtainClosedStateFinalJudge = false;
		_bgmManager = GameObject.FindWithTag ("BGMManager").GetComponent<BGMManager> ();
		_bgmManager.UpdateBGM ();
		//GameDataManager.CheckPoint.GET_EVIDENCE6を取得していて探偵が遷移してきた時犯人指摘UIを表示する処理----------------------------
		if (_gameDataManager.CheckAdvancedData(GameDataManager.CheckPoint.GET_EVIDENCE6) ) {
			_laboUIManager.DisplayCriminalChoiseButton ();//※LaboUIManagerでもStartで非アクティブしているがUnityの設定でLaboUIManagerを先に初期化するように設定
		}
		//---------------------------------------------------------------------------------------------------------------------------
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
			_detective.DesignationMove (_detective.GetInitialPos ());//探偵を初期位置に移動
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
			InvestigateAction();
			break;
		case State.DETECTIVE_TALKING://探偵によるテキスト表示
			DetectiveTalkingAction();
			break;
		case State.CRIMINAL_CHOISE://犯人指摘
			CriminalChoiseAction();
			break;
		case State.DANGEROUS_WEAPON_CHOISE://凶器選択
			DangerousWeaponChoiseAction();
			break;
		case State.FINAL_JUDGE://最終確認
			FinalJudgeAction();
			break;
		default :
			break;
		}

		//各UIのオンオフを処理---------------------------------------------------------------------------------------------------------------
		ChangeActive (_detectiveTalkingUI, State.DETECTIVE_TALKING);
		ChangeActive (_criminalChoiseUI, State.CRIMINAL_CHOISE, /*false*/!_detective.GetCheckPos(), _cursorForCriminalChoise.GetSelectedFlag());//探偵が初期位置に戻るまで強制非表示＆犯人選択をしたら強制表示
		ChangeActive (_dangerousWeaponChoiseUI, State.DANGEROUS_WEAPON_CHOISE, false, _cursorForDangerousWeaponChoise.GetSelectedFlag());//凶器選択をしたら強制表示
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


		//調査Stateでカーテンが開ききっている時のみ探偵やＵＩの操作を受け付ける処理----------------------------------------
		if (_state == State.INVESTIGATE && _curtain.IsStateOpen() && _curtain.ResearchStatePlayTime() >= 1f) {
			//_detective.enabled = true;
			//時計ＵＩ表示時は探偵の操作を受け付けない処理----------------------
			for ( int i = 0; i < _clockUIs.Length; i++ ) {
				if (_clockUIs [i].activeInHierarchy) {
					_detective.SetIsMove (false);
					break;
				}
				if (i == _clockUIs.Length - 1) _detective.SetIsMove (true);
			}
			//---------------------------------------------------------------
			_laboUIManager.ChangeLaboButtonIntaractive(true);
		} else {
			//_detective.enabled = false;
			_detective.SetIsMove(false);//タッチで反応しなくなる関数
			_laboUIManager.ChangeLaboButtonIntaractive(false);
		}
		//---------------------------------------------------------------------------------------------------------------



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


	//--INVESTIGATE時の処理をする関数
	void InvestigateAction() {
		//犯人指摘のフラグが立っていたら犯人指摘ステートへ--------
		if (_laboUIManager.GetCriminalChoiseFlag ()) {
			_detective.DesignationMove (_detective.GetInitialPos ());//探偵を初期位置に移動
			_state = State.CRIMINAL_CHOISE;
			_bgmManager.UpdateBGM ();//音を変える処理
		}
		//-----------------------------------------------------
	}


	//--DETECTIVE_TALKING時の処理をする関数
	void DetectiveTalkingAction() {
		if (!_detectiveTalk [_detectiveTalkIndex].gameObject.activeInHierarchy) {
			_detectiveTalk [_detectiveTalkIndex].gameObject.SetActive (true);
			_detective.SetIsTalk (true);//話すアニメーション開始
		}
		//次の文を表示する処理------------------------------
		if (Input.GetMouseButtonDown (0) ) {
			_detectiveTalk [_detectiveTalkIndex].Talk ();
		}
		//-------------------------------------------------
		//文章が読み終わっていたら次のStateへ--------------------------------------
		if (_detectiveTalk [_detectiveTalkIndex].GetTalkFinishedFlag ()) {
			switch (_detectiveTalkIndex) {
			case 1://証拠品3取得後の発言
				_state = State.INVESTIGATE;
				_cookBoxCollider2D.enabled = true;
				break;
			case 2://犯人指摘前の発言
				_state = State.INVESTIGATE;
				_laboUIManager.DisplayCriminalChoiseButton ();//犯人指摘ボタン表示
				break;
			case 3://凶器選択前の発音
				_state = State.DANGEROUS_WEAPON_CHOISE;
				_detectiveTalk [_detectiveTalkIndex].ResetTalkedInfo ();
				break;
			case 4://最終確認前の発言
				_laboUIManager.DisplayJudgeUI ();
				_state = State.FINAL_JUDGE;
				_detectiveTalk [_detectiveTalkIndex].ResetTalkedInfo ();
				break;
			default :
				_state = State.INVESTIGATE;
				break;
			}
			_detective.SetIsTalk (false);//話すアニメーション終了
			_detectiveTalk [_detectiveTalkIndex].gameObject.SetActive (false);
		}
		//----------------------------------------------------------------------------
	}


	//--CRIMINAL_CHOISE時の処理をする関数
	void CriminalChoiseAction() {
		//犯人選択後の処理-----------------------------------------------------------------------------------------------------
		if (_cursorForCriminalChoise.GetSelectedFlag ()) {
			//カーテンを閉じる処理----------------------------------------------------
			if (!_curtain.IsStateClose () && !_curtainClosedStateCriminalChose) {
				_curtain.Close ();
				_curtainClosedStateCriminalChose = true;
				_selectedNpcPosition = _cursorForCriminalChoise.GetSelectedGameObject ().transform.position;	//選んだNpcの座標を格納
			}
			//-----------------------------------------------------------------------

			//選択していないNPCを非アクティブにしコライダーを非アクティブにする処理----------------------------------------------
			if (_curtain.IsStateWait () && _curtain.ResearchStatePlayTime () >= 1f && !_curtainOpenedStateCriminalChose) {	/*_curtain.IsStateClose () && _curtain.ResearchStatePlayTime () >= 1f	//※これだとカーテンSEを組み込み時に音ズレ発生*/
				for (int i = 0; i < _npcCharacters.Length; i++) {
					if (_npcCharacters [i] != _cursorForCriminalChoise.GetSelectedGameObject ()) {
						_npcCharacters [i].SetActive (false);
					} else {
						_npcCharacters [i].GetComponent<BoxCollider2D> ().enabled = false;//凶器カーソルも反応してしまうバグ修正
						_npcCharacters[i].transform.position = _npcCharacters[_npcCharacters.Length - 1].transform.position;//選択したNPCを右端に移動
					}
				}
				_curtain.Open ();
				_curtainOpenedStateCriminalChose = true;
			}
			//---------------------------------------------------------------------------------------------------------------

			//カーソルを消す処理------------------------------------------------------------------------------------------------
			if (_curtain.IsStateOpen () && _curtain.ResearchStatePlayTime () >= 0.5f && _curtainOpenedStateCriminalChose) {
				_cursorForCriminalChoise.gameObject.SetActive (false);	//「これでいいんだな？」で「いいえ」を選んだらカーソルをアクティブに戻す！
			}
			//-----------------------------------------------------------------------------------------------------------------

			//カーテンを再び開いたら変数を初期化して次のStateへ-----------------------------------------------------------------
			if (_curtain.IsStateOpen () && _curtain.ResearchStatePlayTime () >= 1f && _curtainOpenedStateCriminalChose) {
				_detectiveTalkIndex = 3;
				_state = State.DETECTIVE_TALKING;
				_curtainClosedStateCriminalChose = false;
				_curtainOpenedStateCriminalChose = false;
				_laboUIManager.SetCriminalChoiseFlag (false);	//犯人指摘フラグを戻す
			}
			//----------------------------------------------------------------------------------------------------------------
		}
		//-----------------------------------------------------------------------------------------------------------------------
	}


	//--DANGEROUS_WEAPON_CHOISE時の処理をする関数
	void DangerousWeaponChoiseAction() {
		if (_cursorForDangerousWeaponChoise.GetSelectedFlag ()) {
			for (int i = 0; i < _dangerousWeaponCollider.Length; i++) {
				_dangerousWeaponCollider [i].enabled = false;
			}
			_detectiveTalkIndex = 4;
			_state = State.DETECTIVE_TALKING;
			_cursorForDangerousWeaponChoise.gameObject.SetActive (false);	//「これでいいんだな？」で「いいえ」を選んだらカーソルをアクティブに戻す！
		}
	}


	//--FINAL_JUDGE時の処理をする関数
	void FinalJudgeAction() {
		switch (_laboUIManager.GetJudge ()) {
		case LaboUIManager.Judge.YES:
			if (!_cutinPlayedFlag) {
				_darkingControll.Darking ();//暗転処理
			}
			if (!_cutinPlayedFlag && _darkingControll.IsStateFadein () && _darkingControll.ResearchStatePlayTime () >= 1f) {
				//_cutinControll.StartCutin ();
				_cutinControll.StartCutinPart2 ();//カットイン処理
				_cutinPlayedFlag = true;
			}
			if (_cutinControll.GetCutinMoveFinishedFlag () && !_curtainClosedStateFinalJudge) {
				//_darkingControll.Bright ();	//明転処理はいらない
				_cutinControll.FinishCutin ();
				_curtain.Close ();
				_curtainClosedStateFinalJudge = true;
			}
			if (_curtainClosedStateFinalJudge && _curtain.IsStateClose () && _curtain.ResearchStatePlayTime () >= 1f) {
				_gameDataManager.SetCriminal (_cursorForCriminalChoise.GetSelectedGameObject ().name);
				_gameDataManager.SetDangerousWeapon (_cursorForDangerousWeaponChoise.GetSelectedGameObject ().name);
				if (!_detective.GetIsForcedMove ()) {
					StartCoroutine ("DetectiveExitAndSceneChange");
				}
				//_scenesManager.ScenesTransition ("ClimaxBattle");
			}
			//_curtain.Close ();
			break;
		case LaboUIManager.Judge.NO:
			if (!_curtain.IsStateClose () && !_curtainClosedStateCriminalChose) {
				GameObject npc = _cursorForCriminalChoise.GetSelectedGameObject ();
				npc.GetComponent<BoxCollider2D> ().enabled = true;
				_curtain.Close ();
				_curtainClosedStateCriminalChose = true;
			}
			if (_curtain.IsStateWait () && _curtain.ResearchStatePlayTime () >= 1f && !_curtainOpenedStateCriminalChose) {	/*_curtain.IsStateClose () && _curtain.ResearchStatePlayTime () >= 1f	//※これだとカーテンSEを組み込み時に音ズレ発生*/
				for (int i = 0; i < _npcCharacters.Length; i++) {
					if (_npcCharacters [i] != _cursorForCriminalChoise.GetSelectedGameObject ()) {
						_npcCharacters [i].SetActive (true);
					} else {
						_npcCharacters [i].transform.position = _selectedNpcPosition;	//Npcを元の位置に戻す
					}
				}
				for (int i = 0; i < _dangerousWeaponCollider.Length; i++) {
					_dangerousWeaponCollider [i].enabled = true;	//凶器UIのコライダーを元に戻す
				}
				_cursorForCriminalChoise.SetSelectedFlag (false);//犯人選択カーソルのSelectedFlagをfalseに
				_cursorForDangerousWeaponChoise.SetSelectedFlag (false);//凶器選択カーソルのSelectedFlagをfalseに

				_curtain.Open ();
				StartCoroutine(FadeOutAndUpdateBGM (BGMManager.BGMClip.CHOOSE));
				_curtainOpenedStateCriminalChose = true;
			}
			if (_curtain.IsStateOpen () && _curtain.ResearchStatePlayTime () >= 1f && _curtainOpenedStateCriminalChose) {
				_curtainClosedStateCriminalChose = false;
				_curtainOpenedStateCriminalChose = false;
				_cursorForCriminalChoise.gameObject.SetActive (true);
				_cursorForDangerousWeaponChoise.gameObject.SetActive (true);
				_laboUIManager.SetJudgeFlag ((int)LaboUIManager.Judge.OTHERWISE);
				_detectiveTalkIndex = 0;//0に戻す(3,4はBGMが異なるため)
				_state = State.INVESTIGATE;
			}
			break;
		default :
			break;
		}
	}


	//--探偵を退場させ,シーン移動する関数(コルーチン)
	IEnumerator DetectiveExitAndSceneChange() {
		_detective.SetIsMove (false);
		_detective.DesignationMove (_exitPos);
		while (_detective.GetIsForcedMove ()) {
			yield return new WaitForSeconds (Time.deltaTime);
		}
		_bgmManager.StopBGMWithFadeOut ();
		while(_bgmManager.IsPlaying(BGMManager.BGMClip.CHOOSE)) {
			yield return new WaitForSeconds(Time.deltaTime);
		}
		_scenesManager.ScenesTransition ("ClimaxBattle");
	}


	//--BGMをフェードアウトさせBGMをアップデートする関数
	IEnumerator FadeOutAndUpdateBGM( BGMManager.BGMClip bgmClip ) {
		_bgmManager.StopBGMWithFadeOut ();
		while (_bgmManager.IsPlaying (bgmClip)) {
			yield return new WaitForSeconds (Time.deltaTime);
		}
		_bgmManager.UpdateBGM ();
	}
}
