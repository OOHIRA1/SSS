using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressConditionManager : MonoBehaviour {
	[System.Serializable]
	public struct KeyTimes {	//証拠品の入手できる時間
		public float _start;
		public float _end;
	};

	[ System.Serializable ]
	public struct KeyPos {		//この座標の間に止まるとストーリーが進行する
		public float _posLeft;
		public float _posRight;
	}

	[ SerializeField ] ScenesManager _scenesManager = null;
	[ SerializeField ] GameObject[] _evidenceIcon = null;
	[ SerializeField ] Detective _detective = null;
	[ SerializeField ] EvidenceFile _evidenceFile = null;
	[ SerializeField ] MoviePlaySystem _moviePlaySystem = null;
	[ SerializeField ] MillioareDieMono _millioareDieMono = null;
	[ SerializeField ] KeyTimes[] _keyTimes = new KeyTimes[ 1 ];
	[ SerializeField ] KeyPos[] _keyPos = new KeyPos[ 1 ];

	EvidenceManager _evidenceManager;
	//MillioareDieMono _millioareDieMono;

	// Use this for initialization
	void Start () {
		_evidenceManager = GameObject.FindGameObjectWithTag( "EvidenceManager" ).GetComponent< EvidenceManager >( );
		//_millioareDieMono = GameObject.FindGameObjectWithTag( "MillioareMonoDie" ).GetComponent< MillioareDieMono >( );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool ShowMillionareMurderAnimProgress( ) {
		if ( _scenesManager.GetNowScenes( ) == "SiteNight"  ) {	//夜だったら
			if ( !( _millioareDieMono.IsStateMillionaireDieMiddle2( ) && _millioareDieMono.ResearchStatePlayTime( ) > 1f ) ) {   //モノクロアニメーションが終わっていなかったら
				return false;
			}
		}
		return true;
	}

	public bool DetectiveFirstTalkProgress( ) {
		if ( _detective.GetCheckPos ( ) ) {
			return true;
		}
		return false;
	}

	public bool FindPoisonedDishProgress( ) {
		if ( _evidenceIcon[ 0 ].activeInHierarchy ) {
			return true;
		} 
		return false;
	}


	public bool GetEvidence1Progress( ) {
        Debug.Log( _evidenceManager.CheckEvidence( EvidenceManager.Evidence.STORY1_EVIDENCE3 ) );
		if ( _evidenceManager.CheckEvidence( EvidenceManager.Evidence.STORY1_EVIDENCE1 ) ) {
			return true;
		}
		return false;
	}
		

	public bool FirstTapEvidenceFileProgress( ) {
		if ( _evidenceFile.gameObject.activeInHierarchy ) {
			return true;
		}
		return false;
	}


	public bool FirstCloseEvidenceFileProgress( ) {
		if ( !_evidenceFile.gameObject.activeInHierarchy ) {
			return true;
		}
		return false;
	}


	public bool FirstComeToKitchenProgress( ) {
		if ( _scenesManager.GetNowScenes( ) == "SiteNight" && SiteMove._nowSiteNum == 1 ) {		//夜のキッチンだったら
			return true;
		}
		return false;
	}
		

	public bool FirstComeToServingRoomProgress( ) {
		if ( _scenesManager.GetNowScenes( ) == "SiteNight" && SiteMove._nowSiteNum == 2 ) {		//夜の給仕室だったら
			return true;
		}
		return false;
	}


	public bool FirstComeToBackyardProgress( ) {
		if ( _scenesManager.GetNowScenes( ) == "SiteNight" && SiteMove._nowSiteNum == 3 ) {		//夜の庭だったら
			return true;
		}
		return false;
	}


	public bool ShowBackYardMovieProgress( ) {
		if ( _moviePlaySystem.MoviTime( ) >= 60f ) {
			return true;
		}
		return false;
	}


	public bool StopMovieWhichGaedenarAteCakeProgress( ) {
		if ( _moviePlaySystem.GetStop( ) &&
		    ( _moviePlaySystem.MoviTime( ) >= _keyTimes[ 0 ]._start && _moviePlaySystem.MoviTime( ) <= _keyTimes[ 0 ]._end ) ) {
			return true;
		}
		return false;
	}


	public bool GetEvidence2Progress( ) {
		if ( _evidenceManager.CheckEvidence( EvidenceManager.Evidence.STORY1_EVIDENCE2 ) ) {
			return true;
		}
		return false;
	}


	
	public bool ShowButlerPutSilverBoxProgress( ) {
		if ( _scenesManager.GetNowScenes( ) == "SiteEvening" && SiteMove._nowSiteNum == 1 ) {
			if ( _moviePlaySystem.MoviTime( ) >= _keyTimes[ 1 ]._start && _moviePlaySystem.MoviTime( ) <= _keyTimes[ 1 ]._end ) {	//特定の時間内だったら
				if ( !_detective.GetIsAnimWalk( ) &&
					( _detective.GetPos( ).x >= _keyPos[ 0 ]._posLeft && _detective.GetPos( ).x <= _keyPos[ 0 ]._posRight ) ) {		//探偵の位置が特定の範囲内で止まっていたら
					return true;
				}
			}
		}
		return false;
	}

  
    public bool ShowCookPutYellowBoxProgress( ) {
        if ( _scenesManager.GetNowScenes( ) == "SiteEvening" && SiteMove._nowSiteNum == 1 ) {
			if ( _moviePlaySystem.MoviTime( ) >= _keyTimes[ 2 ]._start && _moviePlaySystem.MoviTime( ) <= _keyTimes[ 2 ]._end ) {	//特定の時間内だったら
				if ( !_detective.GetIsAnimWalk( ) &&
					( _detective.GetPos( ).x >= _keyPos[ 0 ]._posLeft && _detective.GetPos( ).x <= _keyPos[ 0 ]._posRight ) ) {		//探偵の位置が特定の範囲内で止まっていたら
					return true;
				}
			}
		}
		return false;;
    }


    public bool GetEvidence4Progress( ) {
        if ( _evidenceManager.CheckEvidence( EvidenceManager.Evidence.STORY1_EVIDENCE4 ) ) {
            return true;
        }
		return false;
    }


    public bool GetEvidence5Progress( ) {
        if ( _evidenceManager.CheckEvidence( EvidenceManager.Evidence.STORY1_EVIDENCE5 ) ) {
            return true;
        }
		return false;
    }


    public bool GetEvidence6Progress( ) {
        if ( _evidenceManager.CheckEvidence( EvidenceManager.Evidence.STORY1_EVIDENCE6 ) ) {
            return true;
		}
		return false;
    }
}
