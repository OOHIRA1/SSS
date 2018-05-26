using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class a001 : MonoBehaviour {

	public string scene;

	public void onClick(){
		SceneManager.LoadScene (scene, LoadSceneMode.Additive);
	}
}
