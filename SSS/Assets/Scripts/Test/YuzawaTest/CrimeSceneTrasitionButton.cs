using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CrimeSceneTrasitionButton : MonoBehaviour {
	//[SerializeField]GameObject _crimeSceneTransitionButton;
	[SerializeField]Animator _animator = null;
	[SerializeField]GameObject[] _crimeSceneButton = new GameObject[4];
    [SerializeField]GameObject[] _speechBalloon = new GameObject[4];
	[SerializeField]GameObject[] _clockApper = new GameObject[4];
	[SerializeField]float[] _time = new float[4];
	[SerializeField]float _animTime = 500;

	int _num;
    bool Disapper = true;
    bool ClockDisapper = true;


	// Use this for initialization
	void Start () {
		_num = -1;
		_time[ 0 ] = 0;
	}
	
	// Update is called once per frame
	void Update () {

		OnClickedClock( _num );

		if ( _num == 0 )
		{
			_time[0] += Time.deltaTime;
		}
		if ( _num == 1 )
		{
			_time[1] += Time.deltaTime;
		}
		if ( _num == 2 )
		{
			_time[2] += Time.deltaTime;
		}
		if ( _num == 3 )
		{
			_time[3] += Time.deltaTime;
		}
	}

	//public void OnClickedButton( ) {
	//	bool value = true;
	//	if ( !_crimeSceneButton[ 0 ].activeInHierarchy ) {
	//		value = true;
	//	}

	//	for ( int i = 0; i < _crimeSceneButton.Length; i++ ) {
	//		_crimeSceneButton [ i ].SetActive( value );
	//	}
	//}

	public void scroll( ){
		bool value = true;
		if (!_animator.GetBool( "scrollFlag" ) ) {
			value = true;
		} else {
			value = false;
		}
		_animator.SetBool ("scrollFlag", value);
	}

    public void SpeechBalloon( int arrayNumber ) {              //吹き出しの表示　　他のボタン押したとき表示されてるものを非表示
		for (int i = 0; i < _speechBalloon.Length; i++){        
			if (i == arrayNumber){                              //iとArrayNumberの変数が同じ時
				_speechBalloon[i].SetActive(true);              //
			} else {
				_speechBalloon[i].SetActive(false);
				_time[i] = 0;
			}
		}
	}



	 void OnClickedClock( int ArrayNumber ) {   //時計ＵＩの表示　　　他のボタン押したとき表示されてるものを非表示
		bool value = true;
		for ( int i = 0; i < 4; i++ ) {
			if ( i == ArrayNumber ) {
				if( _time[ i ] > _animTime ) {
					_clockApper[ i ].SetActive( true );
					}
				} else {
					_clockApper[ i ].SetActive( false );
					}
			}
		}


    public void OnClickedAgainBalloon( ) {           //もう一度押されたとき吹き出しを非表示
        bool value = false;
        if ( Disapper == true ) {
            Disapper = false;
        } else {
            Disapper = true;
        }
        if ( Disapper ) {
            for ( int i = 0; i < _speechBalloon.Length; i++ ) {
                _speechBalloon [ i ].SetActive( value );
            }
        }
    }

    public void OnClickedAgainCLock( ) {             //もう一度押されたとき時計ＵＩを非表示
        bool value = false;
        if ( ClockDisapper == true ) {
            ClockDisapper = false;
        } else {
            ClockDisapper = true;
        }
        if ( ClockDisapper ) {
            for ( int i = 0; i < _clockApper.Length; i++ ) {
                _clockApper [ i ].SetActive( value );
                _time [ i ] = 0;
                _num = -1;
            }
        }
    }
    public void OnClickedButtonNum( int ArrayNumber ) {
		_num = ArrayNumber;
	}

    public void OnClickedSpeech() {
        bool value = false;
        for( int i = 0; i < _speechBalloon.Length; i++ ) {
            _speechBalloon[ i ].SetActive( value );
    	}
    }
    public void OnClickedClockDisappare() {                 
        bool value = false;
        for( int i = 0; i < _clockApper.Length; i++ ) {
            _clockApper[ i ].SetActive( value );
            _time[i] = 0;
            _num = -1;
    	}
    }
	//public void speech() {
	//	bool value = true;
	//	if (!_animation.GetBool( "SpeechBalloonFlag" )) {
	//		value = true;
	//	}else {
	//		value = false;
	//	}
	//	_animation.SetBool ("SpeechBalloonFlag", value);
	//}


}
