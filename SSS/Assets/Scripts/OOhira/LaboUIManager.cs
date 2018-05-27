﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==探偵ラボUIを管理するクラス
//
//使用方法：LaboUIにアタッチ
public class LaboUIManager : MonoBehaviour {
	public enum Judge {
		YES,
		NO,
		OTHERWISE
	}
		
	[SerializeField] GameObject _controllUI = null;
	[SerializeField] GameObject _judgeUI = null;
	[SerializeField] Judge _judged;					//JudgeUIで何を選択したかを持つ変数


	//=================================================
	//ゲッター
	public Judge GetJudge() { return _judged; }
	//=================================================
	//=================================================

	//=================================================
	//セッター
	public void SetJudgeFlag(int x) { _judged = (Judge)x; }
	//=================================================
	//=================================================


	// Use this for initialization
	void Start () {
		_judged = Judge.OTHERWISE;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//===========================================
	//public関数

	//--ジャッジパネルを表示する関数
	public void DisplayJudgeUI( ) {
		_judgeUI.SetActive (true);
		//_controllUI.SetActive (false);	//judgeUIの表示位置変更により非アクティブする必要なし
	}

	//--ジャッジパネルを非表示にする関数
	public void DisappearJudgeUI( ) {
		_judgeUI.SetActive (false);
		//_controllUI.SetActive (true);		//judgeUIの表示位置変更により非アクティブする必要なし
	}

	//===========================================
	//===========================================
}
