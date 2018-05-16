using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==証拠品を手に入れたかどうかを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ(他のシーンに引き継げるようにDontDestroyにする)
public class EvidenceManager : MonoBehaviour {
	public enum Evidence {
		STORY1_EVIDENCE1 = 1,
		STORY1_EVIDENCE2 = 2,
		STORY1_EVIDENCE3 = 4,
		STORY1_EVIDENCE4 = 8,
		STORY1_EVIDENCE5 = 16,
		STORY1_EVIDENCE6 = 32,
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
		for (int i = 0; i < evidenceManager.Length; i++) {
			if (this.gameObject != evidenceManager [i]) {
				Destroy (evidenceManager [i]);
			} else {
				if (this.gameObject.scene.name != "DontDestroyOnLoad") {
					GameObject.DontDestroyOnLoad (this);	//DontDestroy処理
				}
			}
		}
		//-------------------------------------------------------------------------------------------
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			UpdateEvidence (Evidence.STORY1_EVIDENCE1);
		}
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			UpdateEvidence (Evidence.STORY1_EVIDENCE2);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			UpdateEvidence (Evidence.STORY1_EVIDENCE3);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			UpdateEvidence (Evidence.STORY1_EVIDENCE4);
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			UpdateEvidence (Evidence.STORY1_EVIDENCE5);
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			UpdateEvidence (Evidence.STORY1_EVIDENCE6);
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			Debug.Log(CheckEvidence (Evidence.STORY1_EVIDENCE1));
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			Debug.Log(CheckEvidence (Evidence.STORY1_EVIDENCE2));
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			Debug.Log(CheckEvidence (Evidence.STORY1_EVIDENCE3));
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			Debug.Log(CheckEvidence (Evidence.STORY1_EVIDENCE4));
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			Debug.Log(CheckEvidence (Evidence.STORY1_EVIDENCE5));
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			Debug.Log(CheckEvidence (Evidence.STORY1_EVIDENCE6));
		}
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
