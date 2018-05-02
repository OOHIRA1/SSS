using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarTest : MonoBehaviour {
	const float MAX_BAR_SIZE = 20.4f;	//バーの長さの最大
	Transform _transform;	//バーのTransform
	public float _maxTime = 60f;	//最大時間
	public float _time;		//デバック用再生時間表示
	Vector3 _nowBarSize;	//現在のバーのサイズ
	Vector3 _mousePos;		//マウスのクリックした位置
	float _unitPerSeconds;	//1秒あたりに伸びるバーの長さ
	bool _stop;				//再生が止まっているかどうか
	[SerializeField]GameObject _point = null;

	// Use this for initialization
	void Start ( ) {
		_transform = GetComponent< Transform > ( );
		_nowBarSize = new Vector3( 0, 1, 1 );
		_mousePos = new Vector3 (0, 0, 0);
		_transform.localScale = _nowBarSize;
		_time = _maxTime;
		_unitPerSeconds = 1.0f / _maxTime;
		_stop = false;
	}

	// Update is called once per frame
	void Update ( ) {
		/*
		if ( Input.GetMouseButtonDown( 0 ) ) {											//マウス押したかどうか
			_mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );			//クリックしたところのY座標が画像と重なっているか
			Debug.Log( "マウスのWorldPoint" + _mousePos );
			if ( ( int )_mousePos.y == 0 ) {
				//_mousePos = Camera.main.ScreenToViewportPoint( Input.mousePosition );	//クリックしたところと画像のサイズの座標を合わすため？
				//_scale.x = _mousePos.x;
				_nowBarSize.x = ( _mousePos.x - _transform.position.x ) / MAX_BAR_SIZE;
				Debug.Log ( "マウスのViewportPoint" + _mousePos );
			}


		}*/
		_nowBarSize.x = ( _point.transform.position.x - _transform.position.x ) / MAX_BAR_SIZE;

		if ( _nowBarSize.x >= 1.0f ) _nowBarSize.x = 1.0f;
		if ( _nowBarSize.x <= 0 ) _nowBarSize.x = 0;


		float back_time = _time;								//計算前のtimeを入れる
		if ( _time >= 0 && !_stop ) _time -= Time.deltaTime;	//再生時間が残っていて　かつ　再生状態だったら　１秒ごとに時間を減らしていく

		_transform.localScale = _nowBarSize;							//サイズを変えていく

		if ( ( int )back_time > ( int )_time ) _nowBarSize.x += _unitPerSeconds;	//もし、計算前のtimeが計算後のtimeより値が大きかったら値を増やす。(1秒たったら値を増やす)
		
	}

	public void StopAndPlayBar( ) {
		if ( !_stop ) {
			_stop = true;
		} else {
			_stop = false;
		}
	}

	////FastBackword
	//public void FB_Bar( ) {
	//	_time += 5;
	//	if ( _time >= _maxTime ) _time = _maxTime;
	//	_nowBarSize.x -= _unitPerSeconds * 5;
	//	_stop = false;
	//}
	//
	////FastForward
	//public void FF_Bar( ) {
	//	_time -= 5;
	//	if ( _time <= 0 ) _time = 0;
	//	_nowBarSize.x += _unitPerSeconds * 5;
	//	_stop = false;
	//}

}
