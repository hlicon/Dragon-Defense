using UnityEngine;
using System.Collections;

public class StalagmiteBolt : ShotClass {

	// Use this for initialization
	void Start () {
		shotName = "Stalagmite";
		damage = 15;
		timeAlive = 5f;
		startTimeAlive = timeAlive;
		amountToFire = 1;
		weaponColorNumber = 4;

		if(canCollideWithShots != true){
			Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
			//If can collide = false, we ignore the shot layer
		}

		rigbod = GetComponent<Rigidbody2D>();
		gravity = rigbod.gravityScale;
	}
	
	void Update(){
		PauseCheck();
		//Temp
		transform.localRotation = Quaternion.Euler(0, 0, 90);
	}

	void OnTriggerEnter2D(Collider2D col) {
		OnHit (col);
	}

	private void PauseCheck(){
		if(!paused){
			if(rigbod.velocity == Vector2.zero && wasPaused){
				trailParticles.Play();
				if(burstParticles.particleCount > 0)
					burstParticles.Play();
				rigbod.velocity = velocity;
				rigbod.gravityScale = gravity;
			}
			velocity = rigbod.velocity;
			CheckTime();
		} else {
			wasPaused = false;
			trailParticles.Pause();
			burstParticles.Pause();
			rigbod.velocity = Vector2.zero;
			rigbod.gravityScale = 0;
		}
	}
}
