using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==ムービーを管理するクラス
//
//使用方法：ムービーにアタッチ
public class Movie : MonoBehaviour {
	Animator _animator;

	// Use this for initialization
	void Start( ) {
		_animator = GetComponent<Animator>( );
	}
	
	// Update is called once per frame
	void Update( ) {
		
	}

	//================================================
	//public関数
	//--ムービーの再生位置を変更する関数
	public void ChangeMovieStartTime( float startTime ) {
		if ( startTime > 1.0f ) startTime = 1f;
		if ( startTime < 0 ) startTime = 0;
		AnimatorStateInfo animatorStateInfo = _animator.GetNextAnimatorStateInfo (0);
		_animator.Play ( animatorStateInfo.shortNameHash, 0, startTime );
	}
	//================================================
	//================================================
}
