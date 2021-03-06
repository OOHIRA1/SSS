﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==カメラの動きを管理するクラス
//
//使用方法：カメラにアタッチ
public class CameraControll : MonoBehaviour {
	public const float DEFAULT_ORTHOGRAPHIC_SIZE = 7.6f;	//カメラの初期orthographicSize
	Camera _camera;

	// Use this for initialization
	void Start () {
		_camera = GetComponent<Camera> ();
	}

	//===================================================
	//public関数

	//--カメラをズームインする関数
	public void Zoom( float zoomvalue ) {
		_camera.orthographicSize -= zoomvalue;
	}

    //--カメラの正投影サイズを変更する関数
    public void ChangeOrthographicSize(float orthographicSize) {
        _camera.orthographicSize = orthographicSize;
    }

	//--カメラをtargetPositionに向かせる処理
	public void LookTarget( Vector3 targetPosition ) {
		_camera.transform.position = new Vector3 (targetPosition.x, targetPosition.y, _camera.transform.position.z);
	}
		

	//--カメラを徐々にズームインする関数
	public void Zoom( float zoomvalue, float time ) {
		StartCoroutine ( ZoomGradually(zoomvalue, time) );
	}

	//--カメラを徐々にズームインする関数(Coroutine)
	IEnumerator ZoomGradually( float zoomvaluePertime, float time ) {
		while (time > 0) {
			Zoom (zoomvaluePertime * Time.deltaTime);
			time -= Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}
	}
	//===================================================
	//===================================================
}
