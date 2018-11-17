using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detective : MonoBehaviour {
    //タッチできる範囲
	[ SerializeField ] float _moveTouchUp = -4.0f;
	[ SerializeField ] float _moveTouchDown = -6.0f;

    [ SerializeField ] float _speed = 0.1f;             //歩くスピード
    [ SerializeField ] float _forcedSpeed = 0.1f;       //強制移動スピード
    
    [ SerializeField ] EffectLibrary _effectLibrary = null;

    [ SerializeField ] RayShooter _rayShooter = null;

    Animator _anim;
    SpriteRenderer _renderer;

    //仮実装
    bool _isAnimWalk;                                   //歩くモーションをするかどうか
	bool _isAnimShocked;								//ショックモーションをするかどうか
    bool _isAnimThink;                                  //トークモーションをするかどうか


    bool _isFlip;                                       //反転するかどうか
	bool _isMove;                              //動けるかどうか
    bool _isRopeTouch;                                  //ロープが触れているかどうか
	bool _isForcedMove;								    //強制移動するかどうか
    bool _isTalk;                                       //トーク状態になるかどうか

    bool _firstStep;                                    //強制移動の第一工程
	bool _secondStep;                                   //強制移動の第二工程

	//bool _checkPos;

    Vector3 _destination;                               //探偵が動く場所
    Vector3 _initialPos;                                //探偵の初期位置
    Vector3 _move;                                      //探偵の移動

	Vector3 _forcedDestination;						    //強制移動場所

    Vector3 _forcedMoveX;                               //強制移動ｘ座標
    Vector3 _forcedMoveY;                               //強制移動ｙ座標

	Rigidbody2D _rigidbody2D;

	// Use this for initialization
	void Start( ) {
		_anim = GetComponent< Animator >( );
        _renderer = GetComponent< SpriteRenderer >( );
        _isAnimWalk = false;
		_isAnimShocked = false;
        _isAnimThink = false;        
        _isFlip = true;
		_isMove = true;
        _isRopeTouch = false;
		_isForcedMove = false;
        _isTalk = false;
        _firstStep = false; 
        _secondStep = false;
		//_checkPos = false;
        _destination = transform.position;
        _initialPos = transform.position;
        _move = new Vector3( _speed ,0, 0 );
		_forcedDestination = Vector3.zero;
        _forcedMoveX = new Vector3( _forcedSpeed, 0, 0 );
        _forcedMoveY = new Vector3( 0, _forcedSpeed, 0 );
		_rigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update( ) {
		if ( _isMove ) Move( );     //動ける状態だったら
		
        Motion( );

        Flip( );

		if ( _isForcedMove ) ForcedMove( );	//マウス以外のところが指定されたら

        Talk( );
        
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
			_isFlip = true;
			//InitialMove( );													//動き状態をリセットする ※これが探偵が天井にワープする原因(ロープアクション中に呼ばれていた。ロープアクション後に呼ばれるとは限らない)
			_destination = _initialPos;//強制的に初期位置を目的地に設定
			_isAnimWalk = false;
        }
    }

    //探偵の動き処理--------------------------------------------------------------------------------------------
    void Move( ) {

        float correction = _speed * Time.deltaTime / 2f + 0.1f;	//補正値(1flameの移動範囲より大きく設定するため)
		if ( transform.position.x <= ( _destination.x + correction ) && transform.position.x >= ( _destination.x - correction ) ) {     //指定範囲内まで移動したら止まって歩くアニメーションをやめる
			transform.position = _destination;										//目的地にピッタリ合うための処理
            _isAnimWalk = false;
        } else {                                                                    //指定範囲外だったらそこまで移動して歩くモーションをする
            _move.x = Time.deltaTime * _speed;
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

        Vector3 mouse = Vector3.zero;
        if ( Input.GetMouseButtonDown( 0 ) ) {                                      //マウスが押されたら
            //押せない領域だったら処理しない
            RaycastHit2D hit = _rayShooter.Shoot( Input.mousePosition );
            if ( hit ) {
			    if ( hit.collider.gameObject.tag == "NoResponseArea" ||
                     hit.collider.gameObject.tag == "Noon" ||
                     hit.collider.gameObject.tag == "Evening" ||
                     hit.collider.gameObject.tag == "Night" ||
                     hit.collider.gameObject.tag == "ClockUIColor" ||
                     hit.collider.gameObject.tag == "EvidenceIcon" ) {
                    return;
                    }
            }
            mouse = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			if ( mouse.y < _moveTouchUp && mouse.y > _moveTouchDown ) {             //押された場所が指定範囲内だったら
				_destination.x = mouse.x;
                mouse.z = 0;                                                        //エフェクトを表示するのに０にする必要がある
                 _effectLibrary.EffectInstantiate( EffectLibrary.Effect.TOUCH, mouse );      //タッチエフェクト生成
			}
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

        if ( _isAnimThink ) {
            _anim.SetBool( "ThinkFlag", true );
        } else {
            _anim.SetBool( "ThinkFlag", false );
        }

    }
    //----------------------------------------------

    //反転するかどうか----------------------------------------
    void Flip( ) {
        if ( _isFlip ) { 
            _renderer.flipX = true;
        } else {
            _renderer.flipX = false;
        }
    }
	//---------------------------------------------------------


	//指定された場所に強制移動する-------------------------------
	void ForcedMove( ) {
		if ( _isMove ) _isMove = false;						//探偵が動ける状態だったら動けなくする

		if ( !_firstStep ) ForcedMoveX( );                    //第一工程が終わってなかったらｘ座標を動かす
        if ( _firstStep && !_secondStep ) ForcedMoveY( );     //第一工程が終わって第二工程が終わってなかったらｙ座標を動かす
        if ( _secondStep ) StepReset( );                      //第二工程が終わったら工程をリセットする
	}
	//-----------------------------------------------------------

    //強制移動(ｘ座標)----------------------------------------------
    void ForcedMoveX( ) {
		_isAnimWalk = true;
		_forcedMoveX.x = Time.deltaTime * _forcedSpeed;
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
		_forcedMoveY.y = Time.deltaTime * _forcedSpeed;
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
		_isMove = true;
        _isFlip = true;                             //強制移動が終わったら探偵が右を向くようにしている
    }
    //------------------------------


    //トーク状態を見て処理する関数----------
    public void Talk( ) {

        if ( _isTalk ) { 
            _isAnimThink = true;
            InitialMove( );
        } else {
            _isAnimThink = false;
        }

    }
    //-------------------------------------



	//動き状態をリセットする-----------------------
    public void  InitialMove( ) {
        _destination = transform.position;                //移動場所をリセット
        //_destination = _initialPos;
        _isAnimWalk = false;        
        //_isFlip = true;                 
    }
    //-----------------------------------------------





	//セッター=========================================================================

    //現場(昼・夕方・夜)マネージャーが動けるかどうか命令するため---------
	public void SetIsAnimShocked( bool x ) { _isAnimShocked = x; }

    public void SetIsMove( bool isMove ) { _isMove = isMove; }

    public void SetIsTalk( bool isTalk ) { _isTalk = isTalk; }
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
		if ( transform.position.x == _initialPos.x ) {              //初期地にいるかどうか
			return true;
		} else {
			return false;
		}
	}

	public Vector3 GetInitialPos( ) { return _initialPos; }

    public bool GetRopeTouch( ) { return _isRopeTouch; }

	public bool GetIsForcedMove( ) { return _isForcedMove; }
	public Vector3 GetPos( ) { return transform.position; }




	//--探偵を落下させる関数(クライマックスシーンで使用)
	public void Fall() {
		StartCoroutine ("FallCoroutine");
	}


	//--探偵を落下させる関数(コルーチン)
	IEnumerator FallCoroutine() {
		SetIsMove (false);
		_rigidbody2D.constraints = RigidbodyConstraints2D.None;
		_rigidbody2D.velocity = new Vector3 (-2,0,0);
		float time = 0;
		const float ROTATE_TIME = 3f;//回転時間
		while(time < ROTATE_TIME) {
			transform.Rotate (0, 0, 1f);
			time += Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}
	}

	//====================================================
	//====================================================

}
