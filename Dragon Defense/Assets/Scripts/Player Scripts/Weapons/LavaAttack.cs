using UnityEngine;
using System.Collections;

public class LavaAttack : ShotClass {

	private float attackWait;
	private float startAttackwait;
	private Vector2 randSpawn;
	private bool attacked;
	public GameObject lava;


	void Start(){
		timeAlive = 5f;
		startTimeAlive = timeAlive;
		paused = false;
		wasPaused = false;
		amountToFire = 3;
		rigbod = GetComponent<Rigidbody2D>();
		velocity = rigbod.velocity;
		gravity = rigbod.gravityScale;


		//Stomp specifics
		attackWait = .5f;
		startAttackwait = attackWait;
		if(canCollideWithShots != true){
			Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
			//If can collide = false, we ignore the shot layer
		}
	}

	void Update(){
		PauseCheck();
		if(!paused){
			CheckTime();
			if(attackWait > 0){
				attackWait -= Time.deltaTime;
				if(attackWait <= 0){
					attacked = true;
					attackWait = startAttackwait;
					SpawnLava();
				}
			}
		}
	}

	protected override void OnTriggerEnter2D(Collider2D col){
		//Override Shotclass trigger enter
		if(col.tag.Equals("Ground") && attacked){
			attacked = false;
			attackWait = startAttackwait;
			timeAlive = startTimeAlive;
			Pooling.Despawn(gameObject);
		}
	}

	private void SpawnLava(){
		randSpawn = new Vector2(Random.Range(-1, 13) + Random.value, Random.Range(-7, -5) + Random.value);
		Vector3 spawnRotation = new Vector3(0, 0, 90);
		GameObject clone = (GameObject)Pooling.Spawn(lava, randSpawn, Quaternion.Euler(spawnRotation));
		clone.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		LavaBolt tempLavaBolt = clone.GetComponent<LavaBolt>();
		tempLavaBolt.moveTimer = 0;
		tempLavaBolt.stopMove = false;
		tempLavaBolt.justSpawned = true;
		clone.GetComponent<ShotClass>().SetPrefabValues();
		attacked = false;
		timeAlive = startTimeAlive;
		Pooling.Despawn(gameObject);
	}
}
