using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==スタッフクレジットの表示・非表示を管理するクラス
//
//使用方法：表示・非表示時にアクティブなゲームオブジェクトにアタッチ
public class StaffCreditPageControll : MonoBehaviour {
	[SerializeField] GameObject _staffCreditPage = null;

	//====================================================
	//public関数

	//--スタッフクレジットを表示する関数
	public void DisplayStaffCreditPage() {
		_staffCreditPage.SetActive (true);
	}


	//--スタッフクレジットを非表示にする関数
	public void DisappearStaffCreditPage() {
		_staffCreditPage.SetActive (false);
	}


	//--スタッフクレジットを開いているかどうかを返す関数
	public bool IsOpeningFile() {
		return _staffCreditPage.activeInHierarchy;
	}
	//====================================================
	//====================================================
}
