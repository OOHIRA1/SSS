using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockUI : MonoBehaviour {
	const float ONE_HOUR = 360f / 12f;						//1時間の角度
	const float MINUTES_HAND_STOP_LEFT = 1f;				//長針を止める範囲(左側)
	const float MINUTES_HAND_STOP_RIGHT = 0;				//長針を止める範囲(右側)
    const float NOT_FF = 55f;                               //この時間以上のときに早送りしたら指定の位置に止める
    const float NOT_FB = 5f;                                //この時間以下のときに早戻ししたら指定の位置に止める
    const float SKIP_TIME = 5f;                             //飛ばす時間

	[ SerializeField ] RayShooter _rayShooter = null;		//レイ飛ばし

	[ SerializeField ] GameObject _minutesHand = null;		//長針
	[ SerializeField ] GameObject _hourHand = null;			//短針
	[ SerializeField ] float _time = 60f;					//動画の時間
    float _hourHandInitialRota;  		                    //短針の初期角度
	float _RotaParSecondMinutes;							//1秒当たりに進む長針の角度
	float _RotaParSecondHour;								//1秒あたりに進む短針の角度
    Vector3 minutesHandRota;                                //長針の角度
    Vector3 hourHandRota;                                   //短針の角度

    [ SerializeField ] MoviePlaySystem _moviPlaySystem = null;    //ムービープレイマネージャー
    string _pushed;                                              //クリックされたもの
    // Use this for initialization
    void Start( ) {
        minutesHandRota = _minutesHand.transform.localEulerAngles;
        hourHandRota = _hourHand.transform.localEulerAngles;
        _hourHandInitialRota = _hourHand.transform.localEulerAngles.z;
		_RotaParSecondMinutes = 360f / _time;
		_RotaParSecondHour = _RotaParSecondMinutes / 12f;
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
        minutesHandRota.z = _RotaParSecondMinutes * _moviPlaySystem.MoviTime( );
		
		if ( ( _minutesHand.transform.localEulerAngles.z < MINUTES_HAND_STOP_LEFT ) &&		//長針を止めたい範囲だったら
			 ( _minutesHand.transform.localEulerAngles.z > MINUTES_HAND_STOP_RIGHT ) ) {

			_minutesHand.transform.localEulerAngles = new Vector3( 0, 0, 0.1f );
      
		}
        _minutesHand.transform.localEulerAngles = -minutesHandRota;
		//-------------------------------------------------------------------------------

		//短針の動き------------------------------------------------------------------------------------
         hourHandRota.z = _hourHandInitialRota - _RotaParSecondHour * _moviPlaySystem.MoviTime( );
		
		if ( _hourHand.transform.localEulerAngles.z <= _hourHandInitialRota - ONE_HOUR ) {	//短針が動いてほしい角度を超えたら
			_hourHand.transform.localEulerAngles = new Vector3( 0, 0, _hourHandInitialRota - ONE_HOUR );
		}
		_hourHand.transform.localEulerAngles = hourHandRota;
		//-----------------------------------------------------------------------------------------------
	}
	//-------------------------------------------------------------------------------------------------------------------

    //時計ＵＩのどの時間帯がタッチされたか
     public string GetPushed( ) { return _pushed; } 

}
