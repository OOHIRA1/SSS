using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour {
	[SerializeField] RayShooter _rayShooter = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = _rayShooter.Shoot ( Input.mousePosition );
		if (hit) {
			Debug.Log (hit.collider.name);
		}
	}
}
