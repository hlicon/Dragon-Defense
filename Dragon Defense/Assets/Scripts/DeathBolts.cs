using UnityEngine;
using System.Collections;

public class DeathBolts : ShotClass {

	void Start(){
		this.shotName = "DeathBolts";
		this.damage = 25f;
		this.timeAlive = 5f;
		this.canRoll = true;
		this.lobShot = true;
		this.amountToFire = 4;
		canCollideWithShots = false;

		if(canCollideWithShots != false){
			Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
			//If can collide = false, we ignore the shot layer
		}
	}

	void Update(){
		if(timeAlive > 0){
			timeAlive -= Time.deltaTime;
		} else if(timeAlive <= 0){ //If the fired shot is alive for longer than timeAlive, we destroy it
			DeleteObject();
		}
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
}
