using UnityEngine;
using System.Collections;

public class FireBolt : ShotClass {

	void Start(){
		this.shotName = "FireBolt";
		this.damage = 10;
		this.canRoll = false;
		this.lobShot = true;
		this.straightShot = false;
		this.amountToFire = 3;
		this.angle = 25;
		this.power = 5;
		canCollideWithShots = false;
		if(!canCollideWithShots){
			Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Ground" && canRoll != true){
			GameObject.Destroy(gameObject);
		} else {
			//col.gameObject.GetComponent<Enemy>().damageEnemy(damage);
		}
	}

}
