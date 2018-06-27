using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceActiveManager : MonoBehaviour {
    [ System.Serializable ]
    public struct ActiveTimes {
        public float _timeStart;
        public float _timeEnd;
    };

	[ System.Serializable ]
	public struct AddActiveTimes {      //追加で表示する時間をつけるための変数
		public float _timeStart;
		public float _timeEnd;
	}

    [ SerializeField ] MoviePlaySystem _moviePlaySystem = null;
    [ SerializeField ] GameObject[ ] _evidenceIcom = new GameObject[ 1 ];
    [ SerializeField ] GameObject[ ] _evidenceTrigger = new GameObject[ 1 ];
    [ SerializeField ] ActiveTimes[ ] _activeTimes = new ActiveTimes[ 1 ];

	[ SerializeField ] int[ ] _index = new int[ 1 ];		//追加で表示する時間をつけるindex;
	[ SerializeField ] AddActiveTimes[ ] _addActiveTimes = new AddActiveTimes[ 1 ];

    int[ ] _evidenceNum;

    bool[ ] _disapear;              //指定時間外の証拠品を非表示にする判断のための変数
    bool _partDisapear;             //一部の証拠品を消すかどうか
    bool _allDisapear;              //すべての証拠品を消すかどうか
	

	// Use this for initialization
	void Start( ) {
		_disapear = new bool[ _evidenceTrigger.Length ];

        for ( int i = 0; i < _disapear.Length; i++ ) {
            _disapear[ i ] = true;
        }

        _evidenceNum = new int[ _evidenceTrigger.Length ];

        _partDisapear = false;
        _allDisapear = false;
	}
	
	// Update is called once per frame
	void Update( ) {
			EvidenceTimeActive( );
			AddEvidenceTimeActive( );
			EvidenceNotTimeDisapear( );

            //publicでこの処理をしても上手くいかなかったのでUpdateで処理することにした
            //フラグが立っていたら処理する
            if ( _partDisapear ) PartEvidenceDisapear( _evidenceNum );  
            if ( _allDisapear ) AllEvidenceDisapear( );

            //関数を呼ばなくなったら処理しないようにするため
            _partDisapear = false;
            _allDisapear = false;
	}

    //再生時間によって証拠品を表示するかどうか処理-----------------
    void EvidenceTimeActive( ) {

        float movieTime = _moviePlaySystem.MovieTime( );        //再生時間

		for ( int i = 0; i < _evidenceTrigger.Length; i++ ) {

            if ( _activeTimes[ i ]._timeStart <= movieTime &&   //指定時間内だったら
                 _activeTimes[ i ]._timeEnd >= movieTime ) {
                _evidenceTrigger[ i ].SetActive( true );           //Triggerを表示する(Icomは自分ほうでまた表示できる)
                _disapear[ i ] = false;                             //消すフラグをfalseにする
            }

        }
    }
	//---------------------------------------------------------------

	//特定の証拠品に追加で表示する時間を設定する-----------------------
	void AddEvidenceTimeActive( ) {
		float movieTime = _moviePlaySystem.MovieTime( );        //再生時間

		for ( int i = 0; i < _index.Length; i++ ) {

            if ( _addActiveTimes[ i ]._timeStart <= movieTime &&   //指定時間内だったら
                 _addActiveTimes[ i ]._timeEnd >= movieTime ) {
                _evidenceTrigger[ _index[ i ] ].SetActive( true );           //指定したindexのTriggerを表示する(Icomは自分ほうでまた表示できる)
                _disapear[ i ] = false;                             //消すフラグをfalseにする
            }

        }

	}
    //--------------------------------------------------------------------

    //指定時間内ではなかった証拠品を非表示にする-------------------------------
    void EvidenceNotTimeDisapear( ) {

         for ( int i = 0; i < _evidenceTrigger.Length; i++ ) {
            if ( _disapear[ i ] ) {                             //消すフラグがtrueだったら
                _evidenceTrigger[ i ].SetActive( false );          //Triggerを消す
                _evidenceIcom[ i ].SetActive( false );          //Triggerを消しただけだとバグるのでIcomも一緒に消す
            }
                _disapear[ i ] = true;                          //消すフラグをすべて初期化する
        }

    }
    //--------------------------------------------------------------------------


    //一部の証拠品だけ消す処理--------------------------------------------------------------
    void PartEvidenceDisapear( int[ ] evidenceNum ) {
        for ( int i = 0; i < evidenceNum.Length; i++ ) {
            for ( int j = 0; j < _evidenceTrigger.Length; j++ ) {

                if ( _evidenceTrigger[ j ].name == "EvidenceTrigger" + evidenceNum[ i ] ) {
                    _evidenceTrigger[ j ].SetActive( false );
                    _evidenceIcom[ j ].SetActive( false );
                }

            }
        }
    }
    //--------------------------------------------------------------------------------------

    //全ての証拠品を消す処理-------------------------------------------------------
    void AllEvidenceDisapear( ) {

        for ( int i = 0; i < _evidenceTrigger.Length; i++ ) {
             _evidenceTrigger[ i ].SetActive( false );          //Triggerを消す
             _evidenceIcom[ i ].SetActive( false );          //Triggerを消しただけだとバグるのでIcomも一緒に消す
        }

    }
    //-------------------------------------------------------------------------------

    //どの証拠品を消すか値を入れるのと一部の証拠品を消す処理をするフラグを立てる------
    public void PartEvidenceDisapearFlag( int[ ] evidenceNum ) {
        for ( int i = 0; i < evidenceNum.Length; i++ ) {
            if ( _evidenceNum.Length < i ) return;

            _evidenceNum[ i ] = evidenceNum[ i ] ;
        }

        _partDisapear = true;
    }
    //--------------------------------------------------------------------------------

    //全ての証拠品を消す処理をするフラグを立てる-----------
    public void AllEvidenceDisapearFlag( ) {
	    _allDisapear = true;
    }
    //-----------------------------------------------------

}
