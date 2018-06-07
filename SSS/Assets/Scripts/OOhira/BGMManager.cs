using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==BGMを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class BGMManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//2つ以上存在しないようにする処理-------------------------------------------------------------
		GameObject[] gameDataManager = GameObject.FindGameObjectsWithTag ("BGMManager");
		if (gameDataManager.Length >= 2) {
			for (int i = 0; i < gameDataManager.Length; i++) {
				if (gameDataManager [i].scene.name != "DontDestroyOnLoad") {
					GameObject.Destroy (gameDataManager [i]);
				}
			}
		} else {
			GameObject.DontDestroyOnLoad (this.gameObject);
		}
		//-------------------------------------------------------------------------------------------
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
