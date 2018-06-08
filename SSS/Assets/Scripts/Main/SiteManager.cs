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
	//[ SerializeField ] EvidenceManager _evidenceManager = null;
	//[ SerializeField ] SiteMove _siteMove = null;
    //[ SerializeField ] GameObject _ui = null;
	[ SerializeField ] Curtain _cutain = null;
    [ SerializeField ] ClockUI _clockUI = null;
	[ SerializeField ] UnityEngine.UI.Button[] _button = new  UnityEngine.UI.Button[ 1 ];
	[ SerializeField ] GameObject _evidenceFile = null;
    [ SerializeField ] Catcher _catcher = null;                                         

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

		/*if ( テキストが存在していたら ) {
			_status = PartStatus.TALK_PART;
			return;
		}*/

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

		/*if ( テキストが存在していたら ) {
			_status = PartStatus.TALK_PART;
			return;
		}*/

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
		/*if ( テキストが存在していなかったら ) {
			_status = PartStatus.INVESTIGATION_PART;
			return;
		}*/

		if ( Input.GetMouseButtonDown( 0 ) ) {
			//トークを進める処理
		}

		/*if ( //トークが最後までいっていたら ) {
				//トークを消す
		}*/

		Regulation( );
    }

    //ストーリの進行状況によって操作に縛りをかける関数
    void  StoryBound( ) {

		bool[,] site = {							//どの部屋を閉じる状態にするかの配列。false：閉じない true：閉じる
			{ false, false, false, false },			//右から寝室、キッチン、給仕室、庭。（部屋番号に対応）
			{ false, false, false, false },			//上から昼、夕方、夜。
			{ false, false, false, false }
		};


		//_storyBoundManeger.CutainCloseBound( site );

		if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_MILLIONARE_MURDER_ANIM ) ) {	//モノクロアニメーションを見ていなかったら
			
			if ( _progressConditionManager.ShowMillionareMurderAnimProgress( ) ) {										//モノクロアニメーションを見終わったら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.SHOW_MILLIONARE_MURDER_ANIM );  //チェックポイントを更新する
				//_storyBoundManeger.ShowMillionareMurderAnimBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIND_POISONED_DISH ) ) {
			
			if ( _progressConditionManager.FindPoisonedDishProgress( ) ) {						//毒のついた皿を発見したら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.FIND_POISONED_DISH );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.FindPoisonedDishBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE1 ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//証拠品1を入手したら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE1 );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}
				
		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_TAP_EVIDENCE_FILE ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//証拠品ファイルを初めてタップしたら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.FIRST_TAP_EVIDENCE_FILE );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_CLOSE_EVIDENCE_FILE ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//証拠品ファイルを初めて閉じたら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.FIRST_CLOSE_EVIDENCE_FILE );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_KITCHEN ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//初めて厨房に来たら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_KITCHEN );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_SERVING_ROOM ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//初めて給仕室に来たら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_SERVING_ROOM );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_BACKYARD ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//初めて裏庭に来たら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_BACKYARD );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.SHOW_BACKYARD_MOVIE ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//裏庭(夜)の動画を見たら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.SHOW_BACKYARD_MOVIE );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.STOP_MOVIE_WHICH_GAEDENAR_ATE_CAKE ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//庭師がケーキを食べた瞬間でストップしたら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.STOP_MOVIE_WHICH_GAEDENAR_ATE_CAKE );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE2 ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//証拠品2を入手したら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE2 );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_DETECTIVE_OFFICE ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//初めて探偵ラボに来たら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.FIRST_COME_TO_DETECTIVE_OFFICE );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE3 ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//証拠品3を入手したら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE3 );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE4 ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//証拠品4を入手したら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE4 );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE5 ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//証拠品5を入手したら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE5 );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE6 ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//証拠品6を入手したら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.GET_EVIDENCE6 );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		} else if ( !_gameDateManager.CheckAdvancedData( GameDataManager.CheckPoint.COME_TO_DETECTIVE_OFFICE_WITH_ALL_EVIDENCE ) ) {

			if ( /*_progressConditionManager.FindPoisonedDishProgress( )*/true ) {						//証拠品を全て揃えて探偵ラボに来たら
				_gameDateManager.UpdateAdvancedData( GameDataManager.CheckPoint.COME_TO_DETECTIVE_OFFICE_WITH_ALL_EVIDENCE );  //チェックポイントを更新する
				//_storyBoundManeger.FindPoisonedDishBound( false );
			} else {
				//_storyBoundManeger.ShowMillionareMurderAnimBound( true );
			}

		}


		if ( _scenesManager.GetNowScenes( ) == "SiteNight" && SiteMove._nowSiteNum == 0 ) {		//夜の寝室だったら
			//_moviePlaySystem.SetFixed( true );
		} else {
			//_moviePlaySystem.SetFixed( false );
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
            _cutain.Close( );							//カーテンを閉める

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

