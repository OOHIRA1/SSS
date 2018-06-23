using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillionareCheck : MonoBehaviour {
    [SerializeField]GameDataManager _millionareCheck = null;
    [SerializeField]GameObject _monoDisapper = null;
    [SerializeField]GameObject _cameraControllManegerDisapper = null;

	// Use this for initialization
	void Start () {
        //最初のCheckPointを通った時モノクロのオブジェクトとカメラコントロールを消す処理------------------------
        _millionareCheck = GameObject.FindGameObjectWithTag("GameDataManager").GetComponent<GameDataManager>( );
        if (_millionareCheck.CheckAdvancedDataUntil(GameDataManager.CheckPoint.SHOW_MILLIONARE_MURDER_ANIM)) {
            _monoDisapper.SetActive(false);
            _cameraControllManegerDisapper.SetActive(false);
        }
    }
	//----------------------------------------------------------------------------------------------------------

	// Update is called once per frame
	void Update () {

    }
}
