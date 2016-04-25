using UnityEngine;
using System.Collections;

public class IceBolt : ShotClass {

	void Start() {
		shotName = "IceBolt";
		timeAlive = 5f;
		startTimeAlive = timeAlive;
		canRoll = false;
		lobShot = true;
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

	void Update() {
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
