using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==証拠品アイコンの動きを管理するクラス
//
//使用方法：証拠品アイコンにアタッチ
public class EvidenceIcon : MonoBehaviour {
	public enum SEClip {
		TAP_OR_APPEAR,
		PUT_AWAY
	}
	const int COUNT_INIT_VALUE = -50;	//_countの初期値

	[System.Serializable]
	class ParabolicTrajectory {//放物軌跡を表現するクラス()
		public Vector2 _initPos = Vector2.zero;		//初期位置(実行前に軌跡を確認する用)
		public Vector2 _destination = Vector2.zero;	//目的位置(通過点)
		public float _a = 0;
		public float _p = 0;
		public float _q = 0;				//2次関数y = a ( x - p ) ^ 2 + qにおけるa, p, q
		public float _speedX = 0;			//x方向への速度
	}

	Animator _animator;
	SoundLibrary _soundLibrary;
	[SerializeField] ParabolicTrajectory _evidenceIconTrajectory = null;	//証拠品アイコンの軌跡　※uGUIで作っているため直に座標を指定
	int _count;	//上下に動く時に使う変数(-50~50)
	Vector3 _firstPos;	//証拠品アイコンの初期位置


	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
		_soundLibrary = GetComponent<SoundLibrary> ();
		Vector2 pos = transform.position;
		Vector2 desPos = _evidenceIconTrajectory._destination;
		float a = _evidenceIconTrajectory._a;
		_evidenceIconTrajectory._p = -(pos.y - desPos.y) / ((pos.x - desPos.x) * 2 * a) + (pos.x + desPos.x) / 2;	//p = -(1/2a)*(平均変化率) + (x座標の相加平均) より算出される
		float p = _evidenceIconTrajectory._p;
		_evidenceIconTrajectory._q = desPos.y - a * (desPos.x - p) * (desPos.x - p);								//q = y -a(x-p)^2 より算出される
		_count = COUNT_INIT_VALUE;
		_firstPos = transform.position;
		this.gameObject.SetActive (false);//ResetPos()がStart()を入る前に呼ばれて_firstPosが証拠品アイコンの初期位置を上手く取れないバグ対策に先にStartを呼んで非アクティブ化
	}

	// Update is called once per frame
	void Update () {
		if (IsStateFadeIn()) {
			transform.Translate ( 0, 0.01f, 0 );
		}
		if (IsStateWait()) {
			//上下に動く処理--------------------------
			if (_count < 0) {
				transform.Translate (0, -0.005f, 0);
				_count++;
			}
			if (_count >= 0) {
				transform.Translate (0, 0.005f, 0);
				_count++;
				if (_count == -COUNT_INIT_VALUE) {
					_count = COUNT_INIT_VALUE;
				}
			}
			//-----------------------------------------
		}
	}


	//======================================================================
	//public関数

	//--証拠品アイコンをしまう関数
	public void PutAway() {
		StartCoroutine (PutAwayCoroutine());
	}


	//--現在のStateの再生時間を返す関数( 返り値：0~1(開始時：0, 終了時：1) )
	public float ResearchStatePlayTime() {
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.normalizedTime;
	}


	//--証拠品アイコンのStateがfade_in状態かどうかを返す関数
	public bool IsStateFadeIn( ) {
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.IsName ("fade_in");
	}


	//--証拠品アイコンのStateがwait状態かどうかを返す関数
	public bool IsStateWait( ) {
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.IsName ("wait");
	}


	//--証拠品アイコンのStateがinto_evidence_file状態かどうかを返す関数
	public bool IsStateIntoEvidenceFile( ) {
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.IsName ("into_evidence_file");
	}


	//--証拠品アイコンを初期位置に戻す関数(_countも初期化する)
	public void ResetPos() {
		_count = COUNT_INIT_VALUE;
		transform.position = _firstPos;
	}
	//======================================================================
	//======================================================================


	//--証拠品アイコンをしまう関数(コルーチン)
	IEnumerator PutAwayCoroutine() {
		_animator.SetTrigger ("intoEvidenceFileTrigger");
		while (!IsStateIntoEvidenceFile ()) {//Stateがinto_evidence_fileになるまで待機
			yield return new WaitForSeconds (Time.deltaTime);
		}
		while( ResearchStatePlayTime() < 1 ) {//軌跡を描いてしまう処理
			transform.position += new Vector3 (_evidenceIconTrajectory._speedX * Time.deltaTime, 0, 0);//x座標の移動
			Vector3 vec = transform.position;
			float a = _evidenceIconTrajectory._a;
			float p = _evidenceIconTrajectory._p;
			float q = _evidenceIconTrajectory._q;
			transform.position = new Vector2 ( vec.x, a * ( vec.x - p ) * ( vec.x - p ) + q );//y座標の移動
			yield return new WaitForSeconds (Time.deltaTime);
		}
		while (_soundLibrary.IsPlaying ((int)SEClip.PUT_AWAY)) {//音が鳴りやむまで待機(音はアニメーション中に再生しています)
			yield return new WaitForSeconds (/*Time.deltaTime*/1.5f);//音が長いので調整
			_soundLibrary.StopSound();
		}
		this.gameObject.SetActive (false);
	}



	//ギズモ表示--------------------------------------------------------------------------------------------------
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		float a = _evidenceIconTrajectory._a;
		Vector2 initPos = _evidenceIconTrajectory._initPos;
		Vector3 desPos = _evidenceIconTrajectory._destination;
		float p = -(initPos.y - desPos.y) / ((initPos.x - desPos.x) * 2 * a) + (initPos.x + desPos.x) / 2;
		float q = desPos.y - a * (desPos.x - p) * (desPos.x - p);
		Vector2 point1 = initPos;
		Vector2 point2 = initPos;

		for (int i = 0; i < 1000; i++) {
			point2 += new Vector2 ( _evidenceIconTrajectory._speedX * Time.deltaTime, 0 );
			point2 = new Vector2 ( point2.x, a * ( point2.x - p ) * ( point2.x - p ) + q );
			Gizmos.DrawLine (point1, point2);
			point1 = point2;
		}
	}
	//-------------------------------------------------------------------------------------------------------------
}
