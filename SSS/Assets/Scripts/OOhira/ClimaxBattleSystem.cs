﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==クライマックスバトルを管理するクラス
//
//使用方法：クライマックスバトル時にアクティブなゲームオブジェクトにアタッチ
public class ClimaxBattleSystem : MonoBehaviour {
	public enum State {
		SHOW_VIDEO,			//ビデオ再生
		CHOOSE_CHOICES,		//選択
		BRIGHTING,			//明転処理
		RESULT				//バトル結果
	}
	public enum Result {
		NOW_FIGHTING,	//対戦中
		WIN,			//勝利
		LOSE			//敗北
	}
	const float INTERVAL = 5.5f;								//次の選択肢までのインターバル　※VideoPlayer.timeへの値の反映は数フレームしないと反映されない(Unityのバグ!? or 仕様!?)のため

	[System.Serializable]
	class BattleTurn {	//バトルターン
		public double _checkPointTime = 0;			//途中再生時の時間の秒数(チェックポイントとなる時間の秒数)
		public double _showChoicesTime = 0;			//選択肢群を表示する時間の秒数
		public GameObject _choices = null;			//選択肢群
		public GameObject _correctchoice = null;	//正しい選択肢
	};

	[SerializeField] State _state;
	[SerializeField] Result _result;
	[SerializeField] VideoController _videoController = null;
	[SerializeField] PlayerLife _playerLife = null;
	[SerializeField] MoviePlaySystem _moviePlaySystem = null;
	[SerializeField] BattleTurn[] _battleTurn = null;
	int _battleTurnArrayIndex;								//現在の_battleTurnの配列番号
	GameObject _playerChoice;									//プレイヤーが選んだ選択肢
	bool _playVideo;	//再生したかどうかのフラグ　※何度も再生しないように(再生する関数を呼んでも再生するまでのラグがある模様…)
	bool _showChoicesFlag;										//選択肢を見せるかどうかのフラグ
	[SerializeField] Image _brightingPanel = null;				//明転処理で使うパネル
	AudioSource _reverseTimeSE;									//巻き戻しSE
	[SerializeField] UnityEngine.UI.Button _evidenceButton = null;	//証拠品ファイルボタン

	//================================================================
	//ゲッター
	public Result GetResult() { return _result; }
	//================================================================
	//================================================================


	//================================================================
	//セッター
	public void SetPlayerChoice( GameObject x ) { _playerChoice = x; }
	//================================================================
	//================================================================


	// Use this for initialization
	void Start () {
		_state = State.SHOW_VIDEO;
		_result = Result.NOW_FIGHTING;
		_battleTurnArrayIndex = 0;
		_moviePlaySystem.SetMouseMove (false);	//マウス操作を受け付けなくする
		_moviePlaySystem.SetOperation(false);
		_playerChoice = null;
		_playVideo = false;
		_showChoicesFlag = true;
		_reverseTimeSE = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch(_state) {
		case State.SHOW_VIDEO:
			ShowVideoAction ();
			break;
		case State.CHOOSE_CHOICES:
			ChooseChoicesAction ();
			break;
		case State.BRIGHTING:
			BrightingAction ();
			break;
		case State.RESULT:
			ResultAction ();
			break;
		default :
			break;
		}
		Debug.Log ("ClimaxBattleSystem:" + _state);

		//_stateがCHOOSE_CHOICESの時,ビデオ停止時,ライフ0の時は証拠品ファイルボタンを押せなくする処理-------------
		if (_state == State.CHOOSE_CHOICES || !_videoController.IsPlaying() || _playerLife.GetDead()) {
			_evidenceButton.interactable = false;
		} else {
			_evidenceButton.interactable = true;
		}
		//---------------------------------------------------------------------------

		//プレイヤーのライフが無くなるか全問正解したらRESULTステートへ--------------------------------------
		if (_playerLife.GetDead () || _battleTurnArrayIndex == _battleTurn.Length) {
			_state = State.RESULT;
		}
		//---------------------------------------------------------------------------------------------
	}


