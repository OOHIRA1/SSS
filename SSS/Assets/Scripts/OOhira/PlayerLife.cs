using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==プレイヤーのライフを管理するクラス
//
//使用方法：ライフ必要時にアクティブなゲームオブジェクトにアタッチ
public class PlayerLife : MonoBehaviour {
	[SerializeField] int _life = 3;	//プレイヤーのライフ
	bool _dead;						//死亡フラグ
	[SerializeField] Image[] _lifeImage = null;	//ライフUI
	[SerializeField] Sprite _redHeart = null;	//赤ハート画像
	[SerializeField] Sprite _blackHeart = null;	//黒ハート画像


	//==========================================
	//ゲッター
	public int GetLife() { return _life; }
	public bool GetDead() { return _dead; }
	//==========================================
	//==========================================


	//==========================================
	//セッター
	//==========================================
	//==========================================

	// Use this for initialization
	void Start () {
		_dead = false;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < _lifeImage.Length; i++) {
			if (i + 1 <= _life) {
				_lifeImage [i].sprite = _redHeart;
			} else {
				_lifeImage [i].sprite = _blackHeart;
			}
		}
	}

	//==========================================
	//public関数

	//--ライフを減らす関数
	public void Damege() {
		if (_life > 0) {
			_life--;
		}
		if (_life == 0) {
			_dead = true;
		}
	}
	//==========================================
	//==========================================
}
