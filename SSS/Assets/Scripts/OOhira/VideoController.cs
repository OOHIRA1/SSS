using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//==Videoを管理するクラス
//
//使用方法：VideoPlayerがアタッチされているゲームオブジェクトにアタッチ
public class VideoController : MonoBehaviour {
	VideoPlayer _videoPlayer;
	[SerializeField] long _flame = 0;
	[SerializeField] double _time = 0;
	[SerializeField] VideoClip[] _videoClips = null;

	// Use this for initialization
	void Start () {
		_videoPlayer = GetComponent<VideoPlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		//デバッグ用------------------------------------
		if (Input.GetKeyDown (KeyCode.A))
			_videoPlayer.Pause ();
		if (Input.GetKeyDown (KeyCode.B))
			_videoPlayer.Play ();
		if (Input.GetKeyDown (KeyCode.C))
			_videoPlayer.Stop ();
		if (Input.GetKeyDown (KeyCode.D))
			_videoPlayer.frame = _flame;
		if (Input.GetKeyDown (KeyCode.E))
			_videoPlayer.time = _time;
		if (Input.GetKeyDown (KeyCode.Alpha0))
			_videoPlayer.clip = _videoClips [0];
		if (Input.GetKeyDown (KeyCode.Alpha1))
			_videoPlayer.clip = _videoClips [1];
		Debug.Log (_videoPlayer.frameCount / _videoPlayer.frameRate);
		Debug.Log (_videoPlayer.time);

		//---------------------------------------------
	}
}
