using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==吹き出し上で表示されるテキストを管理クラス
//
//使用方法：吹き出しのゲームオブジェクトにアタッチ
public class TextOnSpeechBalloon : MonoBehaviour {
	Animator _animator;
	[SerializeField] GameObject[] _text = null;


	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		if (animatorStateInfo.IsName ("SpeechBalloon")) {
			for (int i = 0; i < _text.Length; i++) {
				_text[i].SetActive (true);
			}
		} else {
			for (int i = 0; i < _text.Length; i++) {
				_text[i].SetActive (false);
			}
		}
	}
}
