using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoColor : MonoBehaviour {
    [SerializeField]float _time;
    [SerializeField]float _animtime;
    Animator _animator;

    // Use this for initialization
    void Start () {

        _time = 0;
        _animator = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {

        MonoColorTransparent();
        _time += Time.deltaTime;

    }
    void MonoColorTransparent() {

        if ( _animtime < _time ) {
            _animator.SetTrigger("MonoColorFlag");
        }

    }
}
