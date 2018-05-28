using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {
    bool fureru;
	// Use this for initialization
	void Start( ) {
        fureru = false;		
	}
	
	// Update is called once per frame
	void Update( ) {
	}


    //void OnTriggerEnter2D( Collider2D collision ) {
    //    if ( collision.gameObject.tag == "Player" ) fureru = true;
    //}

    public bool Getfureru( ) { return fureru; }
}
