using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==動画再生システムを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class MoviePlaySystem : MonoBehaviour {
	[SerializeField] Point _point = null;
	[SerializeField] Bar _bar = null;
	Anim _anim;

	//Vector3 _mousePos;
	Vector3 _pos;
	[SerializeField] float _maxTime = 60.0f;
	[SerializeField] float _time = 60.0f;
	[SerializeField] float _leftPos = 0;
	[SerializeField] float _rightPos = 0;
	float _maxPos;
	float _posParSecond;
	bool _stop;

	// Use this for initialization
	/*void Awake( ) {
		//_mousePos = new Vector3( 0, 0, 0 );
		_pos = new Vector3( 0, 0, 0 );
		_time = _maxTime;
		_maxPos = _rightPos - _leftPos;
		_posParSecond = _maxPos / _maxTime;
		_stop = false;

		_point.SetPos( _leftPos, _rightPos );
	}*/

	void Start( ) {
		_pos = new Vector3( 0, 0, 0 );
		_time = _maxTime;
		_maxPos = _rightPos - _leftPos;
		_posParSecond = _maxPos / _maxTime;
		_stop = false;

		_point.SetMoveRange( _leftPos, _rightPos );
	}
	
	// Update is called once per frame
	void Update( ) {
		if ( Input.GetMouseButtonDown( 0 ) ) {	//マウス押したかどうか
			Vector3 mousePos;
			mousePos = GetMouse( );
			mousePos.y = 0;
			mousePos.z = 0;
			if ( ( int )mousePos.y == 0 ) {
				_point.MovePosition( mousePos );
				Vector3 nowBarSize = Vector3.one;
				nowBarSize.x = ( _point.transform.position.x - _bar.transform.position.x ) / _maxPos;
				_bar.MoveBarSize( nowBarSize );
			}
		}



	}


	Vector3 GetMouse( ) {	return Camera.main.ScreenToWorldPoint( Input.mousePosition ); }


	public void StopAndPlayTime( ) {
		if ( !_stop ) {
			_stop = true;
		} else {
			_stop = false;
		}
	}


	public void FB_Time( ) {
		_time += 5;
		if ( _time >= _maxTime ) _time = _maxTime + 0.5f;	//0.5fは戻した直後にシークバーがすぐに動くのを防ぐため
		//_pos.x -= _posParSeconds * 5;
	}


	public void FF_Time( ) {
		_time -= 5;
		if ( _time <= 0 ) _time = 0;
		//_pos.x += _posParSeconds * 5;
	}
	
}
