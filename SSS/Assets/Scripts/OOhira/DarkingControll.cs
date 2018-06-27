using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==暗転処理を管理するクラス
//
//使用方法：暗転処理をするパネルにアタッチ
public class DarkingControll : MonoBehaviour {
	Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
	}


	//=============================================================================================
	//public関数
	//--暗転処理をする関数
	public void Darking() {
		_animator.SetBool ("fadeinFlag", true);
	}


	//--暗転処理を解除する関数
	public void Bright() {
		_animator.SetBool ("fadeinFlag", false);
	}


	//--Stateが暗転状態かどうかを返す関数
	public bool IsStateFadein( ) {
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.IsName ("fadein");
	}


	//--現在のStateの再生時間を返す関数( 返り値：0~1(開始時：0, 終了時：1) )
	public float ResearchStatePlayTime() {
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.normalizedTime;
	}
	//=============================================================================================
	//=============================================================================================
}
