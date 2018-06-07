using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour {
	[SerializeField] GameObject _title = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}
	public void OnClick() {

		Debug.Log( "Button Click" );
		//非表示
		gameObject.SetActive(false);
		_title.SetActive (false);
	}
	//public void Title() {
	//
	//	SceneManager.LoadScene( "StageSelect" );
	//
	//}

}
