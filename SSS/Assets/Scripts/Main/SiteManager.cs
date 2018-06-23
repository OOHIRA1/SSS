﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//なぜかラボ遷移UIを押しても動かない

public class SiteManager : MonoBehaviour {
	 Vector3 a = new Vector3( 0,0,0 );


	[ SerializeField ] Detective _detective = null;
	[ SerializeField ] MoviePlaySystem _moviePlaySystem = null;
	[ SerializeField ] ScenesManager _scenesManager = null;
	[ SerializeField ] ProgressConditionManager _progressConditionManager = null;
	[ SerializeField ] StoryBoundManeger _storyBoundManeger = null;
    //[ SerializeField ] EffectLibrary _effectLibrary = null;
	[ SerializeField ] SiteMove _siteMove = null;
	//[ SerializeField ] GameObject _ui = null;
	[ SerializeField ] Curtain _cutain = null;
	[ SerializeField ] ClockUI _clockUI = null;
	[ SerializeField ] UnityEngine.UI.Button[] _button = new  UnityEngine.UI.Button[ 1 ];
	[ SerializeField ] GameObject _evidenceFile = null;
	[ SerializeField ] Catcher _catcher = null;
	[ SerializeField ] DetectiveTalk[ ] _detectiveTalk = null;
	[ SerializeField ] RayShooter _camera = null;

	[ SerializeField ] Vector3[ ] _cursorPos = new Vector3[ 1 ];		//注目カーソルのpos

	EvidenceManager _evidenceManager;
    BGMManager _bgmManager;
    GameObject[] _evidenceTrigger;
	GameDataManager _gameDateManager = null;
	GameObject _cursorForAttention;

	int _talkIndex;						//どのトークを表示させるか
	bool _onlyOne;						//一回だけ処理したいとき
	bool _remark;                       //発言したかどうか
    bool _bgm;                          //BGMを鳴らしたかどうか
	bool _pushLaboTransitionUI;         //ラボ遷移UIが押されたかどうか
	bool _talkNow;						//トーク中かどうか


    public static bool _conditions1 = false;     //証拠品４を表示する条件を満たしたかどうか(台詞が全部言ったかどうか)
    public static bool _conditions2 = false;     //証拠品４を表示する条件を満たしたかどうか(台詞が全部言ったかどうか)
    public static bool _endStory = false;       //現場捜査での最後のチェックポイントにいったかどうか
    

	enum PartStatus {
		INVESTIGATION_PART,
		MOVE_PLAY_PART,
		TALK_PART
	}

	PartStatus _status;     //現場のステータス

    enum Text {
        SATISFY_DETECTIVE_FIRST_TALK,
        SATISFY_FIND_POISONED_DISH,
        SATISFY_GET_EVIDENCE1,
        SATISFY_FIRST_CLOSE_EVIDENCE_FILE,
        SATISFY_FIRST_COME_TO_KITCHEN,
        SATISFY_FIRST_COME_TO_SERVING_ROOM,
        SATISFY_FIRST_COME_TO_BACKYARD,
        SATISFY_SHOW_BACKYARD_MOVIE,
        SATISFY_STOP_MOVIE_WHICH_GAEDENAR_ATE_CAKE,
        SATISFY_GET_EVIDENCE2,
        SATISFY_FIRST_COME_TO_KITCHEN_AT_NOON_OR_NIGHT,
        SATISFY_SHOW_BUTLER_PUT_SILVER_BOX,
        SATISFY_SHOW_COOK_PUT_YELLOW_BOX,
        SATISFY_CONDITIONS1_AND_CONDITIONS2,
        SATISFY_GET_EVIDENCE5,
        SATISFY_END_STORY
    }

	//[ SerializeField ] MillioareDieMono _millioareDieMono = null;

