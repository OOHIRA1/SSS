using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillioareDieMono : MonoBehaviour {

    Vector3 _move_dir = Vector3.right;
    [SerializeField] float _time;
    [SerializeField] float _animatime;
    [SerializeField]GameObject _effectBlood = null;
    Animator _animator;
    const float SPEED = 0.01f;
    
	// Use this for initialization
	void Start () {

        //MillioareScroll( );

        _time = 0;
        _animator = GetComponentInChildren<Animator>( );
    }

    // Update is called once per frame
    void Update( ) {
        
        MillioareRoop(  );

        _time += Time.deltaTime;

        if ( transform.position.x == 0 ) {　//ｘが0よりも小さいならの条件式MillioareScrollを呼ぶ
            MillioareScroll( );             
            transform.position += _move_dir * SPEED * 0;
        } else {
            transform.position = new Vector3( 0, transform.position.y, 0 );　//止まる座標
        }

	}

    void MillioareRoop( ) {

        if ( _animatime < _time ) {

            _animator.SetBool( "MillioareDieFlag", true );
        }
    }

    void MillioareScroll( ) {

        transform.position += _move_dir * SPEED; //スピード

    }

   
    public void Effectblood( ) {

        bool value = true;

        if ( !_effectBlood.activeInHierarchy ) {

            value = true;

        } else {

            value = false;

        }
        _effectBlood.SetActive( value );
    }

    public bool IsStateMillionaireDieMiddle2( ) {
        int layer = _animator.GetLayerIndex( "Base Layer" );
        AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo( layer );
        return animatorStateInfo.IsName( "MillioareDieMiddle2" );
    }

    public float ResearchStatePlayTime( ) {
        int layer = _animator.GetLayerIndex( "Base Layer" );
        AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo( layer );
        return animatorStateInfo.normalizedTime;
    }

}
