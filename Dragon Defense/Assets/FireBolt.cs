using UnityEngine;
using System.Collections;

public class FireBolt : ShotClass {

	void Start(){
		this.shotName = "FireBolt";
		this.damage = 10;
		this.canRoll = false;
		this.lobShot = true;
		this.amountToFire = 3;
		this.angle = 25;
		this.power = 5;
		this.timeAlive = 2;
		canCollideWithShots = false;

		if(canCollideWithShots != true){
			Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
			//If can collide = false, we ignore the shot layer
		}
	}

	void Update(){
		if(timeAlive > 0){
			timeAlive -= Time.deltaTime;
		} else if(timeAlive <= 0){ //If the fired shot is alive for longer than timeAlive, we destroy it
			GameObject.Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Ground" && canRoll != true){ //If we collide with the ground and cannot roll, we destroy the shot
			Instantiate(Resources.Load("FireBoltParticles"), transform.position, Quaternion.identity);
			//Instantiate the appropriate particle from resources
			GameObject.Destroy(gameObject);
			//Destroy the firebolt
		} else {
			//col.gameObject.GetComponent<Enemy>().damageEnemy(damage);
			
		}
	}

}
