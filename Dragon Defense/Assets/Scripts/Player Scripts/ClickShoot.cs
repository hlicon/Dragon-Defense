using UnityEngine;
using System.Collections;

public class ClickShoot : MonoBehaviour {

	private Vector2 mouseClickPosition;
	private bool canShoot;
	private bool paused;
	private int selection = 0;
	private float power;
	private float angle;
	private float shakeWait;
	private Vector2 shotSpawnPos;
	private Camera mainCam;
	private GameStateManager gameStateManager;

	public Transform shotSpawn;

	[Header("Time Between Shots")]
	public float shotTimer;

	[Header("Weapons")]
	public GameObject[] Shots;
	public GameObject Stalags;

	public delegate void SelectionEvent(int selection);
	public static event SelectionEvent OnSelectionChanged;

	public delegate void ShootEvent(int selection, float cooldown);
	public static event ShootEvent OnShoot;

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

	void Start(){
		canShoot = true;
		shotSpawnPos = shotSpawn.transform.position;
		gameStateManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateManager>();

		Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Shot")); 
		//Ignores the shot layer so the player doesn't get pushed around by the shots
		paused = false;

		mainCam = Camera.main;

		for(int i = 0; i < 2; i++){
			Pooling.Preload(Shots[i], 20);
		}
		//temp
		Pooling.Preload(Shots[4], 20);
		Pooling.Preload(Stalags, 20);
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
		if(shakeWait > 0){
			shakeWait -=Time.deltaTime;
			if(shakeWait <= 0){
				camShake();
			}
		}
	}
		
	private void CalculateShot(){
		shotSpawnPos = shotSpawn.transform.position;
		Debug.DrawLine(shotSpawnPos, mouseClickPosition, Color.red);
		//Drawing a line in the scene (not the game view) to help display where we're clicking

		Vector2[] Dir = new Vector2[Shots[selection].GetComponent<ShotClass>().amountToFire];

		//Where we want to apply the force to

		for(int i = 0; i < Dir.Length; i++){
			float tempLength = Dir.Length;
			float splitShot = i/tempLength;
			Vector2 splitOffset = new Vector2(splitShot, splitShot);
			Dir[i] = mouseClickPosition - shotSpawnPos - splitOffset;

			if(Dir[i].x > 4f)
				Dir[i].x = 4f + splitShot;
			if(Dir[i].y > 4f)
				Dir[i].y = 4f + splitShot;
		}
		//Limit the distance so we don't fire too far
		if(gameStateManager.weaponCDUI[selection].cooldown <= .01)
		FireWeapon(Dir); //After we've "calculated" the distance we're going to fire the shot
	}

	private void FireWeapon(Vector2[] Dir){
		for(int i = 0; i < Dir.Length; i++){
		canShoot = false;
		GameObject clone = (GameObject)Pooling.Spawn(Shots[selection], shotSpawnPos, Quaternion.identity);
		//Spawn the shot from the selection
		ShotClass cloneShotClass = clone.GetComponent<ShotClass>();
		//Getting the shot class
		clone.GetComponent<Rigidbody2D>().AddForce(Dir[i] + Dir[i], ForceMode2D.Impulse);
		clone.GetComponent<Collider2D>().enabled = true;
		//Applying the force we passed in from calculating
			if(OnShoot != null){
				OnShoot(selection, cloneShotClass.cooldown);
			}

		if(!cloneShotClass.lobShot){
			clone.GetComponent<Rigidbody2D>().gravityScale = 0; //Turn gravity off for straight shots
		}
		if(cloneShotClass.shotName.Equals("Stomp")){
			//play stomp animation
			shakeWait = .5f;
		}
		}
	}

	private void camShake(){
		mainCam.GetComponent<CameraShake>().StartShake();
		shakeWait = 0;
	}

	public void GetSelection(int newSelection){ //Setting selection to the selected shot in the Shots array
		if(selection != newSelection){
			selection = newSelection;
		}

		for(int i = 0; i < 10; i++){
			if(Input.GetKeyDown(KeyCode.Alpha0 + i)){
				selection = i - 1;
			}
			if(Input.GetKeyDown(KeyCode.Alpha0)){
				selection = 9;
			}
		}
		OnSelectionChanged(selection);
	}

	public void OnRoundWin(){
		paused = !paused;
	}

	public void OnPause(){
		paused = !paused;
	}

}
