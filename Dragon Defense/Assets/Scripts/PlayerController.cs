using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[Header("Player Values")]
	public float speed; //Speed of the player
	public Transform shotSpawn; //Spawn point of shots (mouth of dragon)
	public float angle;
	public float power;

	[Header("Weapons")]
	public GameObject[] Shots; //Array of GameObjects (weapons to spawn)

	private Rigidbody2D rb; //Rigidbody component
	private int shotSelection; //Which shot is currently selected
	private bool canShoot; //Tells us whether the player can shoot or not.

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

		canShoot = true;

		Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Shot")); 
		//Ignores the shot layer so the player doesn't get pushed around by the shots
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