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
		_audioSource.PlayOneShot(_audioClips[index]);
	}
	//=========================================================================
	//=========================================================================
}
