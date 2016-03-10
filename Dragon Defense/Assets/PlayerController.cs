using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[Header("Player Values")]
	public float speed; //Speed of the player
	public Transform shotSpawn; //Spawn point of shots (mouth of dragon)

	[Header("Weapons")]
	public GameObject[] Shots; //Array of GameObjects (weapons to spawn)

	private Rigidbody2D rb; //Rigidbody component
	private int shotSelection; //Which shot is currently selected
	private bool canShoot; //Tells us whether the player can shoot or not

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

		canShoot = true;

		Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Shot")); 
		//Ignores the shot layer so the player doesn't get pushed around by the shots
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		//Getting input

		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
		//Getting a vector of where to move

		rb.AddForce (movement * speed);
		//Actually moving the player by adding force to the rigidbody component

	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			shotSelection = 0;
			//Lets the player choose their weapon
	}
		if (Input.GetKeyDown (KeyCode.Space) && canShoot) {
			StartCoroutine(FireShot(shotSelection, Shots[shotSelection].GetComponent<ShotClass>().amountToFire));
			//Fires the selected shot
		}
	}

	private IEnumerator FireShot(int currentWeapon, int amountOfShots){

		canShoot = false; //Making it so the player cannot fire again
		GameObject clone = (GameObject)Instantiate(Shots[currentWeapon], shotSpawn.position, shotSpawn.rotation);
		//Instantiating the weapon
		ShotClass cloneShotClass = clone.GetComponent<ShotClass>();
		//Getting the Shot Class
		Vector2 forceArea = new Vector2(shotSpawn.localPosition.x + cloneShotClass.power, shotSpawn.localPosition.y + cloneShotClass.angle/10);
		//Setting up where the fire the firebolt
		clone.GetComponent<Rigidbody2D>().AddForce(forceArea, ForceMode2D.Impulse);
		//Moving the spawned weapon in the forceArea direction
		if(!cloneShotClass.lobShot) {
			clone.GetComponent<Rigidbody2D>().gravityScale = 0;
			//Turning off gravity in order to make it a "straight" shot
		}
		yield return new WaitForSeconds(.1f); //Wait's .1 of a second between shots fired
		if(amountOfShots > 1){
			//If the shots are > 1 we'll fire another
			amountOfShots -= 1;
			StartCoroutine(FireShot(currentWeapon, amountOfShots));
		}

		StartCoroutine(ShotWait());
	}

	private IEnumerator ShotWait(){
		yield return new WaitForSeconds(.5f);
		canShoot = true; //Allows the player to fire again after .5 of a second
	}
}
