using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {
	Transform point_pos;
	Vector3 pos;
	Vector3 _mousePos;
	public float _maxTime = 60.0f;
	public float time;
	float seconds;
	bool stop;
	// Use this for initialization
	void Start( ) {
		point_pos = GetComponent< Transform >( );
		pos = new Vector3( -10.2f, 0, 0 );
		_mousePos = new Vector3 (0, 0, 0);
		time = _maxTime;
		seconds = 20.4f / time;
		stop = false;
	}
	
	// Update is called once per frame
	void Update( ) {
		
		if ( Input.GetMouseButtonDown( 0 ) ) {											//マウス押したかどうか
			_mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );			//クリックしたところのY座標が画像と重なっているか
			if ( ( int )_mousePos.y == 0 ) {
				_mousePos = Camera.main.ScreenToWorldPoint ( Input.mousePosition );	//クリックしたところと画像のサイズを合わすため？
				pos.x = _mousePos.x;
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

		if ( pos.x >= 10.2f ) pos.x = 10.2f;
		if ( pos.x <= -10.2f ) pos.x = -10.2f;
	
		float back_time = time;

		if ( time >= 0 && !stop ) time -= Time.deltaTime;

		point_pos.position = pos;

		if ( ( int )back_time > ( int )time ) pos.x += seconds;
		

	}

	public void StopAndPlayPoint( ) {
		if ( !stop ) {
			stop = true;
		} else {
			stop = false;
		}
	}

	public void FB_Point( ) {
		time += 5;
		if ( time >= _maxTime ) time = 60;
		pos.x -= seconds * 5;
		stop = false;
	}

	public void FF_Point( ) {
		time -= 5;
		if ( time <= 0 ) time = 0;
		pos.x += seconds * 5;
		stop = false;
	}
	
}
