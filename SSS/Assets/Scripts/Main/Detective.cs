using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detective : MonoBehaviour {
    //タッチできる範囲
	[ SerializeField ] float _moveTouchUp = -4.0f;
	[ SerializeField ] float _moveTouchDown = -6.0f;

    [ SerializeField ] float _speed = 0.1f;             //歩くスピード

    Animator _anim;

    bool _isAnimWalk;                                   //歩くモーションをするかどうか
	bool _isAnimShocked;								//ショックモーションをするかどうか
    bool _isFlip;                                       //反転するかどうか
    bool _isMove;                                       //動けるかどうか
    bool _isRopeTouch;                                  //ロープが触れているかどうか
	bool _isM;											//マウス以外の指定された場所に移動するかどうか
	

    Vector3 _destination;                               //探偵が動く場所
    Vector3 _initialPos;                                //探偵の初期位置
    Vector3 _move;                                      //探偵の移動

    Vector3 _flip;                                      //反転する
    Vector3 _notFlip;                                   //反転しない

	Vector3 _Pos;										//マウス以外の指定された場所

	// Use this for initialization
	void Start( ) {
		_anim = GetComponent< Animator >( );
        _isAnimWalk = false;
		_isAnimShocked = false;        
        _isFlip = true;
        _isMove = true;
        _isRopeTouch = false;
		_isM = false;
        _destination = transform.position;
        _initialPos = transform.position;
        _move = new Vector3( _speed ,0, 0 );
        _flip = new Vector3( -1, 1, 1 );
        _notFlip = new Vector3( 1, 1, 1 );
		_Pos = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update( ) {
        if ( _isMove ) Move( );     //動ける状態だったら
		Debug.Log( _isMove );
        Motion( );

        Flip( );

		if ( _isM ) HeadThere( );	//マウス以外のところが指定されたら

	}

    void OnTriggerEnter2D( Collider2D collision ) {
        if ( collision.gameObject.tag == "Rope" ) {							//ロープに触れたら
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

    //アニメーション処理---------------------
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
    //---------------------------------------

    //反転するかどうか----------------------------------------
    void Flip( ) {
        if ( _isFlip ) { 
            transform.localScale = _flip;
        } else {
            transform.localScale = _notFlip;
        }
    }
	//---------------------------------------------------------


	//マウス以外に指定された場所に移動する----------------------
	void HeadThere( ) {
		if ( _Pos.x < transform.position.x ) {
			transform.position -= new Vector3( 0.1f,0,0 );
		}

		if ( _Pos.x > transform.position.x ) {
			Vector3 a = Vector3.zero;
			a.x = _Pos.x;
			a.y = transform.position.y;
			transform.position = a;
		}

		if ( _Pos.x == transform.position.x ) {
			InitialMove( );
			_isM = false;
		}
	}
	//-----------------------------------------------------------

	//動き状態をリセットする-----------------------
    public void  InitialMove( ) {
        _destination = _initialPos;                //移動場所をリセット
        _isAnimWalk = false;        
        _isFlip = true;                 
    }
    //-----------------------------------------------





	//セッター=========================================================================

    //現場(昼・夕方・夜)マネージャーが動けるかどうか命令するため--
    public void SetIsMove( bool isMove ) { _isMove = isMove; }
    //------------------------------------------------------------

	//マウス以外の指定された場所に移動する場所を決めて、その場所に動くようにする--
	public void DesignationMove( Vector3 pos ) {
		_Pos = pos;
		_isM = true;
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

	public bool GetIsM( ) { return _isM; }


	//====================================================
	//====================================================

}
