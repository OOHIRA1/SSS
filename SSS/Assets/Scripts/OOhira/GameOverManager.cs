using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==GameOverシーンを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class GameOverManager : MonoBehaviour {
	public enum State {
		FADE_IN,	//明転処理
		BOOING,		//ブーイング
		SHOW_TEXT,	//テキスト解説
		CONTINUE	//コンティニュー画面
	}

	State _state;
	[SerializeField] Image _fadeInPanel = null;
	[SerializeField] float _fadeInSpeed = 0;	//明転処理のスピード(alpha/second)
	[SerializeField] AudioSource _bgm = null;
	[SerializeField] AudioSource _se = null;
	[SerializeField] DetectiveOfficeScript _detectiveAnimManager = null;
	[SerializeField] GameObject _gameOverText = null;	//ゲームクリアテキスト
	bool _booingStarted;							//ファンファーレが始まったかどうかのフラグ
	[SerializeField] GameObject _spotLight = null;	//スポットライト
	[SerializeField] DoubleDoorCurtain _curtain = null;
	[SerializeField] GameObject _confettiParticles = null;	//紙吹雪
	[SerializeField] GabegeShooter _gabegeShooter = null;
	[SerializeField] GameObject _gabeges = null;			//ゴミのゲームオブジェクト
	[SerializeField] DetectiveTalk _detectiveTalk = null;
	[SerializeField] GameObject _continuePanel = null;		//コンティニュー確認UI
	[SerializeField] ScenesManager _scenesManager = null;
	[SerializeField] ClockUI _clockUI = null;

	// Use this for initialization
	void Start () {
		_state = State.FADE_IN;
		_clockUI.SetRewind (true);
		_clockUI.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		switch (_state) {
		case State.FADE_IN:
			FadeInAction ();
			break;
		case State.BOOING:
			BooingAction ();
			break;
		case State.SHOW_TEXT:
			ShowTextAction ();
			break;
		case State.CONTINUE:
			ContinueAction ();
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
			_spotLight.SetActive (true);
			_state = State.BOOING;
			_bgm.PlayOneShot (_bgm.clip);
		}
	}


	//--BOOING時の処理をする関数
	void BooingAction() {
		if (!_booingStarted) {
			_detectiveAnimManager.DetectiveGameOver ();
			_se.PlayOneShot (_se.clip);
			StartCoroutine("GameOverTextDisplay");
			_gabegeShooter.Shoot ();
			_booingStarted = true;
		}
		if (_booingStarted && !_se.isPlaying ) {
			_state = State.SHOW_TEXT;
		}
	}


	//--SHOW_TEXT時の処理をする関数
	void ShowTextAction() {
		if (!_detectiveTalk.gameObject.activeInHierarchy) {
			_detectiveTalk.gameObject.SetActive (true);
		} else {
			if (Input.GetMouseButtonDown (0)) {
				if (!_detectiveTalk.GetTalkFinishedFlag ()) {
					_detectiveTalk.Talk ();
				} else {
					_detectiveTalk.gameObject.SetActive (false);
					_curtain.Close ();
					_confettiParticles.SetActive (false);
					_spotLight.SetActive (false);
					_gameOverText.SetActive (false);
					_gabeges.SetActive (false);
					_state = State.CONTINUE;
				}
			}
		}
	}


	//--CONTINUE時の処理をする関数
	void ContinueAction() {
		if (!_continuePanel.activeInHierarchy && _curtain.IsStateClose() && _curtain.ResearchStatePlayTime() > 1f) {
			_continuePanel.SetActive (true);
		}
	}


	//--GameClearテキストを表示する関数(コルーチン)
	IEnumerator GameOverTextDisplay() {
		while (!_detectiveAnimManager.IsStateGameOver() || _detectiveAnimManager.ResearchStatePlayTime () < 3.0f / 5) {
			yield return new WaitForSeconds (Time.deltaTime);
		}
		_gameOverText.SetActive (true);
	}
}
