using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour {

    public void BoxLayer(int num) {
        var Box = GetComponent<SpriteRenderer>();
        Box.sortingOrder += num;
    }
}
