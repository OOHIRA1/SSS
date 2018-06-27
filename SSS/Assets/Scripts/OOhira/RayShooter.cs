using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==Rayを発射するクラス
//
//アタッチ：カメラにアタッチ(カメラから発射するため)
public class RayShooter : MonoBehaviour {
	[SerializeField] float _drawRayDistance = 100f;	//描画するRayの長さ(距離)
	[SerializeField] bool _rayShootable = true;		//Rayを飛ばせるかどうかのフラグ


	//============================================================================
	//セッター
	public void SetRayShootable( bool x ) { _rayShootable = x; }
	//============================================================================
	//============================================================================

	//============================================================================
	//public関数
	//============================================================================
	//--RayをscreenPointへ飛ばし検出した2Dコライダーの情報(RaycastHit2D)を返す関数
	public RaycastHit2D Shoot( Vector3 screenPoint ){
		if (!_rayShootable) return Physics2D.Raycast(Vector2.zero, Vector2.zero);
		Ray ray = Camera.main.ScreenPointToRay ( screenPoint );
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction);
		Debug.DrawRay (ray.origin, ray.direction * _drawRayDistance, Color.red);
		return hit;
	}
	//============================================================================
	//============================================================================
}
