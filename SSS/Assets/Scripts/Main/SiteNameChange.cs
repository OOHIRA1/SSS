using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiteNameChange : MonoBehaviour {
	[ SerializeField ] Sprite[] _placeName = new Sprite[ 1 ];		//各部屋の画像
	[ SerializeField ] SiteMove _siteMove = null;

	Image _textImage;

	enum PlaceName {
		BED_ROOM,
		KITCHEN,
		SERVING_ROOM,
		GARDEN
	};
	// Use this for initialization
	void Start( ) {
		_textImage = GetComponent< Image >( );

		NameChange( );
	}
	
	// Update is called once per frame
	void Update( ) {
		if ( _siteMove.GetCheckTiming( ) ) NameChange( );		//シーン遷移したら
	}

	void NameChange( ) {
		switch ( SiteMove._nowSiteNum ) {
			
		case ( int )PlaceName.BED_ROOM:
			_textImage.sprite = _placeName[ ( int )PlaceName.BED_ROOM ];
			break;

		case ( int )PlaceName.KITCHEN:
			_textImage.sprite = _placeName[ ( int )PlaceName.KITCHEN ];
			break;

		case ( int )PlaceName.SERVING_ROOM:
			_textImage.sprite = _placeName[ ( int )PlaceName.SERVING_ROOM ];
			break;

		case ( int )PlaceName.GARDEN:
			_textImage.sprite = _placeName[ ( int )PlaceName.GARDEN ];
			break;
		}

	}

}
