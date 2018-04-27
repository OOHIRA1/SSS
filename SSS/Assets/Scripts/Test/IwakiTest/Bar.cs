using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {
	Transform _transform;	//バーのTransform
	public float _maxTime = 60f;	//最大時間
	public float time;		//デバック用再生時間表示
	Vector3 size;			//バーを動かす用ベクトル
	Vector3 _mousePos;		//マウスのクリックした位置
	float seconds;			//1秒あたりに伸びるバーの長さ
	bool stop;				//再生が止まっているかどうか

	// Use this for initialization
	void Start ( ) {
		_transform = GetComponent< Transform > ( );
		size = new Vector3( 0, 1, 1 );
		_mousePos = new Vector3 (0, 0, 0);
		_transform.localScale = size;
		time = _maxTime;
		seconds = 1.0f / _maxTime;
		stop = false;
	}

	// Update is called once per frame
	void Update ( ) {

		if ( Input.GetMouseButtonDown( 0 ) ) {											//マウス押したかどうか
			_mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );			//クリックしたところのY座標が画像と重なっているか
			if ( ( int )_mousePos.y == 0 ) {
				_mousePos = Camera.main.ScreenToViewportPoint( Input.mousePosition );	//クリックしたところと画像のサイズを合わすため？
				size.x = _mousePos.x;
				Debug.Log (_mousePos.x);

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

		if (size.x >= 1.0f) size.x = 1.0f;
		if (size.x <= 0) size.x = 0;

		float back_time = time;
		if ( time >= 0 && !stop ) time -= Time.deltaTime;

		_transform.localScale = size;

		if ( ( int )back_time > ( int )time ) size.x += seconds;
		
	}

	public void StopAndPlayBar( ) {
		if (!stop) {
			stop = true;
		} else {
			stop = false;
		}
	}

	//FastBackword
	public void FB_Bar( ) {
		time += 5;
		if ( time >= _maxTime ) time = _maxTime;
		size.x -= seconds * 5;
		stop = false;
	}

	//FastForward
	public void FF_Bar( ) {
		time -= 5;
		if ( time <= 0 ) time = 0;
		size.x += seconds * 5;
		stop = false;
	}

}