	// Use this for initialization
	void Start( ) {
		_gameDateManager = GameObject.FindGameObjectWithTag( "GameDataManager" ).GetComponent< GameDataManager >( );
		_evidenceManager = GameObject.FindGameObjectWithTag ( "EvidenceManager" ).GetComponent< EvidenceManager >( );
        _bgmManager = GameObject.FindGameObjectWithTag ( "BGMManager" ).GetComponent< BGMManager >( );
		_talkIndex = -1;
		_onlyOne = true;
		_remark = false;
		_pushLaboTransitionUI = false;
        _evidenceTrigger = GameObject.FindGameObjectsWithTag( "EvidenceTrigger" );
		_cursorForAttention = GameObject.FindGameObjectWithTag( "CursorForAttention" );
        
		if ( _cursorForAttention != null )	_cursorForAttention.SetActive( false );		//参照を取ったあとに非表示にする
	}

	// Update is called once per frame
	void Update( ) {
        //モノクロアニメーションを見ていてBGMを一回鳴らしてなかったら-----------------------------------------------------
        if ( !_bgm && _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_MILLIONARE_MURDER_ANIM ) ) {
            _bgmManager.UpdateBGM( );
            _bgm = true;
        }
        //----------------------------------------------------------------------------------------------------------------

		switch ( _status ) {
			case PartStatus.INVESTIGATION_PART:
				InvestigationPart( );
				break;

			case PartStatus.MOVE_PLAY_PART:
				MovePlayPart( );
				break;

			case PartStatus.TALK_PART:
				TalkPart( );
				break;
		}

