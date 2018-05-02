using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
	Animator _animator;
    AnimatorStateInfo _info;
	float _time;
	[SerializeField] GameObject _bar = null;

    // Use thisfor initialization
    void Start( ) {
        _animator = gameObject.GetComponent< Animator >( );
        _info = _animator.GetCurrentAnimatorStateInfo( 0 );
        _time = 0.0f;
    }

    // Update is called once per frame
    void Update( ) {
		
			_time = _bar.transform.localScale.x;
			if ( _time > 1.0f ) _time = 0.9f;
			if ( _time < 0.0f ) _time = 0.0f;
		//Playは毎フレーム呼ぶ必要はない。TimeManagerを作った時にPlayを呼びたいタイミングで呼べるように修正する
			_animator.Play ( _info.shortNameHash, -1, _time );
    }

	public void StopAndPlayAnim( ) {
		if ( _animator.speed != 0 ) {
			_animator.speed = 0;
		} else {
			_animator.speed = 1;
		}
	}
}
