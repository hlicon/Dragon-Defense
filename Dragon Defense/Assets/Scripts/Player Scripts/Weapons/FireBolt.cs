using UnityEngine;
using System.Collections;

public class FireBolt : ShotClass {

	void Start(){
		shotName = "FireBolt";
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

	void Update(){
		PauseCheck();
	}
}
