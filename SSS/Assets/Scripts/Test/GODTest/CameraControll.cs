using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==カメラの動きを管理するクラス
//
//使用方法：カメラにアタッチ
public class CameraControll : MonoBehaviour {
	public const float DEFAULT_ORTHOGRAPHIC_SIZE = 7.65f;	//カメラの初期orthographicSize
	Camera _camera;

	// Use this for initialization
	void Start () {
		_camera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//===================================================
	//public関数

	//--カメラをズームインする関数
	public void Zoom( float zoomvalue ) {
		_camera.orthographicSize -= zoomvalue;
	}

	//--カメラをtargetに向かせる処理
	public void LookTarget( Transform target ) {
		Vector3 targetPos = target.position;
		_camera.transform.position = new Vector3 (targetPos.x, targetPos.y, _camera.transform.position.z);
	}
	//===================================================
	//===================================================
}
