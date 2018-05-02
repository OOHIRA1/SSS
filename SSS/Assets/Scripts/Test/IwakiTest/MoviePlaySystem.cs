using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==動画再生システムを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class MoviePlaySystem : MonoBehaviour {
	[SerializeField]Point _point = null;
	[SerializeField]Bar   _bar   = null;
	[SerializeField]Movie _movie = null;
								
	[SerializeField] float _maxTime  = 60.0f;		//最大再生時間

	float _posParSecond;							//pointの1秒当たりに進む座標
	bool _stop;										//再生されているか

	// Use this for initialization
	void Start( ) {
		_posParSecond = _point.GetMaxRange( ) / _maxTime;
		_stop = false;
	}
	
	// Update is called once per frame
	void Update( ) {
		
		PointUpdate( );

		BarUpdate( );

		MovieUpdate( );

	}

	//--ポイントの座標を更新する関数
	void PointUpdate( ) {
		Vector3 pointPos = _point.GetTransform( ).position; 
		if ( !_stop ) pointPos.x = _point.GetTransform ().position.x + ( _posParSecond * Time.deltaTime );

		if ( Input.GetMouseButtonDown( 0 ) ) {	//マウスを押したかどうか
			Vector3 mousePos = Vector3.zero;
			mousePos = GetMouse( );
			if ( ( int )mousePos.y == ( int )_bar.GetTransform( ).position.y ) {
				pointPos.x = mousePos.x;
			}
		}
		_point.MovePosition( pointPos );
	}

	//--バーのスケールを更新する関数
	void BarUpdate( ) {
		Vector3 nowBarSize = Vector3.one;
		nowBarSize.x = ( _point.GetTransform( ).position.x - _bar.GetTransform( ).position.x ) / _point.GetMaxRange( );
		_bar.MoveScale( nowBarSize );
	}

	//--ムービーの再生位置を更新する関数
	void MovieUpdate( ) {
		float movieStartTime = _bar.GetTransform ( ).localScale.x;
		_movie.ChangeMovieStartTime ( movieStartTime );
	}

	Vector3 GetMouse( ) {	return Camera.main.ScreenToWorldPoint( Input.mousePosition ); }

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
		Vector3 pointPos = _point.GetTransform( ).position;
		pointPos.x -= _posParSecond * 5;
		_point.MovePosition ( pointPos );  //0.5fは戻した直後にシークバーがすぐに動くのを防ぐため
	}

	//--5秒早送り(FastForward)をする関数
	public void FastForward( ) {
		Vector3 pointPos = _point.GetTransform( ).position;
		pointPos.x += _posParSecond * 5;
		_point.MovePosition ( pointPos );
	}
	//=======================================================================
	//=======================================================================
}
