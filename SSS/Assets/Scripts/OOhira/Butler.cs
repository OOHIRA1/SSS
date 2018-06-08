using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==執事を管理するクラス
//
//使用方法：執事にアタッチ
public class Butler : MonoBehaviour {
	DetectiveOfficeScriptButler _animManager;
	[SerializeField]float _moveSpeed = 0;	//動く速さ(unit/second)

	// Use this for initialization
	void Start () {
		_animManager = GetComponent<DetectiveOfficeScriptButler> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//=================================================================
	//public関数
	//--posに移動させる関数(x座標→y座標)(コルーチン)
	IEnumerator MoveToPos( Vector3 pos ) {
		do{
			Vector3 dir = pos - transform.position;	//目的地までの方向ベクトル
			if (Mathf.Abs (dir.x) > 0.1f) {
				if (dir.x > 0) {
					transform.Translate (_moveSpeed * Time.deltaTime, 0, 0);
				} else {
					transform.Translate (-_moveSpeed * Time.deltaTime, 0, 0);
				} 
			} else if (Mathf.Abs (dir.y) > 0.1f) {
				if (dir.y > 0) {
					transform.Translate (0,_moveSpeed * Time.deltaTime, 0);
				} else {
					transform.Translate (0,-_moveSpeed * Time.deltaTime, 0);
				}
			}
			yield return new WaitForSeconds(Time.deltaTime);
		} while((pos - transform.position).magnitude > 0.1f);
	}
	//=================================================================
	//=================================================================
}
