using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==UIのアニメーションを管理するクラス
//
//使用方法：アニメーションをするUIにアタッチ
public class UiAnimation : MonoBehaviour {
    Image _image;
    [SerializeField] Sprite[ ] _sprites = new Sprite[ 1 ];     //アニメーションで変える画像
    [SerializeField] int _animationTime = 0;          //アニメーションの再生時間
    [SerializeField] int _time = 0;
    bool _animationStartFlag;

	// Use this for initialization
	void Start () {
		_image = GetComponent<Image>( );
        _animationStartFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
        if ( ( _time + 1 ) / ( _animationTime / _sprites.Length ) < _sprites.Length && _animationStartFlag ) {
		    _time++;
            if ( _time % ( _animationTime / _sprites.Length ) == 0  ) {
                  _image.sprite = _sprites[_time / ( _animationTime / _sprites.Length )];
            }
        }
	}

    IEnumerator Anim() {
        for ( int i = 0; i < _sprites.Length; i++ ) {
            _image.sprite = _sprites[ i ];
            yield return new WaitForSeconds(0.01f);
        }
    }


    //=========================================================================
    //public関数
    //--アニメーションをスタートする関数
    public void StartAnimation( ) {
        StartCoroutine("Anim");
    }

    public void AnimationStart( ) {
        _animationStartFlag =true;
    }
    //=========================================================================
    //=========================================================================
}
