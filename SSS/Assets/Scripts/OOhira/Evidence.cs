﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==証拠品の機能を付けるクラス
//
//使用方法：コライダーの付いた証拠品にアタッチ
[RequireComponent(typeof(BoxCollider2D))]
public class Evidence : MonoBehaviour {
	[SerializeField] GameObject _evidenceIcon = null;
	EvidenceManager _evidenceManager;
	[SerializeField] RayShooter _rayShooter = null;
	[SerializeField] EvidenceManager.Evidence _enum = EvidenceManager.Evidence.STORY1_EVIDENCE1;
	bool _stayEvidenceTriggerFlag;	//トリガー内にいるかどうかのフラグ(複数の証拠品を配置した際にどの証拠品か判別するため)

	// Use this for initialization
	public void Start () {
		_evidenceManager = GameObject.FindGameObjectWithTag ("EvidenceManager").GetComponent<EvidenceManager>();
		_stayEvidenceTriggerFlag = false;
	}
	
	// Update is called once per frame
	public void Update () {
		//証拠品取得処理-----------------------------------------------------
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = _rayShooter.Shoot (Input.mousePosition);
			if (hit) {
				if ( _stayEvidenceTriggerFlag && hit.collider.tag == "EvidenceIcon") {
					_evidenceManager.UpdateEvidence (_enum);
					this.gameObject.SetActive (false);
					hit.collider.gameObject.SetActive (false);
				}
			}
		}
		//-------------------------------------------------------------------
	}


	//=====================================================
	//コライダー検出系関数
	public void OnTriggerStay2D( Collider2D col ) {
		if (col.tag == "Player") {
			Detective detective = col.gameObject.GetComponent<Detective> ();
			if (!detective.GetIsAnimWalk ()) {
				_evidenceIcon.SetActive(true);
				_stayEvidenceTriggerFlag = true;
			}
		}
	}


	public void OnTriggerExit2D( Collider2D col ) {
		if (col.tag == "Player") {
			_evidenceIcon.SetActive(false);
			_stayEvidenceTriggerFlag = false;
		}
	}
		
	//=====================================================
	//=====================================================
}
