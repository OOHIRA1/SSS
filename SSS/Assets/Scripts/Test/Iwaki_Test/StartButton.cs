using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
    Image buttonImge;

    public Sprite ON_sprite;
    public Sprite OFF_sprite;

    bool start_button;
    // Use this for initialization
    void Start( ) {
        buttonImge = GetComponent< Image >( );
        start_button = false;
    }
    // Update is called once per frame
    void Update( ) {
    }

    public void Click( ) {

        if ( !start_button ) {
            buttonImge.sprite = ON_sprite;
            start_button = true;
        }
        else {
            buttonImge.sprite = OFF_sprite;
            start_button = false;
        }

    }

    public void Other_Click( ) {
        if ( start_button ) {
            buttonImge.sprite = OFF_sprite;
            start_button = false;
        }
    }
}
