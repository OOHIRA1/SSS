using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==凶器UI用のスポットライトを操作するクラス
//
//使用方法：SpotlightType2にアタッチ
public class SpotlightType2 : MonoBehaviour {
	[SerializeField] RayShooter _rayShooter = null;
    	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = _rayShooter.Shoot (Input.mousePosition);
			if (hit) {
				if (hit.collider.tag == "DangerousWepon") {
					transform.position = hit.transform.position;
				}
			}
		}
	}
}
