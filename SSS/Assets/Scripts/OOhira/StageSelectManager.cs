using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==StageSelectシーンを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class StageSelectManager : MonoBehaviour {
	[SerializeField] RayShooter _rayShooter = null;
	[SerializeField] Curtain _curtain = null;
	[SerializeField] ScenesManager _scenesManager = null;
	GameDataManager _gameDataManager;
	BGMManager _bgmManager;

	// Use this for initialization
	void Start () {
		_gameDataManager = GameObject.FindWithTag ("GameDataManager").GetComponent<GameDataManager>();
		_bgmManager = GameObject.FindWithTag ("BGMManager").GetComponent<BGMManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		//ボタンの検出------------------------------------------------------------
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = _rayShooter.Shoot (Input.mousePosition);
			if (hit) {
				if (hit.collider.name == "Stage1Button") {
					_curtain.Close ();
				}
			}
		}
		//------------------------------------------------------------------------

		if (_curtain.IsStateClose () && _curtain.ResearchStatePlayTime () >= 1f) {
			Destroy (_bgmManager.gameObject);
			if (!_gameDataManager.CheckAdvancedData (GameDataManager.CheckPoint.SHOW_MILLIONARE_MURDER_ANIM)) {
				_scenesManager.ScenesTransition ("SiteNight_Bedroom");
			} else {
				_scenesManager.ScenesTransition ("DetectiveOffice");
			}
		}

		//デバッグ用---------------------------------------
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			_scenesManager.ScenesTransition ("Title");
		}
		//------------------------------------------------
	}
}
