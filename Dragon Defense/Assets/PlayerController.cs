using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	public float speed;

	public GameObject shot;
	public Transform shotSpawn;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);

		rb.AddForce (movement * speed);


	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
}
