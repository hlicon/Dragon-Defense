using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

	public delegate void PauseEvent();
	public static event PauseEvent OnPause;
	public static event PauseEvent OnRoundWin;
	private bool paused;

	public GameObject pausePanel;
	public WeaponCoolDownUI[] weaponCDUI;

	#region Event Subscriptions
	void OnEnable()
	{
		PlayerController.OnDestroyPlayer += OnDestroyPlayer;
		ClickShoot.OnShoot += OnShoot;
	}

	void OnDisable()
	{
		PlayerController.OnDestroyPlayer -= OnDestroyPlayer;
		ClickShoot.OnShoot -= OnShoot;
	}

	void OnDestroy()
	{
		PlayerController.OnDestroyPlayer -= OnDestroyPlayer;
		ClickShoot.OnShoot -= OnShoot;
	}
	#endregion

	void Start(){
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
	}

	void Update(){
		if(!paused){
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

	public static void PauseGameEnd(){
		if(OnRoundWin != null){
			OnRoundWin();
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
