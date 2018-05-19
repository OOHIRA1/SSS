﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==タイトルシーンを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class TitleManager : MonoBehaviour {
	[SerializeField] RayShooter _rayShooter = null;
	[SerializeField] Curtain _curtain = null;
	[SerializeField] ScenesManager _scenesManager = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = _rayShooter.Shoot (Input.mousePosition);
			if (hit) {
				if (hit.collider.name == "StartButton") {
					_curtain.Close ();
				}
			}
		}
		if (_curtain.IsStateClose () && _curtain.ResearchStatePlayTime () >= 1f) {
			_scenesManager.ScenesTransition ("StageSelect");
		}
	}
}