	//--SHOW_VIDEOのステートの時の処理をする関数
	void ShowVideoAction() {
		if (!_videoController.IsPlaying () && !_playVideo) {
			_videoController.PlayVideo ();
			_playVideo = true;
		}
		//選択肢群を表示する時間に到達したら行う処理---------------------------------------------
		if (_videoController.IsPlaying ()) {//再生する関数を呼んでも再生するまでのラグがあるバグ( or 仕様)対策
//			if (_videoController.GetTime () >= _showChoicesGroupsTime [_showChoicesGroupsIndex]) {
//				_choicesGroups [_showChoicesGroupsIndex].SetActive (true);
//				_moviePlaySystem.StopAndPlayTime ();
//				_moviePlaySystem.SetOperation (true);
//				_state = State.CHOOSE_CHOICES;
//			}
			/*VideoPlayer.timeへの値の反映は数フレームしないと反映されない(Unityのバグ!? or 仕様!?)のため,コルーチンで意図的に間を作る！*/
			if (_videoController.GetTime () >= _battleTurn [_battleTurnArrayIndex]._showChoicesTime) {
				if (_showChoicesFlag) {
					StartCoroutine (ShowChices());
				}
			}
		}
		//---------------------------------------------------------------------------------

	}


	//--CHOOSE_CHOICESのステートの時の処理をする関数
	void ChooseChoicesAction() {
		//選択肢表示の時間が切れたら行う処理----------------------------------------------------------------------------------------
		if (_moviePlaySystem.MovieTimeMax ()) {
			_battleTurn[_battleTurnArrayIndex]._choices.SetActive (false);
			if (_playerChoice == _battleTurn [_battleTurnArrayIndex]._correctchoice) {//正解処理
				_battleTurnArrayIndex++;
			} else {//不正解or未選択処理
				_playerLife.Damege ();
				//_videoController.PlayVideo (_battleTurn [_battleTurnArrayIndex]._checkPointTime);//チェックポイントとなる時間に戻す
				if (!_playerLife.GetDead ()) {
					StartCoroutine (ReverseTime ());
				}
			}
			_moviePlaySystem.FastBackword ();	//バーを5秒巻き戻し
			_moviePlaySystem.SetOperation (false);
			_moviePlaySystem.StopAndPlayTime ();
			_playerChoice = null;
			_state = State.SHOW_VIDEO;
		}
		//-----------------------------------------------------------------------------------------------------------------------
		if (_playerChoice) {
			_battleTurn[_battleTurnArrayIndex]._choices.SetActive (false);
		}
	}


	//--RESULTのステートの時の処理をする関数
	void BrightingAction() {
		
	}


	//--RESULTのステートの時の処理をする関数
	void ResultAction() {
		if (_result == Result.NOW_FIGHTING) {
			if (_playerLife.GetDead ()) {//ライフが無くなっていたら
				_result = Result.LOSE;
				_videoController.FinishVideo ();
			} else if (!_videoController.IsPlaying () && _videoController.GetTime() >= _videoController.GetMaxTime()) {//ライフがあり　かつ　ビデオをを最後まで見れたら
				_result = Result.WIN;
				_videoController.FinishVideo ();
			}
		}
	}


	//--選択肢を表示する関数(コルーチン)
	IEnumerator ShowChices() {
		_showChoicesFlag = false;
		_battleTurn [_battleTurnArrayIndex]._choices.SetActive (true);
		_moviePlaySystem.StopAndPlayTime ();
		_moviePlaySystem.SetOperation (true);
		_state = State.CHOOSE_CHOICES;
		double interval = INTERVAL;
		while (interval > 0) {
			interval -= Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}
		_showChoicesFlag = true;
	}


	//--明転処理後に時間をチェックポイントの時間に戻す関数(コルーチン)
	IEnumerator ReverseTime( ) {
		_videoController.PauseVideo ();
		_reverseTimeSE.PlayOneShot (_reverseTimeSE.clip);
		//明転処理----------------------------------------------------------------------
		float fadeInSpeed = 2f;
		while (_brightingPanel.color.a < 1) {
			_brightingPanel.color += new Color (0, 0, 0, fadeInSpeed * Time.deltaTime);
			yield return new WaitForSeconds (Time.deltaTime);
		}
		yield return new WaitForSeconds(2f);//画面白の状態で一時待機
		//------------------------------------------------------------------------------
		_reverseTimeSE.Stop();
		_brightingPanel.color = new Color(1, 1, 1, 0);//色を元に戻す
		_videoController.PlayVideo (_battleTurn [_battleTurnArrayIndex]._checkPointTime);//チェックポイントとなる時間に戻す
	}
}
