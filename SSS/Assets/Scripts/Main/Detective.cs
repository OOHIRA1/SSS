using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detective : MonoBehaviour {
    Animator _anim;

    [ SerializeField ] float _posY = -3.88f;

    bool _isAnimWalk;
    bool _isFlip;

    Vector3 _move;
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

        if ( _isFlip ) Flip( );
	}

    
    void Move( ) {

        Vector3 mouse = Vector3.zero;
        if ( Input.GetMouseButtonDown( 0 ) ) {
            mouse = Camera.main.ScreenToWorldPoint( Input.mousePosition );
            _move.x = mouse.x;
        }

        if ( transform.position.x != _move.x ) {
            if ( transform.position.x < _move.x ) transform.position += new Vector3( 0.1f, 0, 0 );
            if ( transform.position.x > _move.x ) transform.position += new Vector3( -0.1f, 0, 0 );
            _isAnimWalk = true;
        } else {
            _isAnimWalk = false;
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
        transform.localScale = new Vector3( -1, 1, 1 );
    }

}
