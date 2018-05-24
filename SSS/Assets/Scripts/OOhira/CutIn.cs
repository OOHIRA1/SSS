using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==カットイン機能を管理するクラス
//
//使用方法：カットイン時にアクティブなゲームオブジェクトにアタッチ

public class CutIn : MonoBehaviour {
	[SerializeField] RectTransform[] _imagesRectTransform = null;
	[SerializeField] Vector2 _repeatPosition;
	int _movingImageIndex;

	// Use this for initialization
	void Start () {
		_movingImageIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (_imagesRectTransform [_movingImageIndex].anchoredPosition.x < 0) {
			_imagesRectTransform [_movingImageIndex].anchoredPosition += new Vector2 (1, 0);
		} else {
			
		}
	}
}
