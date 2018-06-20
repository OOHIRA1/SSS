using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour {
    Animator _animator;
	// Use this for initialization
	void Start () {
		_animator = GetComponent< Animator >( );
	}
	
	// Update is called once per frame
	void Update () {
        AnimEndDestroy( );
        OtherEffectDestroy( );
	}

    //アニメーションが終わったら自分を消す処理------------------------------------------------
    void AnimEndDestroy( ) {
        int layer = _animator.GetLayerIndex( "Base Layer" );
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo( layer );

        if ( stateInfo.normalizedTime >= 1f ) Destroy( this.gameObject );
    }
    //----------------------------------------------------------------------------------------

    //他のタッチエフェクトがあったら自分を消す処理---------------------------------------
    void OtherEffectDestroy( ) {
        GameObject[ ] gameObject = GameObject.FindGameObjectsWithTag( "TouchEffect" );
        if ( gameObject.Length >= 2 ) Destroy( this.gameObject );       //ここで自分を消すため2個目を見るときにはすでに消えていて１つになっているので一緒には消えないのかな？
    }
    //------------------------------------------------------------------------------------

    
}
