﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BoxLayer(int num) {
        var Box = GetComponent<SpriteRenderer>();
        Box.sortingOrder += num;
    }
}
