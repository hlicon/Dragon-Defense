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
}
