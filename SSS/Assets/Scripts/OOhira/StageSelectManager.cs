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
	EvidenceManager _evidenceManager;
	BGMManager _bgmManager;
	[SerializeField] AudioSource _openingBuzzer = null;
	bool _buzzerSounded; //ブザー音が鳴ったかどうかのフラグ
	[SerializeField] GameObject _clearStamp = null;	//クリアスタンプ
	[SerializeField] StaffCreditPageControll _staffCreditPageControll = null;
	[SerializeField] StageSelectUIManager _stageSelectUIManager = null;
	bool _staffCreditButtonInteractableChanged;//スタッフクレジットを押せるようにするフラグ

	// Use this for initialization
	void Start () {
		_gameDataManager = GameObject.FindWithTag ("GameDataManager").GetComponent<GameDataManager>();
		_evidenceManager = GameObject.FindWithTag ("EvidenceManager").GetComponent<EvidenceManager> ();
		_bgmManager = GameObject.FindWithTag ("BGMManager").GetComponent<BGMManager> ();
		_buzzerSounded = false;
		if (!_gameDataManager.CheckAdvancedData (GameDataManager.CheckPoint.CLEAR_EPISODE1)) {
			_clearStamp.SetActive (false);//エピソード1をクリアしていないとスタンプが押されない
		}
		_bgmManager.UpdateBGM ();//BGMを鳴らす処理(ゲームリザルトシーンから遷移後に鳴らすため)
		_staffCreditButtonInteractableChanged = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (_staffCreditPageControll.IsOpeningFile ()) return;//スタッフクレジットを開いている時は処理しない

		//カーテンが開ききるまでスタッフクレジットボタンを押せなくする処理-----------
		if (!_staffCreditButtonInteractableChanged) {
			if (_curtain.IsStateWait ()) {
				_stageSelectUIManager.StaffCreditButtonIntaractive (false);
			} else if (_curtain.ResearchStatePlayTime () >= 1f) {
				_stageSelectUIManager.StaffCreditButtonIntaractive (true);
				_staffCreditButtonInteractableChanged = true;
			}
		}
		//------------------------------------------------------------------------

		//ボタンの検出------------------------------------------------------------
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = _rayShooter.Shoot (Input.mousePosition);
			if (hit) {
				if (hit.collider.name == "Stage1Button") {
					_openingBuzzer.PlayOneShot (_openingBuzzer.clip);
					_buzzerSounded = true;
					SpriteRenderer sr = hit.collider.GetComponent<SpriteRenderer> ();
					sr.color = new Color (200 / 255f, 200 / 255f, 200 / 255f, 1f);//色を変える処理
					if (_staffCreditPageControll.IsOpeningFile ()) {
						_staffCreditPageControll.DisappearStaffCreditPage ();//非表示にする　※パットでセレクトボタンとスタッフクレジットボタンのダブルタップ対策
					}
					_stageSelectUIManager.StaffCreditButtonIntaractive(false);//スタッフクレジットボタンを反応しなくする
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
			//エピソード1をクリアしていたらクリアデータのみを残し、データをリセットしてシーン遷移--------------------
			if (_gameDataManager.CheckAdvancedData (GameDataManager.CheckPoint.CLEAR_EPISODE1)) {
				_evidenceManager.AllResetEvidenceData ();
				_gameDataManager.AllResetAdvencedData ();
				_gameDataManager.UpdateAdvancedData (GameDataManager.CheckPoint.CLEAR_EPISODE1);
				//static変数の初期化----------------
				SiteManager._remark = false;
				SiteManager._conditions1 = false;
				SiteManager._conditions2 = false;
				SiteManager._endStory = false;
				SiteMove._nowSiteNum = 0;
				DetectiveOfficeManager._showCursorForCriminalChoiceButton = true;
				//----------------------------------
				_scenesManager.ScenesTransition ("SiteNight_Bedroom");
			}
			//---------------------------------------------------------------------------------------------------
			//富豪の殺人アニメーションを見ているかで遷移先を変える--------------------------------------------------
			if (!_gameDataManager.CheckAdvancedData (GameDataManager.CheckPoint.SHOW_MILLIONARE_MURDER_ANIM)) {
				_scenesManager.ScenesTransition ("SiteNight_Bedroom");
			} else {
				_scenesManager.ScenesTransition ("DetectiveOffice");
			}
			//----------------------------------------------------------------------------------------------------
		}
		//----------------------------------------------------------------------------------------------------------

		//デバッグ用---------------------------------------
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			_scenesManager.ScenesTransition ("Title");
		}
		//------------------------------------------------
	}
}
