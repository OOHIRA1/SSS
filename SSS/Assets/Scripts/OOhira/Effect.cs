﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==エフェクトを管理するクラス
//
//使用方法：エフェクトにアタッチ
public class Effect : MonoBehaviour {
	Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
	}


	//--現在のStateの再生時間を返す関数( 返り値：0~1(開始時：0, 終了時：1) )
	public float ResearchStatePlayTime() {
		if (!_animator) return 0;	//_animatorが初期化される前に入ったら0を返す
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.normalizedTime;
	}
}
