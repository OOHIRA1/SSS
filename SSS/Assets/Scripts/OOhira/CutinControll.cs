using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==カットイン機能を管理するクラス
//
//使用方法：カットイン時にアクティブなゲームオブジェクトにアタッチ

public class CutinControll : MonoBehaviour {
	[SerializeField] bool _reverseFlag = false;	//逆からカットイン演出するかどうかのフラグ(通常：左→右)

	//背景画像情報=================================================================================================================
	[SerializeField] RectTransform[] _backgroundImagesRectTransform = new RectTransform[2];	//背景画像のRectTransform
	int _movingImageIndex;																	//現在動いている背景画像の配列番号
	Vector2 _backgroundFirstAnchoredPosition;												//背景画像の初期位置
	[SerializeField] float _backgroundMovingTime = 1.5f;									//背景の移動時間
	//============================================================================================================================

	//カットイン画像情報====================================================================================================================
	[SerializeField] RectTransform _cutinImageRectTransform = null;			//カットイン画像のRectTransform
	Vector2 _cutinImageFirstAnchoredPosition;								//カットイン画像の初期位置
	[SerializeField] float[] _cutinImageMovingTime = new float[3];			//カットイン画像の移動時間(速さを複数段階設定できるように配列化)
	[SerializeField] Vector2[] _destination = new Vector2[3];				//カットイン画像の目的地(速さを複数段階設定できるように配列化)
	//======================================================================================================================================

	bool _finishFlag;							//カットイン演出を終了したかどうかのフラグ
	[SerializeField] Vector3 _finishSpeedPerSecond = new Vector3(0, 0.1f, 0);	//終了演出時の画像の縮小速度
	bool _cutinMoveFinishedFlag;				//カットインの動きを終了したかどうかのフラグ


	//========================================
	//ゲッター
	public bool GetFinishFlag() { return _finishFlag; }
	public bool GetCutinMoveFinishedFlag() { return _cutinMoveFinishedFlag;	}
	//========================================
	//========================================


	//========================================
	//セッター
	public void SetFinishFlag ( bool x ) { _finishFlag = x; }
	//========================================
	//========================================

	// Use this for initialization
	void Start () {
		_movingImageIndex = 0;
		_backgroundFirstAnchoredPosition = _backgroundImagesRectTransform [0].anchoredPosition;
		_cutinImageFirstAnchoredPosition = _cutinImageRectTransform.anchoredPosition;
		_finishFlag = false;
		_cutinMoveFinishedFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		//デバッグ用--------------------------------
		if (Input.GetKeyDown (KeyCode.A))
			StartCutin();
		if (Input.GetKeyDown (KeyCode.B))
			//SetFinishFlag ( true );
			FinishCutin();
		if (Input.GetKeyDown (KeyCode.Z))
			StartCutinPart2();
		//------------------------------------------

	}


	//=========================================================================
	//public関数
	//--カットインをする関数
	public void StartCutin() {
		StartCoroutine ("Cutin");
	}

	//--カットインをする関数Part2
	public void StartCutinPart2() {
		StartCoroutine ("CutinPart2");
	}

	//--カットイン演出を終了する関数
	public void FinishCutin() {
		StartCoroutine ("Finish");
	}
	//=========================================================================
	//=========================================================================