		Story( );
	}

	//調査パート
	void InvestigationPart( ) {

		if ( !_moviePlaySystem.GetStop( ) ) {                                   //動画停止状態になったら 
			_status = PartStatus.MOVE_PLAY_PART;
			return;
		}

		for ( int i = 0; i < _detectiveTalk.Length; i++ ) {

			if ( _detectiveTalk[ i ].gameObject.activeInHierarchy ) {
				_status = PartStatus.TALK_PART;
				return;
			}

		}

		_detective.SetIsMove( true );
		_clockUI.Operation( true );
		_moviePlaySystem.SetOperation( true );
		AllButtonInteractable( true );
		RayShooterEnabled( true );

        SiteMoveNow( );
		RegurateByCurtainState( );
		IsRopeActionAndForcedMove( );
		ClockUIScenesTransitionWithAnim( );
		LaboTransitionUIScenesTransitionWithAnim( );
		if ( !_moviePlaySystem.GetStop( ) ) RopeActionOrForcedMove( );					//ムービーが再生されていたら

	}
	//動画再生パート
	void MovePlayPart( ) {

		if ( _moviePlaySystem.GetStop( ) ) {                                    //動画再生状態になったら
			_status = PartStatus.INVESTIGATION_PART;
			return;
		}

		for ( int i = 0; i < _detectiveTalk.Length; i++ ) {

			if ( _detectiveTalk[ i ].gameObject.activeInHierarchy ) {
				_status = PartStatus.TALK_PART;
				return;
			}

		}

		_detective.SetIsMove( false );
		_clockUI.Operation( true );
		_moviePlaySystem.SetOperation( true );
		AllButtonInteractable( true );
		RayShooterEnabled( true );

        SiteMoveNow( );
		RegurateByCurtainState( );
		IsRopeActionAndForcedMove( );
		ClockUIScenesTransitionWithAnim( );
		LaboTransitionUIScenesTransitionWithAnim( );
		if ( !_moviePlaySystem.GetStop( ) ) RopeActionOrForcedMove( );
	}

	//お話パート
	void TalkPart( ) {

		if ( !_detectiveTalk[ _talkIndex ].gameObject.activeInHierarchy ) {
			_detectiveTalk [ _talkIndex ].gameObject.SetActive( true );
		}

		//トークを進める処理
		if ( Input.GetMouseButtonDown( 0 ) ) {

			_detectiveTalk[ _talkIndex ].Talk( );
		}

		//トークが終わったら
		if ( _detectiveTalk[ _talkIndex ].GetTalkFinishedFlag( ) ) {
			//if ( Input.GetMouseButtonDown( 0 ) ) {
				_detectiveTalk[ _talkIndex ].gameObject.SetActive( false );
                if ( _talkIndex == ( int )Text.SATISFY_SHOW_BUTLER_PUT_SILVER_BOX ) _conditions1 = true;                    //この台詞が終わったらフラグを立てる
                if ( _talkIndex == ( int )Text.SATISFY_SHOW_COOK_PUT_YELLOW_BOX ) _conditions2 = true;                    //この台詞が終わったらフラグを立てる
                _detective.SetIsTalk( false );
				_status = PartStatus.INVESTIGATION_PART;
				_talkNow = false;
                return;
			//}
		}

		_talkNow = true;
		RayShooterEnabled( false );
		Regulation( );
        _detective.SetIsTalk( true );           //探偵をトーク状態にする
	}

	//ストーリに関する処理をまとめる関数--------------------------------------------------------------------------------------------------------------------------------------------------------------
	void  Story( ) {

		if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_MILLIONARE_MURDER_ANIM ) ) {

			if ( _progressConditionManager.ShowMillionareMurderAnimProgress( ) ) {								//モノクロアニメーションを見終わったら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.SHOW_MILLIONARE_MURDER_ANIM );  //チェックポイントを更新する
				_storyBoundManeger.ShowMillionareMurderAnimBound( false );
			} else {
				_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_MILLIONARE_MURDER_ANIM ) &&	//モノクロアニメーションを見終わっていて、初めて探偵の解説が表示されていなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.DETECTIVE_FIRST_TALK )  ) {

			if ( _progressConditionManager.DetectiveFirstTalkProgress ( ) ) {
				_talkIndex = ( int )Text.SATISFY_DETECTIVE_FIRST_TALK;				//最初の解説テキストと近づいてみようテキストまで
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData ( GameDataManager.CheckPoint.DETECTIVE_FIRST_TALK );
				_storyBoundManeger.DetectiveFirstTalkBound ( false );
			} else {
				_storyBoundManeger.DetectiveFirstTalkBound ( true );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.DETECTIVE_FIRST_TALK ) &&	//初めて探偵の解説が表示されていて、毒の食器を発見していなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIND_POISONED_DISH ) ) {

			if (_progressConditionManager.FindPoisonedDishProgress ()) {

				_talkIndex = ( int )Text.SATISFY_FIND_POISONED_DISH;			//近づけたテキストと証拠品タップしてみようテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.FIND_POISONED_DISH );
				_storyBoundManeger.FindPoisonedDishBound( false );
				CursorForAttentionPrint( false );										//カーソル非表示
			} else {
				_storyBoundManeger.FindPoisonedDishBound( true );
				if ( !_talkNow ) CursorForAttentionPrint( true, _cursorPos[ 0 ] );		//注目カーソルを表示される
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIND_POISONED_DISH ) &&	//毒の食器を発見していて、証拠品１（食器）を入手していなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE1 ) ) {

			if ( _progressConditionManager.GetEvidence1Progress( ) ) {
				_talkIndex = ( int )Text.SATISFY_GET_EVIDENCE1;			//証拠品タップできたテキストと証拠品ファイルタップしてみようテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE1 );
				_storyBoundManeger.GetEvidence1Bound( false );
				//CursorForAttentionPrint( false );
			} else {
				_storyBoundManeger.GetEvidence1Bound( true );
				//if ( !_talkNow ) CursorForAttentionPrint( true, _cursorPos[ 1 ] );		//注目カーソルを表示される
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE1 ) &&	//証拠品１（食器）を入手していて、初めて証拠品ファイルをタップしていなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_TAP_EVIDENCE_FILE ) ) {

			if ( _progressConditionManager.FirstTapEvidenceFileProgress( ) ) {
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.FIRST_TAP_EVIDENCE_FILE );
				_storyBoundManeger.FirstTapEvidenceFileBound( false );
			} else {
				_storyBoundManeger.FirstTapEvidenceFileBound( true );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_TAP_EVIDENCE_FILE ) &&	//初めて証拠品ファイルをタップしていて、証拠品ファイルを閉じていなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_CLOSE_EVIDENCE_FILE ) ) {

			if (_progressConditionManager.FirstCloseEvidenceFileProgress ()) {
				_talkIndex = ( int )Text.SATISFY_FIRST_CLOSE_EVIDENCE_FILE;			//説明まとめテキストと検死結果テキストと移動してみようテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData (GameDataManager.CheckPoint.FIRST_CLOSE_EVIDENCE_FILE);
				_storyBoundManeger.FirstCloseEvidenceFileBound( false );
			} else {
				_storyBoundManeger.FirstCloseEvidenceFileBound( true );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_CLOSE_EVIDENCE_FILE ) &&	//証拠品ファイルを閉じていて、キッチンに移動していなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_KITCHEN ) ) {
			//庭に現場を移動できないようにする
			if (_progressConditionManager.FirstComeToKitchenProgress ()) {
				_talkIndex = ( int )Text.SATISFY_FIRST_COME_TO_KITCHEN;			//厨房きたよテキストとシークバー説明テキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData (GameDataManager.CheckPoint.FIRST_COME_TO_KITCHEN);
				_storyBoundManeger.FirstComeToKitchenBound( false );
			} else {
				_storyBoundManeger.FirstComeToKitchenBound( true );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_KITCHEN ) &&	//キッチンに移動していて、給仕室に移動していなっかたら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_SERVING_ROOM ) ) {
			//シークバー解禁
			if ( _progressConditionManager.FirstComeToServingRoomProgress( ) ) {
				_talkIndex = ( int )Text.SATISFY_FIRST_COME_TO_SERVING_ROOM;			//給仕室きたよテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData (GameDataManager.CheckPoint.FIRST_COME_TO_SERVING_ROOM);
				_storyBoundManeger.FirstComeToServingRoomBound( false );
			} else {
				_storyBoundManeger.FirstComeToServingRoomBound( true );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_SERVING_ROOM ) &&	//給仕室に移動していて、庭に移動していなっかたら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_BACKYARD ) ) {	

			if (_progressConditionManager.FirstComeToBackyardProgress( ) ) {
				_talkIndex = ( int )Text.SATISFY_FIRST_COME_TO_BACKYARD;			//庭きたよテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_BACKYARD );
				_storyBoundManeger.FirstComeToBackyardBound( false );
				_moviePlaySystem.MovieReset( );							//ここだけ動画をリセットする
			} else {
				_storyBoundManeger.FirstComeToBackyardBound( true );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_BACKYARD ) &&	//庭に移動していて、庭（夜）の動画をみていなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_BACKYARD_MOVIE ) ) {	

			if ( _progressConditionManager.ShowBackYardMovieProgress( ) ) {	
				_talkIndex = ( int )Text.SATISFY_SHOW_BACKYARD_MOVIE;			//動画最後まで見たテキストとそこで止めてみようテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData (GameDataManager.CheckPoint.SHOW_BACKYARD_MOVIE);
				_storyBoundManeger.ShowBackYardMovieBound( false );
			} else {
				_storyBoundManeger.ShowBackYardMovieBound( true );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_BACKYARD_MOVIE ) &&	//庭（夜）の動画を見ていて、決定的瞬間で一時停止していなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.STOP_MOVIE_WHICH_GAEDENAR_ATE_CAKE ) ) {	

			if ( _progressConditionManager.StopMovieWhichGaedenarAteCakeProgress( ) ) {	//クリックとボタンの操作を制限するかも
				_talkIndex = ( int )Text.SATISFY_STOP_MOVIE_WHICH_GAEDENAR_ATE_CAKE;			//そこに行ってみようテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData (GameDataManager.CheckPoint.STOP_MOVIE_WHICH_GAEDENAR_ATE_CAKE);
				_storyBoundManeger.StopMovieWhichGaedenarAteCakeBound( false );
			} else {
				_storyBoundManeger.StopMovieWhichGaedenarAteCakeBound( true );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.STOP_MOVIE_WHICH_GAEDENAR_ATE_CAKE ) &&	//決定的瞬間で一時停止していて、証拠品２を入手していなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE2 ) ) {	

			if ( _progressConditionManager.GetEvidence2Progress( ) ) {	
				_talkIndex = ( int )Text.SATISFY_GET_EVIDENCE2;			//矛盾あったねテキストとラボに行ってみようテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE2 );
				_storyBoundManeger.GetEvidence2Bound( false );
			} else {
				_storyBoundManeger.GetEvidence2Bound( true );
			}

		}

		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE2 ) &&	
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_DETECTIVE_OFFICE ) ) {	

			_storyBoundManeger.FirstComeToDetectiveOfficeBound( );

		}

		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE3 ) &&
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_KITCHEN_AT_NOON_OR_NIGHT ) ) {

			if ( _progressConditionManager.FirstComeToKitchenAtNoonOrNightProgress( ) ) {
				_talkIndex = ( int )Text.SATISFY_FIRST_COME_TO_KITCHEN_AT_NOON_OR_NIGHT;		//初めて違う時間帯に遷移したテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_KITCHEN_AT_NOON_OR_NIGHT );
			}                                                                                                                   //昼か夕方の厨房に移動しかできなくて移動したら条件を満たすため縛りがいらない

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_KITCHEN_AT_NOON_OR_NIGHT ) &&	//証拠品３を入手していて、執事が箱をしまったところで停止してなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_BUTLER_PUT_SILVER_BOX ) ) {	

			if ( _progressConditionManager.ShowButlerPutSilverBoxProgress( ) ) {	
				_talkIndex = ( int )Text.SATISFY_SHOW_BUTLER_PUT_SILVER_BOX;			//執事が箱しまってたテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.SHOW_BUTLER_PUT_SILVER_BOX );
				_storyBoundManeger.SilverAndYellowBoxBound( false );
			} else {
				_storyBoundManeger.SilverAndYellowBoxBound( true );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_KITCHEN_AT_NOON_OR_NIGHT ) &&	//証拠品３を入手していて、料理長が箱をしまったところで停止してなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_COOK_PUT_YELLOW_BOX ) ) {

			if ( _progressConditionManager.ShowCookPutYellowBoxProgress( ) ) {
				_talkIndex = ( int )Text.SATISFY_SHOW_COOK_PUT_YELLOW_BOX;            //料理長が箱しまってたテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.SHOW_COOK_PUT_YELLOW_BOX );
				_storyBoundManeger.SilverAndYellowBoxBound( false );
			} else {
				_storyBoundManeger.SilverAndYellowBoxBound( true );
			}

		}


		if ( ( _conditions1 && _conditions2 ) &&	
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE4 ) ) {		//箱を見たときの台詞が二つとも終わっていて、証拠品４を入手していなかったら

			if ( !_remark ) {               //一回発言したら処理しない
				_talkIndex = ( int )Text.SATISFY_CONDITIONS1_AND_CONDITIONS2;            //なんか気付いたよテキスト  //ここだけ会話の特別処理
				_status = PartStatus.TALK_PART;
				_remark = true;
			}

			if ( _progressConditionManager.GetEvidence4Progress( ) ) {	
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE4 );
				_storyBoundManeger.GetEvidence4Bound( false );
			} else {
				_storyBoundManeger.GetEvidence4Bound( true );
			}

		}



		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE4 ) &&	//証拠品４を入手していて、証拠品５を入手していなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE5 ) ) {	

			if ( _progressConditionManager.GetEvidence5Progress( ) ) {
				_talkIndex = ( int )Text.SATISFY_GET_EVIDENCE5;			//なんかわかったテキストと次が最後テキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE5 );
				_storyBoundManeger.GetEvidence5Bound( false );
			} else {
				_storyBoundManeger.GetEvidence5Bound( true );
			}

		}

			
		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE5 ) &&	!_endStory ) {		//証拠品５を入手してendStoryがtrueじゃなかったら	

			if ( _progressConditionManager.GetEvidence6Progress( )  ) {
				_talkIndex = ( int )Text.SATISFY_END_STORY;			//全貌見えてきたテキスト
				_status = PartStatus.TALK_PART;
				_endStory = true;
			}
				_storyBoundManeger.GetEvidence6Bound( true );
		}

		if ( _endStory ) {
			_storyBoundManeger.LastBound( );
		}



		if ( _scenesManager.GetNowScenes( ) == "SiteNight" && SiteMove._nowSiteNum == 0 ) {		//夜の寝室だったら
			_moviePlaySystem.SetFixed( true );
			_moviePlaySystem.SetMouseMove( false );
			_button[ ( int )StoryBoundManeger.ButtonNum.START_AND_STOP_BUTTON ].interactable = false;
		} else {
			_moviePlaySystem.SetFixed( false );
			_moviePlaySystem.SetMouseMove( true );
		}
	}
	//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


	//操作をいろいろ制限する------------------------------------------------------------
	void Regulation( ) {
		_evidenceFile.SetActive( false );			//証拠品ファイルを非表示
		_detective.SetIsMove( false );				//探偵を動けないようにする
		_clockUI.Operation( false );				//時計ＵＩを操作不可にする
		_moviePlaySystem.SetOperation( false );		//動画(シークバー)を操作不可にする
		AllButtonInteractable( false );				//ボタンを押せないようにする
	}
	//-----------------------------------------------------------------------------------

	//押されたものが時計ＵＩの昼か夕方か夜だったらする処理------------------------------------------------------------------------------------------
	void ClockUIScenesTransitionWithAnim( ) {

		if ( _clockUI.GetPushed( ) != "none"  ) {       //時計UIのいずれかの時間帯がタッチされたら       
			Regulation( );

			//遷移する前に初期位置に戻す処理---------------------------------------------------------
			RopeActionOrForcedMove( );

			if ( _onlyOne &&  !( _catcher.GetIsCatch( ) || _detective.GetIsForcedMove( ) ) ) {								//毎フレームだと間に合わないのかカーテンがOpenのステートのときかつアニメーションが終わっている時にしても複数呼ばれてしまう	//強制移動かロープアクションをしていなかったら
				_cutain.Close( );							//カーテンを閉める
				_onlyOne = false;
			}
			//----------------------------------------------------------------------------------

			if ( _cutain.IsStateClose( ) && _cutain.ResearchStatePlayTime( ) >= 1f )    //カーテンが閉まりきったらシーン遷移する
				_scenesManager.SiteScenesTransition( _clockUI.GetPushed( ) );		

		}

	}
	//-------------------------------------------------------------------------------------------------------------------------------------------


	//ラボ遷移UIが押されたらする処理-----------------------------------------------------------------
	void LaboTransitionUIScenesTransitionWithAnim( ) {
		if ( _pushLaboTransitionUI ) {
			Regulation( );

			//遷移する前に初期位置に戻す処理---------------------------------------------------------
			RopeActionOrForcedMove( );

			if ( _onlyOne &&  !( _catcher.GetIsCatch( ) || _detective.GetIsForcedMove( ) ) ) {								//毎フレームだと間に合わないのかカーテンがOpenのステートのときかつアニメーションが終わっている時にしても複数呼ばれてしまう	//強制移動かロープアクションをしていなかったら
				_cutain.Close( );							//カーテンを閉める
				_onlyOne = false;
			}
			//----------------------------------------------------------------------------------

			if ( _cutain.IsStateClose( ) && _cutain.ResearchStatePlayTime( ) >= 1f )    //カーテンが閉まりきったらシーン遷移する
				_scenesManager.ScenesTransition( "DetectiveOffice" );

		}
	}
	//-------------------------------------------------------------------------------------------------


	//カーテンがアニメーションをしてたときの処理-------------------------------
	void RegurateByCurtainState( ) {
		if ( _cutain.ResearchStatePlayTime( ) < 1f ) {		//カーテンが動いていたら
			Regulation( );
		}
	}
	//-------------------------------------------------------------------------

	//すべてのボタンを操作不能にする------------------------------
	void AllButtonInteractable( bool inter ) {
		for (int i = 0; i < _button.Length; i++) {
			_button [i].interactable = inter;
		}
	}
	//------------------------------------------------------------


	//ロープアクションか強制移動していたときの処理------------------------------------------
	void IsRopeActionAndForcedMove( ) {
		if ( _catcher.GetIsCatch( ) || _detective.GetIsForcedMove( ) ) {     //ロープアクションか強制移動していたら
			Regulation( );
		}
	}
	//----------------------------------------------------------------------------------------


	//初期地にいなかったらロープアクションをするか強制移動にするかの判定---
	void RopeActionOrForcedMove( ) {
		if (  !_detective.GetCheckPos( ) ) {   								 //動画が再生されていて探偵が初期値にいなかったら
			if ( _detective.transform.position.x < 0 ) {						//探偵が指定位置より左側にいたら歩いて戻る。右側にいたらロープアクションで戻る

				if ( !_catcher.GetIsCatch( ) ) {									//探偵がロープアクションをしていなかったら(毎フレーム呼んでいるためこの処理がないと境目を越えたときにバグる)
					_detective.DesignationMove( _detective.GetInitialPos( ) );
				}

			} else {
				_catcher.ToRopeAction( );
			}
		}
	}
	//-------------------------------------------------------------------------------------------


	//証拠品押せなくなる関数----------------------------------------------------
	void RayShooterEnabled( bool value ) { _camera.SetRayShootable( value ); }
	//----------------------------------------------------------------------------------------------

    //現場が動いていたら処理---------------------
    void SiteMoveNow( ) {
        if ( _siteMove.GetMoveNow( ) ) {
            EvidenceTriggerDisplay( false );
			Regulation( );
        } else {
            EvidenceTriggerDisplay( true );
        }
    }
    //--------------------------------------------

    //EvideneTriggerのコライダーを外す処理---------------------------------------------------------
    void EvidenceTriggerDisplay( bool value ) {
        for ( int i = 0; i < _evidenceTrigger.Length; i++ ) {
            BoxCollider2D boxCollider = _evidenceTrigger[ i ].GetComponent< BoxCollider2D >( );
            boxCollider.enabled = value;
        }
    }
    //----------------------------------------------------------------------------------------------

	//注目カーソルを表示・非表示する----------------------------------------------------------------------
	void CursorForAttentionPrint( bool active, Vector3? pos = null ) {	//?をつけるとVectorにnullでデフォルト引数をいれられる
		if ( _cursorForAttention == null ) return;

		if ( active ) {																				
			//表示するのであれば---------------------------------------------------------------------
			if ( pos == null ) pos = Vector3.zero;					//引数がnullだったら０を入れる			
			if ( !_cursorForAttention.activeInHierarchy ) {			//非表示状態だったら
				_cursorForAttention.SetActive( true );
				_cursorForAttention.transform.position = ( Vector3 )pos;
			}
			//-------------------------------------------------------------------------------------
		} else {													
			//非表示するのであれば-----------------------------------------------------------------
			if ( _cursorForAttention.activeInHierarchy ) _cursorForAttention.SetActive( false );	//表示状態だったら
			//-------------------------------------------------------------------------------------
		}

	}
	//---------------------------------------------------------------------------------------------------

	//ラボ遷移UIが押されたらフラグを立てる関数--
	public void LaboTransitionUIButton( ) {
		_pushLaboTransitionUI = true;
	}
	//------------------------------------------

	//----------------------------------------------------------------------

	//ムービーが再生状態だったらＵＩを非表示-------
	/*void MoviState( ) {
		if ( !_movePlaySystem.GetStop( ) ) {
			_ui[ 1 ].SetActive( false );	//三角ＵＩ
			_ui[ 2 ].SetActive( false );	//ラボ遷移ＵＩ
		} else {
			_ui[ 1 ].SetActive( true );
			_ui[ 2 ].SetActive( true );
		}
	}*/
	//---------------------------------------------


	public void  aaaa () {
		_evidenceManager.UpdateEvidence( EvidenceManager.Evidence.STORY1_EVIDENCE2 );
	}

	public void bbbb( ) {
		_evidenceManager.UpdateEvidence( EvidenceManager.Evidence.STORY1_EVIDENCE6 );
	}

}

