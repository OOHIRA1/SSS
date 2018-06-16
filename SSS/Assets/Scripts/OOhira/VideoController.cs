using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//==Videoを管理するクラス
//
//使用方法：VideoPlayerがアタッチされているゲームオブジェクトにアタッチ
public class VideoController : MonoBehaviour {
	VideoPlayer _videoPlayer;
	[SerializeField] GameObject _videoScreen = null;	//Videoを映すスクリーン
//	[SerializeField] double[] _checkPointTime = null;	//途中再生時の時間の秒数(チェックポイントとなる時間の秒数)
	[SerializeField] double _maxTime = 0;				//動画の最大時間(単位：second) 読み取り専用(正直、無くてもよいが開発しやすくするため)
	[SerializeField] double _time = 0;					//現在の再生時間(単位：second) 読み取り専用(正直、無くてもよいが開発しやすくするため)
	[SerializeField] VideoClip[] _videoClips = null;


	//========================================================
	//ゲッター
	public double GetMaxTime() { return _maxTime; }
	public double GetTime() { return _videoPlayer.time;}//_time; }
	//========================================================
	//========================================================


	// Use this for initialization
	void Start () {
		_videoPlayer = GetComponent<VideoPlayer> ();
		_maxTime = (double)_videoPlayer.frameCount / _videoPlayer.frameRate;
		_videoPlayer.Stop ();//停止をする
		_videoScreen.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//デバッグ用------------------------------------
		if (Input.GetKeyDown (KeyCode.A))
			_videoPlayer.Pause ();
		if (Input.GetKeyDown (KeyCode.B))
			PlayVideo ();
		if (Input.GetKeyDown (KeyCode.C))
			FinishVideo ();
		if (Input.GetKeyDown (KeyCode.D))
			PauseVideo();//_videoPlayer.frame = _flame;
		if (Input.GetKeyDown (KeyCode.E))
			_videoPlayer.time = _time;
		if (Input.GetKeyDown (KeyCode.Alpha0))
			ChangeVideoClip ( 0 );
		if (Input.GetKeyDown (KeyCode.Alpha1))
			ChangeVideoClip ( 1 );
		//---------------------------------------------
		_maxTime = (double)_videoPlayer.frameCount / _videoPlayer.frameRate;	//_maxTimeの更新
		_time = _videoPlayer.time;												//_timeの更新
	}


	//======================================================================================
	//public関数

	//--ビデオを(time秒目から)再生する関数
	public void PlayVideo( double time = 0 ) {
//		if (!_videoScreen.activeInHierarchy) {//この方法だとビルド後にクラッシュする
//			_videoScreen.SetActive (true);
//		}
//		_videoPlayer.time = time;
//		_videoPlayer.Play ();
		StartCoroutine(PlayVideoCoroutine(time));
	}


	//--ビデオのclipを入れ替える関数
	public void ChangeVideoClip( int videoClipsIndex ) {
		_videoPlayer.Stop ();
		_videoPlayer.clip = _videoClips[videoClipsIndex];
	}


	//--ビデオ再生を終了する関数
	public void FinishVideo() {
		_videoPlayer.Stop ();
		_videoScreen.SetActive (false);
	}


	//--ビデオを一時停止する関数
	public void PauseVideo() {
		_videoPlayer.Pause ();
	}


	//--ビデオを再生中かどうかを返す関数
	public bool IsPlaying() {
		return _videoPlayer.isPlaying;
	}


	//--ビデオを一時停止した地点から再生する関数(ボタンで呼ぶため)
	public void PlayVideoFromPausePoint() {
		_videoPlayer.Play ();
	}
	//======================================================================================
	//======================================================================================


	//--ビデオを(time秒目から)再生する関数(コルーチン)
	IEnumerator PlayVideoCoroutine( double time = 0 ) {
		if (!_videoScreen.activeInHierarchy) {
			_videoScreen.SetActive (true);
		}
		_videoPlayer.Play ();
		bool timeSelected = false;
		while (!timeSelected) {
			if (_videoPlayer.isPlaying) {
				_videoPlayer.time = time;
				timeSelected = true;
			}
			yield return new WaitForSeconds (Time.deltaTime);
		}
	}
}
