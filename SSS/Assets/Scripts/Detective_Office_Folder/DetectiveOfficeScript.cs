using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveOfficeScript : MonoBehaviour {

	Animator _animator;

	// Use this for initialization
	void Start () {
		
		_animator = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
    }

	public void DetectiveWalk01(){
		_animator.SetBool( "DetectiveWalk", true );
	}
	public void DetectiveWalk02(){
		_animator.SetBool( "DetectiveWalk", false );
	}
	public void DetectiveThink01(){
		_animator.SetBool( "DetectiveThink", true );
	}
	public void DetectiveThink02(){
		_animator.SetBool( "DetectiveThink", false );
	}
	public void DetectiveBow01(){
		_animator.SetBool( "BowFlag", true );
	}
	public void DetectiveBow02(){
		_animator.SetBool( "BowFlag", false );
	}
	public void DetectiveConcetrate01(){
		_animator.SetBool( "DetectiveConcetrate", true );
	}
	public void DetectiveConcetrate02(){
		_animator.SetBool( "DetectiveConcetrate", false );
	}
	public void DetectiveDown01(){
		_animator.SetBool( "DetectiveDown", true );
	}
	public void DetectiveDown02(){
		_animator.SetBool( "DetectiveDown", false );
	}
	public void DetectiveGameOver(){
		_animator.SetBool ("GameOver", true);
	}


	//--現在のStateの再生時間を返す関数( 返り値：0~1(開始時：0, 終了時：1) )
	public float ResearchStatePlayTime() {
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.normalizedTime;
	}


	//--探偵のStateがおじぎ状態かどうかを返す関数
	public bool IsStateBowStart( ) {
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.IsName ("DetectiveBowStart");
	}


	//--探偵のStateがおじぎ状態かどうかを返す関数
	public bool IsStateGameOver( ) {
		int layer = _animator.GetLayerIndex ("Base Layer");
		AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo (layer);
		return animatorStateInfo.IsName ("game_over");
	}

}