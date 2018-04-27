using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{

    Animator animator;
    AnimatorStateInfo info;
    float time;
    // Use thisfor initialization
    void Start( ) {
        animator = gameObject.GetComponent< Animator >( );
        info = animator.GetCurrentAnimatorStateInfo( 0 );
        time = 0.0f;
    }

    // Update is called once per frame
    void Update( ) {
    }

	public void StopAndPlayAnim( ) {
		if ( animator.speed != 0 ) {
			animator.speed = 0;
		} else {
			animator.speed = 1;
		}
	}

    public void FastForwardAnim( ) {
        time += 0.3f;
        if ( time > 1.0f ) time = 0.9f;

        animator.Play( info.shortNameHash, -1, time );
        animator.speed = 1;
    }

    public void FastBackwardAnim( ) {
        time -= 0.3f;
        if ( time < 0.0f ) time = 0.0f;

        animator.Play( info.shortNameHash, -1, time );
        animator.speed = 1;
    }

}