	//--カットインをする関数(コルーチン)
	IEnumerator Cutin() {
		float time = 0;	//カットインしている時間
		Vector2 destination = new Vector2 (0, _backgroundFirstAnchoredPosition.y);	//背景画像の目的地
		Vector2 backgroundVelocity = (destination - _backgroundFirstAnchoredPosition) / _backgroundMovingTime;	//背景画像の速度
		Vector2[] cutinImageVelocity = {	//カットイン画像の速度	//要素数：_cutinImageMovingTime.Length, 各要素をfor文で上手く初期化出来そう(その方が汎用性あり)
			(_destination [0] - _cutinImageFirstAnchoredPosition) / _cutinImageMovingTime [0],
			(_destination [1] - _destination[0]) / _cutinImageMovingTime [1],
			(_destination [2] - _destination[1]) / _cutinImageMovingTime [2]
		};
		float cutinImageMaxMovingTime = 0;	//カットイン画像の最大移動時間
		for( int i = 0; i < _cutinImageMovingTime.Length; i++ ) {
			cutinImageMaxMovingTime += _cutinImageMovingTime [i];
		}
		float movingTime = ( cutinImageMaxMovingTime > _backgroundMovingTime ) ? cutinImageMaxMovingTime : _backgroundMovingTime;	//カットインの時間
		do {
			//背景画像の演出------------------------------------------------------------------------
			if ( time < _backgroundMovingTime ) {
				_backgroundImagesRectTransform[0].anchoredPosition += backgroundVelocity * Time.deltaTime;
			} else {
				_backgroundImagesRectTransform [0].anchoredPosition = destination;
			}
			//-------------------------------------------------------------------------------------

			//カットイン画像の演出--------------------------------------------------------------------
			for ( int i = 0; i < _cutinImageMovingTime.Length; i++ ) {
				bool conditionalExpression = false;	//条件式
				if ( i == 0 ) {
					conditionalExpression = ( time <= _cutinImageMovingTime[i] );
				} else {
					float t = 0;
					for ( int j = 0; j < i; j++ ) {
						t += _cutinImageMovingTime[j];
					}
					conditionalExpression = ( time > t && time <= t + _cutinImageMovingTime[i] );
				}
				if ( conditionalExpression ) {
					_cutinImageRectTransform.anchoredPosition += cutinImageVelocity[i] * Time.deltaTime;
				}
			}
			if ( time > cutinImageMaxMovingTime ) {
				_cutinImageRectTransform.anchoredPosition = _destination [_destination.Length - 1];
			}
			/*	//最高1回の条件式評価で済む(処理速度重視!?)	※上記は汎用性重視！
			if ( time < _cutinImageMovingTime[0] ) {
				_cutinImageRectTransform.anchoredPosition += cutinImageVelocity[0] * Time.deltaTime;
			} else if ( time < _cutinImageMovingTime[0] + _cutinImageMovingTime[1] ) {
				_cutinImageRectTransform.anchoredPosition += cutinImageVelocity[1] * Time.deltaTime;
			} else if ( time < cutinImageMaxMovingTime ) {
				_cutinImageRectTransform.anchoredPosition += cutinImageVelocity[2] * Time.deltaTime;
			} else {
				_cutinImageRectTransform.anchoredPosition = _destination [1];
			}
			*/
			//※後でCutinPart2の方とまとめて関数化してもよい

			//---------------------------------------------------------------------------------------
			time += Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		} while( time < movingTime );
		_backgroundImagesRectTransform [0].anchoredPosition = destination;
		_cutinImageRectTransform.anchoredPosition = _destination [_destination.Length - 1];
		yield return new WaitForSeconds (1f);	//カットインをしばらくするための時間稼ぎ
		_cutinMoveFinishedFlag = true;
	}


