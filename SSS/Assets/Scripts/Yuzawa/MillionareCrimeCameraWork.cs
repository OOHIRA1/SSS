using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillionareCrimeCameraWork : MonoBehaviour {
	[SerializeField]CameraControll _millionareCamera = null;
	[SerializeField]Camera _camera = null;
    [SerializeField]float _defaultOrthgraphicSize = 4f;    //カメラ拡大した時のカメラサイズ
    [SerializeField]float _time;                           //Millionareの殺害アニメーションの時間を見てる
	[SerializeField]float _distance;                       //カメラの初期位置から最終位置までの距離
    [SerializeField]int _count;                             //何フレーム処理したか見るもの

    int Number_of_times = 360; //何回ズーム処理を行うか
	Vector3 _nowPos;          //現在のカメラのPosition
    float SPEED = -0.01f;   //カメラのズームしてる時のSPEED

    // Use this for initialization
    void Start ( ) {
        _count = 0;
        _time = 0; 
		_nowPos = _camera.transform.position; 
		_distance = _nowPos.y - (-5.5f);	//移動距離の算出
		//pos = new Vector3 (1, 1, 1);
        //_millionareCamera.Zoom( _camera.orthographicSize + _defaultOrthgraphicSize);
		//Vector3 _nowPos
		//pos.y = _nowPos + a;
	}

	// Update is called once per frame
	void Update ( ) {

        //時間が0の時、初期位置と初期サイズを決めてる------------------------------------------
        if (_time == 0) {
			_millionareCamera.LookTarget(new Vector3(0f, -5.5f, _camera.transform.position.z));
			_millionareCamera.ChangeOrthographicSize(_defaultOrthgraphicSize);
        }

        //--------------------------------------------------------------------------------------

        //365回カメラの拡大処理を行いながらカメラのPositionを変更-------------------------------
        if ( _count < Number_of_times ) {
            _millionareCamera.Zoom( SPEED );
			_camera.transform.Translate(0, _distance / Number_of_times, 0);
			//transform.Translate(0f, , 0f);
			//_camera.transform.Translate(0, 0.01f, 0);

        } else {
        _millionareCamera.ChangeOrthographicSize(CameraControll.DEFAULT_ORTHOGRAPHIC_SIZE);
        _camera.transform.position = _nowPos;
    }

        //--------------------------------------------------------------------------------------
        _count++;
        _time += Time.deltaTime;



    }
}
