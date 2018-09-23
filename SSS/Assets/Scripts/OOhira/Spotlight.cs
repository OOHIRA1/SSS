using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==スポットライトを管理するクラス
//
//使用方法：スポットライトにアタッチ
public class Spotlight : MonoBehaviour {
	[SerializeField] Transform _cursorTransform = null;		//カーソルのTransform
	[SerializeField] GameObject _spotLight = null;

	bool _check;
	bool _soundCheck;

	public AudioClip Spotlight1;
    AudioSource audioSource;


	// Use this for initialization
	void Start () {
		
		audioSource = GetComponent<AudioSource>();
		_check = true;

	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.x = _cursorTransform.position.x;
		transform.position = pos;

		if (_check ) {
			SpotLight ();
			_check = false;
		}

	}
	public void SpotLight() {
		if( _spotLight.activeInHierarchy ) {
			audioSource.PlayOneShot (Spotlight1, 0.7f);
		}
	}

	public void Check(){
		if (!_spotLight.activeInHierarchy) {
			_check = true;
		}
	}
}
