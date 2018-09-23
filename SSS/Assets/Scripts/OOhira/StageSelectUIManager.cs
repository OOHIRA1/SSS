using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==ステージセレクトUIを管理するクラス
//
//使用方法：StageSelectUICanvasにアタッチ
public class StageSelectUIManager : MonoBehaviour {
	[SerializeField] Button _staffCreditButton = null;//スタッフクレジットボタン

	//======================================================
	//public関数

	//--スタッフクレジットボタンを反応しなくようにする関数
	public void StaffCreditButtonIntaractive( bool x ) {
		_staffCreditButton.interactable = x;
	}
	//======================================================
	//======================================================
}
