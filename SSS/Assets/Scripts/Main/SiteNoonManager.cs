using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiteNoonManager : MonoBehaviour {
    [ SerializeField ] Detective _detective = null;
    [ SerializeField ] MoviePlaySystem _movePlaySystem = null;
    [ SerializeField ] ScenesManager _scenesManager = null;
	[ SerializeField ] Curtain _cutain = null;
    [ SerializeField ] ClockUI _clockUI = null;
	[ SerializeField ] GameObject[] _ui = new GameObject[ 1 ];
	[ SerializeField ] UnityEngine.UI.Button[] _button = new  UnityEngine.UI.Button[ 1 ];
	[ SerializeField ] GameObject _evidenceFile = null;

	bool tach;
	// Use this for initialization
	void Start( ) {
		tach = false;
	}
	
	// Update is called once per frame
	void Update( ) {
        ScenesTransitionWithAnim( );
		if ( !tach ) CutainState( );

        if ( !_movePlaySystem.GetStop( ) ) {
            Regulation( );
        } else {
            _detective.SetIsMove( true );
        }

	}

    //押されたものが時計ＵＩの夜か夕方だったらする処理--------------------------------------------------------------
    void ScenesTransitionWithAnim( ) {

        if ( _clockUI.GetPushed( ) == "Night" ) {
			AllButtonInteractable( false );				//ボタンを押せないようにする
			//AllUiActive( false );
			tach = true;
            _cutain.Close( );							//カーテンを閉める
            if ( _cutain.IsStateClose( ) && _cutain.ResearchStatePlayTime( ) >= 1f ) _scenesManager.SiteScenesTransition( "SiteNight" );		//カーテンが閉まりきったらシーン遷移する
            
        }
		//クロックＵＩはコライダーを消す
        if ( _clockUI.GetPushed( ) == "Evening" ) {
			AllButtonInteractable( false );
			//AllUiActive( false );
			tach = true;
            _cutain.Close( );
            if ( _cutain.IsStateClose( ) && _cutain.ResearchStatePlayTime( ) >= 1f ) _scenesManager.SiteScenesTransition( "SiteEvening" );

        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------

    void Regulation( ) {
        _detective.SetIsMove( false );
        _detective.ResetPos( );
    }

	//カーテンがアニメーションをしてたらUIを非表示-------------------------
	void CutainState( ) {
		if ( _cutain.ResearchStatePlayTime( ) < 1f ) {		//カーテンが動いているか閉まるモーション状態だったら(わずかに1を超えてもWaitモーションに入らず閉まるモーションのため)
			_evidenceFile.SetActive( false );
			_detective.SetIsMove( false );
			AllUiActive( false );

		} else {
			
			_detective.SetIsMove( true );
			AllUiActive( true );

		}
			
	}

	void AllUiActive( bool active ) {
		for ( int i = 0; i < _ui.Length; i++ ) {
			_ui[ i ].SetActive( active );
		}
	}

	void AllButtonInteractable( bool inter ) {
		for (int i = 0; i < _button.Length; i++) {
			_button [i].interactable = inter;
		}
	}
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
