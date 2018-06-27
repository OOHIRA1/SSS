using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==ゴミを投げる処理を管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class GabegeShooter : MonoBehaviour {
	[SerializeField] Rigidbody2D[] _gabeges = null;
	[SerializeField] Vector3[] _velocities = null;
	[SerializeField] float[] _shootTime = null;
	
	// Update is called once per frame
	void Update () {
		//デバッグ用------------------------
		if (Input.GetKeyDown (KeyCode.A))
			Shoot ();
		//----------------------------------
	}


	IEnumerator ShootGabegeCoroutine() {
		int maxTimeIndex = 0;
		for (int i = 0; i < _shootTime.Length; i++) {
			if (_shootTime[maxTimeIndex] < _shootTime [i]) {
				maxTimeIndex = i;
			}
		}
		bool[] shootFlag = { false, false, false, false };
		float time = 0;
		while(!shootFlag[maxTimeIndex]) {
			for (int i = 0; i < _shootTime.Length; i++) {
				if (time >= _shootTime [i] && !shootFlag[i]) {
					_gabeges [i].velocity = _velocities [i];//new Vector3 (-3f, 12, 0);
					shootFlag[i] = true;
				}
			}
			time += Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}
	}

	//===============================================================
	//public関数
	//--ゴミを投げる関数
	public void Shoot() {
		StartCoroutine ("ShootGabegeCoroutine");
	}
	//===============================================================
	//===============================================================
}
