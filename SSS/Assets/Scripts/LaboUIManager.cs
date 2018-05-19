using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==探偵ラボUIを管理するクラス
//
//使用方法：LaboUIにアタッチ
public class LaboUIManager : MonoBehaviour {
	[SerializeField] GameObject _evidenceFile = null;
	[SerializeField] GameObject _judgePanel = null;
	[SerializeField] bool _judgeFlag = false;			//ジャッジをするかどうかのフラグ


	//=================================================
	//ゲッター
	public bool GetJudgeFlag() { return _judgeFlag; }
	//=================================================
	//=================================================

	//=================================================
	//セッター
	public void SetJudgeFlag(bool x) { _judgeFlag = x; }
	//=================================================
	//=================================================


	// Use this for initialization
	void Start () {
		_judgeFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (_judgeFlag) {
			_judgePanel.SetActive (true);
		} else {
			_judgePanel.SetActive (false);
		}
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
	//public void 
	//===========================================
	//===========================================
}
