using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteNoonManager : MonoBehaviour {
    [ SerializeField ] ScenesManager _scenesManager = null;	//シーンマネージャー
	[ SerializeField ] Curtain _cutain = null;
    [ SerializeField ] ClockUI _clockUI = null;
	// Use this for initialization
	void Start( ) {
	}
	
	// Update is called once per frame
	void Update( ) {
        ScenesTransitionWithAnim( );
	}

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
}
