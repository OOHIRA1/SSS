﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLibrary : MonoBehaviour {
    GameObject[] _gameObject = new GameObject[ 1 ];
    public enum Effect {
        TOUCH,
        MAX_EFFECT
    };
	// Use this for initialization
	void Start () {
        _gameObject[ 0 ] = ( GameObject )Resources.Load( "Touch/effect_touch" );
	}
	
    public void EffectInstantiate ( Effect effect, Vector3 pos ) {
        
        Instantiate( _gameObject[ ( int )effect ], pos, Quaternion.identity );
        //_gameObject[ ( int )effect ] = Instantiate( _gameObject[ ( int )effect ], pos, Quaternion.identity );

    }

    public void EffectDestroy( Effect effect ) {
        Destroy( _gameObject[ ( int )effect ] );
    }


}
