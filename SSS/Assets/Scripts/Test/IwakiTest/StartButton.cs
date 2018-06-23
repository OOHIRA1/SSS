using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
    [SerializeField] AudioProjector _audioProjector = null;
    [SerializeField] AgainBGM _againBGM = null;
	Image _buttonImage;

    public Sprite _stopSprite;
    public Sprite _startSprite;

	bool _playing;

	public AudioClip tap_playback;
	public AudioClip tap_pause;
    AudioSource audioSource;

    // Use this for initialization
    void Start( ) {
        _buttonImage = GetComponent< Image >( );
		audioSource = GetComponent<AudioSource>();
        _playing = true;
    }
    // Update is called once per frame
    void Update( ) {
    }

    public void Click( ) {

        if ( !_playing ) {
			_playing = true;
			_buttonImage.sprite = _stopSprite;
			audioSource.PlayOneShot(tap_playback, 0.7F);
            _audioProjector.ProjectorPause();
            _againBGM.AgainPlayBGM();
        }
        else {
			_playing = false;
			_buttonImage.sprite = _startSprite;
			audioSource.PlayOneShot(tap_pause, 0.7F);
            _audioProjector.ProjectorPlay();
            _againBGM.StopBGM();
        }

    }

	//一時停止画像に切り替える関数-----------------
    public void StopImageChange( ) {
        if ( !_playing ) {
			_playing = true;
			_buttonImage.sprite = _stopSprite;
        }
    }
	//---------------------------------------------
	
	//再生画像に切り替える関数---------------------
	public void StartImageChange( ) {
		if ( _playing ) {
			_playing = false;
			_buttonImage.sprite = _startSprite;
        }
	}
	//--------------------------------------------
}
