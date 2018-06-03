using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoundManeger : MonoBehaviour {
    [ SerializeField ] GameDataManager _gameDateManager = null;
    [ SerializeField ] Detective _detective = null;
    [ SerializeField ] MoviePlaySystem _moviePlaySystem = null;
    [ SerializeField ] ScenesManager _scenesManager = null;
	[ SerializeField ] Curtain _cutain = null;
    [ SerializeField ] ClockUI _clockUI = null;
	[ SerializeField ] UnityEngine.UI.Button[] _button = new  UnityEngine.UI.Button[ 1 ];
	[ SerializeField ] GameObject _evidenceFile = null;
	// Use this for initialization
	void Start( ) {
	}
	
	// Update is called once per frame
	void Update( ) {
		if ( _gameDateManager.GetAdvancedData( ) == ( int )GameDataManager.CheckPoint.SHOW_MILLIONARE_MURDER_ANIM ) {

        }
	}
}
