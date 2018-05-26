using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==カットイン機能を管理するクラス
//
//使用方法：カットイン時にアクティブなゲームオブジェクトにアタッチ

public class CutinControll : MonoBehaviour {
	[SerializeField] bool _reverseFlag = false;	//逆からカットイン演出するかどうかのフラグ(通常：左→右)
	[SerializeField] RectTransform[] _imagesRectTransform = null;
	[SerializeField] Vector2 _repeatPosition = new Vector2(0,0);
	Vector2 _firstAnchoredPosition;
	int _movingImageIndex;
	[SerializeField] float _speed = 0;
	[SerializeField] RectTransform _cutinImageRectTransform = null;
	[SerializeField] bool _startFlag = false;
	[SerializeField] bool _cutinStartFlag = false;
	[SerializeField] bool _paturn2 = false;
	[SerializeField] float[] _cutinSpeed = null;

	float _time;		//時間を格納する変数
	[SerializeField] float _cutinBackgroundMovingTime = 1.5f;			//カットイン背景の移動時間
	[SerializeField] float[] _cutinImageMovingTime = new float[2];		//カットイン画像の移動時間(速さを複数段階設定できるように配列化)
	[SerializeField] Vector2[] _destination = new Vector2[2];			//カットイン画像の目的地(速さを複数段階設定できるように配列化)
	Vector2 _cutinImageFirstAnchoredPosition;


	// Use this for initialization
	void Start () {
		_movingImageIndex = 0;
		_firstAnchoredPosition = _imagesRectTransform [0].anchoredPosition;
		if (_reverseFlag) {
			_speed *= -1;
			for (int i = 0; i < _cutinSpeed.Length; i++) {
				_cutinSpeed [i] *= -1;
			}
			_repeatPosition *= -1;
		}
		_time = 0;
		_cutinImageFirstAnchoredPosition = _cutinImageRectTransform.anchoredPosition;
	}
	
