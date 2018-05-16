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

	float _posParSecond;							//pointの1秒当たりに進む座標
	bool _stop;										//再生されているか

	float time;//デバッグ
	[SerializeField] int _barTouchDown = 180;
	[SerializeField] int _barTouchUp = 240;

	// Use this for initialization
	void Start( ) {
		_posParSecond = _point.GetMaxRange( ) / _maxTime;
		_stop = false;
		time = _maxTime;
	}
	
	// Update is called once per frame
	void Update( ) {
		time -= Time.deltaTime;
			 
		//Debug.Log( time );
		PointUpdate( );

		BarUpdate( );

		MovieUpdate( );

	}

	//--ポイントの座標を更新する関数
	void PointUpdate( ) {
		Vector2 pointPos = _point.GetTransform( ); 
		if ( !_stop ) pointPos.x = _point.GetTransform ().x + ( _posParSecond * Time.deltaTime );

		if ( Input.GetMouseButtonDown( 0 ) ) {	//マウスを押したかどうか
			Vector2 mousePos = Vector2.zero;
			mousePos = GetMouse( );
			if ( mousePos.y >= _barTouchDown && mousePos.y <= _barTouchUp ) {
				pointPos.x = mousePos.x;
			}
		}
		_point.MovePosition( pointPos );
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

	Vector2 GetMouse( ) { return Input.mousePosition; }

	//=======================================================================
	//public関数
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
		pointPos.x -= _posParSecond * 5;
		_point.MovePosition ( pointPos );  //0.5fは戻した直後にシークバーがすぐに動くのを防ぐため
        _stop = false;
	}

	//--5秒早送り(FastForward)をする関数
	public void FastForward( ) {
		Vector3 pointPos = _point.GetTransform( );
		pointPos.x += _posParSecond * 5;
		_point.MovePosition ( pointPos );
        _stop = false;
	}
	//=======================================================================
	//=======================================================================
}
