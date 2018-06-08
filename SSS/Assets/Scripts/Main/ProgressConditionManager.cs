using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressConditionManager : MonoBehaviour {
	[ SerializeField ] ScenesManager _scenesManager = null;
	[ SerializeField ] MillioareDieMono _millioareDieMono = null;
	// Use this for initialization
	void Start () {
		
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

	public bool FindPoisonedDishProgress( ) {
		//if ( ) {										//毒の付いた皿を発見したら
		//	return true;
		//} else {
		//	return false;
		//}									
		return false;
	}

	

}
