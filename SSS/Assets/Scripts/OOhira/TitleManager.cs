using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==タイトルシーンを管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class TitleManager : MonoBehaviour {
	[SerializeField] RayShooter _rayShooter = null;
	[SerializeField] Curtain _curtain = null;
	[SerializeField] ScenesManager _scenesManager = null;
	[SerializeField] Image _fadeInPanel = null;
	[SerializeField] float _fadeInSpeed = 0;	//明転処理のスピード(alpha/second)
	[SerializeField] UnityEngine.UI.Button _stratButton = null;
	bool _fadeInFinishedFlag;					//フェードインを終わったかどうかのフラグ

	// Use this for initialization
	void Start () {
		_fadeInFinishedFlag = false;
		_stratButton.enabled = false;//最初はボタンを押せなくする
	}
	
	// Update is called once per frame
	void Update () {
		if (!_fadeInFinishedFlag) {
			FadeInAction ();
			return;
		}
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = _rayShooter.Shoot (Input.mousePosition);
			if (hit) {
				if (hit.collider.name == "StartButton") {
					_curtain.Close ();
				}
			}
		}
		if (_curtain.IsStateClose () && _curtain.ResearchStatePlayTime () >= 1f) {
			_scenesManager.ScenesTransition ("StageSelect");
		}
	}


	//--FADE_IN時の処理をする関数
	void FadeInAction() {
		if (_fadeInPanel.color.a > 0) {
			Color color = _fadeInPanel.color;
			_fadeInPanel.color = new Color (color.r, color.g, color.b, color.a - _fadeInSpeed * Time.deltaTime);
		} else {
			_fadeInPanel.gameObject.SetActive (false);
			_stratButton.enabled = true;//ボタンを押せるようにする
		}
	}
}
