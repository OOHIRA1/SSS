using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detective : MonoBehaviour {
    //タッチできる範囲
	[ SerializeField ] float _moveTouchUp = -4.0f;
	[ SerializeField ] float _moveTouchDown = -6.0f;

    [ SerializeField ] float _speed = 0.1f;             //歩くスピード
    [ SerializeField ] float _forcedSpeed = 0.1f;       //強制移動スピード

    Animator _anim;

    //仮実装
    bool _isAnimWalk;                                   //歩くモーションをするかどうか
	bool _isAnimShocked;								//ショックモーションをするかどうか


    bool _isFlip;                                       //反転するかどうか
    bool _isForcedMoveove;                              //動けるかどうか
    bool _isRopeTouch;                                  //ロープが触れているかどうか
	bool _isForcedMove;								    //強制移動するかどうか

    bool _firstStep;                                    //強制移動の第一工程
	bool _secondStep;                                   //強制移動の第二工程

    Vector3 _destination;                               //探偵が動く場所
    Vector3 _initialPos;                                //探偵の初期位置
    Vector3 _move;                                      //探偵の移動

    Vector3 _flip;                                      //反転する
    Vector3 _notFlip;                                   //反転しない

	Vector3 _forcedDestination;						    //強制移動場所

    Vector3 _forcedMoveX;                               //強制移動ｘ座標
    Vector3 _forcedMoveY;                               //強制移動ｙ座標

	// Use this for initialization
	void Start( ) {
		_anim = GetComponent< Animator >( );
        _isAnimWalk = false;
		_isAnimShocked = false;        
        _isFlip = true;
        _isForcedMoveove = true;
        _isRopeTouch = false;
		_isForcedMove = false;
        _firstStep = false; 
        _secondStep = false;
        _destination = transform.position;
        _initialPos = transform.position;
        _move = new Vector3( _speed ,0, 0 );
        _flip = new Vector3( -1, 1, 1 );
        _notFlip = new Vector3( 1, 1, 1 );
		_forcedDestination = Vector3.zero;
        _forcedMoveX = new Vector3( _forcedSpeed, 0, 0 );
        _forcedMoveY = new Vector3( 0, _forcedSpeed, 0 ); 
	}
	
	// Update is called once per frame
	void Update( ) {
        if ( _isForcedMoveove ) Move( );     //動ける状態だったら
		
        Motion( );

        Flip( );

		if ( _isForcedMove ) ForcedMove( );	//マウス以外のところが指定されたら

	}

    void OnTriggerEnter2D( Collider2D collision ) {
        if ( collision.gameObject.tag == "Rope" ) {							//ロープに触れたら
			_isAnimWalk = false;
			_isRopeTouch = true;
			_isAnimShocked = true;											//ショックモーションをする   
		}
    }

    void OnTriggerExit2D( Collider2D collision ) {
        if ( collision.gameObject.tag == "Rope" ) {                         //ロープから離れたら
            _isRopeTouch = false;
			_isAnimShocked = false;											//ショックモーションをやめる
            InitialMove( );													//動き状態をリセットする
        }
    }

    //探偵の動き処理--------------------------------------------------------------------------------------------
    void Move( ) {

        Vector3 mouse = Vector3.zero;
        if ( Input.GetMouseButtonDown( 0 ) ) {                                      //マウスが押されたら
            mouse = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			
			if ( mouse.y < _moveTouchUp && mouse.y > _moveTouchDown ) {             //押された場所が指定範囲内だったら
				_destination.x = mouse.x;
			}
        }

        if ( transform.position.x < ( _destination.x + 0.1f ) && transform.position.x > ( _destination.x - 0.1f ) ) {     //指定範囲内まで移動したら止まって歩くアニメーションをやめる
            _isAnimWalk = false;
        } else {
                                                                                   //指定範囲外だったらそこまで移動して歩くモーションをする
            if ( transform.position.x < _destination.x ) {
				transform.position += _move;
				_isFlip = true;
			}

            if ( transform.position.x > _destination.x ) {
                transform.position -= _move;
                _isFlip = false;
            }

            _isAnimWalk = true;
        }
        

    }
    //---------------------------------------------------------------------------------------------------------------------

    //アニメーション処理---------------------------
    void Motion( ) {
        if ( _isAnimWalk ) {
            _anim.SetBool( "WalkFlag", true );
        } else {
            _anim.SetBool( "WalkFlag", false );
        }

		if ( _isAnimShocked ) {
            _anim.SetBool( "ShockedFlag", true );
        } else {
            _anim.SetBool( "ShockedFlag", false );
        }

    }
    //----------------------------------------------

    //反転するかどうか----------------------------------------
    void Flip( ) {
        if ( _isFlip ) { 
            transform.localScale = _flip;
        } else {
            transform.localScale = _notFlip;
        }
    }
	//---------------------------------------------------------


	//指定された場所に強制移動する-------------------------------
	void ForcedMove( ) {
		if ( !_firstStep ) ForcedMoveX( );                    //第一工程が終わってなかったらｘ座標を動かす
        if ( _firstStep && !_secondStep ) ForcedMoveY( );     //第一工程が終わって第二工程が終わってなかったらｙ座標を動かす
        if ( _secondStep ) StepReset( );                      //第二工程が終わったら工程をリセットする
	}
	//-----------------------------------------------------------

    //強制移動(ｘ座標)----------------------------------------------
    void ForcedMoveX( ) {
		_isAnimWalk = true;
        if ( _forcedDestination.x < transform.position.x ) {        //目的地が探偵より左側にあったら

			transform.position -= _forcedMoveX;
			_isFlip = false;

            if ( _forcedDestination.x > transform.position.x ) {    //目的地より左側に行ってしまったら
			    Vector3 adjustment = Vector3.zero;
			    adjustment.x = _forcedDestination.x;
			    adjustment.y = transform.position.y;
			    transform.position = adjustment;
		    }   
   
		} else {                                                    //目的地が探偵より右側にあったら

			transform.position += _forcedMoveX;
			_isFlip = true;
			
            if ( _forcedDestination.x < transform.position.x ) {    //目的地より右側に行ってしまったら
			    Vector3 adjustment = Vector3.zero;
			    adjustment.x = _forcedDestination.x;
			    adjustment.y = transform.position.y;
			    transform.position = adjustment;
		    }   
		}


		if ( _forcedDestination.x == transform.position.x ) _firstStep = true;  //座標が合ったら第一工程完了
		
    }
    //---------------------------------------------------------------------

    //強制移動(ｙ座標)-----------------------------------------------------
    void ForcedMoveY( ) {
        if ( _forcedDestination.y < transform.position.y ) {        //目的地が探偵より下にあったら

			transform.position -= _forcedMoveY;

            if ( _forcedDestination.y > transform.position.y ) {    //目的地より下に行ってしまったら
			    Vector3 adjustment = Vector3.zero;
			    adjustment.y = _forcedDestination.y;
			    adjustment.x = transform.position.x;
			    transform.position = adjustment;
		    }   
   
		} else {                                                    //目的地が探偵より上にあったら

			transform.position += _forcedMoveY;
			
            if ( _forcedDestination.y < transform.position.y ) {    //目的地より上に行ってしまったら
			    Vector3 adjustment = Vector3.zero;
			    adjustment.y = _forcedDestination.y;
			    adjustment.x = transform.position.x;
			    transform.position = adjustment;
		    }   
		}

		
		if ( _forcedDestination.y == transform.position.y ) _secondStep = true; //座標が合ったら第二工程完了
		
    }
    //------------------------------------------------------------------------

    //強制移動の工程リセット--------
    void StepReset( ) {
        InitialMove( );
        _isForcedMove = false;
        _firstStep = false;
        _secondStep = false;
    }
    //------------------------------

	//動き状態をリセットする-----------------------
    public void  InitialMove( ) {
        _destination = transform.position;                //移動場所をリセット
        //_destination = _initialPos;
        _isAnimWalk = false;        
        _isFlip = true;                 
    }
    //-----------------------------------------------





	//セッター=========================================================================

    //現場(昼・夕方・夜)マネージャーが動けるかどうか命令するため---------
    public void SetIsMove( bool isMove ) { _isForcedMoveove = isMove; }
    //-------------------------------------------------------------------

	//マウス以外の指定された場所に移動する場所を決めて、その場所に動くようにする--
	public void DesignationMove( Vector3 pos ) {
		_forcedDestination = pos;
		_isForcedMove = true;
	}
	//----------------------------------------------------------------------------

	//==================================================================================

    //====================================================
	//ゲッター
	public bool GetIsAnimWalk() { return _isAnimWalk; }

    public bool GetCheckPos( ) {
		if ( transform.position == _initialPos ) {              //初期地にいるかどうか
			return true;
		} else {
			return false;
		}
	}

	public Vector3 GetInitialPos( ) { return _initialPos; }

    public bool GetRopeTouch( ) { return _isRopeTouch; }

	public bool GetIsM( ) { return _isForcedMove; }


	//====================================================
	//====================================================

}