	// Update is called once per frame
	void Update () {
		//デバッグ用--------------------------------
		if (Input.GetKeyDown (KeyCode.A))
			_startFlag = true;
		if (Input.GetKeyDown (KeyCode.Z))
			//_cutinStartFlag = true;
			StartCutinPart2();
		//------------------------------------------

		if (!_startFlag) return;
		_time += Time.deltaTime;
		if (!_paturn2) {
			if (!_reverseFlag) {
				if (-_firstAnchoredPosition.x - _imagesRectTransform [_movingImageIndex].anchoredPosition.x > 0.5f) {
					for (int i = 0; i < _imagesRectTransform.Length; i++) {
						_imagesRectTransform [i].anchoredPosition += new Vector2 (-_firstAnchoredPosition.x / _cutinBackgroundMovingTime * Time.deltaTime, 0);
					}
				} else {
					_imagesRectTransform [_movingImageIndex].anchoredPosition = _imagesRectTransform [(_movingImageIndex + 1) % _imagesRectTransform.Length].anchoredPosition + new Vector2 (_firstAnchoredPosition.x, 0);
					_movingImageIndex = ++_movingImageIndex % _imagesRectTransform.Length;
				}
			} else {
				if (-_firstAnchoredPosition.x - _imagesRectTransform [_movingImageIndex].anchoredPosition.x < -0.5f) {
					for (int i = 0; i < _imagesRectTransform.Length; i++) {
						_imagesRectTransform [i].anchoredPosition += new Vector2 (-_firstAnchoredPosition.x / _cutinBackgroundMovingTime * Time.deltaTime, 0);
					}
				} else {
					_imagesRectTransform [_movingImageIndex].anchoredPosition = _imagesRectTransform [(_movingImageIndex + 1) % _imagesRectTransform.Length].anchoredPosition + new Vector2 (_firstAnchoredPosition.x, 0);
					_movingImageIndex = ++_movingImageIndex % _imagesRectTransform.Length;
				}
			}
		} else {
			if (_time < _cutinBackgroundMovingTime) {
				_imagesRectTransform [0].anchoredPosition += new Vector2 (-_firstAnchoredPosition.x / _cutinBackgroundMovingTime * Time.deltaTime, 0);
			} else {
				_imagesRectTransform [_movingImageIndex].anchoredPosition = new Vector2 (0, _imagesRectTransform [_movingImageIndex].anchoredPosition.y);
			}
		}

		if (_cutinStartFlag) {
			if (_time < _cutinImageMovingTime [0]) {
				_cutinImageRectTransform.anchoredPosition += new Vector2 (Mathf.Abs (_destination [0].x - _cutinImageFirstAnchoredPosition.x) / _cutinImageMovingTime [0] * Time.deltaTime, 0);
			} else if (_time < _cutinImageMovingTime [1]) {
				_cutinImageRectTransform.anchoredPosition += new Vector2 (Mathf.Abs (_destination [1].x - _destination [0].x) / (_cutinImageMovingTime [1] - _cutinImageMovingTime [0]) * Time.deltaTime, 0);
			}/* else {
				_cutinImageRectTransform.anchoredPosition = new Vector2 (1024f - _cutinImageRectTransform.sizeDelta.x / 2, _cutinImageRectTransform.anchoredPosition.y);
			}*/
		}
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
	//=========================================================================
	//=========================================================================


	//--カットインをする関数(コルーチン)
	IEnumerator Cutin() {
		float time = 0;	//カットインしている時間
		Vector2 destination = new Vector2 (0, _firstAnchoredPosition.y);	//背景画像の目的地
		Vector2 backgroundVelocity = (destination - _firstAnchoredPosition) / _cutinBackgroundMovingTime;	//背景画像の速度
		Vector2[] cutinImageVelocity = {	//カットイン画像の速度
			(_destination [0] - _cutinImageFirstAnchoredPosition) / _cutinImageMovingTime [0],
			(_destination [1] - _destination[0]) / _cutinImageMovingTime [1]
		};
		float cutinImageMaxMovingTime = _cutinImageMovingTime [0] + _cutinImageMovingTime [1];	//カットイン画像の最大移動時間
		float movingTime = ( cutinImageMaxMovingTime > _cutinBackgroundMovingTime ) ? cutinImageMaxMovingTime : _cutinBackgroundMovingTime;	//カットインの時間
		do {
			//背景画像の演出------------------------------------------------------------------------
			if ( time < _cutinBackgroundMovingTime ) {
				_imagesRectTransform[0].anchoredPosition += backgroundVelocity * Time.deltaTime;
			} else {
				_imagesRectTransform [0].anchoredPosition = destination;
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
		_imagesRectTransform [0].anchoredPosition = destination;
		_cutinImageRectTransform.anchoredPosition = _destination [1];
	}


	//--カットインをする関数Part2(コルーチン)
	IEnumerator CutinPart2() {
		float time = 0;	//カットインしている時間
		Vector2 destination = new Vector2 (-_firstAnchoredPosition.x, _firstAnchoredPosition.y);	//背景画像の目的地
		Vector2 backgroundVelocity = (destination - _firstAnchoredPosition) / _cutinBackgroundMovingTime;	//背景画像の速度
		Vector2[] cutinImageVelocity = {	//カットイン画像の速度
			(_destination [0] - _cutinImageFirstAnchoredPosition) / _cutinImageMovingTime [0],
			(_destination [1] - _destination[0]) / _cutinImageMovingTime [1]
		};
		float cutinImageMaxMovingTime = _cutinImageMovingTime [0] + _cutinImageMovingTime [1];	//カットイン画像の最大移動時間
		float movingTime = ( cutinImageMaxMovingTime > _cutinBackgroundMovingTime ) ? cutinImageMaxMovingTime : _cutinBackgroundMovingTime;	//カットインの時間
		do {
			//背景画像の演出------------------------------------------------------------------------
			switch(_reverseFlag) {
			case true:
				if (destination.x - _imagesRectTransform [_movingImageIndex].anchoredPosition.x < -0.5f) {
					for (int i = 0; i < _imagesRectTransform.Length; i++) {
						_imagesRectTransform [i].anchoredPosition += backgroundVelocity * Time.deltaTime;
					}
				} else {
					_imagesRectTransform [_movingImageIndex].anchoredPosition = _imagesRectTransform [(_movingImageIndex + 1) % _imagesRectTransform.Length].anchoredPosition + new Vector2 (_imagesRectTransform[_movingImageIndex].sizeDelta.x, 0);
					_movingImageIndex = ++_movingImageIndex % _imagesRectTransform.Length;
				}
				break;
			case false:
				if (-_firstAnchoredPosition.x - _imagesRectTransform [_movingImageIndex].anchoredPosition.x > 0.5f) {
					for (int i = 0; i < _imagesRectTransform.Length; i++) {
						_imagesRectTransform [i].anchoredPosition += backgroundVelocity * Time.deltaTime;
					}
				} else {
					_imagesRectTransform [_movingImageIndex].anchoredPosition = _imagesRectTransform [(_movingImageIndex + 1) % _imagesRectTransform.Length].anchoredPosition + new Vector2 (-_imagesRectTransform[_movingImageIndex].sizeDelta.x, 0);
					_movingImageIndex = ++_movingImageIndex % _imagesRectTransform.Length;
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
		} while( true );
		_imagesRectTransform [0].anchoredPosition = destination;
		_cutinImageRectTransform.anchoredPosition = _destination [1];
	}


	//--カットインPart2での背景画像の演出をする関数
	void BackgroundMove() {
	}
}
