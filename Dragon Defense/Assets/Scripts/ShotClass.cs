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
	public GameObject particleToSpawn;

	protected bool paused;
	protected bool wasPaused;

	public delegate void DamageEvent(float damage, GameObject col);
	public static event DamageEvent OnDamage;


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


	void OnCollisionEnter2D(Collision2D col){
		OnDamage(damage, col.gameObject);
		Instantiate(particleToSpawn, transform.position, Quaternion.identity);
		DeleteObject();
	}

	public void DeleteObject(){
		Destroy(this.gameObject);
	}

	public void OnPause(){
		paused = !paused;
		wasPaused = true;
	}

	protected void CheckTime(){
		if(timeAlive > 0){
			timeAlive -= Time.deltaTime;
		} else if(timeAlive <= 0){ //If the fired shot is alive for longer than timeAlive, we destroy it
			DeleteObject();
		}
	}
}