using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {

	// Use this for initialization
	void Start( ) {
	}
	

    //シーン遷移------------------------------------------------------------
	public void ScenesTransition( string scene ) {
		switch ( scene ) {
		case "Title":
			SceneManager.LoadScene( "Title" );
			break;
		
		case "StageSelect":
			SceneManager.LoadScene( "StageSelect" );
			break;

		case "DetectiveOffice":
			SceneManager.LoadScene( "DetectiveOffice" );
			break;

		case "SiteNoon_Bedroom":
			SiteMove._nowSiteNum = ( int )SiteMove._siteNum.BEDROOM;
			SceneManager.LoadScene( "SiteNoon" );
			break;
		
		case "SiteNoon_Garden":
			SiteMove._nowSiteNum = ( int )SiteMove._siteNum.GARDEN;
			SceneManager.LoadScene( "SiteNoon" );
			break;

		case "SiteNoon_Kitchen":
			SiteMove._nowSiteNum = ( int )SiteMove._siteNum.KITCHEN;
			SceneManager.LoadScene( "SiteNoon" );
			break;

		case "SiteNoon_ServingRoom":
			SiteMove._nowSiteNum = ( int )SiteMove._siteNum.SERVING_ROOM;
			SceneManager.LoadScene( "SiteNoon" );
			break;

		case "SiteEvening_Bedroom":
			SiteMove._nowSiteNum = ( int )SiteMove._siteNum.BEDROOM;
			SceneManager.LoadScene( "SiteEvening" );
			break;

		case "SiteEvening_Garden":
			SiteMove._nowSiteNum = ( int )SiteMove._siteNum.GARDEN;
			SceneManager.LoadScene( "SiteEvening" );
			break;

		case "SiteEvening_Kitchen":
			SiteMove._nowSiteNum = ( int )SiteMove._siteNum.KITCHEN;
			SceneManager.LoadScene( "SiteEvening" );
			break;

		case "SiteEvening_ServingRoom":
			SiteMove._nowSiteNum = ( int )SiteMove._siteNum.SERVING_ROOM;
			SceneManager.LoadScene( "SiteEvening" );
			break;
		
		case "SiteNight_Bedroom":
			SiteMove._nowSiteNum = ( int )SiteMove._siteNum.BEDROOM;
			SceneManager.LoadScene( "SiteNight" );
			break;

		case "SiteNight_Garden":
			SiteMove._nowSiteNum = ( int )SiteMove._siteNum.GARDEN;
			SceneManager.LoadScene( "SiteNight" );
			break;

		case "SiteNight_Kitchen":
			SiteMove._nowSiteNum = ( int )SiteMove._siteNum.KITCHEN;
			SceneManager.LoadScene( "SiteNight" );
			break;

		case "SiteNight_ServingRoom":
			SiteMove._nowSiteNum = ( int )SiteMove._siteNum.SERVING_ROOM;
			SceneManager.LoadScene( "SiteNight" );
			break;

		case "ClimaxBattle":
			SceneManager.LoadScene ("ClimaxBattle");
			break;

		case "GameClear":
			SceneManager.LoadScene ("GameClear");
			break;
		case "GameOver":
			SceneManager.LoadScene ("GameOver");
			break;
		}
			
	}
    //---------------------------------------------------------------------------


    //別の時間帯の同じ事件現場に遷移するための関数---------------------
    public void SiteScenesTransition( string scene ) {
        int siteNum = SiteMove._nowSiteNum;

        if ( scene == "SiteNoon" ) {
            switch ( siteNum ) {
                case ( int )SiteMove._siteNum.BEDROOM:
                ScenesTransition( "SiteNoon_Bedroom" );
                break;

                case ( int )SiteMove._siteNum.GARDEN:
                ScenesTransition( "SiteNoon_Garden" );
                break;

                case ( int )SiteMove._siteNum.KITCHEN:
                ScenesTransition( "SiteNoon_Kitchen" );
                break;

                case ( int )SiteMove._siteNum.SERVING_ROOM:
                ScenesTransition( "SiteNoon_ServingRoom" );
                break;
            }
        }

        if ( scene == "SiteEvening" ) {
            switch ( siteNum ) {
                case ( int )SiteMove._siteNum.BEDROOM:
                ScenesTransition( "SiteEvening_Bedroom" );
                break;

                case ( int )SiteMove._siteNum.GARDEN:
                ScenesTransition( "SiteEvening_Garden" );
                break;

                case ( int )SiteMove._siteNum.KITCHEN:
                ScenesTransition( "SiteEvening_Kitchen" );
                break;

                case ( int )SiteMove._siteNum.SERVING_ROOM:
                ScenesTransition( "SiteEvening_ServingRoom" );
                break;
            }
        }

        if ( scene == "SiteNight" ) {
            switch ( siteNum ) {
                case ( int )SiteMove._siteNum.BEDROOM:
                ScenesTransition( "SiteNight_Bedroom" );
                break;

                case ( int )SiteMove._siteNum.GARDEN:
                ScenesTransition( "SiteNight_Garden" );
                break;

                case ( int )SiteMove._siteNum.KITCHEN:
                ScenesTransition( "SiteNight_Kitchen" );
                break;

                case ( int )SiteMove._siteNum.SERVING_ROOM:
                ScenesTransition( "SiteNight_ServingRoom" );
                break;
            }
        }
    }
    //-------------------------------------------------------------

    //現在のシーンの名前を返す
    public string GetNowScenes( ) { return SceneManager.GetActiveScene( ).name; } 



}
