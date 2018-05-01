using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {
	const float MAX_POINT_POS = 20.4f;	//点のX座標の最大移動値

	Transform _pointPos;
	Vector3 _pos;
	Vector3 _mousePos;
	public float _maxTime = 60.0f;
	public float _time;
	float _posParSeconds;
	bool _stop;
	// Use this for initialization
	void Start( ) {
		_pointPos = GetComponent< Transform >( );
		_pos = new Vector3( -10.2f, 0, 0 );
		_mousePos = new Vector3 (0, 0, 0);
		_time = _maxTime;
		_posParSeconds = MAX_POINT_POS / _time;
		_stop = false;
	}
	
	// Update is called once per frame
	void Update( ) {
		
		if ( Input.GetMouseButtonDown( 0 ) ) {											//マウス押したかどうか
			_mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );			//クリックしたところのY座標が画像と重なっているか
			if ( ( int )_mousePos.y == 0 ) {
				_pos.x = _mousePos.x;
				Debug.Log ( _mousePos.x );

				//if ( _mousePos.x <= 0 )						     time = _maxTime;
				//if ( _mousePos.x > 0    && _mousePos.x <= 0.1f ) time = _maxTime / 10;
				//if ( _mousePos.x > 0.1f && _mousePos.x <= 0.2f ) time = _maxTime / 9;
				//if ( _mousePos.x > 0.2f && _mousePos.x <= 0.3f ) time = _maxTime / 8;
				//if ( _mousePos.x > 0.3f && _mousePos.x <= 0.4f ) time = _maxTime / 7;
				//if ( _mousePos.x > 0.4f && _mousePos.x <= 0.5f ) time = _maxTime / 6;
				//if ( _mousePos.x > 0.5f && _mousePos.x <= 0.6f ) time = _maxTime / 5;
				//if ( _mousePos.x > 0.6f && _mousePos.x <= 0.7f ) time = _maxTime / 4;
				//if ( _mousePos.x > 0.7f && _mousePos.x <= 0.8f ) time = _maxTime / 3;
				//if ( _mousePos.x > 0.8f && _mousePos.x <= 0.9f ) time = _maxTime / 2;
				//if ( _mousePos.x > 0.9f && _mousePos.x <= 1.0f ) time = _maxTime / 1;
				//if ( _mousePos.x >= 1.0f )                       time = 0;

			}
		}

		if ( _pos.x >= 10.2f ) _pos.x = 10.2f;
		if ( _pos.x <= -10.2f ) _pos.x = -10.2f;
	
		//float back_time = _time;
		_time = (10.2f - _pos.x) / MAX_POINT_POS * _maxTime;

		if (_time > 0 && !_stop) {
			_time -= Time.deltaTime;
			_pos.x += _posParSeconds * Time.deltaTime;
		}
		_pointPos.position = _pos;


		//if ( ( int )back_time > ( int )_time ) _pos.x += _posParSeconds;

	}

	public void StopAndPlayPoint( ) {
		if ( !_stop ) {
			_stop = true;
		} else {
			_stop = false;
		}
	}

	public void FB_Point( ) {
		_time += 5;
		if ( _time >= _maxTime ) _time = _maxTime + 0.5f;	//0.5fは戻した直後にシークバーがすぐに動くのを防ぐため
		_pos.x -= _posParSeconds * 5;
		//stop = false;
	}

	public void FF_Point( ) {
		_time -= 5;
		if ( _time <= 0 ) _time = 0;
		_pos.x += _posParSeconds * 5;
		//stop = false;
	}
	
}
