using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopButton : MonoBehaviour {
    Image buttonImge;

    public Sprite ON_sprite;
    public Sprite OFF_sprite;

    bool stop_button;
    // Use this for initialization
    void Start( ) {
        buttonImge = GetComponent< Image >( );
        stop_button = false;
    }

    public void Click( ) {

        if ( !stop_button ) {
            buttonImge.sprite = ON_sprite;
            stop_button = true;
        } else {
            buttonImge.sprite = OFF_sprite;
            stop_button = false;
        }

    }

    public void Other_Click( ) {
        if ( stop_button ) {
            buttonImge.sprite = OFF_sprite;
            stop_button = false;
        }
    }
}