using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==落下トリガーを管理するクラス
//
//使用方法：FalledTriggerにアタッチ
[RequireComponent(typeof(BoxCollider2D))]
public class FalledTrigger : MonoBehaviour {
	[SerializeField] Effect _fallEffectPlayer = null;
	[SerializeField] Effect _fallEffectNpc = null;
	bool _falledFlag;		//落ちたかどうかのフラグ
	GameObject _falledGameObject;	//落下したゲームオブジェクト


	//======================================================
	//ゲッター
	public bool GetFallFlag() { return _falledFlag;	}
	public GameObject GetFalledGameObject() { return _falledGameObject;	}
	//======================================================
	//======================================================

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_fallEffectPlayer.gameObject.activeInHierarchy) {
			//アニメーションを終了したら消す処理------------------
			if (_fallEffectPlayer.ResearchStatePlayTime () >= 0.9f) {	//1fにすると2ループ目エフェクトが表示されてしまうため0.9f
				_fallEffectPlayer.gameObject.SetActive (false);
				_falledFlag = true;
			}
			//--------------------------------------------------
		}
		if (_fallEffectNpc.gameObject.activeInHierarchy) {
			//アニメーションを終了したら消す処理------------------
			if (_fallEffectNpc.ResearchStatePlayTime () >= 0.9f) {	//1fにすると2ループ目エフェクトが表示されてしまうため0.9f
				_fallEffectNpc.gameObject.SetActive (false);
				_falledFlag = true;
			}
			//--------------------------------------------------
		}
	}


	//======================================================
	//衝突検出関数
	void OnTriggerEnter2D( Collider2D col ) {
		if ( col.gameObject.tag == "Player" ) {
			_fallEffectPlayer.gameObject.SetActive (true);
			_falledGameObject = col.gameObject;
		}
		if ( col.gameObject.tag == "Npc" ) {
			_fallEffectNpc.gameObject.SetActive (true);
			_falledGameObject = col.gameObject;
		}
	}
	//======================================================
	//======================================================
}
