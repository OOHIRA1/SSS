using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStartButton : MonoBehaviour {
	const float COLOR_A_MAX = 1f;
	const float COLOR_A_MIN = 0;
	[ SerializeField ] float _speed = 0.01f;

	SpriteRenderer _spriteRenderer;
	Color _color;
	bool _transparencyaMax;
	// Use this for initialization
	void Start () {
		_spriteRenderer = GetComponent< SpriteRenderer >( );
		_color = new Color( _spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a );
		_transparencyaMax = true;
	}

	// Update is called once per frame
	void Update () {
		Flashing( );
	}

	void Flashing( ) {
		if ( _color.a > COLOR_A_MAX ) {
			_transparencyaMax = true;
			_color.a = COLOR_A_MAX;
		}
		if ( _color.a < COLOR_A_MIN ) {
			_transparencyaMax = false;
			_color.a = COLOR_A_MIN;
		}

		if ( _transparencyaMax ) {
			_color.a -= _speed * Time.deltaTime;
		} else {
			_color.a += _speed * Time.deltaTime;
		}

		_spriteRenderer.color = _color;
	}

}