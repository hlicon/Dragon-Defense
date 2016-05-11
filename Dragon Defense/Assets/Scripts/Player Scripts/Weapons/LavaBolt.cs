using UnityEngine;
using System.Collections;

public class LavaBolt : ShotClass
{

	public float attackTimer;
	private float startAttackTimer;

	private bool moveBack;
	public bool stopMove;
	public float moveTimer;
	public bool justSpawned;

	public ParticleSystem bodyParticles;

	// Use this for initialization
	void Start ()
	{
		startAttackTimer = attackTimer;
		startTimeAlive = timeAlive;
		wasPaused = false;
		paused = false;

		moveBack = false;
		stopMove = false;

		if (canCollideWithShots != true) {
			Physics2D.IgnoreLayerCollision (gameObject.layer, gameObject.layer);
			//If can collide = false, we ignore the shot layer
		}

		rigbod = GetComponent<Rigidbody2D> ();
		velocity = rigbod.velocity;
		gravity = rigbod.gravityScale;
	}
	
	// Update is called once per frame
	void Update ()
	{
		PauseCheck ();
		if (!paused) {
			TimerSet ();
			ShotMovement ();
		}
		transform.localRotation = Quaternion.Euler(0, 0, 90);
	}

	protected override void OnTriggerEnter2D (Collider2D col)
	{
		if (attackTimer <= 0 && col.GetComponent<EnemyClass> () != null) {
			DoDamage (col);
			attackTimer = startAttackTimer;
		}
	}

	void OnTriggerStay2D (Collider2D col)
	{
		if (attackTimer <= 0 && col.gameObject.GetComponent<EnemyClass> () != null && !col.GetComponent<EnemyClass>().firedParticles.isPlaying) {
			DoDamage (col);
			attackTimer = startAttackTimer;
		}
	}

	private void ShotMovement ()
	{
		if(!stopMove){
			transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, 0f), .8f * Time.deltaTime);
		}

		if(transform.position.y >= -.5f){
			stopMove = true;
			moveTimer += Time.deltaTime;
		}

		if(moveTimer >= .05f){
			transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, -8f), .7f * Time.deltaTime);
			if(transform.position.y <= -5.4f){
				stopMove = false;
				moveTimer = 0;
				MoveShot();
			}
		}

	}

	protected override void PauseCheck ()
	{
		if (!paused) {
			if (wasPaused) {
				if (trailParticles != null)
					trailParticles.Play ();
				if (bodyParticles != null) {
					if (bodyParticles.particleCount > 0){
						bodyParticles.Play ();
					}
				}
				if (burstParticles != null) {
					burstParticles.Play ();
				}
			}
			wasPaused = false;
			CheckTime ();
		} else {
			wasPaused = false;
			if (trailParticles != null)
				trailParticles.Pause ();
			if (burstParticles != null)
				burstParticles.Pause ();
			if (bodyParticles != null)
				bodyParticles.Pause ();
		}
	}

	private void TimerSet ()
	{
		attackTimer -= Time.deltaTime;
	}
}
