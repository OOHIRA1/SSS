using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==スポットライトを管理するクラス
//
//使用方法：スポットライトにアタッチ
public class Spotlight : MonoBehaviour {
	[SerializeField] Transform _cursorTransform = null;		//カーソルのTransform

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.x = _cursorTransform.position.x;
		transform.position = pos;
	}
}
