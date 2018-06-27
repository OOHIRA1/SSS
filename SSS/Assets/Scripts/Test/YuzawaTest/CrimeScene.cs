using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrimeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	

	public void CrimeSceneKitchen () {
		SceneManager.LoadScene( "Kitchen" );
		}
}
