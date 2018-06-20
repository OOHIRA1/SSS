using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadControll : MonoBehaviour {

	[SerializeField] GameObject [] _gameObject = new GameObject [1];
    [SerializeField] SiteMove _siteMove = null;
    [SerializeField] int _siteNum = 0;
	//float x;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		

		if( SiteMove._nowSiteNum != _siteNum || _siteMove.GetMoveNow() ) {

			for( int i = 0; i < _gameObject.Length; i++ ) {

			_gameObject[i].SetActive( false );
	
            }
		}else {

			for( int i = 0; i < _gameObject.Length; i++ ) {

			_gameObject[i].SetActive( true );
	
			}

    }
	}

    //フェードアウト処理
   //void Fadeout () {

			//gameObject.SetActive( false );


   // }

}
