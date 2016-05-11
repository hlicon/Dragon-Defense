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
}
