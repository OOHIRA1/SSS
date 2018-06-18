using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==探偵によるテキストを表示するクラス
//
//使用方法：テキストを表示する全てのImageの親オブジェクトにアタッチ
public class DetectiveTalk : MonoBehaviour {
	const int STOP_SPRITE_INDEX = 34;					//_stopSpriteを表示する_imagesの配列番号
	enum AudioClips {//AudioClipの列挙体
		TYPING1,
		TYPING2,
		TYPING3,
		NEW_LINE,
		DING
	};

	[SerializeField] string[] _filePaths = null;		//取得するファイルのパス
	Sprite[][] _sprites;								//取得するスプライトを格納する変数
	Image[] _images;									//テキストを表示するImage
	[SerializeField] int _characterPerRow = 10;			//1行当たりの文字数
	int _index = 0;										//表示している_imagesの配列番号
	[SerializeField] Vector2 _padding = new Vector2(0, 0);
//	float _time = 0;									//経過時間(_sprites[]の要素毎に更新)
	[SerializeField] float _speed = 0.3f;				//テキストを表示する時間(second/char)
	[SerializeField] int _statementNumber = 0;			//表示中の文章の番号(0,1,2, ... ,_filePaths.Length - 1 )
	[SerializeField] Sprite _stopSprite = null;			//文章終わりのマーク
	[SerializeField] RuntimeAnimatorController _runtimeAnimatorController = null;	//文章終わりのマークのアニメーション
	bool _moreFast;										//文章を一気に表示するかどうかのフラグ
	[SerializeField] bool _talkFinishedFlag;			//話し終わったかどうかのフラグ
	SoundLibrary _soundLibrary;
	bool _newLineSESoundable;							//改行音を鳴らすかどうかのフラグ
	bool _dingSESounding;								//チーン音を鳴っているかどうかのフラグ
	bool _dingSESoundable;								//チーン音を鳴らすかどうかのフラグ


	//===========================================================
	//ゲッター
	public bool GetTalkFinishedFlag() {
		return _talkFinishedFlag;
	}

	public int GetStateMentNumber() {
		return _statementNumber;
	}

	public bool GetDingSESounding() {
		return _dingSESounding;
	}
	//===========================================================
	//===========================================================

	// Use this for initialization
	void Start () {
		//スプライトの取得-----------------------------------------------
		_sprites = new Sprite[_filePaths.Length][];
		for (int i = 0; i < _filePaths.Length; i++) {
			_sprites[i] = Resources.LoadAll <Sprite>(_filePaths[i]);
		}
		//--------------------------------------------------------------

		//_imagesの整列-----------------------------------------------------------------------------------------------------------------------------------------------------
		_images = GetComponentsInChildren<Image> ();
		for ( int i = 0; i < _images.Length; i++ ) {
			RectTransform rectTransform = _images [i].GetComponent<RectTransform> ();
			Vector2 size = rectTransform.rect.size;				//Imageのサイズ
			rectTransform.anchoredPosition = new Vector2 ( size.x * ( 1 + i % _characterPerRow ), -size.y * ( 1 + i / _characterPerRow ) ) + _padding;
			if (i == STOP_SPRITE_INDEX) {
				rectTransform.anchoredPosition = new Vector2 ( size.x * ( 1 + _characterPerRow ), -size.y * ( 1 + ( i - 1 ) / _characterPerRow ) ) + _padding;
			}
			_images [i].color = new Color ( 1f, 1f, 1f, 0 );	//一度透明にする
		}
		//-------------------------------------------------------------------------------------------------------------------------------------------------------------------

		_moreFast = false;
		_talkFinishedFlag = false;
		_newLineSESoundable = false;
		_dingSESounding = false;
		_dingSESoundable = false;

		_soundLibrary = GetComponent<SoundLibrary> ();

		Talk ();
	}
	
	// Update is called once per frame
	void Update () {
		//1文字ずつ表示する処理---------------------------------------------------------
//		_time += Time.deltaTime;
//		if ( _time > _speed ) {
//			_images[_index].sprite = _sprites[_statementNumber][_index];
//			_images [_index].color = new Color (1f, 1f, 1f, 1f);	//透明をリセット
//			_index = ++_index % _sprites[_statementNumber].Length;
//			_time = 0;												//_timeのリセット
//		}
		//-----------------------------------------------------------------------------

		//次の文章を表示する準備--------------------------------------------------------
//		if (Input.GetMouseButtonDown (0)) {
//			_index = 0;
//			_statementNumber = ++_statementNumber % _sprites.Length;
//			for ( int i = 0; i < _images.Length; i++ ) {
//				_images [i].color = new Color (1f, 1f, 1f, 0);
//			}
//		}
		//-----------------------------------------------------------------------------

		//デバッグ用---------------------------
//		if (Input.GetKeyDown (KeyCode.A)) {
//			Talk ();
//		}
		//-------------------------------------
	}

