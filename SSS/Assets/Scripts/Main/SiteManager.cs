using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiteManager : MonoBehaviour {
    [ SerializeField ] Detective _detective = null;
    [ SerializeField ] MoviePlaySystem _moviePlaySystem = null;
    [ SerializeField ] ScenesManager _scenesManager = null;
	[ SerializeField ] Curtain _cutain = null;
    [ SerializeField ] ClockUI _clockUI = null;
	//[ SerializeField ] GameObject[] _ui = new GameObject[ 1 ];
	[ SerializeField ] UnityEngine.UI.Button[] _button = new  UnityEngine.UI.Button[ 1 ];
	[ SerializeField ] GameObject _evidenceFile = null;
    [ SerializeField ] Catcher _catcher = null;

    [ SerializeField ] MillioareDieMono _millioareDieMono = null;

	// Use this for initialization
	void Start( ) {
	}
	
	// Update is called once per frame
	void Update( ) {
		CutainState( );
        ScenesTransitionWithAnim( );

        if ( !_moviePlaySystem.GetStop( ) ) {								//動画が停止していなかったら(停止していたらCutainStateのほうでtrueになる)（修正したほうがいいか）
            _detective.SetIsMove( false );			 
        }

        if ( _catcher.GetIsCatch( ) || _detective.GetIsM( ) ) {             //ロープアクションしている途中か探偵がマウス以外の指定された場所に歩いている途中だったら
            Regulation( );
        } 
        
       if ( _millioareDieMono.IsStateMillionaireDieMiddle2( ) && _millioareDieMono.ResearchStatePlayTime( ) < 1f ) {
            //モノクロアニメーションをしていたら処理
        }

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

    

	//カーテンがアニメーションをしてたときとしていないときの処理-------------------------
	void CutainState( ) {
		if ( _cutain.ResearchStatePlayTime( ) < 1f ) {		//カーテンが動いていたら
			Regulation( );
		} else {
			_detective.SetIsMove( true );
			_clockUI.Operation( true );
			_moviePlaySystem.SetOperation( true );
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

