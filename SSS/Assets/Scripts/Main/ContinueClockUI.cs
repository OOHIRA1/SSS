using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueClockUI : MonoBehaviour {
	[ SerializeField ] GameObject _minutesHand = null;		//長針
	[ SerializeField ] GameObject _hourHand = null;			//短針

	[ SerializeField ] float _speed = 0;					//針のスピード


	Vector3 _minutesHandRota;                               //長針の角度
    Vector3 _hourHandRota;                                  //短針の角度

	bool _isRewind;											//バイツァ・ダストをするかどうか
	// Use this for initialization
	void Start( ) {
		 _minutesHandRota = _minutesHand.transform.localEulerAngles;
        _hourHandRota = _hourHand.transform.localEulerAngles;
		_isRewind = false;
	}
	
	// Update is called once per frame
	void Update( ) {
		if ( _isRewind ) Rewind( );
	}

	void Rewind( ) {
		//長針の動き---------------------------------------------------------------------
        _minutesHandRota.z += _speed;
		if ( _minutesHandRota.z > 360 ) _minutesHandRota.z = 0;
      
        _minutesHand.transform.localEulerAngles = _minutesHandRota;
		//-------------------------------------------------------------------------------

		//短針の動き------------------------------------------------------------------------------------
         _hourHandRota.z += _speed;
		if ( _hourHandRota.z > 360 ) _hourHandRota.z = 0;
		
		_hourHand.transform.localEulerAngles = _hourHandRota;
		//-----------------------------------------------------------------------------------------
	}

	public void SetRewind( bool value ) { _isRewind = value; }
}
