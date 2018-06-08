using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteMove : MonoBehaviour {
    const float DOWN_ROOM_forcedPos = -30f;
    [ SerializeField ] GameObject[ ] _site    = new GameObject[ 4 ];        //現場四つ	1:Bedroom 2:Graden 3:Kitchen 4:ServingRoom の順番で入れること 
	[ SerializeField ] float _neighborRoom = 21f;							//左右の部屋のpos(-を付けると左の部屋のpos。そのままだと右の部屋のpos。)
    [ SerializeField ] float _moveSpeed = 1f; 
    bool _leftSitemove;                                                     //左に移動する場合
    bool _rightSitemove;                                                    //右に移動する場合
    bool _oneTimeOnly;                                                      //移動するときの一回だけの処理
	bool _checkTiming;														//移動したそのシーンが縛りをかける場所かチェックしてもらうタイミング
    public static int _nowSiteNum = 0;                                      //現在のシーン(別シーンから現場シーンに移動するときはこれを変えてから遷移すること) 0:Bedroom 1:Graden 2:Kitchen 3:ServingRoom
    int[ ] _nextNextSiteNum;                                                //現在のシーンから1つ先と2つ先と3つ先のシーン

	Vector3 _nowSitePos;													//現在の現場のpos
	Vector3 _nextOneSitePos;												//次に真ん中にくる現場のpos
	Vector3 _nextTwoSitePos;												//下から真ん中の現場の隣にくる現場のpos
	Vector3 _nextThreeSitePos;												//真ん中の現場の隣から下にいく現場のpos

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
		_checkTiming = false;
        _nextNextSiteNum = new int [ 3 ];

		_nowSitePos = new Vector3( _moveSpeed, 0, 0 );
		_nextOneSitePos = new Vector3( _moveSpeed, 0, 0 );
		_nextTwoSitePos = new Vector3( _neighborRoom, 0, 0 );
		_nextThreeSitePos = new Vector3( 0, DOWN_ROOM_forcedPos, 0 );

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
        if ( _leftSitemove )  RightSiteMove( );
        if ( _rightSitemove ) LeftSiteMove( );
        //--------------------------------------

	}


    //シーン遷移直後の現場の配置-----------------------------------------------------------------
    void SiteInitialPos( ) {
         for ( int i = 0; i < _nextNextSiteNum.Length; i++ ) {
            _nextNextSiteNum[ i ] = AddSiteNum( i + 1 );
        }

         _site[ _nowSiteNum ].transform.position = new Vector3( 0, 0, 0 );
         _site[ _nextNextSiteNum[ 0 ] ].transform.position = new Vector3( _neighborRoom, 0, 0 );
         _site[ _nextNextSiteNum[ 1 ] ].transform.position = new Vector3( 0, DOWN_ROOM_forcedPos, 0 );
         _site[ _nextNextSiteNum[ 2 ] ].transform.position = new Vector3( -_neighborRoom, 0, 0 );

    }
    //--------------------------------------------------------------------------------------------


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
    void RightSiteMove( ) {
        
        if ( _site[ _nextNextSiteNum[ 0 ] ].transform.position.x != 0 ) {			//右の現場が真ん中に移動している途中だったら

            _site[ _nowSiteNum ].transform.position -= _nowSitePos;					//現在真ん中にある現場が左の場所に移動

            _site[  _nextNextSiteNum[ 0 ] ].transform.position -= _nextOneSitePos;	//右の現場が真ん中に移動

            _site[ _nextNextSiteNum[ 1 ] ].transform.position = _nextTwoSitePos;	//下の現場を右の場所に移す	

            _site[ _nextNextSiteNum[ 2 ] ].transform.position = _nextThreeSitePos;	//左の現場を下の場所に移す

        } else {
            _leftSitemove = false;													//移動し終わったらこの関数に入らないようにする
			_checkTiming = true;													//チェックしてもらうタイミングを立てる
            _nowSiteNum++;															//現場番号を合わす
            if ( _nowSiteNum > 3 ) 
				_nowSiteNum = 0;									//最大番号を超えていたら最初にループさせる
        }

    }
    //-----------------------------------------------------------------------------------


    //右に移動する処理------------------------------------------------------------------
    void LeftSiteMove( ) {

        if ( _site[ _nextNextSiteNum[ 0 ] ].transform.position.x != 0 ) {			//左の現場が真ん中に移動している途中だったら

            _site[ _nowSiteNum ].transform.position += _nowSitePos;					//現在真ん中にある現場が右の場所に移動

            _site[  _nextNextSiteNum[ 0 ] ].transform.position += _nextOneSitePos;	//左の現場が真ん中に移動

            _site[ _nextNextSiteNum[ 1 ] ].transform.position = -_nextTwoSitePos;	//下の現場を左の場所に移す

            _site[ _nextNextSiteNum[ 2 ] ].transform.position = _nextThreeSitePos;	//右の現場を下の場所に移す

        } else {
            _rightSitemove = false;													//移動し終わったらこの関数に入らないようにする
			_checkTiming = true;													//チェックしてもらうタイミングを立てる
            _nowSiteNum--;															//現場番号を合わす
             if ( _nowSiteNum < 0 ) _nowSiteNum = 3;								//最小番号を下回っていたら最後にループさせる
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


	public bool GetCheckTiming( ) {
		bool relay = _checkTiming;

		if ( _checkTiming ) _checkTiming = false;

		return relay;
	}

    
}
