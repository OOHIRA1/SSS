using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteNoonManager : MonoBehaviour {
    [ SerializeField ] Detective _detective = null;
    [ SerializeField ] MoviePlaySystem _movePlaySystem = null;
    [ SerializeField ] ScenesManager _scenesManager = null;
	[ SerializeField ] Curtain _cutain = null;
    [ SerializeField ] ClockUI _clockUI = null;
	// Use this for initialization
	void Start( ) {
	}
	
	// Update is called once per frame
	void Update( ) {
        ScenesTransitionWithAnim( );

        if ( !_movePlaySystem.GetStop( ) ) {
            Regulation( );
        } else {
            _detective.SetIsMove( true );
        }
	}

    //押されたものが時計ＵＩの夜か夕方だったら、カーテンを閉めてその時間に遷移する--------------------------------------------------------------
    void ScenesTransitionWithAnim( ) {
        if ( _clockUI.GetPushed( ) == "Night" ) {

            _cutain.Close( );
            if ( _cutain.IsStateClose( ) && _cutain.ResearchStatePlayTime( ) >= 1f ) _scenesManager.SiteScenesTransition( "SiteNight" );
            
        }

        if ( _clockUI.GetPushed( ) == "Evening" ) {

            _cutain.Close( );
            if ( _cutain.IsStateClose( ) && _cutain.ResearchStatePlayTime( ) >= 1f ) _scenesManager.SiteScenesTransition( "SiteEvening" );

        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------

    void Regulation( ) {
        _detective.SetIsMove( false );
        _detective.ResetPos( );

    }


}
