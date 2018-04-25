using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {
	Transform point_pos;
	Vector3 pos;
	public float set_time;
	public float time;
	float seconds;
	bool stop;
	// Use this for initialization
	void Start( ) {
		point_pos = GetComponent< Transform >( );
		pos = new Vector3( -10.2f, 0, 0 );
		time = set_time;
		seconds = 20.4f / time;
		stop = false;
	}
	
	// Update is called once per frame
	void Update( ) {
		float back_time = time;

		if ( time > 0 && !stop ) time -= Time.deltaTime;

		point_pos.position = pos;

		if ( ( int )back_time > ( int )time ) pos.x += seconds;
		
		if ( pos.x > 10.2f) pos.x = 10.2f;

	}


	public void Stop_Point( ) { stop = true; }

	public void Start_Point( ) { stop = false; }

	public void FB_Point( ) {
		time += 5;
		if ( time < 0 ) time = 0;
		pos.x -= seconds * 5;
		stop = false;
	}

	public void FF_Point( ) {
		time -= 5;
		if ( time > set_time ) time = 60;
		pos.x += seconds * 5;
		stop = false;
	}
	
}
