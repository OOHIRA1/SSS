using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousWeaponCheckers : MonoBehaviour {

	[SerializeField]Cursor _cursor = null;
	[SerializeField]GameObject _dengerWwaponUI = null;
	bool _cheker = true;       //音を一回だけならすためのもの


	public AudioClip Accent44;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
		
		audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		if (_cheker) {
			Check ();
		}

		if (!_dengerWwaponUI.activeInHierarchy) {
			Checking ();
		}
	}

    //凶器選択で決定したら音を鳴らす----------------
	public void Check() {
		if( _cursor.GetSelectedFlag() == true ) {
		audioSource.PlayOneShot(Accent44, 0.7F);
			_cheker = false;
		}
	}
    //----------------------------------------------

    //二回目以降で凶器選択で音を鳴らすためのもの---------
	public void Checking(){
			_cheker = true;
	}
    //----------------------------------------------------
}
