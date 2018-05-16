﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==探偵ラボUIを管理するクラス
//
//使用方法：LaboUIにアタッチ
public class LaboUIManager : MonoBehaviour {
	[SerializeField] GameObject _evidenceFile = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//===========================================
	//public関数

	//--証拠品ファイルを表示する関数
	public void DisplayEvidenceFile() {
		_evidenceFile.SetActive (true);
	}

	//--証拠品ファイルを非表示にする関数
	public void DisappearEvidenceFile() {
		_evidenceFile.SetActive (false);
	}
	//===========================================
	//===========================================
}
