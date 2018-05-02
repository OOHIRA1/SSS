using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {
	Transform _transform;
	// Use this for initialization
	void Start( ) {
		_transform = GetComponent<Transform>( );
	}
	
	// Update is called once per frame
	void Update( ) {
		
	}

	public void MoveBarSize( Vector3 rate ) {
		_transform.localScale = rate;
	}
}
