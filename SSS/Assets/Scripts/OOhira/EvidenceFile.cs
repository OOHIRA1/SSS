using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==証拠品ファイルバナーUIを管理するクラス
//
//使用方法：証拠品ファイルにアタッチ
public class EvidenceFile : MonoBehaviour {
	EvidenceManager _evidenceManager;
	[SerializeField] GameObject[] _evidenceBannar = new GameObject[6];		//証拠品バナー
	[SerializeField] EvidenceManager.Evidence[] _enum = null;				//証拠品バナーに対応した証拠品(列挙体)
	[SerializeField] GameObject _content = null;							//証拠品バナーの親オブジェクト

	// Use this for initialization
	void Start () {
		_evidenceManager = GameObject.FindGameObjectWithTag ("EvidenceManager").GetComponent<EvidenceManager>();

		for (int i = 0; i < _evidenceBannar.Length; i++) {
			_evidenceBannar [i] = (GameObject)Resources.Load ("EvidenceBannar/Evidence" + (i + 1));	//証拠品バナーのロード
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//=========================================================================================================================================
	//public関数
	//--証拠品バナーを最新の状態にアップデートする関数
	public void UpdateEvidenceBannar() {
		//証拠品バナーの追加---------------------------------------------------------------------------------------------------------------
		RectTransform contentRectTransform = _content.GetComponent<RectTransform> ();
		Transform[ ] evidenceBannarTransform = _content.GetComponentsInChildren<Transform> (); 
		for (int i = 0; i < _evidenceBannar.Length; i++) {
			if (_evidenceManager.CheckEvidence (_enum [i])) {
				bool instantiateFlag = true;	//証拠品バナーを新しく生成するかのフラグ
				for (int j = 0; j < evidenceBannarTransform.Length; j++) {
					if (evidenceBannarTransform [j].gameObject.name ==  "Evidence" + (i + 1) + "(Clone)") {
						instantiateFlag = false;
					}
				}
				if (instantiateFlag) {
					GameObject.Instantiate (_evidenceBannar [i], _content.transform.position, Quaternion.identity, _content.transform);
					Vector2 sizeDelta = _evidenceBannar [i].GetComponent<RectTransform> ().sizeDelta;
					contentRectTransform.sizeDelta += new Vector2 (0, sizeDelta.y);
				}
			}
		}
		contentRectTransform.anchoredPosition = Vector2.zero;	//anchoredPositionの初期化(これをしないとなぜか毎回yが200の状態で始まる)
		//--------------------------------------------------------------------------------------------------------------------------------
	}
	//=========================================================================================================================================
	//=========================================================================================================================================
}
