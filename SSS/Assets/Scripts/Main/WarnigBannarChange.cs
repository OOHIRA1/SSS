using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarnigBannarChange : MonoBehaviour {
    enum WarnigBannar { 
        WARNIG_EVIDENCE_4,
        WARNIG_EVIDENCE_5
    }

    [ SerializeField ] ScenesManager _scenesManager = null;
    [ SerializeField ] Curtain _cutain = null;
    [ SerializeField ] Sprite[ ] _sprite = new Sprite[ 2 ];     //注意書き画像
    [ SerializeField ] SiteMove _siteMove = null;
    [ SerializeField ] float _speed = 0;

    Image _image;
    Color _color;                                               //画像の色・透明度 
    float _transparency;                                        //変更する画像の透明度の値
    bool _isTransparencyChange;

	// Use this for initialization
	void Start( ) {
		_image = gameObject.GetComponent<Image>( );
        _transparency = 0;
        _color = new Color( 1.0f, 1.0f, 1.0f, _transparency );
        _isTransparencyChange = false;

        _image.color = _color;
	}
	
	// Update is called once per frame
	void Update( ) {
		/*if ( _siteMove.GetCheckTiming( ) ) {    //現場を動かしたら
            BannarChange( );
            //画像の透明度を元に戻す------
            _transparency = 0;
            _color.a = _transparency;
            _image.color = _color;
            //----------------------------
        }*/

        TransparencyChange( );
	}

    public void BannarChange( ) { 
        if ( _scenesManager.GetNowScenes( ) == "SiteNoon" && 
           ( SiteMove._nowSiteNum ==  ( int )SiteMove._siteNum.BEDROOM || SiteMove._nowSiteNum ==  ( int )SiteMove._siteNum.SERVING_ROOM || SiteMove._nowSiteNum == ( int )SiteMove._siteNum.GARDEN ) ) { 
            _image.sprite = _sprite[ ( int )WarnigBannar.WARNIG_EVIDENCE_4 ];
            _isTransparencyChange = true;
            _image.color = _color;
            return;
        }

        if ( _scenesManager.GetNowScenes( ) == "SiteEvening" && 
           ( SiteMove._nowSiteNum ==  ( int )SiteMove._siteNum.BEDROOM || SiteMove._nowSiteNum == ( int )SiteMove._siteNum.SERVING_ROOM || SiteMove._nowSiteNum == ( int )SiteMove._siteNum.GARDEN ) ) { 
            _image.sprite = _sprite[ ( int )WarnigBannar.WARNIG_EVIDENCE_5 ];
            _isTransparencyChange = true;
            _image.color = _color;
            return;
        }
        
        _isTransparencyChange = false;
    }


    //画像を透明にしていく関数------------------------------
    void TransparencyChange( ) {
        if ( !_isTransparencyChange ) return;
        if ( _cutain.ResearchStatePlayTime( ) < 1f && !_cutain.IsStateClose( )  ) return;

        
        _transparency += _speed * Time.deltaTime;
        if ( _transparency > 1f )  {
            _transparency = 1;
            return;
        }

        _color.a = _transparency;
        _image.color = _color;
    }
    //-----------------------------------------------------

    public void BannarReset( ) { 
         _isTransparencyChange = false;
        //画像の透明度を元に戻す------
         _transparency = 0;
         _color.a = _transparency;
         _image.color = _color;
        //----------------------------
    }

}