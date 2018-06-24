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
	[ SerializeField ] Detective _detective = null;
	[ SerializeField ] EvidenceFile _evidenceFile = null;
	[ SerializeField ] MoviePlaySystem _moviePlaySystem = null;
	[ SerializeField ] KeyTimes[] _keyTimes = new KeyTimes[ 1 ];
	[ SerializeField ] KeyPos[] _keyPos = new KeyPos[ 1 ];
    [ SerializeField ] Curtain _curtain = null;

	MillioareDieMono _millioareDieMono;
	EvidenceManager _evidenceManager;
	GameObject _evidenceIcon1 = null;
	//MillioareDieMono _millioareDieMono;

	// Use this for initialization
	void Start () {
		GameObject evidenceManager = GameObject.FindGameObjectWithTag( "EvidenceManager" );
		if ( evidenceManager != null ) _evidenceManager = evidenceManager.GetComponent< EvidenceManager >( );

		_evidenceIcon1 = GameObject.Find( "EvidenceIcon1" );

		GameObject millioareDieMono = GameObject.FindGameObjectWithTag( "MillioareMonoDie" );
		if ( millioareDieMono != null ) _millioareDieMono = millioareDieMono.GetComponent< MillioareDieMono >( );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool ShowMillionareMurderAnimProgress( ) {
		if ( _scenesManager.GetNowScenes( ) == "SiteNight"  ) {	//夜だったら

			if ( _millioareDieMono != null ) {
				if ( !( _millioareDieMono.IsStateMillionaireDieMiddle2( ) && _millioareDieMono.ResearchStatePlayTime( ) > 1f ) ) {   //モノクロアニメーションが終わっていなかったら
					return false;
				}
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
		_evidenceIcon1 = GameObject.Find( "EvidenceIcon1" );
		if ( _evidenceIcon1 != null ) {					
			if ( _evidenceIcon1.activeInHierarchy ) {
				return true;
			}
		}
		return false;
	}


	public bool GetEvidence1Progress( ) {
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
		if ( _moviePlaySystem.MovieTime( ) >= 60f ) {
			return true;
		}
		return false;
	}


	public bool StopMovieWhichGaedenarAteCakeProgress( ) {
		if ( _moviePlaySystem.GetStop( ) &&
		    ( _moviePlaySystem.MovieTime( ) >= _keyTimes[ 0 ]._start && _moviePlaySystem.MovieTime( ) <= _keyTimes[ 0 ]._end ) ) {
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


    public bool FirstComeToKitchenAtNoonOrNightProgress( ) {

        if( _scenesManager.GetNowScenes( ) == "SiteNoon" && SiteMove._nowSiteNum == 1 ) {
            if ( _curtain.IsStateOpen( ) && _curtain.ResearchStatePlayTime( ) >= 1f ) {
                return true;
            }
        }

        if ( _scenesManager.GetNowScenes( ) == "SiteEvening" && SiteMove._nowSiteNum == 1 ) {
            if ( _curtain.IsStateOpen( ) && _curtain.ResearchStatePlayTime( ) >= 1f ) {
                return true;
            }
        }

        return false;
    }

	
	public bool ShowButlerPutSilverBoxProgress( ) {
		if ( _scenesManager.GetNowScenes( ) == "SiteEvening" && SiteMove._nowSiteNum == 1 ) {
			if ( _moviePlaySystem.MovieTime( ) >= _keyTimes[ 1 ]._start && _moviePlaySystem.MovieTime( ) <= _keyTimes[ 1 ]._end ) {	//特定の時間内だったら
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
			if ( _moviePlaySystem.MovieTime( ) >= _keyTimes[ 2 ]._start && _moviePlaySystem.MovieTime( ) <= _keyTimes[ 2 ]._end ) {	//特定の時間内だったら
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
