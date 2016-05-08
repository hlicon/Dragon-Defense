using UnityEngine;
using System.Collections;

public class StickyBolt : ShotClass {

	private bool stuck;
	public float stickTime;
	private float startStickTime;
	private Vector2 stickPosition;

	void Start(){

		startTimeAlive = timeAlive;
		startStickTime = stickTime;
		wasPaused = false;
		paused = false;

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
		if(stuck && !paused){
			CheckStickTime();
			if(transform.position.x < 100)
				transform.position = stickPosition;
		}
	}

	protected override void OnTriggerEnter2D(Collider2D col){

		if(col.gameObject.GetComponent<EnemyClass>() != null){
			stuck = false;
			DoDamage(col);
		} 
		else if(col.gameObject.tag.Contains("Ground") && !stuck){
			rigbod.velocity = Vector2.zero;
			velocity = rigbod.velocity;
			stuck = true;
			stickPosition = transform.position;
		}
	}

	private void CheckStickTime(){
		stickTime -= Time.deltaTime;
		if(stickTime <= 0){
			timeAlive = startTimeAlive;
			velocity = Vector2.zero;
			rigbod.velocity = velocity;
			stickTime = startStickTime;
			stickPosition = new Vector2(999,999);
			stuck = false;
			burstParticles.Play();
			timeAlive = 1;
			StartCoroutine(MoveWait());
		}
	}
}
