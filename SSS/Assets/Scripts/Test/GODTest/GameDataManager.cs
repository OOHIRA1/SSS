using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲーム進行状況を管理するクラス
//
//使用方法：常にアクティブなゲームオブジェクトにアタッチ
public class GameDataManager : MonoBehaviour {
	public enum CheckPoint {
		SHOW_MILLIONARE_MURDER_ANIM 				= 1,		//富豪の殺人アニメーションを見る
		FIND_POISONED_DISH							= 2,		//毒の付いた食器を発見する
		GET_EVIDENCE1 								= 4,		//証拠品1を入手する
		FIRST_TAP_EVIDENCE_FILE 					= 8,		//証拠品ファイルを初めてタップする
		FIRST_CLOSE_EVIDENCE_FILE 					= 16,		//証拠品ファイルを初めて閉じる
		FIRST_COME_TO_KITCHEN 						= 32,		//初めて厨房に来る
		FIRST_COME_TO_SERVING_ROOM 					= 64,		//初めて給仕室に来る
		FIRST_COME_TO_BACKYARD 						= 128,		//初めて裏庭に来る
		SHOW_BACKYARD_MOVIE 						= 256,		//裏庭(夜)の動画を見る
		STOP_MOVIE_WHICH_GAEDENAR_ATE_CAKE 			= 512,		//庭師がケーキを食べた瞬間でストップ
		GET_EVIDENCE2 							  	= 1024,		//証拠品2を入手する
		FIRST_COME_TO_DETECTIVE_OFFICE 			  	= 2048,		//初めて探偵ラボに来る
		GET_EVIDENCE3 								= 4096,		//証拠品3を入手する
		GET_EVIDENCE4 								= 8192,		//証拠品4を入手する
		GET_EVIDENCE5 							   	= 16384,	//証拠品5を入手する
		GET_EVIDENCE6 							   	= 32768,	//証拠品6を入手する
		COME_TO_DETECTIVE_OFFICE_WITH_ALL_EVIDENCE 	= 65536		//証拠品を全て揃えて探偵ラボに来る
	}

	[SerializeField] int _advancedData;	                //進行状況を格納する変数
	[SerializeField] string _criminal;				//プレイヤーが選択した犯人(探偵ラボで指摘)※GameObjectはDontDestroy出ないとシーン間で引き継げないのでstring型
	[SerializeField] string _dangerousWeapon;		//プレイヤーが選択した凶器(探偵ラボで指摘)※GameObjectはDontDestroy出ないとシーン間で引き継げないのでstring型


	//==================================================================================
	//ゲッター
	public int GetAdvancedData() { return _advancedData; }
	public string GetCriminal() { return _criminal;	}
	public string GetDangerousWeapon() { return _dangerousWeapon; }
	//==================================================================================
	//==================================================================================


	//==================================================================================
	//セッター
	public void SetCriminal( string x ) { _criminal = x; }
	public void SetDangerousWeapon( string x ){ _dangerousWeapon = x; }
	//==================================================================================
	//==================================================================================



	// Use this for initialization
	void Start () {
		//2つ以上存在しないようにする処理-------------------------------------------------------------
		GameObject[] gameDataManager = GameObject.FindGameObjectsWithTag ("GameDataManager");
		if (gameDataManager.Length >= 2) {
			for (int i = 0; i < gameDataManager.Length; i++) {
				if (gameDataManager [i].scene.name != "DontDestroyOnLoad") {
					GameObject.Destroy (gameDataManager [i]);
				}
			}
		} else {
			GameObject.DontDestroyOnLoad (this.gameObject);
		}
		//-------------------------------------------------------------------------------------------
	}
	
	// Update is called once per frame
	void Update( ) {
		
	}


	//===============================================================================================
	//public関数

	//--checkPointを通過した情報を格納する関数
	public void UpdateAdvancedData( CheckPoint checkPoint ) {
		_advancedData = _advancedData | (int)checkPoint;
	}


	//--checkPointを通過しているか確認する関数
	public bool CheckAdvancedData( CheckPoint checkPoint ) {
		return ( _advancedData & (int)checkPoint ) == (int)checkPoint;
	}


	//--checkPointまでの全てチェックポイントを通過しているか確認する関数
	public bool CheckAdvancedDataUntil( CheckPoint checkPoint ) {
		int num = (int)checkPoint;
		int x = 0;
		do {
			x |= num;
			num >>= 1;
		} while(num != 0);
		return ( _advancedData & x ) == x;
	}
	//===============================================================================================
	//===============================================================================================

}
