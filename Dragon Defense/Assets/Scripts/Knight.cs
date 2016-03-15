using UnityEngine;
using System.Collections;

public class Knight : EnemyClass {
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(-velocity, 0f, 0f);

        rb.velocity = movement;

        if(health <= 0) {
            DeleteObject();
        }
	}

    /*void OnCollisionEnter2D(Collision2D other)
    {

    }*/
}
