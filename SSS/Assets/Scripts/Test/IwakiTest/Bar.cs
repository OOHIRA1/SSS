using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {
	Transform bar_size;
	public float set_time;
	public float time;
	Vector3 size;
	float seconds;
	bool stop;
	// Use this for initialization
	void Start ( ) {
		bar_size = GetComponent< Transform > ( );
		size = new Vector3( 0, 1, 1 );
		bar_size.localScale = size;
		time = set_time;
		seconds = 1.0f / time;
		stop = false;
	}

	// Update is called once per frame
	void Update ( ) {
		float back_time = time;
		if ( time > 0 && !stop ) time -= Time.deltaTime;

		bar_size.localScale = size;

		if ( ( int )back_time > ( int )time ) size.x += seconds;
		
		if (size.x > 1.0f) size.x = 1.0f;
	}

	public void Stop_Bar( ) { stop = true; }

	public void Start_Bar( ) { stop = false; }

	public void FB_Bar( ) {
		time += 5;
		if ( time < 0 ) time = 0;
		size.x -= seconds * 5;
		stop = false;
	}

	public void FF_Bar( ) {
		time -= 5;
		if ( time > set_time ) time = 60;
		size.x += seconds * 5;
		stop = false;
	}

}
