using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
    [SerializeField] AudioProjector _audioProjector = null;
    [SerializeField] AgainBGM _againBGM = null;
    [SerializeField] MoviePlaySystem _moviePlaySystem = null;
	Image _buttonImage;

    public Sprite _stopSprite;
    public Sprite _startSprite;

	bool _playing;
    bool _checker;

	public AudioClip tap_playback;
	public AudioClip tap_pause;
    AudioSource audioSource;

    // Use this for initialization
    void Start( ) {
        _buttonImage = GetComponent< Image >( );
		audioSource = GetComponent<AudioSource>();
        _playing = true;
        _checker = true;

    }
    // Update is called once per frame
    void Update( ) {

        //アニメーション再生が止まっていれば映写機音を一時停止、BGMを再生---------
        if( _moviePlaySystem.GetStop() && _checker ) {
            _audioProjector.ProjectorPause();
            _againBGM.AgainPlayBGM();
            _checker = false;
        }
        //-------------------------------------------------------------------------

        //アニメーション再生中であれば映写機音を再生、BGMを止める------------------
        if( !_moviePlaySystem.GetStop() && !_checker ) {
            _audioProjector.ProjectorPlay();
            _againBGM.StopBGM();
            _checker = true;
        }
        //-------------------------------------------------------------------------

    }

    public void Click( ) {

        if ( !_playing ) {
			_playing = true;
			_buttonImage.sprite = _stopSprite;
			audioSource.PlayOneShot(tap_playback, 0.7F);
        }
        else {
			_playing = false;
			_buttonImage.sprite = _startSprite;
			audioSource.PlayOneShot(tap_pause, 0.7F);
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
