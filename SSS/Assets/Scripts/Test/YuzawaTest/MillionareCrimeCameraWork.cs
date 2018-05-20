using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillionareCrimeCameraWork : MonoBehaviour {
    [SerializeField]float _time;
    [SerializeField]float _zoomTime;
	[SerializeField]CameraControll _millionareCamera = null;
    [SerializeField]float _defaultOrthgraphicSize = 4f;
	[SerializeField]Camera _camera = null;
    [SerializeField]int _count;
	[SerializeField]float _distance;

    int Number_of_times = 365;
	Vector3 pos;
	Vector3 _nowPos;
    float SPEED = -0.01f;
	float a;
    // Use this for initialization
    void Start ( ) {
        _count = 0;
        //_millionareCamera.Zoom( _camera.orthographicSize + _defaultOrthgraphicSize);
        _time = 0; 
		//pos = new Vector3 (1, 1, 1);
		_nowPos = _camera.transform.position; 
		_distance = _nowPos.y - (-5.3f);	//移動距離の算出
		//Vector3 _nowPos
		//pos.y = _nowPos + a;
	}

	// Update is called once per frame
	void Update ( ) {

        if (_time == 0) {
			_millionareCamera.LookTarget(new Vector3(0f, -5.3f, _camera.transform.position.z));
			_millionareCamera.ChangeOrthographicSize(_defaultOrthgraphicSize);
        }

        if ( _count < Number_of_times ) {
            _millionareCamera.Zoom( SPEED );
			//transform.Translate(0f, , 0f);
			//_camera.transform.Translate(0, 0.01f, 0);
			_camera.transform.Translate(0, _distance / Number_of_times, 0);

        } else {
			_millionareCamera.ChangeOrthographicSize( CameraControll.DEFAULT_ORTHOGRAPHIC_SIZE );
			_camera.transform.position = _nowPos;
        }


        _count++;
        _time += Time.deltaTime;



    }
}
