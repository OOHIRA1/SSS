using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour {
    const float ROPE_STOP_forcedDestination = 15f;                                //ロープが停止する位置(Y座標)

    [ SerializeField ] float _speed = 0.1f;                         //ロープアクションの速度
    [ SerializeField ] float _toTakeHeight = 10f;                   //探偵を持ち上げる高さ

    [ SerializeField ] Detective _player = null;
	[ SerializeField ] GameObject _rope = null;
	//[ SerializeField ] MoviePlaySystem _moviePlaySystem = null;

    bool _initialRope;                                              //ロープの初期化を行ったかどうか
	bool _isRopeAction;                                             //ロープアクションをするかどうか
    bool _firstStep;                                                //ロープアクションの第一工程が終わったかどうか
    bool _secondStep;                                               //ロープアクションの第二工程が終わったかどうか
    Vector3 _destination;                                           //探偵を持ってく目的地
    Vector3 _moveX;                                                 //ロープアクションのｘ座標の動き                                                 
    Vector3 _moveY;                                                 //ロープアクションのｙ座標の動き

	// Use this for initialization
	void Start( ) {
		_isRopeAction = false;
        _initialRope = true;
        _firstStep = false;
        _secondStep = false;
        _destination = _player.GetInitialPos( );
        _moveX = new Vector3( _speed, 0, 0 );
        _moveY = new Vector3( 0, _speed, 0 );
	}
	
	// Update is called once per frame
	void Update( ) {

        if ( _isRopeAction ) {                      //ロープアクションをするかどうか

            if (_initialRope) {
                InitialRope( );     //一回ロープの位置の初期化をしたかどうか
                _destination = _player.GetInitialPos( );
            }
			
            ToCarryMove( );
        }	
	}

    //ロープの位置の初期化---------------------------------
    void InitialRope( ) {
        Vector3 initialPos = Vector3.zero;
        initialPos.x = _player.transform.position.x;
        initialPos.y = _rope.transform.position.y;
        _rope.transform.position = initialPos;
        _initialRope = false;
    }
    //------------------------------------------------------

    //運ぶ動きをまとめた関数--------------------------------
    void ToCarryMove( ) {
        if ( !_firstStep ) CatchAndUpMove( );						//第一工程が終わってなかったら第一工程をする
        if ( _firstStep && !_secondStep ) InitialPosPut( );         //第一工程が終わってて、第二工程が終わってなかったら第二工程をする
        if ( _secondStep ) RopeExit( );								//第二工程が終わったら最後の動きをする
    }
    //------------------------------------------------------

    
    //探偵を捕まえて持ち上げるまでの動き-------------------------------
    void CatchAndUpMove( ) {
        if ( !_player.GetRopeTouch( ) ) {                       //探偵がロープに触れていなかったらロープを下げる
            _rope.transform.position -= _moveY;
        } else {                                                //触れたら持ち上げる
            _rope.transform.position += _moveY;
            _player.transform.position += _moveY;
        }

        if ( _player.transform.position.y > _toTakeHeight ) {   //指定の高さより持ち上げたら第一工程を完了にする
            _firstStep = true;
        } 
    }
    //---------------------------------------------------------------------

    //探偵を初期地まで持っていくまでの動き(初期地より左側にいることはないので左側にいたときの計算はしない)----
    void InitialPosPut( ) {  
        if ( _destination.x < _player.transform.position.x ) {          //初期地より探偵のｘ座標がずれていたらまずｘ座標を初期地に持ってく

            _rope.transform.position -= _moveX;
            _player.transform.position -= _moveX;

			if ( _destination.x > _player.transform.position.x ) {		//ｘ座標を超えたときに戻す処理
				Vector3 vec = Vector3.zero;
				vec.x = _destination.x;
				vec.y = _player.transform.position.y;
				_player.transform.position = vec;
			}
		

        } else if ( _player.transform.position.y > _destination.y ) {   //探偵のｘ座標が合っていてｙ座標をがずれていたらｙ座標を初期地に持っていく

            _rope.transform.position -= _moveY;
            _player.transform.position -= _moveY;
			
			if ( _destination.y > _player.transform.position.y ) {		//ｙ座標を超えたときに戻す処理
				Vector3 vec = Vector3.zero;
				vec.y = _destination.y;
				vec.x = _player.transform.position.x;
				_player.transform.position = vec;
			}


        } else {

            _secondStep = true;                                         //ｘ座標もｙ座標も合っていたら第二工程を完了にする

        }
    }
    //-----------------------------------------------------------------------------------------------------------

    //探偵が初期地に着いたらロープを退場させる動き-----------
    void RopeExit( ) {
        _rope.transform.position += _moveY;

        if ( _rope.transform.position.y > ROPE_STOP_forcedDestination ) StepReset( );     //ロープが指定位置まで行ったら工程をリセットさせて終了
        
    }
    //-----------------------------------------------------------------------

    //工程をリセットする---------
    void StepReset( ) {
        _isRopeAction = false;
        _initialRope = true;
        _firstStep = false;
        _secondStep = false;
    }
    //---------------------------

	//ロープアクションをする
	public void ToRopeAction(  ) { _isRopeAction = true; }

    
    public bool GetIsCatch( ) { return _isRopeAction; }
}

//お仕事--------------

//探偵が指定の位置まで歩いて戻る機能の実装
//初期地からの距離を計算して近かったら歩いて、遠かったらロープアクションで戻るようにする
//ロープアクション中に探偵がアニメーションをするようにする