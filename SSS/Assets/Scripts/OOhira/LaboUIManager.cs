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
	public enum ClockUIKind {//時計UIの種類
		BEDROOM,
		KITCHEN,
		SERVING_ROOM,
		GARDEN,
		CLOCKUI_MAX
	}
		
	//[SerializeField] GameObject _controllUI = null;
	[SerializeField] GameObject _judgeUI = null;
	[SerializeField] Judge _judged;								//JudgeUIで何を選択したかを持つ変数
	bool _crimimalChoiseFlag;									//犯人指摘をするかどうかのフラグ
	[SerializeField] UnityEngine.UI.Button _criminalChoiseButton = null;	//犯人指摘ボタン
	[SerializeField] UnityEngine.UI.Button _evidenceButton = null;				//証拠品ファイルボタン(UnityEngine.UI.Buttonで宣言しないとinteractableが使えない)
	[SerializeField] UnityEngine.UI.Button _crimeSceneButton = null;			//事件現場遷移ボタン
	[SerializeField] UnityEngine.UI.Button _mapButton = null;					//マップボタン
	UnityEngine.UI.Button[] _laboUIButtons = null;								//ラボUIのボタン
	[SerializeField] GameObject[] _clockUIs = new GameObject[(int)ClockUIKind.CLOCKUI_MAX];	//時計UI
	[SerializeField] CrimeSceneTrasitionButton _crimeSceneTransitionButton = null;

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
		_laboUIButtons = GetComponentsInChildren<UnityEngine.UI.Button> ();
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


	//--事件現場遷移ボタンを表示する関数
	public void DisplayCrimeSceneButton( ) {
		_crimeSceneButton.gameObject.SetActive (true);
	}


	//--事件現場遷移ボタンを非表示にする関数
	public void DisappearCrimeSceneButton( ) {
		_crimeSceneButton.gameObject.SetActive (false);
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


	//--ラボUIのボタンを反応しないようにする関数
	public void ChangeAllButtonIntaractive( bool x ) {
		_laboUIButtons = GetComponentsInChildren<UnityEngine.UI.Button> ();
		for (int i = 0; i < _laboUIButtons.Length; i++) {
			_laboUIButtons [i].interactable = x;
		}
	}


	//--厨房の昼と夕方しか反応しなくする関数(現状この状況しか使わない可能性が高いのでピンポイントで作成)
	public void ClockUIButtonIntaractive( ) {
		for (int i = 0; i < _clockUIs.Length; i++) {
			if (_clockUIs [i].activeInHierarchy) {//時計UIが表示されていたら
				Button[] _buttons = _clockUIs [i].GetComponentsInChildren<Button>();
				for (int j = 0; j < _buttons.Length; j++) {
					//厨房でない　または　（厨房だとしても）NoonButtonでもAfterNoonButtonでもない時-------------------------------------------------------------------
					if (i != (int)ClockUIKind.KITCHEN || (_buttons [j].gameObject.name != "NoonButton" && _buttons [j].gameObject.name != "AfterNoonButton") ) {
						_buttons [j].interactable = false;
					}
					//-----------------------------------------------------------------------------------------------------------------------------------------------
				}
			}
		}
	}


	//--事件現場遷移ボタンを押したかどうかを返す関数
	public bool GetCrimeSceneTransitionButtonPushed() {
		return _crimeSceneTransitionButton.GetCrimeSceneTransitionButtonPushed ();
	}


	//--各事件現場ボタンを押したかどうか確認する関数
	public bool CheckCrimeSceneButton( int num ) {
		return _crimeSceneTransitionButton.CheckCrimeSceneButton ( num );
	}


	//--事件現場ボタンを押せなくする関数
	public void ChangeCrimeSceneButtonInteractive( bool x, int notChangeNum = -1 ) {
		_crimeSceneTransitionButton.ChangeCrimeSceneButtonInteractive (x, notChangeNum);
	}


	//--時計UIを押したかどうかを確認する関数
	public bool CheckClockUIPushed() {
		return _crimeSceneTransitionButton.CheckClockUIPushed ();
	}
	//===========================================
	//===========================================
}
