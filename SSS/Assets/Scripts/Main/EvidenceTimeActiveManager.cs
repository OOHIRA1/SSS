using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceTimeActiveManager : MonoBehaviour {
    [ System.Serializable ]
    public struct ActiveTimes {
        public float _timeStart;
        public float _timeEnd;
    };

	[ System.Serializable ]
	public struct AddActiveTimes {
		public float _timeStart;
		public float _timeEnd;
	}

    [ SerializeField ] MoviePlaySystem _moviePlaySystem = null;
    [ SerializeField ] GameObject[ ] _evidenceIcom = new GameObject[ 1 ];
    [ SerializeField ] GameObject[ ] _evidenceTrigger = new GameObject[ 1 ];
    [ SerializeField ] ActiveTimes[ ] _activeTimes = new ActiveTimes[ 1 ];

	[ SerializeField ] int[ ] _index = new int[ 1 ];		//追加で表示する時間をつけるindex;
	[ SerializeField ] AddActiveTimes[ ] _addActiveTimes = new AddActiveTimes[ 1 ];

	// Use this for initialization
	void Start( ) {
		
	}
	
	// Update is called once per frame
	void Update( ) {
        EvidenceActive( );
		AddEvidenceActive( );
	}

    //再生時間によって証拠品を表示するかどうか処理-----------------
    void EvidenceActive( ) {

        float movieTime = _moviePlaySystem.MovieTime( );        //再生時間

		for ( int i = 0; i < _evidenceTrigger.Length; i++ ) {

            if ( _activeTimes[ i ]._timeStart <= movieTime &&   //指定時間内だったら
                 _activeTimes[ i ]._timeEnd >= movieTime ) {
                _evidenceTrigger[ i ].SetActive( true );           //Triggerを表示する(Icomは自分ほうでまた表示できる)
            } else {
                _evidenceTrigger[ i ].SetActive( false );          //Triggerを消す
                _evidenceIcom[ i ].SetActive( false );          //Triggerを消しただけだとバグるのでIcomも一緒に消す
            }

        }
    }
	//---------------------------------------------------------------

	//特定の証拠品に追加で表示する時間を設定する-----------------------
	void AddEvidenceActive( ) {
		float movieTime = _moviePlaySystem.MovieTime( );        //再生時間

		for ( int i = 0; i < _index.Length; i++ ) {

            if ( _addActiveTimes[ i ]._timeStart <= movieTime &&   //指定時間内だったら
                 _addActiveTimes[ i ]._timeEnd >= movieTime ) {
                _evidenceTrigger[ _index[ i ] ].SetActive( true );           //指定したindexのTriggerを表示する(Icomは自分ほうでまた表示できる)
            } else {
                _evidenceTrigger[ _index[ i ] ].SetActive( false );          //指定したindexのTriggerを消す
                _evidenceIcom[ _index[ i ] ].SetActive( false );          //指定したTriggerを消しただけだとバグるのでIcomも一緒に消す
            }

        }

	}
    //--------------------------------------------------------------------
}
