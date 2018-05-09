using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenesTest : MonoBehaviour {
	ScenesManager aaa;
	// Use this for initialization
	void Start () {
		aaa = GetComponent<ScenesManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			aaa.ScenesTransition ("SiteNoon_Bedroom");
		}
	}
}
