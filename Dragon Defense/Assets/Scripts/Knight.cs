using UnityEngine;
using System.Collections;

public class Knight : EnemyClass {
    private Rigidbody2D rb;
	private float gravity;
    public float speed;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
		gravity = rb.gravityScale;
		velocity = rb.velocity;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 movement = Vector2.left;

        rb.velocity = movement * speed;

		PauseCheck();
	}

	private void PauseCheck(){
		if(!paused){
			if(rb.velocity == Vector2.zero && wasPaused){
				rb.velocity = velocity;
				rb.gravityScale = gravity;
			}
			velocity = rb.velocity;
		} else {
			wasPaused = false;
			rb.velocity = Vector2.zero;
			rb.gravityScale = 0;
		}
	}

    /*void OnCollisionEnter2D(Collision2D other)
    {

    }*/
}
