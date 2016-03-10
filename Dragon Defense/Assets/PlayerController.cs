using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;

	[Header("Player Values")]
	public float speed;
	public GameObject shot;
	public Transform shotSpawn;

	[Header("Weapons")]
	public GameObject[] Shots;

	private int shotSelection;
	private int amountOfShots;
	private bool canShoot;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

		canShoot = true;

		Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Shot"));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);

		rb.AddForce (movement * speed);


	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			shotSelection = 0;
	}
		if (Input.GetKeyDown (KeyCode.Space) && canShoot) {
			amountOfShots = Shots[shotSelection].GetComponent<ShotClass>().amountToFire;
			StartCoroutine(fireShot(shotSelection));
		}
	}

	private IEnumerator fireShot(int currentWeapon){
		yield return new WaitForSeconds(.1f);
		GameObject clone = (GameObject)Instantiate(Shots[currentWeapon], shotSpawn.position, shotSpawn.rotation);
		ShotClass cloneShotClass = clone.GetComponent<ShotClass>();
		Vector2 forceArea = new Vector2(shotSpawn.localPosition.x + cloneShotClass.power, shotSpawn.localPosition.y + cloneShotClass.angle/10);
		clone.GetComponent<Rigidbody2D>().AddForce(forceArea, ForceMode2D.Impulse);
		if(cloneShotClass.straightShot) {
			clone.GetComponent<Rigidbody2D>().gravityScale = 0;
		}
		if(amountOfShots > 1){
			amountOfShots -= 1;
			StartCoroutine(fireShot(currentWeapon));
		}

		StartCoroutine(shotWait());
	}

	private IEnumerator shotWait(){
		canShoot = false;
		yield return new WaitForSeconds(.5f);
		canShoot = true;
	}
}
