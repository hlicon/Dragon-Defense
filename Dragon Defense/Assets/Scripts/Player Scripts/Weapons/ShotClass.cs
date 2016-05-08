using UnityEngine;
using System.Collections;

public class ShotClass : MonoBehaviour {

	public string shotName; //Name of the weapon
	public float timeAlive;
	public int weaponColorNumber;
	public bool canRoll; //Can it roll on the ground? (not sure what this will be for quite yet)
	public bool canCollideWithShots; //Can it collide with other shots?
	public bool lobShot; //Will it lob, or be a straight shot?
	public string affectType;
	[Header("Upgrade Values")]
	public int amountToFire = 1; //Amount of shots to fire (increased with upgrade system etc)
	public float cooldown;
	public float damage; //Damage the weapon deals
	public float damageMultiplier; //How much the weapon's damage is multiplied by
	public float affectDamage; //How much damage the affect will have on the enemy, if any.
	public float affectTime; //How long the affect will last
	public float affectDamageMultiplier; //How much the affect damage is multiplied by, if any.
	public float affectTimeMultiplier; //How much the affect time is multiplied by, if any.
	public float affectHitRate;
	protected Vector2 velocity;
	protected Rigidbody2D rigbod;
	protected float gravity;

	protected bool paused;
	protected bool wasPaused;

	[Header("Particles")]
	public ParticleSystem trailParticles;
	public ParticleSystem burstParticles;

	public delegate void DamageEvent(float damage, GameObject col, int weaponNumber, Vector3 shotPosition);
	public static event DamageEvent OnDamage;

	public delegate void AffectEvent(GameObject col, string affectType, float affectDamage, float affectTime, float affectHitRate);
	public static event AffectEvent OnAffect;

	protected float startTimeAlive;

	#region Event Subscriptions
	void OnEnable(){
		GameStateManager.OnPause += OnPause;
		GameStateManager.OnRoundWin += OnRoundWin;
	}
	void OnDisable(){
		GameStateManager.OnPause -= OnPause;
		GameStateManager.OnRoundWin -= OnRoundWin;
	}
	void OnDestroy(){
		GameStateManager.OnPause -= OnPause;
		GameStateManager.OnRoundWin -= OnRoundWin;
	}
	#endregion

	protected virtual void OnTriggerEnter2D(Collider2D col){
		DoDamage(col);
		StartCoroutine(MoveWait()); //Need this so burstparticles are shown in correct area
		//They were spawning after the move even though the play is called first? @_@
	}

	protected void DoDamage(Collider2D col){
		burstParticles.Play();
		GetComponent<Collider2D>().enabled = false;
		if (OnDamage != null) {
			float tDamage = damage*damageMultiplier;
			OnDamage(tDamage, col.gameObject, weaponColorNumber, transform.position);
		}
		if (OnAffect != null){
			float tAffectDamage = affectDamage * damageMultiplier;
			float tAffectTime = affectTime * affectTimeMultiplier;
			OnAffect(col.gameObject, affectType, tAffectDamage, tAffectTime, affectHitRate);
		}

		StartCoroutine(MoveWait());
	}

	void OnRoundWin(){
		StartCoroutine(MoveWait());
	}

	protected IEnumerator MoveWait(){
		yield return new WaitForSeconds(.01f);
		MoveShot();
		GetComponent<Collider2D>().enabled = true;
	}

	public void MoveShot(){
		transform.position = new Vector3(999,999,999);
		trailParticles.Stop();
	}

	public void OnPause(){
		paused = !paused;
		wasPaused = true;
	}

	protected void CheckTime(){
		if(timeAlive > 0){
			timeAlive -= Time.deltaTime;
		} else if(timeAlive <= 0){ //If the fired shot is alive for longer than timeAlive, we destroy it
			timeAlive = startTimeAlive;
			velocity = Vector2.zero;
			rigbod.velocity = Vector2.zero;
			Pooling.Despawn(gameObject);
		}
	}

	protected void PauseCheck(){
		if(!paused){
			if(rigbod.velocity == Vector2.zero && wasPaused){
				if(trailParticles != null)
				trailParticles.Play();
				if(burstParticles != null){
					if(burstParticles.particleCount > 0)
						burstParticles.Play();
				}
				rigbod.velocity = velocity;
				rigbod.gravityScale = gravity;
			}
			velocity = rigbod.velocity;
			CheckTime();
		} else {
			wasPaused = false;
			if(trailParticles != null)
			trailParticles.Pause();
			if(burstParticles != null)
			burstParticles.Pause();
			rigbod.velocity = Vector2.zero;
			rigbod.gravityScale = 0;
			print("This is happened");
		}
	}
}