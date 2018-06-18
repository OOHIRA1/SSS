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
	[SerializeField] AudioSource _openingBuzzer = null;
	bool _buzzerSounded; //ブザー音が鳴ったかどうかのフラグ

	// Use this for initialization
	void Start () {
		_gameDataManager = GameObject.FindWithTag ("GameDataManager").GetComponent<GameDataManager>();
		_bgmManager = GameObject.FindWithTag ("BGMManager").GetComponent<BGMManager> ();
		_buzzerSounded = false;
	}
	
	// Update is called once per frame
	void Update () {
		//ボタンの検出------------------------------------------------------------
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = _rayShooter.Shoot (Input.mousePosition);
			if (hit) {
				if (hit.collider.name == "Stage1Button") {
					_openingBuzzer.PlayOneShot (_openingBuzzer.clip);
					_buzzerSounded = true;
					hit.collider.enabled = false;//2回以上反応しないため
					//_curtain.Close ();
				}
			}
		}
		//------------------------------------------------------------------------

		//ブザー音が鳴りやんだらカーテンを閉める--------------------------------------
		if ( _buzzerSounded && !_openingBuzzer.isPlaying ) {
			_curtain.Close ();
			_buzzerSounded = false;	//_buzzerSoundedのリセット
			_bgmManager.StopBGMWithFadeOut();
		}
		//--------------------------------------------------------------------------

		//BGMのフェードアウトが終わったらシーン遷移------------------------------------------------------------------
		//if (_curtain.IsStateClose () && _curtain.ResearchStatePlayTime () >= 1f ) {
		if (!_bgmManager.IsPlaying(BGMManager.BGMClip.TITLE)) {
			if (!_gameDataManager.CheckAdvancedData (GameDataManager.CheckPoint.SHOW_MILLIONARE_MURDER_ANIM)) {
				_scenesManager.ScenesTransition ("SiteNight_Bedroom");
			} else {
				_scenesManager.ScenesTransition ("DetectiveOffice");
			}
		}
		//----------------------------------------------------------------------------------------------------------

		//デバッグ用---------------------------------------
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			_scenesManager.ScenesTransition ("Title");
		}
		//------------------------------------------------
	}
}
