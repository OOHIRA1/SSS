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
	bool _crimimalChoiseFlag;									//犯人指摘をするかどうかのフラグ
	[SerializeField] UnityEngine.UI.Button _criminalChoiseButton = null;	//犯人指摘ボタン
	[SerializeField] UnityEngine.UI.Button _evidenceButton = null;				//証拠品ファイルボタン(UnityEngine.UI.Buttonで宣言しないとinteractableが使えない)
	[SerializeField] UnityEngine.UI.Button _crimeSceneButton = null;			//事件現場遷移ボタン
	[SerializeField] UnityEngine.UI.Button _mapButton = null;					//マップボタン

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
		_criminalChoiseButton.gameObject.SetActive (false);
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
		_criminalChoiseButton.gameObject.SetActive (true);
	}

	//--犯人指摘ボタンを非表示にする関数
	public void DisappearCriminalChoiseButton( ) {
		_criminalChoiseButton.gameObject.SetActive (false);
	}

	//--犯人指摘ボタンを反応しないようにする関数
	public void ChangeCriminalChoiseButtonIntaractive( bool x ) {
		_criminalChoiseButton.interactable = x;
	}


	//--証拠品ファイルボタンを反応しないようにする関数
	public void ChangeEvidenceButtonIntaractive( bool x ) {
		_evidenceButton.interactable = x;
	}


	//--事件現場遷移ボタンを反応しないようにする関数
	public void ChangeCrimeSceneButtonIntaractive( bool x ) {
		_crimeSceneButton.interactable = x;
	}


	//--マップボタンを反応しないようにする関数
	public void ChangeMapButtonIntaractive( bool x ) {
		_mapButton.interactable = x;
	}


	//--ラボシーンボタンを反応しないようにする関数
	public void ChangeLaboButtonIntaractive( bool x ) {
		ChangeCriminalChoiseButtonIntaractive (x);
		ChangeEvidenceButtonIntaractive (x);
		ChangeCrimeSceneButtonIntaractive (x);
		ChangeMapButtonIntaractive (x);
	}
	//===========================================
	//===========================================
}
