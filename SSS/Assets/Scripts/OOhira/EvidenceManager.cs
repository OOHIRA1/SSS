using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==証拠品を手に入れたかどうかを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ(他のシーンに引き継げるようにDontDestroyにする)
public class EvidenceManager : MonoBehaviour {
	public enum Evidence {
		STORY1_EVIDENCE1 = 1 << 0,	//1,
		STORY1_EVIDENCE2 = 1 << 1,	//2,
		STORY1_EVIDENCE3 = 1 << 2,	//4,
		STORY1_EVIDENCE4 = 1 << 3,	//8,
		STORY1_EVIDENCE5 = 1 << 4,	//16,
		STORY1_EVIDENCE6 = 1 << 5,	//32,
	}

	[SerializeField] int _evidenceData = 0;	//証拠品取得状況を管理する変数(ビットフィールド)


	//==================================================================================
	//ゲッター
	public int GetEvidenceData() { return _evidenceData; }
	//==================================================================================
	//==================================================================================


	// Use this for initialization
	void Start () {
		//2つ以上存在しないようにする処理-------------------------------------------------------------
		GameObject[] evidenceManager = GameObject.FindGameObjectsWithTag ("EvidenceManager");
		if (evidenceManager.Length >= 2) {
			for (int i = 0; i < evidenceManager.Length; i++) {
				if (evidenceManager [i].scene.name != "DontDestroyOnLoad") {
					GameObject.Destroy (evidenceManager [i]);
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


	//===============================================================================================
	//public関数

	//--evidenceを取得した情報を格納する関数
	public void UpdateEvidence( Evidence evidence ) {
		_evidenceData = _evidenceData | (int)evidence;
	}


	//--evidenceを取得しているか確認する関数
	public bool CheckEvidence( Evidence evidence ) {
		return ( _evidenceData & (int)evidence ) == (int)evidence;
	}
	//===============================================================================================
	//===============================================================================================
}
