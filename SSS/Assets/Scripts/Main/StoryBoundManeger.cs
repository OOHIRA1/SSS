using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoundManeger : MonoBehaviour {
    //[ SerializeField ] GameDataManager _gameDateManager = null;
    [ SerializeField ] Detective _detective = null;
    [ SerializeField ] MoviePlaySystem _moviePlaySystem = null;
    [ SerializeField ] ScenesManager _scenesManager = null;
	[ SerializeField ] Curtain _cutain = null;
    [ SerializeField ] ClockUI _clockUI = null;
	[ SerializeField ] UnityEngine.UI.Button[] _button = new  UnityEngine.UI.Button[ 1 ];
	//[ SerializeField ] GameObject _evidenceFile = null;
	[ SerializeField ] GameObject _ui = null;
	//[ SerializeField ] MillioareDieMono _millioareDieMono = null;
	[ SerializeField ] SiteMove _siteMove = null;

    GameObject _evidenceTrigger2;
	GameObject _evidenceTrigger4;

	public enum ButtonNum {
		START_AND_STOP_BUTTON,
		FF_BUTTON,
		FB_BUTTON,
		LABO_TRANSITION_UI,
		TIRANGLE_LEFT,
		TIRANGLE_RIGHT,
		EVIDENCE_UI,
        MAP_UI
	};

	bool _onlyOnce;					//最初の一回だけ処理したいとき

	// Use this for initialization
	void Start( ) {
		_onlyOnce = true;
        _evidenceTrigger2 = GameObject.Find( "EvidenceTrigger2" );
		_evidenceTrigger4 = GameObject.Find( "EvidenceTrigger4" );
	}
	
	// Update is called once per frame
	void Update( ) {
	}

	public void ShowMillionareMurderAnimBound( bool application ) {
		if ( application ) {
			_ui.SetActive( false );
			_clockUI.gameObject.SetActive( false );
			_detective.transform.position = new Vector3( -18, _detective.transform.position.y, 0 );
		} else {
			_ui.SetActive( true );
			_clockUI.gameObject.SetActive( true );
			_detective.DesignationMove( _detective.GetInitialPos( ) );			//初期位置に移動させる
		}
	}


	public void DetectiveFirstTalkBound( bool application ) {
		bool able = true;

		if ( application ) {
			able = false;
		} else {
			able = true;
		}

		
		_ui.SetActive( able );
		_clockUI.gameObject.SetActive( able );
		_moviePlaySystem.SetMouseMove( able );
		_moviePlaySystem.SetFixed( application );
		
	}

	public void FindPoisonedDishBound( bool application ) {
		bool able = true;

		if ( application ) {		//適用するのであれば利用できなくする
			able = false;
		} else {					//適用しないのであれば利用できる様にする
			able = true;
		}


		//for (int i = 0; i < _button.Length; i++) {
			//_button[ i ].interactable = able;
		//}

		_clockUI.gameObject.SetActive( able );
		_moviePlaySystem.SetMouseMove( able );
		//_moviePlaySystem.SetFixed( application );
		for ( int i = 0; i < _button.Length; i++ ) {
			_button[ i ].gameObject.SetActive( able );
		}
	}

	public void GetEvidence1Bound( bool application ) {
		bool able = true;

		if ( application ) {		
			able = false;
		} else {
			able = true;
		}

		//_detective.SetIsMove( able );
		_clockUI.gameObject.SetActive( able );
		_moviePlaySystem.SetMouseMove( able );
		//_moviePlaySystem.SetFixed( application );
		for ( int i = 0; i < _button.Length; i++ ) {
			_button[ i ].gameObject.SetActive( able );
		}
	}

	public void FirstTapEvidenceFileBound( bool application ) {
		bool able = true;

		if ( application ) {		
			able = false;
		} else {
			able = true;
		}

		//_detective.SetIsMove( able );
		_clockUI.gameObject.SetActive( able );
		_moviePlaySystem.SetMouseMove( able );
		//_moviePlaySystem.SetFixed( application );
		for ( int i = 0; i < _button.Length; i++ ) {
			if ( i != ( int )ButtonNum.EVIDENCE_UI ) {		//証拠品ファイルは規制をかけない
				_button[ i ].gameObject.SetActive( able );
			}
		}
	}

	public void FirstCloseEvidenceFileBound( bool application ) {
		bool able = true;

		if ( application ) {		
			able = false;
		} else {
			able = true;
		}

		//_detective.SetIsMove( able );
		_clockUI.gameObject.SetActive( able );
		_moviePlaySystem.SetMouseMove( able );
		//_moviePlaySystem.SetFixed( application );
		for ( int i = 0; i < _button.Length; i++ ) {
			if ( i != ( int )ButtonNum.EVIDENCE_UI ) {		//証拠品ファイルは規制をかけない
				_button[ i ].gameObject.SetActive( able );
			}
		}

	}


	public void FirstComeToKitchenBound( bool application ) {
		bool able = true;

		if ( application ) {		
			able = false;
		} else {
			able = true;
		}
			
		_clockUI.gameObject.SetActive( able );
		for ( int i = 0; i < _button.Length; i++ ) {
			if ( i != ( int )ButtonNum.EVIDENCE_UI && i != ( int )ButtonNum.TIRANGLE_RIGHT && i != ( int )ButtonNum.MAP_UI ) {		//証拠品ファイルと三角UI（右）とマップには規制をかけない
				_button[ i ].gameObject.SetActive( able );
			}
		}
	}


	public void FirstComeToServingRoomBound( bool application ) {
		bool able = true;

		if ( application ) {		
			able = false;
		} else {
			able = true;
		}

		_clockUI.gameObject.SetActive( able );
		_button[ ( int )ButtonNum.LABO_TRANSITION_UI ].gameObject.SetActive( able );
		_button[ ( int )ButtonNum.TIRANGLE_LEFT ].gameObject.SetActive( able );
	}

	public void FirstComeToBackyardBound( bool application ) {
		bool able = true;

		if ( application ) {		
			able = false;
		} else {
			able = true;
		}

        _evidenceTrigger2 = GameObject.Find( "EvidenceTrigger2" );		//条件を満たすまでは表示されないようにする
		if ( _evidenceTrigger2 != null ) {
			_evidenceTrigger2.GetComponent< BoxCollider2D >( ).enabled = able;
		}

		_clockUI.gameObject.SetActive( able );
		_button[ ( int )ButtonNum.LABO_TRANSITION_UI ].gameObject.SetActive( able );
		_button[ ( int )ButtonNum.TIRANGLE_LEFT ].gameObject.SetActive( able );
	}


	public void ShowBackYardMovieBound( bool application ) {
		bool able = true;

		if ( application ) {		
			able = false;
		} else {
			able = true;
		}

        _evidenceTrigger2 = GameObject.Find( "EvidenceTrigger2" );		//条件を満たすまでは表示されないようにする
		if ( _evidenceTrigger2 != null ) {
			_evidenceTrigger2.GetComponent< BoxCollider2D >( ).enabled = able;
		}

		_clockUI.gameObject.SetActive( able );
		_button[ ( int )ButtonNum.LABO_TRANSITION_UI ].gameObject.SetActive( able );
		_button[ ( int )ButtonNum.TIRANGLE_LEFT ].gameObject.SetActive( able );
		_button[ ( int )ButtonNum.TIRANGLE_RIGHT ].gameObject.SetActive( able );
	}


	public void StopMovieWhichGaedenarAteCakeBound( bool application ) {
		bool able = true;

		if ( application ) {		
			able = false;
		} else {
			able = true;
		}

        _evidenceTrigger2 = GameObject.Find( "EvidenceTrigger2" );		//条件を満たすまでは表示されないようにする
		if ( _evidenceTrigger2 != null ) {
			_evidenceTrigger2.GetComponent< BoxCollider2D >( ).enabled = able;
		}

		_clockUI.gameObject.SetActive( able );
		_button[ ( int )ButtonNum.LABO_TRANSITION_UI ].gameObject.SetActive( able );
		_button[ ( int )ButtonNum.TIRANGLE_LEFT ].gameObject.SetActive( able );
		_button[ ( int )ButtonNum.TIRANGLE_RIGHT ].gameObject.SetActive( able );
	}


	public void GetEvidence2Bound( bool application ) {
		bool able = true;

		if ( application ) {		
			able = false;
		} else {
			able = true;
		}

		_clockUI.gameObject.SetActive( able );
		_button[ ( int )ButtonNum.LABO_TRANSITION_UI ].gameObject.SetActive( able );
		_button[ ( int )ButtonNum.TIRANGLE_LEFT ].gameObject.SetActive( able );
		_button[ ( int )ButtonNum.TIRANGLE_RIGHT ].gameObject.SetActive( able );
	}

    public void FirstComeToDetectiveOfficeBound( ) {

        _clockUI.gameObject.SetActive( false );
		_button[ ( int )ButtonNum.TIRANGLE_LEFT ].gameObject.SetActive( false );
		_button[ ( int )ButtonNum.TIRANGLE_RIGHT ].gameObject.SetActive( false );

    }

    
    public void SilverAndYellowBoxBound( bool application ) {
		bool able = true;

		if ( application ) {		
			able = false;
		} else {
			able = true;
		}


		_evidenceTrigger4 = GameObject.Find( "EvidenceTrigger4" );		//条件を満たすまでは表示されないようにする
		if ( _evidenceTrigger4 != null ) {
			_evidenceTrigger4.GetComponent< BoxCollider2D >( ).enabled = able;
		}


		bool[,] site = {									//どの部屋を閉じる状態にするかの配列。false：閉じない true：閉じる
			{ true, false, true, true },					//右から寝室、キッチン、給仕室、庭。（部屋番号に対応）
			{ true, false, true, true },					//上から昼、夕方、夜。
			{ true, false, false, false }
		};

		if ( _siteMove.GetCheckTiming( ) ||																//現場がうごきたとき
			( ( _cutain.IsStateOpen( ) && _cutain.ResearchStatePlayTime( ) >= 1f ) && _onlyOnce ) ) {	//最初のカーテンが開く処理が終わった後の最初の一回のとき
			CutainCloseBound( site );
			_onlyOnce = false;												//最初の一回処理したら現場が動いたとき以外入らないようにする
		}
	}


    public void GetEvidence4Bound( bool application ) {
        bool[,] site = {
			{ true, false, true, true },
			{ true, false, true, true },
			{ true, false, false, false }
		};

		if ( _siteMove.GetCheckTiming( ) ||																
			( ( _cutain.IsStateOpen( ) && _cutain.ResearchStatePlayTime( ) >= 1f ) && _onlyOnce ) ) {	
			CutainCloseBound( site );
			_onlyOnce = false;
		}
    }

    public void GetEvidence5Bound( bool application ) {
        bool[,] site = {
			{ false, false, false, false },
			{ true, false, true, true },
			{ true, false, false, false }
		};

		if ( _siteMove.GetCheckTiming( ) ||																
			( ( _cutain.IsStateOpen( ) && _cutain.ResearchStatePlayTime( ) >= 1f ) && _onlyOnce ) ) {	
			CutainCloseBound( site );
			_onlyOnce = false;
		}
    }

    public void GetEvidence6Bound( bool application ) {
		bool[,] site = {
			{ false, false, false, false },
			{ false, false, false, false },
			{ true, false, false, false }
		};

		if ( _siteMove.GetCheckTiming( ) ||																
			( ( _cutain.IsStateOpen( ) && _cutain.ResearchStatePlayTime( ) >= 1f ) && _onlyOnce ) ) {	
			CutainCloseBound( site );
			_onlyOnce = false;
		}
    }

	public void LastBound( ) {
		bool[,] site = {
			{ false, false, false, false },
			{ false, false, false, false },
			{ true, false, false, false }
		};

		if ( _siteMove.GetCheckTiming( ) ||																
			( ( _cutain.IsStateOpen( ) && _cutain.ResearchStatePlayTime( ) >= 1f ) && _onlyOnce ) ) {	
			CutainCloseBound( site );
			_onlyOnce = false;
		}
	}

	//縛りがかかる場所だったらカーテンを閉じてそうでなかったらカーテンを開ける関数--------
	//カーテンのフラグがトリガーのためやりずらい。ブールならやりやすいかも
	public void CutainCloseBound( bool[,] site ) {
		int iSite = 0;								//事件現場
		string sTimeZone = "none";					//時間帯

		int i = 0;
		int j = 0;
		for ( i = 0; i < 3; i++ ) {						//時間帯ループ
			for ( j = 0; j < 4; j++ ) {					//事件現場ループ

				if ( site[ i, j ] == true ) {			//閉じたい場所だったら
					
					iSite = j;							//閉じたい事件現場を入れる
					switch ( i ) {						//時間帯を調べる
						case 0:
							sTimeZone = "SiteNoon";
							break;
						case 1:
							sTimeZone = "SiteEvening";
							break;
						case 2:
							sTimeZone = "SiteNight";
							break;
					}

					if ( sTimeZone == _scenesManager.GetNowScenes( ) && iSite == SiteMove._nowSiteNum ) {	//閉じたい場所と現在いる場所が同じだったら
						if ( _cutain.IsStateOpen( ) && _cutain.ResearchStatePlayTime( ) >= 1f ) {			//カーテンが空いていたら		//この処理をしないと空いてもすぐに閉じるというバグが発生した(閉まっているのにフラグが立ってしまうためか？)
							_cutain.Close( );
						}
						return;
					}
		
				} 

			}
		}

		//最後まで終了しなかったら(閉じたい場所と現在いる場所が同じじゃなかったら)
		if ( _cutain.IsStateWait( ) ) {	//カーテンが閉まっている状態だったら
			_cutain.Open( );							
		}

	}
	//----------------------------------------------------------------------------------------------------------------------

}


