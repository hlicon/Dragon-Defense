using UnityEngine;
using System.Collections;

public class Glider : EnemyClass {
    private Rigidbody2D rb;
    private float nextAttack = 0.0f;

    public float attackSpeed;
    public float moveSpeed;

    // Use this for initialization
    void Start () {
        isMelee = false;
        rb = GetComponent<Rigidbody2D>();
        velocity = rb.velocity;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 movement = Vector2.left;

        rb.velocity = movement * moveSpeed;

        PauseCheck();
    }

    private void PauseCheck()
    {
        if (!paused)
        {
            if (rb.velocity == Vector2.zero && wasPaused)
            {
                rb.velocity = velocity;
            }
            velocity = rb.velocity;
        }
        else {
            wasPaused = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }
    }
}
