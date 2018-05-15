using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class YuzawaTest : MonoBehaviour {
	//[SerializeField]GameObject _crimeSceneTransitionButton;
	[SerializeField]Animator _animator = null;
	[SerializeField]GameObject[] _crimeSceneButton = new GameObject[4];
    [SerializeField]GameObject[] _speechBalloon = new GameObject[4];
	[SerializeField]GameObject[] _clockApper = new GameObject[4];
	[SerializeField]float[] _time = new float[4];
	[SerializeField]float _animTime = 500;

	int _num;


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

    public void SpeechBalloon( int arrayNumber ) {
        //bool value = true;
        //if ( !_speechBalloon[ arrayNumber ].activeInHierarchy ) {
        //    value = true;
        // } else {
        //    value = false;
        //}
        //_speechBalloon [ arrayNumber ].SetActive( value );
        //if ( value ) {
        //    for ( int i = 0; i < _speechBalloon.Length; i++ ) {
        //        if ( i != arrayNumber ) _speechBalloon[ i ].SetActive( false );
        //    }
        //}
        for ( int i = 0; i < _speechBalloon.Length; i++ ) {
            if ( i == arrayNumber ) {
                _speechBalloon[i].SetActive(true);
            } else {
                _speechBalloon[i].SetActive(false);
            }
        }
    }

    public void OnClickedSpeech() {
        bool value = false;
        for( int i = 0; i < _speechBalloon.Length; i++ ) {
            _speechBalloon[ i ].SetActive( value );
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

	//	public void CrimeSceneBedRoom () {
	//		SceneManager.LoadScene( "Bedroom" );
	//		}

	//	public void CrimeSceneKitchen () {
	//		SceneManager.LoadScene( "Kitchen" );
	//		}

	public void OnClickedClock( int ArrayNumber ) {
		bool value = true;
		for ( int i = 0; i < 4; i++ ) {
			if ( i == ArrayNumber ) {
				if( _time[ i ] > _animTime ) {
					_clockApper[ i ].SetActive( value );
				}
			}
		}
	}

	public void OnClickedButtonNum( int ArrayNumber ) {
		_num = ArrayNumber;
	}

}
