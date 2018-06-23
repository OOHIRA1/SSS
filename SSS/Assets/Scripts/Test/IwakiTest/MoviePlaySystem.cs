using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==動画再生システムを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class MoviePlaySystem : MonoBehaviour {
	[ SerializeField ]Point _point = null;
	[ SerializeField ]Bar   _bar   = null;
	[ SerializeField ]Movie[ ] _movie = new Movie[ 1 ];
	[ SerializeField ] StartButton _startAndStopButton = null;
								
	[SerializeField] float _maxTime  = 60.0f;		//最大再生時間
	[SerializeField] int _barTouchDown = 180;		//バーのタッチできる領域
	[SerializeField] int _barTouchUp = 240;

	float _forcedPosParSecond;					    //pointの1秒当たりに進む座標

	bool _stop;										//再生されているか
	bool _isOperation;								//操作可能かどうか
	bool _isFixed;									//固定するかどうか
	bool _isReturn;									//場所を戻すかどうか
	bool _isMouseMove;								//マウス操作が出来るかどうか

    bool _onlyFlame;                                  //一フレームだけしたい処理

	Vector2 _pointPos;
	Vector2 _prePointPos;
    Vector2 _FastTouchPos;                            //タッチした最初のpos

	// Use this for initialization
	void Start( ) {
		_pointPos    = Vector2.zero;
		_prePointPos = Vector2.zero;
        _FastTouchPos = Vector2.zero;
		_forcedPosParSecond = _point.GetMaxRange( ) / _maxTime;
		_stop = true;
		_isOperation = true;
		_isFixed = false;
		_isReturn = false;
		_isMouseMove = true;
        _onlyFlame = false; 
	}
	
	// Update is called once per frame
	void Update( ) {
	
		Fixed( );

		if ( _isOperation ) {
			PointUpdate( );     //操作不能だったらポイントを更新しない
		} else {
			TouchStateReset( );	//タッチ状態をリセット（タッチ状態のまま更新しなくなったとき用）
		}
			
			BarUpdate( );

			MovieUpdate( );

			StopTime( );


	}

	//--ポイントの座標を更新する関数----------------------------------------------------------------------------------
	void PointUpdate( ) {
		 _pointPos = _point.GetTransform( );
			

		if ( !_stop ) {             //動画が再生されていたら
			_pointPos.x = _point.GetTransform( ).x + ( _forcedPosParSecond * Time.deltaTime );
		}

		
		if ( _isMouseMove ) {       //マウス操作が可能だったら

            //マウスを押したら--------------------------------------------------------------------------------------------------------
			if ( Input.GetMouseButton( 0 ) ) {	//マウスを押したかどうか

				Vector2 mousePos = Vector2.zero;
				mousePos = GetMouse( );

                //ボタンが押されたときの最初のフレームの座標を取得する-----------------------
                if ( !_onlyFlame ) {
                    _FastTouchPos = mousePos;
					
                    _onlyFlame = true;      //１フレームだけ取得したら処理できないようにする
                }
                //----------------------------------------------------------------------------

                //タッチ操作処理--------------------------------------------------------------
                if ( _FastTouchPos.y >= _barTouchDown && _FastTouchPos.y <= _barTouchUp ) {     //押したときの最初の位置がタッチ領域内だったら処理する(ホールド対応)
				    //if ( mousePos.y >= _barTouchDown && mousePos.y <= _barTouchUp ) {           //押している場所がタッチ領域だったら処理(ホールド対応)
				    	_pointPos.x = mousePos.x;
						
                    //}
				}
                //-----------------------------------------------------------------------------
			}
            //---------------------------------------------------------------------------------------------

		}

		 //マウスを放したら処理-----------------
            if ( Input.GetMouseButtonUp( 0 ) ) {
                TouchStateReset( );
            }
         //--------------------------------------

		_point.MovePosition( _pointPos );
	}
    //----------------------------------------------------------------------------------------------------------------------------------

	//--バーのスケールを更新する関数
	void BarUpdate( ) {
		Vector2 nowBarSize = new Vector2( 0, 1 );
		nowBarSize.x = ( _point.GetTransform( ).x - _bar.GetTransform( ).x ) / _point.GetMaxRange( );
		_bar.MoveScale( nowBarSize );
	}

	//--ムービーの再生位置を更新する関数
	void MovieUpdate( ) {
		float movieStartTime = _bar.GetBarScale( ).x;

		for ( int i = 0; i < _movie.Length; i++ ) {
			_movie[ i ].ChangeMovieStartTime( movieStartTime );
		}

	}

	//シークバーを特定の位置で固定する関数-----------------------------------
	void Fixed( ) {
		if ( !_isFixed ) {
			if ( _isReturn ) {
				_pointPos.x = _prePointPos.x;
				_point.MovePosition( _pointPos );
				_isReturn = false;
			} else {
				return;
			}
		}

		if ( _isFixed ) {
			if ( !_isReturn ) {
				_prePointPos.x = _point.GetTransform( ).x;
				_isReturn = true;
			}

			_pointPos.x = 2000f;
			_pointPos.y = _point.GetTransform ().y;		//無理やり高さを合わせた。一回非アクティブにするせいか次にアクティブになる時にこの処理をするとｙ軸の値がバグる
			_point.MovePosition( _pointPos );
		}

	}
	//----------------------------------------------------------------------

	//60秒を超えたら一時停止する----------------------
	void StopTime( ) {
		if ( MovieTime( ) >= 60f ) {
			_stop = true;
			_startAndStopButton.StartImageChange( );	//再生一時停止ボタンを画像を切り替える
		} 
	}
	//--------------------------------------------------

	//タッチ状態をリセットする-----------------------
	void TouchStateReset( ) {
		_FastTouchPos = Vector2.zero;
        _onlyFlame = false;
	}
	//------------------------------------------------


	Vector2 GetMouse( ) { return Input.mousePosition; }

	//=======================================================================
	//public関数

	//停止しているかどうかを取得する
    public bool GetStop( ) { return _stop; }

	//操作可能にするか不可にするかを切り替える
	public void SetOperation( bool value ) {
		_isOperation = value;
	}

	//動画を最後のほうで固定するかどうか
	public void SetFixed( bool value ) { _isFixed = value; }

	//マウスで操作できるかどうかを切り替える
	public void SetMouseMove( bool value ) { _isMouseMove = value; }

	//--再生・一時停止を処理する関数
	public void StopAndPlayTime( ) {
		if ( !_stop ) {
			_stop = true;
		} else {
			_stop = false;
		}
	}

	//--5秒巻き戻し(FastBackword)をする関数
	public void FastBackword( ) {
		Vector3 pointPos = _point.GetTransform( );
		pointPos.x -= _forcedPosParSecond * 5;
		_point.MovePosition ( pointPos );  //0.5fは戻した直後にシークバーがすぐに動くのを防ぐため
        _stop = false;
	}

	//--5秒早送り(FastForward)をする関数
	public void FastForward( ) {
		Vector3 pointPos = _point.GetTransform( );
		pointPos.x += _forcedPosParSecond * 5;
		_point.MovePosition ( pointPos );
        _stop = false;
	}

	//最初の再生位置に戻す
	public void MovieReset( ) {
        _point.MovePosition( _point.GetInitialPos( ) );
        BarUpdate( );                                           //MovieTimeと同一フレーム上で比較したいためにここでUpdateしている
        MovieUpdate( );
    }

    //時間を取得する(バーの大きさを時間に変換)
    public float MovieTime( ) { return _bar.GetBarScale( ).x * _maxTime; }


    //ムービーが終わっているか取得する（ポイントの座標が最後まで行っているか取得）------      //MovieTimeだと同一フレーム上で計算するとき不都合があるので作成
    public bool MovieTimeMax( ) {
        if ( _point.GetTransform( ).x >= _point.GetMaxRange( ) ) return true;

        return false;

    }
    //---------------------------------------------------------------------------------

	//=======================================================================
	//=======================================================================
}
