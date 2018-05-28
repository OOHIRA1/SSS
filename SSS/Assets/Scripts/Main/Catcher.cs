using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour {
	[ SerializeField ] Detective _player = null;
	[ SerializeField ] Rope _rope = null;
	[ SerializeField ] MoviePlaySystem _moviePlaySystem = null;
	bool _catcher;
    bool _transported;
	float count;
	bool a;
	bool b;
	bool c;



	int debug;
	// Use this for initialization
	void Start( ) {
		_catcher = false;
        //_transported = 
		count = 0;
		a = false;
		b = false;
		c = false;

		debug = 0;
	}
	
	// Update is called once per frame
	void Update( ) {
		//ロープにコライダーをつけてisTriggerにしてプレイヤーと触れたら一緒に一定以上、上がるようにして、上がったら初期posをみてそこにあわせて連れて行く
		//別と別のゲームオブジェクトが当たったことを判定できるものを探す
		Debug.Log(_rope.Getfureru());

        if ( _catcher ) {
            
			if (!b) {
				_rope.transform.position = new Vector3( _player.transform.position.x, _rope.transform.position.y, 0 );
				b = true;
			}

            if (_player.transform.position.y < 1.7f ) {
                if (!_player.GetRopeTouch( )){
                    _rope.transform.position -= new Vector3(0, 0.1f, 0);
                }else{
                    _rope.transform.position += new Vector3(0, 0.1f, 0);
                    _player.transform.position += new Vector3(0, 0.1f, 0);
                    //count++;
                }
            }

            if (_player.transform.position.y >= 1.7f && !a) {
				Vector3 aa = _player.GetInitialPos ();
				if (aa.x != _player.transform.position.x) {
					if (aa.x < _player.transform.position.x) {
						_rope.transform.position -= new Vector3 (0.1f, 0, 0);
						_player.transform.position -= new Vector3 (0.1f, 0, 0);
					} else {
						_rope.transform.position += new Vector3 (0.1f, 0, 0);
						_player.transform.position += new Vector3 (0.1f, 0, 0);
					}
				} else {
					if (aa.y != _player.transform.position.y) {
						_rope.transform.position -= new Vector3 (0, 0.1f, 0);
						_player.transform.position -= new Vector3 (0, 0.1f, 0);
					} else {
						a = true;
					}
				}
            }

			if (a) {
				_rope.transform.position += new Vector3 (0, 0.1f, 0);
			}

        }

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
