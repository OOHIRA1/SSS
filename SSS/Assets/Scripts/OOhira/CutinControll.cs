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
	[SerializeField] float[] _cutinImageMovingTime = new float[2];			//カットイン画像の移動時間(速さを複数段階設定できるように配列化)
	[SerializeField] Vector2[] _destination = new Vector2[2];				//カットイン画像の目的地(速さを複数段階設定できるように配列化)
	//======================================================================================================================================

	bool _finishFlag;							//カットイン演出を終了したかどうかのフラグ
	[SerializeField] Vector3 _finishSpeedPerSecond = new Vector3(0, 0.1f, 0);	//終了演出時の画像の縮小速度


	//========================================
	//ゲッター
	public bool GetFinishFlag() { return _finishFlag; }
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
		Vector2[] cutinImageVelocity = {	//カットイン画像の速度
			(_destination [0] - _cutinImageFirstAnchoredPosition) / _cutinImageMovingTime [0],
			(_destination [1] - _destination[0]) / _cutinImageMovingTime [1]
		};
		float cutinImageMaxMovingTime = _cutinImageMovingTime [0] + _cutinImageMovingTime [1];	//カットイン画像の最大移動時間
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
			if ( time < _cutinImageMovingTime[0] ) {
				_cutinImageRectTransform.anchoredPosition += cutinImageVelocity[0] * Time.deltaTime;
			} else if ( time < cutinImageMaxMovingTime ) {
				_cutinImageRectTransform.anchoredPosition += cutinImageVelocity[1] * Time.deltaTime;
			} else {
				_cutinImageRectTransform.anchoredPosition = _destination [1];
			}
			//---------------------------------------------------------------------------------------
			time += Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		} while( time < movingTime );
		_backgroundImagesRectTransform [0].anchoredPosition = destination;
		_cutinImageRectTransform.anchoredPosition = _destination [1];
		yield return new WaitForSeconds (1f);	//カットインをしばらくするための時間稼ぎ
		SetFinishFlag (true);
	}


	//--カットインをする関数Part2(コルーチン)
	IEnumerator CutinPart2() {
		float time = 0;	//カットインしている時間
		Vector2 destination = new Vector2 (-_backgroundFirstAnchoredPosition.x, _backgroundFirstAnchoredPosition.y);	//背景画像の目的地
		Vector2 backgroundVelocity = (destination - _backgroundFirstAnchoredPosition) / _backgroundMovingTime;	//背景画像の速度
		Vector2[] cutinImageVelocity = {	//カットイン画像の速度
			(_destination [0] - _cutinImageFirstAnchoredPosition) / _cutinImageMovingTime [0],
			(_destination [1] - _destination[0]) / _cutinImageMovingTime [1]
		};
		float cutinImageMaxMovingTime = _cutinImageMovingTime [0] + _cutinImageMovingTime [1];	//カットイン画像の最大移動時間
		//float movingTime = ( cutinImageMaxMovingTime > _backgroundMovingTime ) ? cutinImageMaxMovingTime : _backgroundMovingTime;	//カットインの時間
		do {
			//背景画像の演出------------------------------------------------------------------------
			switch(_reverseFlag) {
			case true:
				if (destination.x - _backgroundImagesRectTransform [_movingImageIndex].anchoredPosition.x < -0.5f) {
					for (int i = 0; i < _backgroundImagesRectTransform.Length; i++) {
						_backgroundImagesRectTransform [i].anchoredPosition += backgroundVelocity * Time.deltaTime;
					}
				} else {
					_backgroundImagesRectTransform [_movingImageIndex].anchoredPosition = _backgroundImagesRectTransform [(_movingImageIndex + 1) % _backgroundImagesRectTransform.Length].anchoredPosition + new Vector2 (_backgroundImagesRectTransform[_movingImageIndex].sizeDelta.x, 0);
					_movingImageIndex = ++_movingImageIndex % _backgroundImagesRectTransform.Length;
				}
				break;
			case false:
				if (-_backgroundFirstAnchoredPosition.x - _backgroundImagesRectTransform [_movingImageIndex].anchoredPosition.x > 0.5f) {
					for (int i = 0; i < _backgroundImagesRectTransform.Length; i++) {
						_backgroundImagesRectTransform [i].anchoredPosition += backgroundVelocity * Time.deltaTime;
					}
				} else {
					_backgroundImagesRectTransform [_movingImageIndex].anchoredPosition = _backgroundImagesRectTransform [(_movingImageIndex + 1) % _backgroundImagesRectTransform.Length].anchoredPosition + new Vector2 (-_backgroundImagesRectTransform[_movingImageIndex].sizeDelta.x, 0);
					_movingImageIndex = ++_movingImageIndex % _backgroundImagesRectTransform.Length;
				}
				break;
			}
			//-------------------------------------------------------------------------------------

			//カットイン画像の演出--------------------------------------------------------------------
			if ( time < _cutinImageMovingTime[0] ) {
				_cutinImageRectTransform.anchoredPosition += cutinImageVelocity[0] * Time.deltaTime;
			} else if ( time < cutinImageMaxMovingTime ) {
				_cutinImageRectTransform.anchoredPosition += cutinImageVelocity[1] * Time.deltaTime;
			} else {
				_cutinImageRectTransform.anchoredPosition = _destination [1];
			}
			//---------------------------------------------------------------------------------------
			time += Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		} while( !_finishFlag );
		_backgroundImagesRectTransform [0].anchoredPosition = destination;
		_cutinImageRectTransform.anchoredPosition = _destination [1];
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
