using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==クライマックスバトルシーンを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class ClimaxBattleManager : MonoBehaviour {
	enum State {
		TRUE_OR_FALSE,	//正誤判定
		INTRODUCTION,	//導入
		WATCH_MOVIE,	//動画鑑賞
		CHOOSE_CHOISES,	//選択
		RESULT			//対決の結果
	};

	[SerializeField] State _state;
	GameDataManager _gameDataManager;
	[SerializeField] Curtain _curtain = null;
	[SerializeField] SpriteRenderer _background = null;	//背景画像
	[SerializeField] RayShooter _rayshooter = null;
	[SerializeField] GameObject _playerLifeUI = null;	//プレイヤーのライフUI
	[SerializeField] GameObject _choises = null;		//選択肢群

	// Use this for initialization
	void Start () {
		_state = State.TRUE_OR_FALSE;
		_gameDataManager = GameObject.FindGameObjectWithTag ("GameDataManager").GetComponent<GameDataManager>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (_state) {
		case State.TRUE_OR_FALSE:
			TrueOrFalseAction ();
			break;
		case State.INTRODUCTION:
			IntroductionAction ();
			break;
		case State.WATCH_MOVIE:
			WatchMovieAction ();
			break;
		case State.CHOOSE_CHOISES:
			ChooseChoisesAction ();
			break;
		case State.RESULT:
			ResultAction ();
			break;
		default:
			break;
		}
		Debug.Log (_state);

		ChangeActive ( _playerLifeUI, State.WATCH_MOVIE, false, _state == State.CHOOSE_CHOISES );
		ChangeActive ( _choises, State.CHOOSE_CHOISES );
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



	//--TRUE_OR_FALSEのステートの時の処理をする関数
	void TrueOrFalseAction(){
		_gameDataManager.GetCriminal ();
		_state = State.INTRODUCTION;
	}


	//--INTRODUCTIONのステートの時の処理をする関数
	void IntroductionAction(){
		
	}


	//--WATCH_MOVIEのステートの時の処理をする関数
	void WatchMovieAction(){
		
	}


	//--CHOOSE_CHOISESのステートの時の処理をする関数
	void ChooseChoisesAction(){
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = _rayshooter.Shoot ( Input.mousePosition );
			if (hit) {
				Debug.Log (hit.collider.name);
			}
		}
	}


	//--RESULTのステートの時の処理をする関数
	void ResultAction(){
		_curtain.Close ();
	}
}
