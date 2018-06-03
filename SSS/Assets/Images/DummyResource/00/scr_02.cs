using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_02 : MonoBehaviour {

	public static int num = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKey ( KeyCode.Q )){
			num++;
		}
	}
}
