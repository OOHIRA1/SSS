using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detective : MonoBehaviour {
    Animator _anim;

    bool _isAnimWalk;                                   //歩くモーションをするかどうか
    bool _isFlip;                                       //反転するかどうか
    bool _isMove;                                       //動けるかどうか

    Vector3 _move;                                      //探偵が動く場所
    Vector3 _initialPos;                                //探偵の初期位置

    //タッチできる範囲
	[SerializeField] float _moveTouchUp = -4.0f;
	[SerializeField] float _moveTouchDown = -6.0f;


	//====================================================
	//ゲッター
	public bool GetIsAnimWalk() { return _isAnimWalk; }
	//====================================================
	//====================================================

	// Use this for initialization
	void Start( ) {
		_anim = GetComponent< Animator >( );
        _isAnimWalk = false;        
        _isFlip = true;
        _isMove = true;
        _move = transform.position;
        _initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update( ) {
        if ( _isMove ) Move( );

        Motion( );

        Flip( );
	}

    //探偵の動き処理--------------------------------------------------------------------------------------------
    void Move( ) {

        Vector3 mouse = Vector3.zero;
        if ( Input.GetMouseButtonDown( 0 ) ) {                                      //マウスが押されたら
            mouse = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			//Debug.Log( mouse );
			if ( mouse.y < _moveTouchUp && mouse.y > _moveTouchDown ) {             //押された場所が指定範囲内だったら
				_move.x = mouse.x;
			}
        }

        if ( transform.position.x < ( _move.x + 0.1f ) && transform.position.x > ( _move.x - 0.1f ) ) {     //指定範囲内まで移動したら止まって歩くアニメーションをやめる
            _isAnimWalk = false;
        } else {
                                                                                                            //指定範囲外だったらそこまで移動して歩くモーションをする
            if ( transform.position.x < _move.x ) {
				transform.position += new Vector3( 0.1f, 0, 0 );
				_isFlip = true;
			}

            if ( transform.position.x > _move.x ) {
                transform.position += new Vector3( -0.1f, 0, 0 );
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

    }
    //---------------------------------------

    //反転するかどうか----------------------------------------
    void Flip( ) {
        if ( _isFlip ) { 
            transform.localScale = new Vector3( -1, 1, 1 );
        } else {
            transform.localScale = new Vector3( 1, 1, 1 );
        }
    }
    //---------------------------------------------------------

    //一時停止したら場所をリセットする--------------
    public void  ResetPos( ) {
        transform.position = _initialPos;   //探偵を初期位置に
        _move = transform.position;         //移動場所をリセット
        _isAnimWalk = false;        
        _isFlip = true;                 
    }
    //-----------------------------------------------

    //現場(昼・夕方・夜)マネージャーが動けるかどうか命令するため--
    public void SetIsMove( bool isMove ) { _isMove = isMove; }
    //------------------------------------------------------------

}
