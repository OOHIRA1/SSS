using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==音データを管理するクラス
//
//使用方法：複数の音を持つGameObjectにアタッチ
public class SoundLibrary : MonoBehaviour {
	AudioSource _audioSource;
	[SerializeField] AudioClip[] _audioClips = null;

	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//=========================================================================
	//public関数
	//--_audioClipsにある音を鳴らす関数
	public void PlaySound( int index ) {
		//_audioSource.clip = _audioClips [index];
		_audioSource.clip = _audioClips[index];
		_audioSource.Play ();
	}


	//--_audioClipsにある音の長さ(単位：secound)を返す関数
	public float CheckSoundLength( int index ) {
		return _audioClips [index].length;
	}


	//--_audioClipsにある音がなっているかどうかを確認する関数
	public bool IsPlaying( int index ) {
		bool x = false;
		if (_audioSource.clip == _audioClips [index] && _audioSource.isPlaying) {
			x = true;
		}
		return x;
	}
	//=========================================================================
	//=========================================================================
}
