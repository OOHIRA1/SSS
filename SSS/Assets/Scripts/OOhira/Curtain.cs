using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//==カーテンの機能を管理するクラス
//
//使い方：Curtainにアタッチ
public class Curtain : MonoBehaviour {
	Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = GetComponentInChildren<Animator> ( );
	}
	
	// Update is called once per frame
	void Update () {
    }


	//----------------------------------------------
	//public関数
	//----------------------------------------------

	//--カーテンを開ける関数
	public void Open( ) {
		_animator.SetTrigger ( "OpenFlag" );
	}


	//--カーテンを閉める関数
	public void Close( ) {
		_animator.SetTrigger ( "CloseFlag" );
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
		return (IsStateClose () && ResearchStatePlayTime () < 1f) || (IsStateOpen () && ResearchStatePlayTime () < 1f);
	}
    //----------------------------------------------
    //----------------------------------------------

}