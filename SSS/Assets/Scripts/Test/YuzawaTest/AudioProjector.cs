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


    public void ProjectorPlay(){
        audioSource.Play();
    }

    public void ProjectorPause() {
        audioSource.Pause();
    }
}
