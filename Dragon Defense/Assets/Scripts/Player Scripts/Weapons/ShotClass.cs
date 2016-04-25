using UnityEngine;
using System.Collections;

public class ShotClass : MonoBehaviour {

	public string shotName; //Name of the weapon
	public float damage; //Damage the weapon deals
	public float timeAlive;
	public bool canRoll; //Can it roll on the ground? (not sure what this will be fure quite yet)
	public bool canCollideWithShots; //Can it collide with other shots?
	public bool lobShot; //Will it lob, or be a straight shot?
	public int amountToFire = 1; //Amount of shots to fire (increased with upgrade system etc)
	public Vector2 velocity;
	protected Rigidbody2D rigbod;
	protected float gravity;
	public int weaponColorNumber;

	protected bool paused;
	protected bool wasPaused;

	public ParticleSystem trailParticles;
	public ParticleSystem burstParticles;

	public delegate void DamageEvent(float damage, GameObject col, int weaponNumber, Vector3 shotPosition);
	public static event DamageEvent OnDamage;

	protected float startTimeAlive;

	#region Event Subscriptions
	void OnEnable(){
		GameStateManager.OnPause += OnPause;
	}
	void OnDisable(){
		GameStateManager.OnPause -= OnPause;
	}
	void OnDestroy(){
		GameStateManager.OnPause -= OnPause;
	}
	#endregion

	void OnTriggerEnter2D(Collider2D col){
		burstParticles.Play();

		GetComponent<Collider2D>().enabled = false;

		if(shotName.Equals("IceBolt") && col.GetComponent<EnemyClass>() != null) //this is shit
		{
			col.GetComponent<EnemyClass>().moveSpeed/= 2;
		}

        if (OnDamage != null) {
			OnDamage(damage, col.gameObject, weaponColorNumber, transform.position);
        }
		StartCoroutine(MoveWait()); //Need this so burstparticles are shown in correct area
		//They were spawning after the move even though the play is called first? @_@
	}

	private IEnumerator MoveWait(){
		yield return new WaitForSeconds(.01f);
		MoveShot();
		GetComponent<Collider2D>().enabled = true;
	}

	public void MoveShot(){
		transform.position = new Vector3(999,999,999);
		trailParticles.Stop();
		timeAlive = timeAlive/2;
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
}