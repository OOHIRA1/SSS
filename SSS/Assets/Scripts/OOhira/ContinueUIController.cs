using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==コンティニューUIを管理するクラス
//
//使用方法：ContinuePanelにアタッチ
public class ContinueUIController : MonoBehaviour {
	[SerializeField] GameObject _clockUI = null;
	[SerializeField] SpriteRenderer[] _clockSpriteRenderers = null;
	[SerializeField] ScenesManager _scenesManager = null;
	Image[] _images;		//コンティニューUIのImage
	Button[] _buttons;	//コンティニューUIのButton
	GameDataManager _gameDataManager;
	EvidenceManager _evidenceManager;


	// Use this for initialization
	void Start () {
		_images = GetComponentsInChildren<Image> ();
		_buttons = GetComponentsInChildren<Button> ();
		_gameDataManager = GameObject.FindWithTag ("GameDataManager").GetComponent<GameDataManager> ();
		_evidenceManager = GameObject.FindWithTag ("EvidenceManager").GetComponent<EvidenceManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//--時間差でシーン遷移する関数(コルーチン)
	IEnumerator SceneTransitionCoroutine( string sceneName, float time = 3f ) {
		yield return new WaitForSeconds(time);
		_scenesManager.ScenesTransition (sceneName);
	}


	//--時間巻き戻し演出後にシーン遷移する関数(コルーチン)
	IEnumerator SceneTransitionWithAnim( string sceneName, float time = 1f ) {
		_clockUI.SetActive (true);
		for (int i = 0; i < _clockSpriteRenderers.Length; i++) {
			_clockSpriteRenderers [i].color = new Color ( 1, 1, 1, 0 );//透明化
		}
		float FadeInOutSpeed = 0.8f;
		//フェードイン処理-----------------------------------------------------------------------
		while (_clockSpriteRenderers [0].color.a < 1) {
			for (int i = 0; i < _clockSpriteRenderers.Length; i++) {
				_clockSpriteRenderers [i].color += new Color (0, 0, 0, FadeInOutSpeed * Time.deltaTime); 
			}
			yield return new WaitForSeconds (Time.deltaTime);
		}
		//--------------------------------------------------------------------------------------
		yield return new WaitForSeconds(1.5f);//1.5秒待機
		//フェードアウト処理-----------------------------------------------------------------------
		while (_clockSpriteRenderers [0].color.a > 0) {
			for (int i = 0; i < _clockSpriteRenderers.Length; i++) {
				_clockSpriteRenderers [i].color += new Color (0, 0, 0, -FadeInOutSpeed * Time.deltaTime); 
			}
			yield return new WaitForSeconds (Time.deltaTime);
		}
		//--------------------------------------------------------------------------------------
		StartCoroutine (SceneTransitionCoroutine(sceneName, time));
	}


	//==================================================================
	//public関数

	//--yesボタンを押したときの処理をする関数
	public void YesSelectAction( ) {
		_clockUI.SetActive (true);
		for (int i = 0; i < _images.Length; i++) {
			_images [i].color = new Color (1, 1, 1, 0);//透明化
		}
		for ( int i = 0; i < _buttons.Length; i++ ) {
			_buttons [i].enabled = false;
		}
		StartCoroutine (SceneTransitionWithAnim("DetectiveOffice"));
	}


	//--noボタンを押したときの処理をする関数
	public void NoSelectAction( ) {
		for (int i = 0; i < _images.Length; i++) {
			_images [i].color = new Color (1, 1, 1, 0);//透明化
		}
		for ( int i = 0; i < _buttons.Length; i++ ) {
			_buttons [i].enabled = false;
		}
		_gameDataManager.AllResetAdvencedData ();
		_evidenceManager.AllResetEvidenceData ();
		StartCoroutine (SceneTransitionCoroutine("StageSelect"));
	}
	//===================================================================
	//===================================================================
}