	//--テキストを表示する関数(コルーチン)
	IEnumerator DisplayText() {
		if (_dingSESounding) yield break;//チーン音が鳴りだしたらもう処理を受け付けない
		//チーン音を鳴らす処理-----------------------------------------
		if (_dingSESoundable) {
			_soundLibrary.PlaySound ( (int)AudioClips.DING );
			_dingSESounding = true;
			while (_soundLibrary.IsPlaying ((int)AudioClips.DING)) {
				yield return new WaitForSeconds (Time.deltaTime);
			}
			_talkFinishedFlag = true;//なり終わったらフラグを立てる
			yield break;
		}
		//-------------------------------------------------------------
		if (_index != 0) {
			for (int i = 0; i < _sprites [_statementNumber].Length; i++) {
				_images [i].color = new Color (1f, 1f, 1f, 1f);	//透明をリセット	※文章を一気に表示させるため
			}
			_moreFast = true;
		} else {
			//シュッ音を鳴らす処理--------------------------------------------------------------
			if(_statementNumber >= 1 && _newLineSESoundable) {
				int se = (int)AudioClips.NEW_LINE;
				_soundLibrary.PlaySound(se);
				_newLineSESoundable = false;
				yield return new WaitForSeconds( _soundLibrary.CheckSoundLength(se) );
			}
			//---------------------------------------------------------------------------------
			//前文をリセットして次の文を入れる処理--------------------------------------------------------------------------------
			for (int i = 0; i < _images.Length; i++) {
				_images [i].color = new Color (1f, 1f, 1f, 0);//透明化
				if (i < _sprites [_statementNumber].Length) _images [i].sprite = _sprites [_statementNumber] [i];//文字を入れる
				if (_images [i].GetComponent<Animator> ()) {
					Destroy (_images [i].GetComponent<Animator> ());
				}
			}
			//------------------------------------------------------------------------------------------------------------------
			do {
				//_images [_index].sprite = _sprites [_statementNumber] [_index];
				_images [_index].color = new Color (1f, 1f, 1f, 1f);	//透明をリセット
				//タイプ音を鳴らす処理---------------------------------------------------
				if (_index % 3 == 2) {
					int typeSE = Random.Range((int)AudioClips.TYPING1, (int)AudioClips.TYPING3 + 1);
					_soundLibrary.PlaySound(typeSE);
				}
				//----------------------------------------------------------------------
//				//改行音を鳴らす処理----------------------------------------------------------------------
//				if (_index + 1 == _characterPerRow) {
//					int se = (int)AudioClips.NEW_LINE;
//					_soundLibrary.PlaySound(se);
//					yield return new WaitForSeconds( _soundLibrary.CheckSoundLength(se) );
//					se = (int)AudioClips.DING;
//					_soundLibrary.PlaySound(se);
//					yield return new WaitForSeconds( _soundLibrary.CheckSoundLength(se) );
//				}
//				//---------------------------------------------------------------------------------------
				_index++;
				yield return new WaitForSeconds (_speed);
			} while(_index != _sprites [_statementNumber].Length && !_moreFast);
			//文章終わりのマーク表示処理-----------------------------------------------------------------------------------------------------
			_images [STOP_SPRITE_INDEX].sprite = _stopSprite;
			_images [STOP_SPRITE_INDEX].gameObject.AddComponent<Animator> ();
			_images [STOP_SPRITE_INDEX].GetComponent<Animator> ().runtimeAnimatorController = _runtimeAnimatorController;
			_images [STOP_SPRITE_INDEX].color = new Color (1f, 1f, 1f, 1f);	//透明をリセット
			//-------------------------------------------------------------------------------------------------------------------------------
			_index = 0;
			if (!_moreFast) {
				_newLineSESoundable = true;
			}
			_moreFast = false;
			_statementNumber = ++_statementNumber % _sprites.Length;
			if (_statementNumber == 0) {
				//_talkFinishedFlag = true;
				_dingSESoundable = true;
			}
		}
	}


	//================================================================================
	//public関数

	//--テキストを表示する関数
	public void Talk() {
		if ( _statementNumber >= 1 && _soundLibrary.IsPlaying ( (int)AudioClips.NEW_LINE ) ) return;
		if (_talkFinishedFlag) {
			for (int i = 0; i < _images.Length; i++) {
				_images [i].color = new Color (1f, 1f, 1f, 0);
				if (_images [i].GetComponent<Animator> ()) {
					Destroy (_images [i].GetComponent<Animator> ());
				}
			}
			return;
		}
		StartCoroutine ("DisplayText");
	}


	//--テキスト表示をリセットし再び読めるようにする関数
	public void ResetTalkedInfo() {
		_talkFinishedFlag = false;
		_dingSESounding = false;
		_dingSESoundable = true;
	}

	//================================================================================
	//================================================================================
}
