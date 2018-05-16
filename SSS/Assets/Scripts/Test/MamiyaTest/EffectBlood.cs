using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBlood : MonoBehaviour {

    [SerializeField]GameObject _effectBlood = null;
    
    // Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
   
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

}
