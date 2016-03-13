using UnityEngine;
using System.Collections;

public class FireBolt : ShotClass {

	private Rigidbody2D rigbod;
	private float gravity;

	void Start(){
		shotName = "FireBolt";
		damage = 10f;
		timeAlive = 5f;
		canRoll = false;
		lobShot = true;
		amountToFire = 1;
		canCollideWithShots = false;
		paused = false;
		wasPaused = false;

		if(canCollideWithShots != true){
			Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
			//If can collide = false, we ignore the shot layer
		}

		rigbod = GetComponent<Rigidbody2D>();
		velocity = rigbod.velocity;
		gravity = rigbod.gravityScale;
	}

	void Update(){
		PauseCheck();
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Ground" && canRoll != true){ //If we collide with the ground and cannot roll, we destroy the shot
			Instantiate(Resources.Load("FireBoltParticles"), transform.position, Quaternion.identity);
			//Instantiate the appropriate particle from resources
			DeleteObject();
			//Destroy the firebolt
		} else {
			//col.gameObject.GetComponent<Enemy>().damageEnemy(damage);
			
		}
	}

	private void PauseCheck(){
		if(!paused){
			if(rigbod.velocity == Vector2.zero && wasPaused){
				rigbod.velocity = velocity;
				rigbod.gravityScale = gravity;
			}
			velocity = rigbod.velocity;
			CheckTime();
		} else {
			wasPaused = false;
			rigbod.velocity = Vector2.zero;
			rigbod.gravityScale = 0;
		}
	}
		
}
