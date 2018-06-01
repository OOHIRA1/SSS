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
		
	//[SerializeField] GameObject _controllUI = null;
	[SerializeField] GameObject _judgeUI = null;
	[SerializeField] Judge _judged;								//JudgeUIで何を選択したかを持つ変数
	[SerializeField] GameObject _criminalChoiseButton = null;	//犯人指摘ボタン
	bool _crimimalChoiseFlag;									//犯人指摘をするかどうかのフラグ


	//=================================================
	//ゲッター
	public Judge GetJudge() { return _judged; }
	public bool GetCriminalChoiseFlag() { return _crimimalChoiseFlag; }
	//=================================================
	//=================================================

	//=================================================
	//セッター
	public void SetJudgeFlag(int x) { _judged = (Judge)x; }
	public void SetCriminalChoiseFlag( bool x ) { _crimimalChoiseFlag = x; }
	//=================================================
	//=================================================


	// Use this for initialization
	void Start () {
		_judged = Judge.OTHERWISE;
		_criminalChoiseButton.SetActive (false);
		_crimimalChoiseFlag = false;
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

	//--犯人指摘ボタンを表示する関数
	public void DisplayCriminalChoiseButton( ) {
		_criminalChoiseButton.SetActive (true);
	}

	//--犯人指摘ボタンを非表示にする関数
	public void DisappearCriminalChoiseButton( ) {
		_criminalChoiseButton.SetActive (false);
	}

	//===========================================
	//===========================================
}
