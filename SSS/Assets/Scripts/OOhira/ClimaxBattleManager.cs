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
		
	}


	//--RESULTのステートの時の処理をする関数
	void ResultAction(){
		_curtain.Close ();
	}
}
