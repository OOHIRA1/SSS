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


	IEnumerator StopSoundWithFadeOutCoroutine() {
		float fadeOutSpeed = 0.5f;
		while (_audioSource.volume > 0) {
			_audioSource.volume -= fadeOutSpeed * Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}
		_audioSource.Stop ();
		_audioSource.volume = 1f;
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

	//--音を止める関数
	public void StopSound() {
		_audioSource.Stop ();
	}


	//--音をフェードアウトし止める関数
	public void StopSoundWithFadeOut() {
		StartCoroutine (StopSoundWithFadeOutCoroutine());
	}


	//--音を一時停止する関数
	public void PauseSound() {
		_audioSource.Pause ();
	}


	//--音量調整をする関数
	public void ChangeVolume( float volume ) {
		_audioSource.volume = volume;
	}
	//=========================================================================
	//=========================================================================
}
