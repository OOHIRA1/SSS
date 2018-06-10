using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiteManager : MonoBehaviour {
	GameDataManager _gameDateManager = null;

    [ SerializeField ] Detective _detective = null;
    [ SerializeField ] MoviePlaySystem _moviePlaySystem = null;
    [ SerializeField ] ScenesManager _scenesManager = null;
	[ SerializeField ] ProgressConditionManager _progressConditionManager = null;
	[ SerializeField ] StoryBoundManeger _storyBoundManeger = null;
	EvidenceManager _evidenceManager;
	[ SerializeField ] SiteMove _siteMove = null;
    //[ SerializeField ] GameObject _ui = null;
	[ SerializeField ] Curtain _cutain = null;
    [ SerializeField ] ClockUI _clockUI = null;
	[ SerializeField ] UnityEngine.UI.Button[] _button = new  UnityEngine.UI.Button[ 1 ];
	[ SerializeField ] GameObject _evidenceFile = null;
    [ SerializeField ] Catcher _catcher = null;
	[ SerializeField ] DetectiveTalk[ ] _detectiveTalk = null;

	int _talkIndex;						//どのトークを表示させるか
	bool _onlyOne;						//一回だけ処理したいとき

    enum PartStatus {
        INVESTIGATION_PART,
        MOVE_PLAY_PART,
        TALK_PART
    }

    PartStatus _status;     //現場のステータス

    //[ SerializeField ] MillioareDieMono _millioareDieMono = null;

    // Use this for initialization
    void Start( ) {
		//GameObject[] gameDataManager = GameObject.FindGameObjectsWithTag ("GameDataManager");
		_gameDateManager = GameObject.FindGameObjectWithTag( "GameDataManager" ).GetComponent< GameDataManager >( );
		_evidenceManager = GameObject.FindGameObjectWithTag ( "EvidenceManager" ).GetComponent< EvidenceManager > ( );
		_talkIndex = -1;
		_onlyOne = true;


	}
	
	// Update is called once per frame
	void Update( ) {

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
                                                                   
        StoryBound( );
       
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
        RegurateByCurtainState( );
        IsRopeActionAndForcedMove( );
        ScenesTransitionWithAnim( );

       
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
        RegurateByCurtainState( );
        IsRopeActionAndForcedMove( );
        ScenesTransitionWithAnim( );
    }

    //お話パート
    void TalkPart( ) {
		
		if (!_detectiveTalk [_talkIndex].gameObject.activeInHierarchy) {
			_detectiveTalk [_talkIndex].gameObject.SetActive (true);
		}

		//トークを進める処理
		if ( Input.GetMouseButtonDown( 0 ) ) {
			
			_detectiveTalk[ _talkIndex ].Talk( );
		}

		//トークを消す
		if ( _detectiveTalk[ _talkIndex ].GetTalkFinishedFlag() ) {
			if (Input.GetMouseButtonDown (0)) {
				_detectiveTalk [_talkIndex].gameObject.SetActive (false);
				_status = PartStatus.INVESTIGATION_PART;
			}
		}



		Regulation( );
    }

    //ストーリの進行状況によって操作に縛りをかける関数
    void  StoryBound( ) {

		bool[,] site = {							//どの部屋を閉じる状態にするかの配列。false：閉じない true：閉じる
			{ false, false, false, true },			//右から寝室、キッチン、給仕室、庭。（部屋番号に対応）
			{ false, false, false, false },			//上から昼、夕方、夜。
			{ false, false, false, false }
		};

		if ( _siteMove.GetCheckTiming( ) ) {
			_storyBoundManeger.CutainCloseBound( site );
		}
			
		//どこかに順番にではなく、すぐに満たしてしまう条件があるかも
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
				_talkIndex = 0;				//最初の解説テキストと近づいてみようテキストまで
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
				
				_talkIndex = 1;			//近づけたテキストと証拠品タップしてみようテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.FIND_POISONED_DISH );
				_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				_storyBoundManeger.FindPoisonedDishBound( true );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIND_POISONED_DISH ) &&	//毒の食器を発見していて、証拠品１（食器）を入手していなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE1 ) ) {

			if ( _progressConditionManager.GetEvidence1Progress( ) ) {
				_talkIndex = 2;			//証拠品タップできたテキストと証拠品ファイルタップしてみようテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE1 );
				_storyBoundManeger.GetEvidence1Bound( false );
			} else {
				_storyBoundManeger.GetEvidence1Bound( true );
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
				_talkIndex = 3;			//説明まとめテキストと検死結果テキストと移動してみようテキスト
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
				_talkIndex = 4;			//厨房きたよテキストとシークバー説明テキスト
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
				_talkIndex = 5;			//給仕室きたよテキスト
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
				_talkIndex = 6;			//庭きたよテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_BACKYARD );
				_storyBoundManeger.FirstComeToBackyardBound( false );
			} else {
				_storyBoundManeger.FirstComeToBackyardBound( true );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_BACKYARD ) &&	//庭に移動していて、庭（夜）の動画をみていなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_BACKYARD_MOVIE ) ) {	

			if ( _progressConditionManager.ShowBackYardMovieProgress( ) ) {	//クリックとボタンの操作を制限するかも
				_talkIndex = 7;			//動画最後まで見たテキストとそこで止めてみようテキスト
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
				_talkIndex = 8;			//そこに行ってみようテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData (GameDataManager.CheckPoint.STOP_MOVIE_WHICH_GAEDENAR_ATE_CAKE);
				_storyBoundManeger.StopMovieWhichGaedenarAteCakeBound( false );
			} else {
				_storyBoundManeger.StopMovieWhichGaedenarAteCakeBound( true );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.STOP_MOVIE_WHICH_GAEDENAR_ATE_CAKE ) &&	//決定的瞬間で一時停止していて、証拠品２を入手していなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE2 ) ) {	

			if ( /*証拠品２を入手していたら*/false ) {	
				//_talkIndex = 8;			//矛盾あったねテキストとラボに行ってみようテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE2 );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE3 ) &&	//証拠品３を入手していて、執事が箱をしまった
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_BUTLER_PUT_SILVER_BOX ) ) {	

			if ( /*その時に冷蔵庫のまえで止まったら*/false ) {	
				//_talkIndex = 9;			//執事が箱しまってたテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.SHOW_BUTLER_PUT_SILVER_BOX );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE3 ) &&	//証拠品３を入手していて、料理長が箱をしまった
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_COOK_PUT_YELLOW_BOX ) ) {	

			if ( /*その時に冷蔵庫のまえで止まったら*/false ) {	
				//_talkIndex = 10;			//料理長が箱しまってたテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.SHOW_COOK_PUT_YELLOW_BOX );
			}

		}


		if ( ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_BUTLER_PUT_SILVER_BOX ) && _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_COOK_PUT_YELLOW_BOX ) ) &&	
			  !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE4 ) ) {		//二つの箱を見ていて、証拠品４を入手していなかったら	

			if ( /*証拠品４を入手したら*/false ) {	
				//_talkIndex = 11;			//なんか気付いたよテキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE4 );
			}

		}



		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE4 ) &&	//証拠品４を入手していて、証拠品５を入手していなかったら
			!_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE5 ) ) {	

			if ( /*証拠品５を入手したら*/false ) {
				//_talkIndex = 12;			//なんかわかったテキストと次が最後テキスト
				_status = PartStatus.TALK_PART;
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE5 );
			}

		}


		if ( _gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE5 ) &&	//証拠品５を入手していて、証拠品６を入手していなかったら
			!_evidenceManager.CheckEvidence( EvidenceManager.Evidence.STORY1_EVIDENCE6 ) ) {	

			if ( /*証拠品６を入手したら*/false ) {
				//_talkIndex = 13;			//全貌見えてきたテキスト
				_status = PartStatus.TALK_PART;
			}

		}

















		if ( _scenesManager.GetNowScenes( ) == "SiteNight" && SiteMove._nowSiteNum == 0 ) {		//夜の寝室だったら
			_moviePlaySystem.SetFixed( true );
		} else {
			_moviePlaySystem.SetFixed( false );
		}


        //縛り処理----------------------------------------------------------------------------------------------------------------------------------------------------------


		/*if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_MILLIONARE_MURDER_ANIM ) ) {	//モノクロアニメーションを見ていなかったら
			if ( _progressConditionManager.ShowMillionareMurderAnimProgress( ) ) {										//モノクロアニメーションを見終わったら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.SHOW_MILLIONARE_MURDER_ANIM );  //チェックポイントを更新する
				_storyBoundManeger.ShowMillionareMurderAnimBound( false );
			} else {
				_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}
		}*/ 



		//if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIND_POISONED_DISH ) ) {	//毒の付いた皿を発見してなかったら
		//	if ( _progressConditionManager.FindPoisonedDishProgress( ) ) {						//毒のついた皿を発見したら
		//		//_evidenceManager.UpdateEvidence( EvidenceManager.Evidence.STORY1_EVIDENCE1 );	//証拠品１を格納する
		//		_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.FIND_POISONED_DISH );  //チェックポイントを更新する
		//		_storyBoundManeger.FindPoisonedDishBound( false );
		//	} else {
		//		if ( true ) {											//テキストが存在していたのなら探偵も操作不能にする
		//			_storyBoundManeger.FindPoisonedDishBound( true, true );
		//			//代わりにマウスを押してテキストを進める処理をする
		//		} else {
		//			_storyBoundManeger.FindPoisonedDishBound( true );
		//		}
		//	}
		//}


        //------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }



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
    void ScenesTransitionWithAnim( ) {

		if ( _clockUI.GetPushed( ) != "none"  ) {       //時計UIのいずれかの時間帯がタッチされたら       
			Regulation( );

			if ( _onlyOne ) {									//毎フレームだと間に合わないのかカーテンがOpenのステートのときかつアニメーションが終わっている時にしても複数呼ばれてしまう
            	_cutain.Close( );							//カーテンを閉める
				_onlyOne = false;
			}

            if ( _cutain.IsStateClose( ) && _cutain.ResearchStatePlayTime( ) >= 1f )    //カーテンが閉まりきったらシーン遷移する
                _scenesManager.SiteScenesTransition( _clockUI.GetPushed( ) );		

		}

    }
    //-------------------------------------------------------------------------------------------------------------------------------------------

    

	//カーテンがアニメーションをしてたときの処理-------------------------------
	void RegurateByCurtainState( ) {
		if ( _cutain.ResearchStatePlayTime( ) < 1f ) {		//カーテンが動いていたら
			//Regulation( );
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


     //再生＆一時停止ボタンを押したらロープアクションをするか強制移動にするかの判定---
    public void RopeAction( ) {
		if ( !_moviePlaySystem.GetStop( ) && !_detective.GetCheckPos( ) ) {    //動画が再生されていて探偵が初期値にいなかったら
			if ( _detective.transform.position.x < 0 ) {						//探偵が指定位置より左側にいたら歩いて戻る。右側にいたらロープアクションで戻る
				_detective.DesignationMove( _detective.GetInitialPos( ) );		
			} else {
				_catcher.ToRopeAction( );
			}
		}
	}
    //----------------------------------------------------------------------

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


}

