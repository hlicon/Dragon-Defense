using UnityEngine;
using System.Collections;

public class PoisonBolt : ShotClass {

	void Start(){
		shotName = "PoisonBolt";
		timeAlive = 5f;
		startTimeAlive = timeAlive;
		canRoll = false;
		lobShot = true;
		canCollideWithShots = false;
		paused = false;
		wasPaused = false;
		rigbod = GetComponent<Rigidbody2D>();
		velocity = rigbod.velocity;
		gravity = rigbod.gravityScale;
		if(canCollideWithShots != true){
			Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
			//If can collide = false, we ignore the shot layer
		}
	}

	void Update(){
		PauseCheck();
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
