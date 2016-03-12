using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[Header("Player Values")]
	public float speed; //Speed of the player
	private Rigidbody2D rb; //Rigidbody component

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		//Getting input

		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
		//Getting a vector of where to move

		rb.AddForce (movement * speed);
		//Actually moving the player by adding force to the rigidbody component
	}

	void Update() {
	}
}