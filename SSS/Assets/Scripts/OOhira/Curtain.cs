using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//==カーテンの機能を管理するクラス
//
//使い方：Curtainにアタッチ
public class Curtain : MonoBehaviour {
	Animator _animator;
	AudioSource _audioSource;
//	bool _seSounded;			//SEを鳴らしたかどうかのフラグ(1回のみ鳴らすようにするため)

	// Use this for initialization
	void Start () {
		_animator = GetComponentInChildren<Animator> ( );
		_audioSource = GetComponentInChildren<AudioSource> ();
//		_seSounded = false;
	}
	
	// Update is called once per frame
	//void Update () {
		//音を出す処理------------------------------------------------------
//		if (IsStateClose () && ResearchStatePlayTime () >= 0f && !_seSounded) {
//			_audioSource.PlayOneShot ( _audioSource.clip );
//			_seSounded = true;
//		}
//		if (IsStateClose () && ResearchStatePlayTime () >= 1f && _seSounded) {
//			_seSounded = false;
//		}
//		if (IsStateOpen () && ResearchStatePlayTime () >= 0f && !_seSounded) {
//			_audioSource.PlayOneShot ( _audioSource.clip );
//			_seSounded = true;
//		}
//		if (IsStateOpen () && ResearchStatePlayTime () >= 1f && _seSounded) {
//			_seSounded = false;
//		}
		//------------------------------------------------------------------
    //}


	//----------------------------------------------
	//public関数
	//----------------------------------------------

	//--カーテンを開ける関数
	public void Open( ) {
		_animator.SetTrigger ( "OpenFlag" );
		_audioSource.pitch = 0.8f;	//丁度いいピッチに変更
		_audioSource.PlayOneShot ( _audioSource.clip );
	}


	//--カーテンを閉める関数
	public void Close( ) {
		_animator.SetTrigger ( "CloseFlag" );
		_audioSource.pitch = 1f;	//丁度いいピッチに変更
		_audioSource.PlayOneShot ( _audioSource.clip );
	}


	//--カーテンのStateが開いている状態かどうかを返す関数
	public bool IsStateOpen( ) {
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.IsName ("curtain_open");
	}


	//--カーテンのStateが閉じている状態かどうかを返す関数
	public bool IsStateClose( ) {
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.IsName ("curtain_close");
	}


	//--カーテンのStateがwait状態かどうかを返す関数
	public bool IsStateWait( ) {
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.IsName ("wait");
	}


	//--現在のStateの再生時間を返す関数( 返り値：0~1(開始時：0, 終了時：1) )
	public float ResearchStatePlayTime() {
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.normalizedTime;
	}


	//--カーテンが動いているかどうかを返す関数
	public bool IsMoving( ) {
		return ( ( IsStateClose () || IsStateOpen () || IsStateWait() ) && ResearchStatePlayTime () < 1f );
	}
    //----------------------------------------------
    //----------------------------------------------

}