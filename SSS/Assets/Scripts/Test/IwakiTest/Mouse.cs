using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {
	Vector3 _mousePosition;		//マウスのスクリーン座標
	Vector3 _pointPosition;		//Pointのワールド座標
	public GameObject _point;

	// Use this for initialization
	void Start () {
		//_pointPosition = _point.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.LogError ( Input.mousePosition );
		//Debug.Log ( Camera.main.WorldToScreenPoint( _point.transform.position ) );

		//if (  )
	}
}
