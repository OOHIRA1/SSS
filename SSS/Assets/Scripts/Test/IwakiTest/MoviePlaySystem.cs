using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==動画再生システムを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class MoviePlaySystem : MonoBehaviour {
	[SerializeField]Point _point = null;
	[SerializeField]Bar   _bar   = null;
	[SerializeField]Movie[ ] _movie = new Movie[ 1 ];
								
	[SerializeField] float _maxTime  = 60.0f;		//最大再生時間

	float _forcedPosParSecond;					    //pointの1秒当たりに進む座標
	bool _stop;										//再生されているか

	[SerializeField] int _barTouchDown = 180;		//バーのタッチできる領域
	[SerializeField] int _barTouchUp = 240;

	bool _isOperation;								//操作可能かどうか
	bool _isFixed;									//固定するかどうか
	bool _isReturn;									//場所を戻すかどうか

	Vector2 _pointPos;
	Vector2 _prePointPos;

	// Use this for initialization
	void Start( ) {
		_pointPos    = Vector2.zero;
		_prePointPos = Vector2.zero;
		_forcedPosParSecond = _point.GetMaxRange( ) / _maxTime;
		_stop = true;
		_isOperation = true;
		_isFixed = false;
		_isReturn = false;
	}
	
	// Update is called once per frame
	void Update( ) {
	
		Fixed( );

		if ( _isOperation ) {

			PointUpdate( );

			BarUpdate( );

			MovieUpdate( );

		}
			
        //Debug.Log(_stop );
	}

	//--ポイントの座標を更新する関数
	void PointUpdate( ) {
		 _pointPos = _point.GetTransform( );

		
			
		if ( !_stop ) {
			_pointPos.x = _point.GetTransform( ).x + ( _forcedPosParSecond * Time.deltaTime );
			//Debug.Log( _pointPos );
		}

		

		if ( Input.GetMouseButtonDown( 0 ) ) {	//マウスを押したかどうか
			Vector2 mousePos = Vector2.zero;
			mousePos = GetMouse( );
			if ( mousePos.y >= _barTouchDown && mousePos.y <= _barTouchUp ) {
				_pointPos.x = mousePos.x;
			}
		}
		_point.MovePosition( _pointPos );
	}

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
			_point.MovePosition( _pointPos );
		}

	}

	Vector2 GetMouse( ) { return Input.mousePosition; }

	//=======================================================================
	//public関数

	//停止しているかどうかを取得する
    public bool GetStop( ) { return _stop; }

	//操作可能にするか不可にするかを切り替える
	public void SetOperation( bool value ) { _isOperation = value; }

	public void SetFixed( bool value ) { _isFixed = value; }


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

    //時間を取得する(バーの大きさを時間に変換)
    public float MoviTime( ) { return _bar.GetBarScale( ).x * _maxTime; }

	//=======================================================================
	//=======================================================================
}
