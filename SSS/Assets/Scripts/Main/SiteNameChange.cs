using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiteNameChange : MonoBehaviour {
	[ SerializeField ] Sprite[] _placeName = new Sprite[ 1 ];		//各部屋の画像
	[ SerializeField ] SiteMove _siteMove = null;

    [ SerializeField ] float _speed = 0.01f;                         //透明になる速さ

	Image _textImage;

    Color _textColor;                                               //画像の色・透明度 
    float _transparency;                                            //変更する画像の透明度の値

	enum PlaceName {
		BED_ROOM,
		KITCHEN,
		SERVING_ROOM,
		GARDEN
	};
	// Use this for initialization
	void Start( ) {
		_textImage = GetComponent< Image >( );
        _transparency = 1.0f;
        _textColor = new Color( 1.0f, 1.0f, 1.0f, _transparency );

		NameChange( );                                              //シーン遷移したら
	}
	
	// Update is called once per frame
	void Update( ) {
        //Debug.Log( _siteMove.GetCheckTiming( ) );
		if ( _siteMove.GetCheckTiming( ) ) {    //現場を動かしたら
            NameChange( );
            _transparency = 1.0f;               //画像の透明度を元に戻す
        }

        TransparencyChange( );
	}

    //画像を対応したものに切り替える関数--------------------------------------------------
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
    //------------------------------------------------------------------------------------


    //画像を透明にしていく関数------------------------------
    void TransparencyChange( ) {

        _transparency -= _speed * Time.deltaTime;
        if ( _transparency < 0 )  {
            _transparency = 0;
            return;
        }

        _textColor.a = _transparency;
        _textImage.color = _textColor;
    }
    //-----------------------------------------------------

}
