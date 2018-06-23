using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetherButton : MonoBehaviour {

	[SerializeField] GameObject[] _clockUi = new GameObject[3];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //ClockUiの画像切り替え---------------------------
	public void ClockUiButton(int ArrayNumber) {
		bool value = true;
		for( int i = 0; i <= _clockUi.Length; i++ )
		{
			if ( i == ArrayNumber ) {
				_clockUi[i].SetActive(value);
				}
			}
		}
    //------------------------------------------------
	}
