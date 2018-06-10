using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressConditionManager : MonoBehaviour {
	[System.Serializable]
	public struct KeyTimes {	//証拠品の入手できる時間
		public float _start;
		public float _end;
	};

	[ SerializeField ] ScenesManager _scenesManager = null;
	[ SerializeField ] MillioareDieMono _millioareDieMono = null;
	[ SerializeField ] GameObject[] _evidenceIcon = null;
	[ SerializeField ] Detective _detective = null;
	[ SerializeField ] EvidenceFile _evidenceFile = null;
	[ SerializeField ] MoviePlaySystem _moviePlaySystem = null;
	[ SerializeField ] KeyTimes[] _keyTimes = new KeyTimes[ 1 ];

	EvidenceManager _evidenceManager;

	// Use this for initialization
	void Start () {
		_evidenceManager = GameObject.FindGameObjectWithTag( "EvidenceManager" ).GetComponent< EvidenceManager >( );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool ShowMillionareMurderAnimProgress( ) {
		if ( _scenesManager.GetNowScenes( ) == "SiteNight"  ) {	//夜だったら
			if ( !( _millioareDieMono.IsStateMillionaireDieMiddle2( ) && _millioareDieMono.ResearchStatePlayTime( ) > 1f ) ) {   //モノクロアニメーションが終わっていなかったら
				return false;
			} else {
				return true;
			}
		}
		return false;
	}

	public bool DetectiveFirstTalkProgress( ) {
		if ( _detective.GetCheckPos ( ) ) {
			return true;
		} else {
			return false;
		}
	}

	public bool FindPoisonedDishProgress( ) {
		if ( _evidenceIcon[ 0 ].activeInHierarchy ) {
			return true;
		} else {
			return false;
		}
	}


	public bool GetEvidence1Progress( ) {
		if ( _evidenceManager.CheckEvidence( EvidenceManager.Evidence.STORY1_EVIDENCE1 ) ) {
			return true;
		} else {
			return false;
		}
	}
		

	public bool FirstTapEvidenceFileProgress( ) {
		if ( _evidenceFile.gameObject.activeInHierarchy ) {
			return true;
		} else {
			return false;
		}
	}


	public bool FirstCloseEvidenceFileProgress( ) {
		if ( !_evidenceFile.gameObject.activeInHierarchy ) {
			return true;
		} else {
			return false;
		}
	}


	public bool FirstComeToKitchenProgress( ) {
		if ( _scenesManager.GetNowScenes( ) == "SiteNight" && SiteMove._nowSiteNum == 1 ) {		//夜のキッチンだったら
			return true;
		} else {
			return false;
		}
	}
		

	public bool FirstComeToServingRoomProgress( ) {
		if ( _scenesManager.GetNowScenes( ) == "SiteNight" && SiteMove._nowSiteNum == 2 ) {		//夜の給仕室だったら
			return true;
		} else {
			return false;
		}
	}


	public bool FirstComeToBackyardProgress( ) {
		if ( _scenesManager.GetNowScenes( ) == "SiteNight" && SiteMove._nowSiteNum == 3 ) {		//夜の庭だったら
			return true;
		} else {
			return false;
		} 
	}


	public bool ShowBackYardMovieProgress( ) {
		if ( _moviePlaySystem.EndPlayBack( ) ) {
			return true;
		} else {
			return false;
		}
	}


	public bool StopMovieWhichGaedenarAteCakeProgress( ) {
		if ( _moviePlaySystem.GetStop ( ) &&
		    ( _moviePlaySystem.MoviTime ( ) >= _keyTimes [ 0 ]._start && _moviePlaySystem.MoviTime ( ) <= _keyTimes [ 0 ]._end)) {
			return true;
		} else {
			return false;
		}
	}

}
