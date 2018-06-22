using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==GameClearシーンを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class GameClearManager : MonoBehaviour {
	public enum State {
		FADE_IN,		//明転処理
		FANFARE_CALL,	//ファンファーレ・拍手喝采
		SHOW_TEXT		//テキスト解説
	};

	State _state;
	[SerializeField] Image _fadeInPanel = null;
	[SerializeField] float _fadeInSpeed = 0;	//明転処理のスピード(alpha/second)
	[SerializeField] AudioSource _bgm = null;
	[SerializeField] AudioSource _se = null;
	[SerializeField] DetectiveOfficeScript _detectiveAnimManager = null;
	[SerializeField] GameObject _clearText = null;	//ゲームクリアテキスト
	bool _fanfareStarted;							//ファンファーレが始まったかどうかのフラグ
	[SerializeField] DoubleDoorCurtain _curtain = null;
	[SerializeField] GameObject _confettiParticles = null;	//紙吹雪
	[SerializeField] DetectiveTalk _detectiveTalk = null;
	[SerializeField] ScenesManager _scenesManager = null;
	GameDataManager _gameDataManager;


	// Use this for initialization
	void Start () {
		_state = State.FADE_IN;
		_fanfareStarted = false;
		_gameDataManager = GameObject.FindWithTag ("GameDataManager").GetComponent<GameDataManager>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (_state) {
		case State.FADE_IN:
			FadeInAction ();
			break;
		case State.FANFARE_CALL:
			FanfareCallAction ();
			break;
		case State.SHOW_TEXT:
			ShowTextAction ();
			break;
		default :
			break;
		}

		Debug.Log (_state);
	}


	//--FADE_IN時の処理をする関数
	void FadeInAction() {
		if (_fadeInPanel.color.a > 0) {
			Color color = _fadeInPanel.color;
			_fadeInPanel.color = new Color (color.r, color.g, color.b, color.a - _fadeInSpeed * Time.deltaTime);
		} else {
			_fadeInPanel.gameObject.SetActive (false);
			_state = State.FANFARE_CALL;
			_bgm.PlayOneShot (_bgm.clip);
		}
	}


	//--FANFARE_CALL時の処理をする関数
	void FanfareCallAction() {
		if (!_fanfareStarted) {
			_detectiveAnimManager.DetectiveBow01 ();
			_se.PlayOneShot (_se.clip);
			//_clearText.SetActive (true);//フェードインとかする場合はアニメーションで行う
			StartCoroutine("ClearTextDisplay");
			_fanfareStarted = true;
		}
		if (_fanfareStarted && !_se.isPlaying && Input.GetMouseButtonDown(0)) {
			_curtain.Close ();
			_confettiParticles.SetActive (false);
			_clearText.SetActive (false);
			_state = State.SHOW_TEXT;
		}
	}


	//--SHOW_TEXT時の処理をする関数
	void ShowTextAction() {
		if (!_detectiveTalk.gameObject.activeInHierarchy && _curtain.IsStateClose() && _curtain.ResearchStatePlayTime() > 1f) {
			_detectiveTalk.gameObject.SetActive (true);
		} else {
			if (Input.GetMouseButtonDown (0)) {
				_detectiveTalk.Talk ();
			}
			if (_detectiveTalk.GetTalkFinishedFlag ()) {
				_detectiveTalk.gameObject.SetActive (false);
				_gameDataManager.UpdateAdvancedData (GameDataManager.CheckPoint.CLEAR_EPISODE1);
				_scenesManager.ScenesTransition ("StageSelect");
			}
		}

	}


	//--GameClearテキストを表示する関数(コルーチン)
	IEnumerator ClearTextDisplay() {
		while (!_detectiveAnimManager.IsStateBowStart() || _detectiveAnimManager.ResearchStatePlayTime () < 3.0f / 5) {
			yield return new WaitForSeconds (Time.deltaTime);
		}
		_clearText.SetActive (true);
	}
}
