﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==Rayを発射するクラス
//
//アタッチ：カメラにアタッチ(カメラから発射するため)
public class RayShooter : MonoBehaviour {
	[SerializeField] float _drawRayDistance = 100f;	//描画するRayの長さ(距離)

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {			
	}

	//============================================================================
	//public関数
	//============================================================================
	//--RayをscreenPointへ飛ばし検出した2Dコライダーの情報(RaycastHit2D)を返す関数
	public RaycastHit2D Shoot( Vector3 screenPoint ){
		Ray ray = Camera.main.ScreenPointToRay ( screenPoint );
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction);
		Debug.DrawRay (ray.origin, ray.direction * _drawRayDistance, Color.red);
		return hit;
	}
	//============================================================================
	//============================================================================
}