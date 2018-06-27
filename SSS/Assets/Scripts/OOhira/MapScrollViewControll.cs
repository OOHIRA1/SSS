using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==マップの表示・非表示を管理するクラス
//
//使用方法：表示・非表示時にアクティブなゲームオブジェクトにアタッチ
public class MapScrollViewControll : MonoBehaviour {
	[SerializeField] GameObject _mapScrollView = null;

	//===========================================
	//public関数

	//--マップファイルを表示する関数
	public void DisplayMapScrollView() {
		_mapScrollView.SetActive (true);
	}

	//--マップファイルを非表示にする関数
	public void DisappearMapScrollView() {
		_mapScrollView.SetActive (false);
	}

	//--マップファイルを開いているかどうかを返す関数
	public bool IsOpeningFile() {
		return _mapScrollView.activeInHierarchy;
	}


	//===========================================
	//===========================================
}
