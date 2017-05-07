using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEditor.VersionControl;
using UnityEditor;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed = 7f;
	public bool jumpControl;
	bool walking;

	Rigidbody2D rb2d;
	SpriteRenderer spr;
	Animator anim;

//	protected Vector2 decellerate;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		spr = GetComponent<SpriteRenderer> ();
		anim = gameObject.GetComponent < Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Walking();

//		if (Input.GetKeyDown (KeyCode.DownArrow) && rb2d.IsTouching(Collider2D.FindObjectOfType())) {
//			Jump();
//		}
	}


	void Walking() {
//		spr.flipX = true;
		float moveHorizontal = Input.GetAxis ("Horizontal");
		walking = true;

		Vector2 horizontalMovement = new Vector2 (15 * moveHorizontal, 0.0f);

		FlipSprite ();

		// CHECKING FOR USER INPUT AND ADJUSTING PLAYER MOVEMENT
		if (moveHorizontal < .5 && moveHorizontal > -0.5) {

			if (rb2d.velocity.x != 0) {
				rb2d.velocity = Vector2.zero; 
			}

			if (!Input.GetKeyDown (KeyCode.LeftArrow) && !Input.GetKeyDown (KeyCode.RightArrow)) {
				walking = false;
			}

		} else if(rb2d.velocity.x <= maxSpeed && rb2d.velocity.x >= maxSpeed * -1){
			rb2d.AddForce (horizontalMovement);
		}

//		Debug.Log("Current X-Axis Velocity: " + horizontalMovement.x);
		Debug.Log("Walking: " + walking);
		anim.SetBool("walking", walking);
	}

	void FlipSprite () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			spr.flipX = true;
		} 

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			spr.flipX = false;
		}
	}

//	void Jump() {
//		
//	}
}


