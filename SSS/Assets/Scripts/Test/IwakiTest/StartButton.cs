using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
	Image _buttonImage;

    public Sprite _stopSprite;
    public Sprite _startSprite;

	bool _playing;


    // Use this for initialization
    void Start( ) {
        _buttonImage = GetComponent< Image >( );
        _playing = true;
    }
    // Update is called once per frame
    void Update( ) {
    }

    public void Click( ) {

        if ( !_playing ) {
			_playing = true;
			_buttonImage.sprite = _stopSprite;
        }
        else {
			_playing = false;
			_buttonImage.sprite = _startSprite;
        }

    }

    public void Other_Click( ) {
        if ( !_playing ) {
			_playing = true;
			_buttonImage.sprite = _stopSprite;
        }
    }
}
