using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class YuzawaTest : MonoBehaviour {
	//[SerializeField]GameObject _crimeSceneTransitionButton;
	[SerializeField]Animator _animator = null;
	[SerializeField]GameObject[] _crimeSceneButton = new GameObject[4];
	[SerializeField]Animator _animation = null;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClicked() {
		bool value = true;
		if ( !_crimeSceneButton[0].activeInHierarchy ) {
			value = true;
		} else {
			value = false;
		}
		for (int i = 0; i < _crimeSceneButton.Length; i++) {
			_crimeSceneButton [i].SetActive (value);
		}
	}

	public void scroll( ){
		bool value = true;
		if (!_animator.GetBool( "scrollFlag" ) ) {
			value = true;
		} else {
			value = false;
		}
		_animator.SetBool ("scrollFlag", value);
	}

	public void speech() {
		bool value = true;
		if (!_animation.GetBool( "SpeechBalloonFlag" )) {
			value = true;
		}else {
			value = false;
		}
		_animation.SetBool ("SpeechBalloonFlag", value);
	}

	public void CrimeSceneBedRoom () {
		SceneManager.LoadScene( "Bedroom" );
		}

	public void CrimeSceneKitchen () {
		SceneManager.LoadScene( "Kitchen" );
		}
}
