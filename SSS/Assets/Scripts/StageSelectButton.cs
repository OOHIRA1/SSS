using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectButton : MonoBehaviour {
	[SerializeField] RayShooter _rayShooter;
	[SerializeField] SpriteRenderer _sr;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			RaycastHit2D hit = _rayShooter.Shoot (Input.mousePosition);
			if (hit) {
				if (hit.collider.gameObject.name == "episode_sumuneil") {
					SpriteRenderer sr = hit.collider.GetComponent<SpriteRenderer> ();
					sr.color = new Color (200 / 255f, 200 / 255f, 200 / 255f, 1f);
				}
			}
		} else {
			_sr.color = new Color (1, 1, 1, 1);
		}
	}
}
