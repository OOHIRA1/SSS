using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//==クライマックスバトルシーンを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class ClimaxBattleManager : MonoBehaviour {
	enum State {
		TRUE_OR_FALSE,	//正誤判定
		INTRODUCTION,	//導入
		WATCH_MOVIE,	//動画鑑賞
		CHOOSE_CHOISES,	//選択
		RESULT,			//対決の結果
		FADE_OUT		//フェードアウト（暗転処理）
	};

	[SerializeField] State _state;
	GameDataManager _gameDataManager;
	[SerializeField] DoubleDoorCurtain _curtain = null;
	[SerializeField] SpriteRenderer _background = null;	//背景画像
	[SerializeField] Sprite _detectiveOffice = null;	//探偵ラボの背景画像
	[SerializeField] Hanamichi _hanamichi = null;
	[SerializeField] Detective _detective = null;
	[SerializeField] RayShooter _rayshooter = null;
	[SerializeField] GameObject _playerLifeUI = null;	//プレイヤーのライフUI
	[SerializeField] GameObject _choises = null;		//選択肢群
	[SerializeField] Vector3 _detectiveMovePos = new Vector3(0, 0, 0);
	[SerializeField] CutinControll[] _cutinControlls = new CutinControll[2];
	bool _cutinFlag;												//カットイン演出をしたかどうかのフラグ
	GameObject _criminal = null;									//選択した犯人
	[SerializeField] Vector3 _npcAppearPos = new Vector3(0, 0, 0);	//選択した犯人の出現する座標
	Butler _butler = null;
	[SerializeField] Vector3 _butlerMovePos = new Vector3(0, 0, 0);	//執事の移動先座標
	[SerializeField] Effect _effectQuestion = null;					//ハテナエフェクト
	bool _questionEffectAppearFlag;									//ハテナエフェクトをしたかどうかのフラグ
	bool _startfallingFlag;													//落ち始めたかどうかのフラグ
	[SerializeField] FalledTrigger _falledTrigger = null;
	[SerializeField] ScenesManager _scenesManager = null;
	[SerializeField] Image _fadeOutPanel = null;
	[SerializeField] float _fadeOutSpeed = 0;	//暗転処理のスピード(alpha/second)
	[SerializeField] GameObject _climaxBattleSystem = null;




	// Use this for initialization
	void Start () {
		_state = State.TRUE_OR_FALSE;
		_gameDataManager = GameObject.FindGameObjectWithTag ("GameDataManager").GetComponent<GameDataManager>();
		_cutinFlag = false;
		//選択した犯人をロードして壇上に生成する-------------------------------------------------------
		_criminal = Resources.Load<GameObject> ( "Characters/" + _gameDataManager.GetCriminal( ) );
		_criminal = Instantiate (_criminal, _npcAppearPos, Quaternion.identity);//生成したGameObjectの参照を渡す
		//-------------------------------------------------------------------------------------------
		_questionEffectAppearFlag = false;
		_startfallingFlag = false;
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
		case State.FADE_OUT:
			FadeOutAction ();
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
		if (_gameDataManager.GetCriminal () == "Butler" && _gameDataManager.GetDangerousWeapon () == "DangerousWepon3") {	//選んだものが正しいかの確認
			_background.sprite = _detectiveOffice;	//背景を探偵ラボに差し替え
			_butler = _criminal.GetComponent<Butler> ();
			_state = State.INTRODUCTION;
		} else {//選択が間違っていたら…
			//ハテナエフェクト・探偵驚き処理-------------------------------------------------------------------------
			if (!_questionEffectAppearFlag && _curtain.IsStateOpen () && _curtain.ResearchStatePlayTime () > 1f) {
				_effectQuestion.gameObject.SetActive (true);
				_questionEffectAppearFlag = true;
				_detective.SetIsAnimShocked (true);
			}
			//------------------------------------------------------------------------------------------------------
			//落下処理----------------------------------------------------------------------------------------------
			if (!_startfallingFlag && _questionEffectAppearFlag && _effectQuestion.ResearchStatePlayTime () > 0.4f) {
				_effectQuestion.gameObject.SetActive (false);
				_detective.Fall ();
				_startfallingFlag = true;
			}
			//------------------------------------------------------------------------------------------------------
			if (_falledTrigger.GetFallFlag ()) {
				_state = State.FADE_OUT;
			}
		}


	}


	//--INTRODUCTIONのステートの時の処理をする関数
	void IntroductionAction(){
		if (_curtain.IsStateOpen () && _curtain.ResearchStatePlayTime () >= 1f) {
			if (!_hanamichi.gameObject.activeInHierarchy) {
				_hanamichi.gameObject.SetActive (true);
			}
			if (_hanamichi.ResearchStatePlayTime () >= 1f) {
				if (!_cutinFlag && !_cutinControlls[0].GetCutinMoveFinishedFlag()) {
					for (int i = 0; i < _cutinControlls.Length; i++) {
						_cutinControlls[i].StartCutinPart2 ();
					}
					_detective.DesignationMove ( _detectiveMovePos );
					_butler.MoveToPos ( _butlerMovePos );
					_cutinFlag = true;
				}
//				if (_detective.GetIsM ()) {
//					_charactors.transform.localScale = new Vector3 (1.1f, 1.1f,0);
//				}
				if (_cutinControlls[0].GetCutinMoveFinishedFlag () && !_detective.GetIsForcedMove() && _cutinFlag) {
					for (int i = 0; i < _cutinControlls.Length; i++) {
						_cutinControlls[i].FinishCutin ();
					}
					_cutinFlag = false;	//フラグをリセット(if文に1回のみ入るようにするため)
				}
				if (_cutinControlls[0].GetFinishFlag ()) {
					_state = State.WATCH_MOVIE;
				}
			}
		}
	}


	//--WATCH_MOVIEのステートの時の処理をする関数
	void WatchMovieAction(){
		_detective.SetIsMove (false);
		_climaxBattleSystem.SetActive (true);
	}


	//--CHOOSE_CHOISESのステートの時の処理をする関数
	void ChooseChoisesAction() {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = _rayshooter.Shoot ( Input.mousePosition );
			if (hit) {
				Debug.Log (hit.collider.name);
			}
		}
	}


	//--RESULTのステートの時の処理をする関数
	void ResultAction() {
		_curtain.Close ();
	}


	//--FADE_OUTのステート時の処理をする関数
	void FadeOutAction() {
		if (_fadeOutPanel.color.a < 1f) {
			Color color = _fadeOutPanel.color;
			_fadeOutPanel.color = new Color (color.r, color.g, color.b, color.a + _fadeOutSpeed * Time.deltaTime);
		} else {
			if (_falledTrigger.GetFalledGameObject () == _detective.gameObject) {
				_scenesManager.ScenesTransition ("GameOver");
			} else {
				_scenesManager.ScenesTransition ("GameClear");
			}
		}
	}
}
