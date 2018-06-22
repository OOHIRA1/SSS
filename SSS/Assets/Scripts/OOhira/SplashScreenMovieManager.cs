using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//==SplashScreenMovieシーンを管理するクラス
//
//==使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class SplashScreenMovieManager : MonoBehaviour {
	[SerializeField] VideoPlayer _videoPlayer = null;
	[SerializeField] ScenesManager _scenesManager = null;
	bool _isPlaying;	//ビデオが再生されたかどうかのフラグ

	// Use this for initialization
	void Start () {
		_isPlaying = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (_videoPlayer.isPlaying) {
			_isPlaying = true;
		}
		if (_isPlaying && !_videoPlayer.isPlaying) {//再生が終わったらタイトルシーンに遷移
			_scenesManager.ScenesTransition ("Title");
		}
	}
}
