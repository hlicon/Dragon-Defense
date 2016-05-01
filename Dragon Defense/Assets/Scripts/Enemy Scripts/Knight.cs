using UnityEngine;
using System.Collections;

public class Knight : EnemyClass {
    private Rigidbody2D rb;
	private float gravity;
    private float nextAttack = 0.0f;

    public float attackSpeed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
		spriteRend = GetComponent<SpriteRenderer>();
		gravity = rb.gravityScale;
		velocity = rb.velocity;
		healthBar.maxValue = health;
		startHealth = health;
		startSpawn = transform.position;
		moveSpeed = startMoveSpeed;
		UpdateHealthBar();
	}


	
	// Update is called once per frame
	void Update () {
		Vector2 movement = Vector2.left;

        rb.velocity = movement * moveSpeed;

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

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.GetComponentInParent<PlayerController>() != null &&
            Time.time > nextAttack)
        {
            nextAttack = Time.time + attackSpeed;
            other.gameObject.GetComponentInParent<PlayerController>().Damage(damage);
            print("Player took " + damage + " damage.");
            print("Player currently has " + other.gameObject.GetComponentInParent<PlayerController>().health + " health.");
        }
    }
}
