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
	//[ SerializeField ] GameObject[] _ui = new GameObject[ 1 ];
	[ SerializeField ] UnityEngine.UI.Button[] _button = new  UnityEngine.UI.Button[ 1 ];
	[ SerializeField ] GameObject _evidenceFile = null;

	// Use this for initialization
	void Start( ) {
	}
	
	// Update is called once per frame
	void Update( ) {
		CutainState( );
        ScenesTransitionWithAnim( );

        if ( !_movePlaySystem.GetStop( ) ) {
            Regulation( );
        } else {
            _detective.SetIsMove( true );
        }

	}

    //押されたものが時計ＵＩの昼か夕方か夜だったらする処理------------------------------------------------------------------------------------------
    void ScenesTransitionWithAnim( ) {

		if ( _clockUI.GetPushed( ) != "none"  ) {
			_evidenceFile.SetActive( false );			//証拠品ファイルを非表示
			_detective.SetIsMove( false );				//探偵を動けないようにする
			_clockUI.Operation( false );				//時計ＵＩを操作不可にする
			_movePlaySystem.SetOperation( false );		//動画(シークバー)を操作不可にする
			AllButtonInteractable( false );				//ボタンを押せないようにする

            _cutain.Close( );							//カーテンを閉める
            if ( _cutain.IsStateClose( ) && _cutain.ResearchStatePlayTime( ) >= 1f ) _scenesManager.SiteScenesTransition( _clockUI.GetPushed( ) );		//カーテンが閉まりきったらシーン遷移する
		}

    }
    //-------------------------------------------------------------------------------------------------------------------------------------------

    void Regulation( ) {
        _detective.SetIsMove( false );
        //_detective.ResetPos( );
    }

	//カーテンがアニメーションをしてたときとしていないときの処理-------------------------
	void CutainState( ) {
		if ( _cutain.ResearchStatePlayTime( ) < 1f ) {		//カーテンが動いていたら
			_evidenceFile.SetActive( false );
			_detective.SetIsMove( false );
			_clockUI.Operation( false );
			_movePlaySystem.SetOperation( false );				
			AllButtonInteractable( false );

		} else {
			_detective.SetIsMove( true );
			_clockUI.Operation( true );
			_movePlaySystem.SetOperation( true );
			AllButtonInteractable( true );
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