	//--カットインをする関数Part2(コルーチン)
	IEnumerator CutinPart2() {
		float time = 0;	//カットインしている時間
		Vector2 destination = new Vector2 (-_backgroundFirstAnchoredPosition.x, _backgroundFirstAnchoredPosition.y);	//背景画像の目的地
		Vector2 backgroundVelocity = (destination - _backgroundFirstAnchoredPosition) / _backgroundMovingTime;	//背景画像の速度
		Vector2[] cutinImageVelocity = new Vector2[_cutinImageMovingTime.Length];	//カットイン画像の速度	//要素数：_cutinImageMovingTime.Length, 各要素をfor文で上手く初期化出来そう(その方が汎用性あり)
		for (int i = 0; i < _cutinImageMovingTime.Length; i++) {
			if (i == 0) {
				cutinImageVelocity [i] = (_destination [i] - _cutinImageFirstAnchoredPosition) / _cutinImageMovingTime [i];
			} else {
				cutinImageVelocity [i] = (_destination [i] - _destination [i - 1]) / _cutinImageMovingTime [i];
			}
		}
		float cutinImageMaxMovingTime = 0;	//カットイン画像の最大移動時間
		for( int i = 0; i < _cutinImageMovingTime.Length; i++ ) {
			cutinImageMaxMovingTime += _cutinImageMovingTime [i];
		}
		//float movingTime = ( cutinImageMaxMovingTime > _backgroundMovingTime ) ? cutinImageMaxMovingTime : _backgroundMovingTime;	//カットインの時間

		do {
			//背景画像の演出------------------------------------------------------------------------
			bool condition = true;//条件
			float diffX = destination.x - _backgroundImagesRectTransform [_movingImageIndex].anchoredPosition.x;//目的地と現在のx座標の差
			int lastMoveImageIndex = (_movingImageIndex + _backgroundImagesRectTransform.Length - 1) % _backgroundImagesRectTransform.Length;//最右端or最左端で動いている背景画像の配列番号
			Vector2 vec = Vector2.zero;
			switch(_reverseFlag) {
			case true:
				condition = diffX < -0.5f;
				vec.x = _backgroundImagesRectTransform[_movingImageIndex].sizeDelta.x;
				break;
			case false:
				condition = diffX > 0.5f;
				vec.x = -_backgroundImagesRectTransform[_movingImageIndex].sizeDelta.x;
				break;
			}
			if (condition) {
				for (int i = 0; i < _backgroundImagesRectTransform.Length; i++) {
					_backgroundImagesRectTransform [i].anchoredPosition += backgroundVelocity * Time.deltaTime;
				}
			} else {
				_backgroundImagesRectTransform [_movingImageIndex].anchoredPosition = _backgroundImagesRectTransform [lastMoveImageIndex].anchoredPosition + vec;
				_movingImageIndex = ++_movingImageIndex % _backgroundImagesRectTransform.Length;
			}
			//-------------------------------------------------------------------------------------

			//カットイン画像の演出--------------------------------------------------------------------
			for ( int i = 0; i < _cutinImageMovingTime.Length; i++ ) {
				bool conditionalExpression = false;	//条件式
				if ( i == 0 ) {
					conditionalExpression = ( time <= _cutinImageMovingTime[i] );
				} else {
					float t = 0;
					for ( int j = 0; j < i; j++ ) {
						t += _cutinImageMovingTime[j];
					}
					conditionalExpression = ( time > t && time <= t + _cutinImageMovingTime[i] );
				}
				if ( conditionalExpression ) {
					_cutinImageRectTransform.anchoredPosition += cutinImageVelocity[i] * Time.deltaTime;
				}
			}
			if ( time > cutinImageMaxMovingTime ) {
				_cutinImageRectTransform.anchoredPosition = _destination [_destination.Length - 1];
			}
			//---------------------------------------------------------------------------------------
			if ( time > cutinImageMaxMovingTime + 1f ) {	//カットインをしばらくするための時間稼ぎ
				_cutinMoveFinishedFlag = true;
			}
			time += Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		} while( !_finishFlag );
		_backgroundImagesRectTransform [0].anchoredPosition = destination;
		_cutinImageRectTransform.anchoredPosition = _destination [_destination.Length - 1];
	}


	//--カットイン演出を終了する関数(コルーチン)
	IEnumerator Finish() {
		float time = 0;
		do {
			for (int i = 0; i < _backgroundImagesRectTransform.Length; i++) {
				_backgroundImagesRectTransform [i].localScale -= _finishSpeedPerSecond * Time.deltaTime; 
			}
			_cutinImageRectTransform.localScale -= _finishSpeedPerSecond * Time.deltaTime;
			time += Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		} while( _finishSpeedPerSecond.y * time < 1f );
		for (int i = 0; i < _backgroundImagesRectTransform.Length; i++) {
			_backgroundImagesRectTransform [i].localScale = Vector3.zero; 
		}
		_cutinImageRectTransform.localScale = Vector3.zero;
		SetFinishFlag ( true );
	}
}
