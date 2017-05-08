using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEditor.VersionControl;
using UnityEditor;
using System.Configuration;
using System.ComponentModel;

public class PlayerMovement : MonoBehaviour {

	// Walking stuff
	public float maxSpeed = 7f;
	bool walking;


	// Jumping stuff
	public float jumpSpeed = 100f;
	public int i = 0;
	public bool jumpControl = false;

	Rigidbody2D rb2d;
	SpriteRenderer spr;
	Animator anim;
	GameObject floor;
	Collider2D floorCollider;


	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.freezeRotation = true;
		spr = GetComponent<SpriteRenderer> ();
		anim = gameObject.GetComponent<Animator> ();
		floor = GameObject.FindGameObjectWithTag ("Ground");
		floorCollider = floor.GetComponent<Collider2D> ();
	}


	// Update is called once per frame
	 void FixedUpdate () {
		Walking();
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
			if (Input.GetKeyDown (KeyCode.LeftArrow) && !Input.GetKeyDown (KeyCode.RightArrow)) {
				spr.flipX = true;
			}

			if (Input.GetKeyDown (KeyCode.RightArrow) && !Input.GetKeyDown (KeyCode.LeftArrow)){
				spr.flipX = false;
			}

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

	void Jump() {

	}
}


