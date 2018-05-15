using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockUI : MonoBehaviour {
	[ SerializeField ] ScenesManager _scenesManager;
	[ SerializeField ] RayShooter _rayShooter;
	// Use this for initialization
	void Start( ) {
	}

	// Update is called once per frame
	void Update( ) {
		RaycastHit2D hit;
		if ( Input.GetMouseButtonDown( 0 ) ) {
			hit = _rayShooter.Shoot( Input.mousePosition );
			if ( hit ) {
				if ( hit.collider.tag == "Night" ) {
					_scenesManager.SiteNightScenesTransition( );
				}
			}
		}



	}
}
