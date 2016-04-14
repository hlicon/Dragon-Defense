using UnityEngine;
using System.Collections;

public class Glider : EnemyClass {
    private Rigidbody2D rb;
    private float nextAttack = 0.0f;
    private float xOffset;
    private float yOffset;
    private float sinAmp; //used to determine the amplitude of the movement

    public float attackSpeed;

    // Use this for initialization
    void Start () {
        xOffset = transform.position.x;
        yOffset = transform.position.y;
        sinAmp = Random.Range(0.5f, 3.0f);
        isMelee = false;
        rb = GetComponent<Rigidbody2D>();
        velocity = rb.velocity;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>()); //this isn't working
    }
	
	// Update is called once per frame
	void Update () {
        float sin = Mathf.Sin(Time.time * moveSpeed) * sinAmp + yOffset;
        xOffset -= Time.deltaTime * moveSpeed;
        transform.position = new Vector2(xOffset, sin);

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
