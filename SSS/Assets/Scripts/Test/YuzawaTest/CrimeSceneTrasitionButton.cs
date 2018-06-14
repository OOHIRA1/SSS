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
	[SerializeField]ScenesManager _scenesManager = null;
	[SerializeField]Curtain _curtain = null;
	[SerializeField]bool[] _clicked = new bool[12];

	int _num;
    bool Disapper = true;
    bool ClockDisapper = true;

	// Use this for initialization
	void Start () {
		_num = -1;
		_time[ 0 ] = 0;

		for ( int i = 0; i < _clicked.Length; i++ ) {
			_clicked[ i ] = false;
		}

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

		_curtain.ResearchStatePlayTime ();
		if (_clicked[0]) {
			NightGarden ();
		}

		if (_clicked[1]) {
			NoonGarden ();
		}

		if (_clicked[2]) {
			EveningGarden ();
		}

		if (_clicked[3]) {
			NightKitchen ();
		}

		if (_clicked[4]) {
			NoonKitchen ();
		}

		if (_clicked[5]) {
			EveningKitchen ();
		}

		if (_clicked[6]) {
			NightServingRoom ();
		}

		if (_clicked[7]) {
			NoonServingRoom ();
		}

		if (_clicked[8]) {
			EveningServingRoom ();
		}

		if (_clicked[9]) {
			NightBedRoom ();
		}

		if (_clicked[10]) {
			NoonBedRoom ();
		}

		if (_clicked[11]) {
			EveningBedRoom ();
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

	public void ButtonJudgement0(){
		_clicked[0] = true;
	}

	public void ButtonJudgement1(){
		_clicked[1] = true;
	}

	public void ButtonJudgement2(){
		_clicked[2] = true;
	}

	public void ButtonJudgement3(){
		_clicked[3] = true;
	}

	public void ButtonJudgement4(){
		_clicked[4] = true;
	}

	public void ButtonJudgement5(){
		_clicked[5] = true;
	}

	public void ButtonJudgement6(){
		_clicked[6] = true;
	}

	public void ButtonJudgement7(){
		_clicked[7] = true;
	}

	public void ButtonJudgement8(){
		_clicked[8] = true;
	}

	public void ButtonJudgement9(){
		_clicked[9] = true;
	}

	public void ButtonJudgement10(){
		_clicked[10] = true;
	}

	public void ButtonJudgement11(){
		_clicked[11] = true;
	}

	public void NightGarden(){
		if (_clicked[0] && _curtain.IsStateWait () ) {
			_scenesManager.ScenesTransition ("SiteNight_Garden");
		}
	}

	public void NoonGarden(){
		if (_clicked[1] && _curtain.IsStateWait ()) {
			_scenesManager.ScenesTransition ("SiteNoon_Garden");
		}
	}

	public void EveningGarden(){
		if (_clicked[2] && _curtain.IsStateWait()) {
			_scenesManager.ScenesTransition ("SiteEvening_Garden");
		}
	}

	public void NightKitchen(){
		if (_clicked[3] && _curtain.IsStateWait()) {
			_scenesManager.ScenesTransition ("SiteNight_Kitchen");
		}
	}

	public void NoonKitchen(){
		if (_clicked[4] && _curtain.IsStateWait()) {
			_scenesManager.ScenesTransition ("SiteNoon_Kitchen");
		}
	}

	public void EveningKitchen(){
		if (_clicked[5] && _curtain.IsStateWait()) {
			_scenesManager.ScenesTransition ("SiteEvening_Kitchen");
		}
	}

	public void NightServingRoom(){
		if (_clicked[6] && _curtain.IsStateWait()) {
			_scenesManager.ScenesTransition ("SiteNight_ServingRoom");
		}
	}

	public void NoonServingRoom(){
		if (_clicked[7] && _curtain.IsStateWait()) {
			_scenesManager.ScenesTransition ("SiteNoon_ServingRoom");
		}
	}

	public void EveningServingRoom(){
		if (_clicked[8] && _curtain.IsStateWait()) {
			_scenesManager.ScenesTransition ("SiteEvening_ServingRoom");
		}
	}

	public void NightBedRoom(){
		if (_clicked[9] && _curtain.IsStateWait()) {
			_scenesManager.ScenesTransition ("SiteNight_Bedroom");
		}
	}

	public void NoonBedRoom(){
		if (_clicked[10] && _curtain.IsStateWait()) {
			_scenesManager.ScenesTransition ("SiteNoon_Bedroom");
		}
	}

	public void EveningBedRoom(){
		if (_clicked[11] && _curtain.IsStateWait()) {
			_scenesManager.ScenesTransition ("SiteEvening_Bedroom");
		}
	}
}
