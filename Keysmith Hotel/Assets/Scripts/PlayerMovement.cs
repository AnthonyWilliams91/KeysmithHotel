using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEditor.VersionControl;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed = 7f;
	Rigidbody2D rb2d;

	protected Vector2 decellerate;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Walking();
	}


	void Walking() {
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector2 horizontalMovement = new Vector2 (8 * moveHorizontal, 0.0f);
		Vector2 stopMovement = new Vector2 (0.0f, 0.0f);

		// CHECKING FOR USER INPUT AND ADJUSTING PLAYER MOVEMENT
		if (moveHorizontal < .5 && moveHorizontal > -0.5) {

			if (rb2d.velocity.x != 0) {
				rb2d.velocity = Vector2.zero; 
			}

		} else if(rb2d.velocity.x <= maxSpeed && rb2d.velocity.x >= maxSpeed * -1){
			rb2d.AddForce (horizontalMovement);
		}

		Debug.Log("Current X-Axis Velocity: " + horizontalMovement.x);
	}

	void Jump() {
		float moveVertical = Input.GetKeyDown (KeyCode.DownArrow);

	}
}


