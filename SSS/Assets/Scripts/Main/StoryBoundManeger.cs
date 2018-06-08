using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoundManeger : MonoBehaviour {
    //[ SerializeField ] GameDataManager _gameDateManager = null;
    [ SerializeField ] Detective _detective = null;
    [ SerializeField ] MoviePlaySystem _moviePlaySystem = null;
    [ SerializeField ] ScenesManager _scenesManager = null;
	[ SerializeField ] Curtain _cutain = null;
    [ SerializeField ] ClockUI _clockUI = null;
	[ SerializeField ] UnityEngine.UI.Button[] _button = new  UnityEngine.UI.Button[ 1 ];
	//[ SerializeField ] GameObject _evidenceFile = null;
	[ SerializeField ] GameObject _ui = null;
	//[ SerializeField ] MillioareDieMono _millioareDieMono = null;
	//[ SerializeField ] SiteMove _siteMove = null;

	// Use this for initialization
	void Start( ) {
	}
	
	// Update is called once per frame
	void Update( ) {
	}

	public void ShowMillionareMurderAnimBound( bool application ) {
		if ( application ) {
				_ui.SetActive( false );
				_detective.transform.position = new Vector3( -20, _detective.transform.position.y, 0 );
			} else {
			    _ui.SetActive( true );
			}
		}


	public void FindPoisonedDishBound( bool application, bool text = false ) {

	}




	//カーテンのフラグがトリガーのためやりずらい。ブールならやりやすいかも
	public void CutainCloseBound( bool[,] site ) {
		int iSite = 0;								//事件現場
		string sTimeZone = "none";					//時間帯

		int i = 0;
		int j = 0;
		for ( i = 0; i < 3; i++ ) {						//時間帯ループ
			for ( j = 0; j < 4; j++ ) {					//事件現場ループ

				if ( site[ i, j ] == true ) {			//閉じたい場所だったら
					

					iSite = j;							//閉じたい事件現場を入れる
					switch ( i ) {						//時間帯を調べる
						case 0:
							sTimeZone = "SiteNoon";
							break;
						case 1:
							sTimeZone = "SiteEvening";
							break;
						case 2:
							sTimeZone = "SiteNight";
							break;
					}


					if ( sTimeZone == _scenesManager.GetNowScenes( ) && iSite == SiteMove._nowSiteNum ) {	//閉じたい場所と現在いる場所が同じだったら	
						
						_cutain.Close( );
						return;
				
					}
		
				} 

			}
		}

		_cutain.Open( );							//最後まで終了しなかったら(閉じたい場所と現在いる場所が同じじゃなかったら)






		//なぜか閉じたときと開いたときに一回だけ繰り返してしまう。その修正をしないといけない

	}

	//SiteManagerにも同じ関数がある。どうしたものか
	void AllButtonInteractable( bool inter ) {
		for (int i = 0; i < _button.Length; i++) {
			_button [i].interactable = inter;
		}
	}

}


