using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLibrary : MonoBehaviour {
    GameObject[] _gameObject = null;

    public enum Effect {
        TOUCH,
        MAX_EFFECT
    };
	// Use this for initialization
	void Start () {
        _gameObject[ 0 ] = Resources.Load< GameObject >( "TouchEffect/Effect_Touch" );
	}
	
	// Update is called once per frame
	void Update () {
		if ( _gameObject[ 0 ] == null ) {
            Debug.Log(false);
        } else {
            Debug.Log(true);
        }
	}

    public void EffectInstantiate ( Effect effect, Vector3 pos ) {

        _gameObject[ ( int )effect ] = Instantiate( _gameObject[ ( int )effect ], pos, Quaternion.identity );

    }

    public void EffectDestroy( Effect effect ) {
        Destroy( _gameObject[ ( int )effect ] );
    }

}
