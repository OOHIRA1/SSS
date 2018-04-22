using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {
	Transform bar_size;
	//float x;
	Vector3 size;
	// Use this for initialization
	void Start ( ) {
		bar_size = GetComponent< Transform > ( );
		size = new Vector3( 0, 1, 1 );
		bar_size.localScale = size;
	}

	// Update is called once per frame
	void Update ( ) {
		bar_size.localScale = size;
		size.x += 0.0001f;

		if (size.x > 1.0f) size.x = 1.0f;
	}

}
