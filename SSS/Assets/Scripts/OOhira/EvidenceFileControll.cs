﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==証拠品ファイルの表示・非表示を管理するクラス
//
//使用方法：表示・非表示時にアクティブなゲームオブジェクトにアタッチ
public class EvidenceFileControll : MonoBehaviour {
	[SerializeField] GameObject _evidenceFile = null;

	// Use this for initialization
	//void Start () {
		//_evidenceFile.SetActive (false);
	//}
	

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

	//--証拠品ファイルを開いているかどうかを返す関数
	public bool IsOpeningFile() {
		return _evidenceFile.activeInHierarchy;
	}
	//===========================================
	//===========================================
}
