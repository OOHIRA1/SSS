using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==BGMを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class BGMManager : MonoBehaviour {
	public enum BGMClip {
		TITLE,
		CRIME_SCENE,
		DETECTIVE_OFFICE,
		CHOOSE,
		SIXTY_ANIMATION,
		CLIMAX_BGM
	}

	SoundLibrary _soundLibrary;


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

		_soundLibrary = GetComponent<SoundLibrary> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Camera.main.gameObject.scene.name);
		//UpdateBGM ();
	}


	//======================================================================
	//public関数

	//--BGMをアップデートする関数
	public void UpdateBGM() {
		if (!_soundLibrary) return;
		switch (Camera.main.gameObject.scene.name) {
		case "Title":

		case "StageSelect":
			if (!_soundLibrary.IsPlaying ((int)BGMClip.TITLE)) {
				_soundLibrary.PlaySound ((int)BGMClip.TITLE);
			}
			break;
		case "SiteNight":

		case "SiteNoon":

		case "SiteEvening"://音を入れたくない部分は上手く制御してください
			if (!_soundLibrary.IsPlaying ((int)BGMClip.CRIME_SCENE)) {
				_soundLibrary.PlaySound ((int)BGMClip.CRIME_SCENE);
			}
			break;
		case "DetectiveOffice":
			DetectiveOfficeManager detectiveOfficeManager = GameObject.Find ("DetectiveOfficeManager").GetComponent<DetectiveOfficeManager> ();
			switch (detectiveOfficeManager.GetState ()) {
			case DetectiveOfficeManager.State.CRIMINAL_CHOISE:

			case DetectiveOfficeManager.State.DANGEROUS_WEAPON_CHOISE:

			case DetectiveOfficeManager.State.FINAL_JUDGE:
				if (!_soundLibrary.IsPlaying ((int)BGMClip.CHOOSE)) {
					_soundLibrary.PlaySound ((int)BGMClip.CHOOSE);
				}
				break;
			default:
				if (detectiveOfficeManager.GetDetectiveTalkIndex () >= 3) {//犯人指摘中の会話のBGM
					if (!_soundLibrary.IsPlaying ((int)BGMClip.CHOOSE)) {
						_soundLibrary.PlaySound ((int)BGMClip.CHOOSE);
					}
				} else {
					if (!_soundLibrary.IsPlaying ((int)BGMClip.DETECTIVE_OFFICE)) {
						_soundLibrary.PlaySound ((int)BGMClip.DETECTIVE_OFFICE);
					}
				}
				break;
			}
			break;
		case "ClimaxBattle":
			if (!_soundLibrary.IsPlaying ((int)BGMClip.CLIMAX_BGM)) {
				_soundLibrary.PlaySound ((int)BGMClip.CLIMAX_BGM);
			}
			break;
		default:
			_soundLibrary.StopSound ();
			break;
		}
	}


	//--音をフェードアウトし止める関数
	public void StopBGMWithFadeOut() {
		_soundLibrary.StopSoundWithFadeOut ();
	}


	//--_audioClipsにある音がなっているかどうかを確認する関数
	public bool IsPlaying( BGMClip bgmClip ) {
		return _soundLibrary.IsPlaying ( (int)bgmClip );
	}


	//--音を止める関数
	public void StopBGM() {
		_soundLibrary.StopSound ();
	}


	//--音を一時停止する関数
	public void PauseBGM() {
		_soundLibrary.PauseSound ();
	}


	//--音量調整をする関数
	public void ChangeVolume( float volume ) {
		_soundLibrary.ChangeVolume (volume);
	}
	//======================================================================
	//======================================================================
}
