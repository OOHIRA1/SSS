using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detective : MonoBehaviour {
    Animator _anim;

    //[ SerializeField ] float _posY = -3.88f;

    bool _isAnimWalk;
    bool _isFlip;

    Vector3 _move;
	[SerializeField] float _moveTouchUp = -4.0f;
	[SerializeField] float _moveTouchDown = -6.0f;
	// Use this for initialization
	void Start( ) {
		_anim = GetComponent< Animator >( );
        _isAnimWalk = false;        //歩くモーションをするかどうか
        _isFlip = true;             //反転するかどうか
        _move = transform.position;
	}
	
	// Update is called once per frame
	void Update( ) {
        Move( );

        Motion( );

        Flip( );
	}

    
    void Move( ) {

        Vector3 mouse = Vector3.zero;
        if ( Input.GetMouseButtonDown( 0 ) ) {
            mouse = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			Debug.Log( mouse );
			if ( mouse.y < _moveTouchUp && mouse.y > _moveTouchDown ) {
				_move.x = mouse.x;
			}
        }

        if ( transform.position.x < ( _move.x + 0.1f ) && transform.position.x > ( _move.x - 0.1f ) ) {
            _isAnimWalk = false;
        } else {

            if ( transform.position.x < _move.x ) {
				transform.position += new Vector3( 0.1f, 0, 0 );
				_isFlip = true;
			}

            if ( transform.position.x > _move.x ) {
                transform.position += new Vector3( -0.1f, 0, 0 );
                _isFlip = false;
            }

            _isAnimWalk = true;
        }
        

    }

    void Motion( ) {
        if ( _isAnimWalk ) {
            _anim.SetBool( "Walk", true );
        } else {
            _anim.SetBool( "Walk", false );
        }

    }

    void Flip( ) {
        if ( _isFlip ) { 
            transform.localScale = new Vector3( -1, 1, 1 );
        } else {
            transform.localScale = new Vector3( 1, 1, 1 );
        }
    }

}
