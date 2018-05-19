using System.Collections;
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

	[SerializeField] GameObject _evidenceFile = null;
	[SerializeField] GameObject _controllUI = null;
	[SerializeField] GameObject _judgeUI = null;
	[SerializeField] Judge _judged;


	//=================================================
	//ゲッター
	public Judge GetJudge() { return _judged; }
	//=================================================
	//=================================================

	//=================================================
	//セッター
	public void SetJudgeFlag(Judge x) { _judged = x; }
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

	//--証拠品ファイルを表示する関数
	public void DisplayEvidenceFile() {
		_evidenceFile.SetActive (true);
	}

	//--証拠品ファイルを非表示にする関数
	public void DisappearEvidenceFile() {
		_evidenceFile.SetActive (false);
	}

	//--ジャッジパネルを表示する関数
	public void DisplayJudgeUI( ) {
		_judgeUI.SetActive (true);
		_controllUI.SetActive (false);
	}

	//--ジャッジパネルを非表示にする関数
	public void DisappearJudgeUI( ) {
		_judgeUI.SetActive (false);
		_controllUI.SetActive (true);
	}

	//===========================================
	//===========================================
}
