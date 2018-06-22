using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioProjector : MonoBehaviour {
	[SerializeField]BGMManager _bgmManeger = null;

    public AudioClip movie_1minutes;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {

        audioSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}


    public void ProjectorPlay(){
        audioSource.PlayOneShot(movie_1minutes, 0.7f);
    }

    public void ProjectorPause() {
        audioSource.Pause();
    }
}
