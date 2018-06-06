using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==観音開きカーテンの機能を管理するクラス
//
//使用方法：DoubleDoorCurtainにアタッチ
public class DoubleDoorCurtain : MonoBehaviour {
	Animator[] _animators;

	// Use this for initialization
	void Start () {
		_animators = GetComponentsInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//----------------------------------------------
	//public関数
	//----------------------------------------------

	//--カーテンを開ける関数
	public void Open( ) {
		for (int i = 0; i < _animators.Length; i++) {
			_animators[i].SetTrigger ("OpenTrigger");
		}
	}


	//--カーテンを閉める関数
	public void Close( ) {
		for (int i = 0; i < _animators.Length; i++) {
			_animators[i].SetTrigger ("CloseTrigger");
		}
	}


	//--カーテンのStateが開いている状態かどうかを返す関数
	public bool IsStateOpen( ) {
		int layer = _animators[0].GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animators[0].GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.IsName ("double_door_curtain_open");
	}


	//--カーテンのStateが閉じている状態かどうかを返す関数
	public bool IsStateClose( ) {
		int layer = _animators[0].GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animators[0].GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.IsName ("double_door_curtain_close");
	}


	//--カーテンのStateがwait状態かどうかを返す関数
	public bool IsStateWait( ) {
		int layer = _animators[0].GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animators[0].GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.IsName ("wait");
	}


	//--現在のStateの再生時間を返す関数( 返り値：0~1(開始時：0, 終了時：1) )
	public float ResearchStatePlayTime() {
		int layer = _animators[0].GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animators[0].GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.normalizedTime;
	}


	//--カーテンが動いているかどうかを返す関数
	public bool IsMoving( ) {
		return ( ( IsStateClose () || IsStateOpen () || IsStateWait() ) && ResearchStatePlayTime () < 1f );
	}
	//----------------------------------------------
	//----------------------------------------------
}
