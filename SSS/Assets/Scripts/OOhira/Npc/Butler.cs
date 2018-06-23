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


	//--posに移動させる関数(x座標→y座標)(コルーチン)
	IEnumerator MoveToPosCoroutine( Vector3 pos ) {
		_animManager.ButlerWalk01 ();
		do{
			Vector3 dir = pos - transform.position;	//目的地までの方向ベクトル
			if (Mathf.Abs (dir.x) > 0.1f) {
				if (dir.x > 0) {
					transform.Translate (_moveSpeed * Time.deltaTime, 0, 0);
				} else if ( dir.x < 0 ){
					transform.Translate (-_moveSpeed * Time.deltaTime, 0, 0);
				} else {
					Vector3 vec = transform.position;
					transform.position = new Vector3( dir.x, vec.y, vec.z );
				}
			} else if (Mathf.Abs (dir.y) > 0.1f) {
				if (dir.y > 0) {
					transform.Translate (0,_moveSpeed * Time.deltaTime, 0);
				} else if ( dir.y < 0 ){
					transform.Translate (0,-_moveSpeed * Time.deltaTime, 0);
				} else {
					Vector3 vec = transform.position;
					transform.position = new Vector3( vec.x, dir.y, vec.z );
				}
			}
			yield return new WaitForSeconds(Time.deltaTime);
		} while((pos - transform.position).magnitude > 0.2f);
		transform.position = pos;
		_animManager.ButlerWalk02 ();
	}


	//=================================================================
	//コライダー検出系
	void OnTriggerStay2D( Collider2D col ) {
		if (col.gameObject.scene.name != "DetectiveOffice") return;//ラボシーン以外受け付けない
		if (col.tag == "Player") {
			Detective detective = col.gameObject.GetComponent<Detective> ();
			if (!detective.GetIsAnimWalk ()) {
				_animManager.ButlerTalk01 ();//話すアニメーション
			}
		}
	}


	void OnTriggerExit2D( Collider2D col ) {
		if (col.gameObject.scene.name != "DetectiveOffice") return;//ラボシーン以外受け付けない
		if (col.tag == "Player") {
			_animManager.ButlerTalk02 ();//話すアニメーション終了
		}
	}
	//=================================================================
	//=================================================================


	//=================================================================
	//public関数
	//--posに移動させる関数(x座標→y座標)
	public void MoveToPos( Vector3 pos ) {
		StartCoroutine (MoveToPosCoroutine (pos));
	}


	//--執事を落下させる関数(クライマックスシーンで使用)
	public void Fall() {
		StartCoroutine ("FallCoroutine");
	}
	//=================================================================
	//=================================================================


	//--執事を落下させる関数(コルーチン)
	IEnumerator FallCoroutine() {
		_animManager.ButlerShock01 ();
		Rigidbody2D rb = (Rigidbody2D)this.gameObject.AddComponent (typeof(Rigidbody2D));
		rb.constraints = RigidbodyConstraints2D.None;
		rb.velocity = new Vector3 (2,0,0);
		float time = 0;
		const float ROTATE_TIME = 3f;//回転時間
		while(time < ROTATE_TIME) {
			transform.Rotate (0, 0, -1f);
			time += Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}
	}
}
