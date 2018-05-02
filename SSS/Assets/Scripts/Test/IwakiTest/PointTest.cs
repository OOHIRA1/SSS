using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTest : MonoBehaviour {
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
		_mousePos = new Vector3 ( 0, 0, 0 );
		_time = _maxTime;
		_posParSeconds = MAX_POINT_POS / _maxTime;
		_stop = false;
	}
	
	// Update is called once per frame
	void Update( ) {
		
		if ( Input.GetMouseButtonDown( 0 ) ) {											//マウス押したかどうか
			_mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );			//クリックしたところのY座標が画像と重なっているか
			if ( ( int )_mousePos.y == 0 ) {
				_pos.x = _mousePos.x;
				Debug.Log ( _mousePos.x );
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
	}

	public void FF_Point( ) {
		_time -= 5;
		if ( _time <= 0 ) _time = 0;
		_pos.x += _posParSeconds * 5;
	}
	
}
