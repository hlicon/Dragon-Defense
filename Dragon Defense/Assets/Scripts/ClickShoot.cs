using UnityEngine;
using System.Collections;

public class ClickShoot : MonoBehaviour {

	private Vector2 mouseClickPosition;
	private bool canShoot;
	private bool paused;
	private int selection = 0;
	private float power;
	private float angle;
	private Vector2 shotSpawnPos;

	public Transform shotSpawn;

	[Header("Time Between Shots")]
	public float shotTimer;

	[Header("Weapons")]
	public GameObject[] Shots;

	public delegate void SelectionEvent(int selection);
	public static event SelectionEvent OnSelectionChanged;

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

	void Start(){
		canShoot = true;
		shotSpawnPos = shotSpawn.transform.position;

		Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Shot")); 
		//Ignores the shot layer so the player doesn't get pushed around by the shots
		paused = false;
	}

	void Update(){

		if(!paused){
			GetSelection(selection);
			TimerSet();
			GetMouseInput();
		}
	}

	private void GetMouseInput(){
		if(Input.GetMouseButton(0)){ //Is the mouse button being held down?
			mouseClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//Setting mouse position so we don't have to call >>^^ everytime
			if(canShoot && selection >= 0 && mouseClickPosition.y >= -2.0f){ //Do we have a proper selection and can we shoot?
				CalculateShot(); //If so, let's "calculate" where we're firing
			}
		} 
	}

	//Removed the ShootWait IEnumerator to this better function
	private void TimerSet(){
		if(canShoot == false)
		shotTimer -= Time.deltaTime;
		if(shotTimer <= 0f){
				shotTimer = .5f;
				canShoot = true;
		}
	}
		
	private void CalculateShot(){
		shotSpawnPos = shotSpawn.transform.position;
		Debug.DrawLine(shotSpawnPos, mouseClickPosition, Color.red);
		//Drawing a line in the scene (not the game view) to help display where we're clicking

		Vector2 Dir = mouseClickPosition - shotSpawnPos;
		//Where we want to apply the force to

		if(Dir.x > 4f)
			Dir.x = 4f;
		if(Dir.y > 4f)
			Dir.y = 4f;
		//Limit the distance so we don't fire too far

		FireWeapon(Dir); //After we've "calculated" the distance we're going to fire the shot
	}

	private void FireWeapon(Vector2 Dir){
		canShoot = false;
		GameObject clone = (GameObject)Instantiate(Shots[selection], shotSpawnPos, Quaternion.identity);
		//Spawn the shot from the selection
		ShotClass cloneShotClass = clone.GetComponent<ShotClass>();
		//Getting the shot class
		clone.GetComponent<Rigidbody2D>().AddForce(Dir + Dir, ForceMode2D.Impulse);
		//Applying the force we passed in from calculating

		if(!cloneShotClass.lobShot){
			clone.GetComponent<Rigidbody2D>().gravityScale = 0; //Turn gravity off for straight shots
		}
	}

	public void GetSelection(int newSelection){ //Setting selection to the selected shot in the Shots array
		if(selection != newSelection){
			selection = newSelection;
		}

		if(Input.GetKeyDown(KeyCode.Alpha1)){
			selection = 0;
		} else if(Input.GetKeyDown(KeyCode.Alpha2)){
			selection = 1;
		}
		OnSelectionChanged(selection);
	}

	public void OnPause(){
		paused = !paused;
	}

}
