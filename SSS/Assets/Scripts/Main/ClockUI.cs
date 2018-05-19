using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockUI : MonoBehaviour {
	const float ONE_HOUR = 360f / 12f;						//1時間の角度
	const float MINUTES_HAND_STOP_LEFT = 1f;				//長針を止める範囲(左側)
	const float MINUTES_HAND_STOP_RIGHT = 0;				//長針を止める範囲(右側)

	[ SerializeField ] RayShooter _rayShooter = null;		//レイ飛ばし

	[ SerializeField ] GameObject _minutesHand = null;		//長針
	[ SerializeField ] GameObject _hourHand = null;			//短針
	[ SerializeField ] float _hourHandInitialRota = 0;		//短針の初期角度
	[ SerializeField ] float _time = 60f;					//動画の時間
	float _RotaParSecondMinutes;							//1秒当たりに進む長針の角度
	float _RotaParSecondHour;								//1秒あたりに進む短針の角度

	bool _stop;
    [ SerializeField ] MoviePlaySystem _moviPlaySystem = null;    //ムービープレイマネージャー
    string _pushed;                                         //クリックされたもの
    // Use this for initialization
    void Start( ) {
		_RotaParSecondMinutes = 360f / _time;
		_RotaParSecondHour = _RotaParSecondMinutes / 12f;
		_stop = false;
        _pushed = "none";

	}

	// Update is called once per frame
	void Update( ) {
		if ( Input.GetMouseButtonDown( 0 ) ) CutainClose( );

		ClockNeedleMove( );
	}

	//レイが当たったtagに応じてシーン遷移-----------------------------------------------------
	void CutainClose( ) {
		RaycastHit2D hit;
		hit = _rayShooter.Shoot( Input.mousePosition );
		if ( hit ) {
            if ( hit.collider.tag == "Night" ) {
                _pushed = "Night";
            }

            if ( hit.collider.tag == "Evening" ) {
                _pushed = "Evening";
            }	
		}
	}
	//-----------------------------------------------------------------------------------------

	//動画にあわせて針を動かす------------------------------------------------------------------------------------------
	void ClockNeedleMove( ) {
		//長針の動き---------------------------------------------------------------------
		Vector3 minutesHandRota = Vector3.zero;
		if ( ( _minutesHand.transform.localEulerAngles.z < MINUTES_HAND_STOP_LEFT ) &&		//長針を止めたい範囲だったら
			 ( _minutesHand.transform.localEulerAngles.z > MINUTES_HAND_STOP_RIGHT ) ) {

			minutesHandRota.z = 0f;
			_minutesHand.transform.localEulerAngles = new Vector3( 0, 0, 0.1f );

		} else {
			if ( !_stop ) minutesHandRota.z = _RotaParSecondMinutes * Time.deltaTime;
		}

		_minutesHand.transform.Rotate( 0, 0, -minutesHandRota.z );
		//-------------------------------------------------------------------------------

		//短針の動き------------------------------------------------------------------------------------
		Vector3 hourHandRota = Vector3.zero;
		if ( _hourHand.transform.localEulerAngles.z <= _hourHandInitialRota - ONE_HOUR ) {	//短針が動いてほしい角度を超えたら
			hourHandRota.z = 0;
			_hourHand.transform.localEulerAngles = new Vector3( 0, 0, _hourHandInitialRota - ONE_HOUR );
		} else {
			if ( !_stop ) hourHandRota.z = _RotaParSecondHour * Time.deltaTime;
		}

		_hourHand.transform.Rotate( 0, 0, -hourHandRota.z );
		//-----------------------------------------------------------------------------------------------
	}
	//-------------------------------------------------------------------------------------------------------------------

	
	public void StopAndPlayClock( ) {
		if ( !_stop ) {
			_stop = true;
		} else {
			_stop = false;
		}
	}

	public void FFClock( ) {
		float rota = 0;
		_stop = false;

		rota = _RotaParSecondMinutes * 5f;
		_minutesHand.transform.Rotate( 0, 0, -rota );
        if ( _moviPlaySystem.MoviTime( ) >= 55f ) {
            _minutesHand.transform.localEulerAngles = new Vector3( 0, 0, 0.1f );
        }


		rota = _RotaParSecondHour * 5f;
		_hourHand.transform.Rotate( 0, 0, -rota );
		if ( _hourHand.transform.localEulerAngles.z <= _hourHandInitialRota - ONE_HOUR ) {
			_hourHand.transform.localEulerAngles = new Vector3( 0, 0, _hourHandInitialRota - ONE_HOUR );
		}
	}

	public void FBClock( ) {
		float rota = 0;
		_stop = false; 

		rota = _RotaParSecondMinutes * 5f;
		_minutesHand.transform.Rotate( 0, 0, rota );
        if ( _moviPlaySystem.MoviTime( ) <= 5 ) {
            _minutesHand.transform.localEulerAngles = new Vector3( 0, 0, 0 );
        }

		rota = _RotaParSecondHour * 5f;
		_hourHand.transform.Rotate( 0, 0, rota );
		if ( _hourHand.transform.localEulerAngles.z >= _hourHandInitialRota ) {
			_hourHand.transform.localEulerAngles = new Vector3( 0, 0, _hourHandInitialRota );
		}
	}

    //public void SceneTransition( ) {
    //    _scenesManager.SiteScenesTransition( _pushed );
    //}

     public string GetPushed( ) { return _pushed; } 

}
