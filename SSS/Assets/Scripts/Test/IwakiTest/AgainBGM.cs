using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgainBGM : MonoBehaviour
{
    [SerializeField] BGMManager _bgmManeger = null;

    bool _check;

    AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _check = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AgainPlayBGM()
    {
        if (_check){
            _bgmManeger.UpdateBGM();
            _check = false;
        }
    }

    public void StopBGM()
    {
        audioSource.Stop();
        _check = true;
    }
}
