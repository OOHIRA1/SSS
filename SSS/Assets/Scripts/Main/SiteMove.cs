using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteMove : MonoBehaviour {
    const float DOWN_ROOM_POS = -30f;
    [ SerializeField ] GameObject[ ] _site    = new GameObject[ 4 ];        //現場四つ	1:Bedroom 2:Graden 3:Kitchen 4:ServingRoom の順番で入れること 
    [ SerializeField ] float _leftRoomPos = -21f;
    [ SerializeField ] float _rightRoomPos = 21f;
    [ SerializeField ] float _moveSpeed = 1f; 
    bool _leftSitemove;                                                     //左に移動する場合
    bool _rightSitemove;                                                    //右に移動する場合
    bool _oneTimeOnly;                                                      //移動するときの一回だけの処理
    public static int _nowSiteNum = 0;                                      //現在のシーン(別シーンから現場シーンに移動するときはこれを変えてから遷移すること)
    int[ ] _nextNextSiteNum;                                                //現在のシーンから1つ先と2つ先と3つ先のシーン

	public enum _siteNum {
		BEDROOM,
		GARDEN,
		KITCHEN,
		SERVING_ROOM
	}


	// Use this for initialization
	void Start( ) {
        _leftSitemove = false;
        _rightSitemove = false;
        _oneTimeOnly = false;
        _nextNextSiteNum = new int [ 3 ];

       SiteInitialPos( );

	}
	
	// Update is called once per frame
	void Update( ) {
        

        //左移動が選択されたとき一回だけの処理--------------------------------
        if ( _oneTimeOnly && _leftSitemove ) {
           LeftSiftSiteNum( );
           _oneTimeOnly = false;
        }
        //------------------------------------------------------------------------


         //右移動が選択されたとき一回だけの処理--------------------------------
        if ( _oneTimeOnly && _rightSitemove ) {
            RightSiftSiteNum( );
            _oneTimeOnly = false;
        }
        //------------------------------------------------------------------------


        //選択されたときの移動処理---------------
        if ( _leftSitemove )  LeftSitemove( );
        if ( _rightSitemove ) RightSitemove( );
        //--------------------------------------

	}


    //シーン遷移直後の現場の配置-------------------------------------------------------
    void SiteInitialPos( ) {
         for ( int i = 0; i < _nextNextSiteNum.Length; i++ ) {
            _nextNextSiteNum[ i ] = AddSiteNum( i + 1 );
        }

         _site[ _nowSiteNum ].transform.position = new Vector3( 0, 0, 0 );
         _site[ _nextNextSiteNum[ 0 ] ].transform.position = new Vector3( _rightRoomPos, 0, 0 );
         _site[ _nextNextSiteNum[ 1 ] ].transform.position = new Vector3( 0, DOWN_ROOM_POS, 0 );
         _site[ _nextNextSiteNum[ 2 ] ].transform.position = new Vector3( _leftRoomPos, 0, 0 );

    }
    //---------------------------------------------------------------------------------


    //左移動が選択されたとき現場番号をずらして入れる----------
    void LeftSiftSiteNum( ) {
         for ( int i = 0; i < _nextNextSiteNum.Length; i++ ) {
            _nextNextSiteNum[ i ] = AddSiteNum( i + 1 );
         }
    }
    //---------------------------------------------------------


    //右移動が選択されたとき現場番号をずらして入れる-----------
    void RightSiftSiteNum( ) {
         for ( int i = 0; i < _nextNextSiteNum.Length; i++ ) {
            _nextNextSiteNum[ i ] = SubSiteNum( i + 1 );
         }
    }
    //---------------------------------------------------------


    //シーン番号を足してずらす--------------------
    int AddSiteNum( int nextSiteNum ) {
        int siteNum = _nowSiteNum;
        siteNum += nextSiteNum;
        if ( siteNum == 4 ) siteNum = 0;
        if ( siteNum == 5 ) siteNum = 1;
        if ( siteNum == 6 ) siteNum = 2;
        return siteNum;
    }
    //-----------------------------------


    //シーン番号を減らしてずらす-------------------
    int SubSiteNum( int nextSiteNum ) {
        int siteNum = _nowSiteNum;
        siteNum -= nextSiteNum;
        if ( siteNum == -1 ) siteNum = 3;
        if ( siteNum == -2 ) siteNum = 2;
        if ( siteNum == -3 ) siteNum = 1; 
        return siteNum;
    }
    //---------------------------------------------


    //左に移動する処理--------------------------------------------------------------------
    void LeftSitemove( ) {
        
        if ( _site[ _nextNextSiteNum[ 0 ] ].transform.position.x != 0 ) {

            _site[ _nowSiteNum ].transform.position += new Vector3( -_moveSpeed, 0, 0 );

            _site[  _nextNextSiteNum[ 0 ] ].transform.position += new Vector3( -_moveSpeed, 0, 0 );

            _site[ _nextNextSiteNum[ 1 ] ].transform.position = new Vector3( _rightRoomPos, 0, 0 );

            _site[ _nextNextSiteNum[ 2 ] ].transform.position = new Vector3( 0, DOWN_ROOM_POS, 0 );

        } else {
            _leftSitemove = false;
            _nowSiteNum++;
            if ( _nowSiteNum > 3 ) _nowSiteNum = 0;    
        }

    }
    //-----------------------------------------------------------------------------------


    //右に移動する処理------------------------------------------------------------------
    void RightSitemove( ) {

        if ( _site[ _nextNextSiteNum[ 0 ] ].transform.position.x != 0 ) {

            _site[ _nowSiteNum ].transform.position += new Vector3( _moveSpeed, 0, 0 );

            _site[  _nextNextSiteNum[ 0 ] ].transform.position += new Vector3( _moveSpeed, 0, 0 );

            _site[ _nextNextSiteNum[ 1 ] ].transform.position = new Vector3( _leftRoomPos, 0, 0 );

            _site[ _nextNextSiteNum[ 2 ] ].transform.position = new Vector3( 0, DOWN_ROOM_POS, 0 );

        } else {
            _rightSitemove = false;
            _nowSiteNum--;
             if ( _nowSiteNum < 0 ) _nowSiteNum = 3;    
        }

    }
    //-----------------------------------------------------------------------------------


    public void RightButton( ) {

        //左移動が選択されたとき----------------------------------------------------
        if ( !_rightSitemove && !_leftSitemove ) {
            _leftSitemove = true;
            _oneTimeOnly = true;
        }
        //-------------------------------------------------------------------------

    }

    
    public void LeftButton( ) {

        //右移動が選択されたとき----------------------------------------------------
        if ( !_rightSitemove && !_leftSitemove ) {
            _rightSitemove = true;
            _oneTimeOnly = true;
        }
        //-------------------------------------------------------------------------
    }

    
}
