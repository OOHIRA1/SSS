using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTest : MonoBehaviour {

	
	// Update is called once per frame
	void Update( ) {
        if ( Input.GetMouseButtonDown( 0 ) ) {
            RaycastHit rayCastHit;
            Physics.Raycast( Camera.main.transform.position, ( Input.mousePosition - Camera.main.transform.position ).normalized, out rayCastHit );
            Debug.DrawRay( Camera.main.transform.position, ( Input.mousePosition - Camera.main.transform.position ).normalized * rayCastHit.distance, Color.yellow, 100, false );
            //Debug.DrawRay(Camera.main.transform.position, Vector3.forward * 100, Color.yellow, 100, false);
        
            Debug.DrawRay( Camera.main.transform.position, ( Input.mousePosition - Camera.main.transform.position ) * 100, Color.yellow, 100, false );
            Debug.Log(( Input.mousePosition - Camera.main.transform.position ).normalized);

            Ray ray = Camera.main.ScreenPointToRay( GetMouse() );
            Debug.DrawRay(ray.origin, ray.direction, Color.white, 100, false);

        }
	}

    Vector3 GetMouse( ) {	return Camera.main.ScreenToWorldPoint( Input.mousePosition ); }
}
