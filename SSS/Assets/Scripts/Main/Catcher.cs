using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour {
	[ SerializeField ] Detective _player = null;
	[ SerializeField ] GameObject _rope = null;
	[ SerializeField ] MoviePlaySystem _moviePlaySystem = null;
	bool _catcher;
	float count;
	bool a;
	bool b;
	bool c;



	int debug;
	// Use this for initialization
	void Start( ) {
		_catcher = false;
		count = 5f;
		a = false;
		b = false;
		c = false;

		debug = 0;
	}
	
	// Update is called once per frame
	void Update( ) {
		//ロープにコライダーをつけてisTriggerにしてプレイヤーと触れたら一緒に一定以上、上がるようにして、上がったら初期posをみてそこにあわせて連れて行く
		//別と別のゲームオブジェクトが当たったことを判定できるものを探す
		Debug.Log(debug);
		Vector3 aa = _player.GetInitialPos( );

		//if ( _catcher ) {
		//	_rope.transform.position = new Vector3( _player.transform.position.x, 0, 0 );
			
		//	if ( !a && _rope.transform.position.y != _player.transform.position.y ) {
		//		_rope.transform.position -= new Vector3( 0, 0.1f, 0 );
		//		debug = 1;
		//	} else {
		//		a = true;
		//	}

		//	if ( a && !b ) {
		//		_rope.transform.position += new Vector3( 0, 0.1f, 0 );
		//		_player.transform.position += new Vector3( 0, 0.1f, 0 );
		//		count -= Time.deltaTime;
		//		debug = 2;
		//		if ( count < 0 ) {
		//			b = true;
		//		}
		//	}

		//	if ( b && !c ) {
		//		debug = 3;
		//		if ( aa.x < _rope.transform.position.x ) {
		//			_rope.transform.position -= new Vector3( 0, 0.1f, 0 );
		//			_player.transform.position -= new Vector3( 0, 0.1f, 0 ); 
		//		} 
				
		//		if ( aa.x > _rope.transform.position.x ) {
		//			_rope.transform.position += new Vector3( 0, 0.1f, 0 );
		//			_player.transform.position += new Vector3( 0, 0.1f, 0 );
		//		}
				
		//		if ( aa.x == _rope.transform.position.x ) {
		//			c = true;
		//		}	
		//	}

		//	if ( c ) {
		//		debug = 4;
		//		if ( aa.y != _rope.transform.position.y ) {
		//			_rope.transform.position -= new Vector3( 0, 0.1f, 0 );
		//			_player.transform.position -= new Vector3( 0, 0.1f, 0 );
		//		} else {
		//			debug = 5;
		//			c = false;
		//			_catcher = false;
		//		}

		//	}


		//}
	}

	//private void OnTriggerEnter( Collider other ) {

	//}

	public void StopButton( ) {
		if ( !_moviePlaySystem.GetStop( ) && !_player.GetCheckPos( ) ) {
			_catcher = true;
		}
	}
}
