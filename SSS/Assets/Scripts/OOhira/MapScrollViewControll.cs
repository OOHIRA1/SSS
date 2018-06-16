using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==マップの表示・非表示を管理するクラス
//
//使用方法：表示・非表示時にアクティブなゲームオブジェクトにアタッチ
public class MapScrollViewControll : MonoBehaviour {
	[SerializeField] GameObject _mapScrollView = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//===========================================
	//public関数

	//--証拠品ファイルを表示する関数
	public void DisplayMapScrollView() {
		_mapScrollView.SetActive (true);
	}

	//--証拠品ファイルを非表示にする関数
	public void DisappearMapScrollView() {
		_mapScrollView.SetActive (false);
	}

	//===========================================
	//===========================================
}
