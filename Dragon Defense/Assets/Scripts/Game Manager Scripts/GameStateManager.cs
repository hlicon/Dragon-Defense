using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

	public delegate void PauseEvent();
	public static event PauseEvent OnPause;
	public static event PauseEvent OnRoundWin;
	private bool paused;
	private bool roundWon;

	public GameObject pausePanel;
	public WeaponCoolDownUI[] weaponCDUI;

	#region Event Subscriptions
	void OnEnable()
	{
		PlayerController.OnDestroyPlayer += OnDestroyPlayer;
		ClickShoot.OnShoot += OnShoot;
		TestSpawner.OnNextWave += OnNextWave;
	}

	void OnDisable()
	{
		PlayerController.OnDestroyPlayer -= OnDestroyPlayer;
		ClickShoot.OnShoot -= OnShoot;
		TestSpawner.OnNextWave -= OnNextWave;
	}

	void OnDestroy()
	{
		PlayerController.OnDestroyPlayer -= OnDestroyPlayer;
		ClickShoot.OnShoot -= OnShoot;
		TestSpawner.OnNextWave -= OnNextWave;
	}
	#endregion

	void Start(){
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
		roundWon = false;
	}

	void Update(){
		if(!paused && !roundWon){
			for(int i = 0; i < weaponCDUI.Length; i++){
				weaponCDUI[i].cooldown -= Time.deltaTime;
			}
		}
	}

    public void PauseGame(){
		if(OnPause != null){
			OnPause();
			paused = !paused;
			pausePanel.SetActive(!pausePanel.activeSelf); 
		}
	}

	void OnNextWave(){
		PauseGameEnd();
		roundWon = false;
	}

	public void PauseGameEnd(){
		if(OnRoundWin != null){
			OnRoundWin();
		}
		if(roundWon == false){
			roundWon = true;
		}
	}

	public void OnShoot(int selection, float cooldown){
		weaponCDUI[selection].cooldown = cooldown;
		weaponCDUI[selection].startCooldown = cooldown;
		for(int i = 0; i < weaponCDUI.Length; i++){
			if(weaponCDUI[i].cooldown < .5f){
				weaponCDUI[i].startCooldown = .5f;
				weaponCDUI[i].cooldown = .5f;
			}
		}
	}

	public void OnDestroyPlayer(){
		//Save high score, save exp (if we have that), save achievements etc etc
	}
}
