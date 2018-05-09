using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {

	// Use this for initialization
	void Start( ) {
	}
	
	// Update is called once per frame
	void Update( ) {
	}


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

		}
			
	}


	public void SiteNoonScenesTransition( ) {
		int siteNum = SiteMove._nowSiteNum;
		switch ( siteNum ) {
		case ( int )SiteMove._siteNum.BEDROOM:
			ScenesTransition("SiteNoon_Bedroom");
			break;

		case ( int )SiteMove._siteNum.GARDEN:
			ScenesTransition("SiteNoon_Garden");
			break;

		case ( int )SiteMove._siteNum.KITCHEN:
			ScenesTransition("SiteNoon_Kitchen");
			break;

		case ( int )SiteMove._siteNum.SERVING_ROOM:
			ScenesTransition("SiteNoon_ServingRoom");
			break;
		}
	}


	public void SiteEveningScenesTransition( ) {
		int siteNum = SiteMove._nowSiteNum;
		switch ( siteNum ) {
		case ( int )SiteMove._siteNum.BEDROOM:
			ScenesTransition("SiteEvening_Bedroom");
			break;

		case ( int )SiteMove._siteNum.GARDEN:
			ScenesTransition("SiteEvening_Garden");
			break;

		case ( int )SiteMove._siteNum.KITCHEN:
			ScenesTransition("SiteEvening_Kitchen");
			break;

		case ( int )SiteMove._siteNum.SERVING_ROOM:
			ScenesTransition("SiteEvening_ServingRoom");
			break;
		}
	}


	public void SiteNightScenesTransition( ) {
		int siteNum = SiteMove._nowSiteNum;
		switch ( siteNum ) {
		case ( int )SiteMove._siteNum.BEDROOM:
			ScenesTransition("SiteNight_Bedroom");
			break;

		case ( int )SiteMove._siteNum.GARDEN:
			ScenesTransition("SiteNight_Garden");
			break;

		case ( int )SiteMove._siteNum.KITCHEN:
			ScenesTransition("SiteNight_Kitchen");
			break;

		case ( int )SiteMove._siteNum.SERVING_ROOM:
			ScenesTransition("SiteNight_ServingRoom");
			break;
		}
	}


}
