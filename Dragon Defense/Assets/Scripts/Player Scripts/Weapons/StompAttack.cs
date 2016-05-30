using UnityEngine;
using System.Collections;

public class StompAttack : ShotClass {

	private float attackWait;
	private float startAttackwait;
	private Vector2 randSpawn;
	private bool attacked;
	public GameObject stalag;


	void Start(){
		shotName = "Stomp";
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
				SpawnStalags();
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

	private void SpawnStalags(){
		randSpawn = new Vector2(Random.Range(-1, 13) + Random.value, Random.Range(8, 10) + Random.value);

		GameObject clone = (GameObject)Pooling.Spawn(stalag, randSpawn, Quaternion.identity);
		clone.GetComponent<ShotClass>().SetPrefabValues();
		clone.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		attacked = false;
		timeAlive = startTimeAlive;
		Pooling.Despawn(gameObject);
	}
}
