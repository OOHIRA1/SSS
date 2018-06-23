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
	[SerializeField]ScenesManager _scenesManager = null;
    [SerializeField]BGMManager _bgmManeger = null;
	[SerializeField]Curtain _curtain = null;
	[SerializeField]Detective _detective = null;
    [SerializeField]float[] _time = new float[4];
	[SerializeField]float _animTime = 500;
	[SerializeField]bool[] _clicked = new bool[12];
    [SerializeField]bool[] _clickedClose = new bool[12];
    [SerializeField]string[] _sceneTrasition = new string[12];

    int _num;
    bool Disapper = false;
    bool ClockDisapper = true;

	public AudioClip crimescene_button2_appear;
	public AudioClip crimescene_button2_disappear2;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {

		audioSource = GetComponent<AudioSource>();
		_num = -1;
		_time[ 0 ] = 0;

		for ( int i = 0; i < _clicked.Length; i++ ) {
			_clicked[ i ] = false;
		}

        for ( int i = 0; i < _clickedClose.Length; i++ ) {
            _clickedClose[i] = false;
        }

		_bgmManeger = GameObject.FindWithTag ("BGMManager").GetComponent<BGMManager> ();

    }
	
	// Update is called once per frame
	void Update () {

        OnClickedClock( _num );


        for( int i = 0; i < _time.Length; i++ ) {
		if ( _num == i ) {
			_time[i] += Time.deltaTime;
            }
		}

		_curtain.ResearchStatePlayTime ();

        //--各シーン遷移--
        for( int i = 0; i < _clicked.Length; i++ ) {
		if (_clicked[i]) {
			CrimeSceneTrasition(i, _sceneTrasition[i]);
            }
		}

		

        //-------------------------------


        //---------ボタンが押されて探偵が初期位置に戻ったら------------
        for( int i = 0; i < _clicked.Length; i++) {
            if (_clickedClose[i] && _detective.GetCheckPos()){
                _curtain.Close();
                _clicked[i] = true;
                _clickedClose[i] = false;
            }
        }
        //---------------------------------------------------------------

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
			audioSource.PlayOneShot(crimescene_button2_appear, 0.2F);
			value = true;
		} else {
			value = false;
		}
		audioSource.PlayOneShot(crimescene_button2_disappear2, 0.2F);
		_animator.SetBool ("scrollFlag", value);
	}

    public void SpeechBalloon( int arrayNumber ) {              //吹き出しの表示　　他のボタン押したとき表示されてるものを非表示
        //ボタンを押したときに表示されていたら非表示にする-----------
        if ( _speechBalloon[arrayNumber].activeInHierarchy ) {
            _time[arrayNumber] = 0;
                _speechBalloon[arrayNumber].SetActive(false);
                return;
        }
        //-----------------------------------------------------------
        //配列番号がarrayNumberの_speechBalloonのみ表示させる-----------------------------------
		for (int i = 0; i < _speechBalloon.Length; i++){        
			if (i == arrayNumber){                              //iとArrayNumberの変数が同じ時
				_speechBalloon[i].SetActive(true);
			} else {
				_speechBalloon[i].SetActive(false);
				_time[i] = 0;
			}
		}
        //-----------------------------------------------------------------------------------
	}



	 void OnClickedClock( int arrayNumber ) {   //時計ＵＩの表示　　　他のボタン押したとき表示されてるものを非表示
		for ( int i = 0; i < 4; i++ ) {
			if ( i == arrayNumber && _speechBalloon[i].activeInHierarchy) {
				if( _time[ i ] > _animTime ) {
					_clockApper[ i ].SetActive( true );
					}
				} else {
					_clockApper[ i ].SetActive( false );
                    _time[i] = 0;
					}
			}
		}


    //public void OnClickedAgainBalloon( ) {  //もう一度押されたとき吹き出しを非表示
    //for( int i = 0; i < _speechBalloon.Length; i++ ) {
    //    if ( _speechBalloon[i].activeInHierarchy ) {
    //        Disapper = true;
    //    }
    //}
    //    if ( Disapper ) {
    //        for ( int i = 0; i < _speechBalloon.Length; i++ ) {
    //            _speechBalloon [ i ].SetActive( false );
    //        }
    //         Disapper = false;
    //    }
    //}

    //public void OnClickedAgainCLock( ) {             //もう一度押されたとき時計ＵＩを非表示
    //    bool value = false;
    //    if ( ClockDisapper == true ) {
    //        ClockDisapper = false;
    //    } else {
    //        ClockDisapper = true;
    //    }
    //    if ( ClockDisapper ) {
    //        for ( int i = 0; i < _clockApper.Length; i++ ) {
    //            _clockApper [ i ].SetActive( value );
    //            _time [ i ] = 0;
    //            _num = -1;
    //        }
    //    }
    //}


    public void OnClickedButtonNum( int arrayNumber ) {
        if (  _clockApper [ arrayNumber ].activeInHierarchy ) {
            _num = -1;
            _time[ arrayNumber ] = 0;
             _clockApper [ arrayNumber ].SetActive(false);
            return;
        }
		_num = arrayNumber;
	}

    public void OnClickedSpeech() {
        for( int i = 0; i < _speechBalloon.Length; i++ ) {
            _speechBalloon[ i ].SetActive( false );
    	}
    }
    public void OnClickedClockDisappare() {                 
        for( int i = 0; i < _clockApper.Length; i++ ) {
            _clockApper[ i ].SetActive( false );
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


    //--------------------ClockUiのボタンが押された時の処理--------------------------------------------------------------------
	public void ButtonJudgement( int arrayNumber ){
        _detective.DesignationMove( _detective.GetInitialPos() );
        _clickedClose[arrayNumber] = true;
        _bgmManeger.StopBGMWithFadeOut();
    }

	
    //---------------------------------------------------------------------

    //----------------各シーン遷移するための条件----------------------------

	public void CrimeSceneTrasition( int arrayNumber, string sceneTrasition  ){
		if (!_bgmManeger.IsPlaying(BGMManager.BGMClip.DETECTIVE_OFFICE) &&
            _detective.GetCheckPos( ) &&
            _clicked[arrayNumber] &&
            _curtain.IsStateWait () ) {

			_scenesManager.ScenesTransition (sceneTrasition);
		}
    }
    //-----------------------------------------------------------------------
}
