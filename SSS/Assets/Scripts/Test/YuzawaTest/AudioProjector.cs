using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioProjector : MonoBehaviour {

    AudioSource audioSource;

    // Use this for initialization
    void Start () {

        audioSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    //映写機音の再生---------------
    public void ProjectorPlay(){
        audioSource.Play();
    }
    //-----------------------------

    //映写機音の一時停止-----------
    public void ProjectorPause() {
        audioSource.Pause();
    }
    //-----------------------------
}
