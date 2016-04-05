using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

	public delegate void PauseEvent();
	public static event PauseEvent OnPause;
	public static event PauseEvent OnRoundWin;

	public GameObject pausePanel;

	#region Event Subscriptions
	void OnEnable()
	{
		PlayerController.OnDestroyPlayer += OnDestroyPlayer;
	}

	void OnDisable()
	{
		PlayerController.OnDestroyPlayer -= OnDestroyPlayer;
	}

	void OnDestroy()
	{
		PlayerController.OnDestroyPlayer -= OnDestroyPlayer;
	}
	#endregion

	void Start(){
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
	}

    public void PauseGame(){
		if(OnPause != null){
			OnPause();
			pausePanel.SetActive(!pausePanel.activeSelf); 
		}
	}

	public static void PauseGameEnd(){
		if(OnRoundWin != null){
			OnRoundWin();
		}
	}

	public void OnDestroyPlayer(){
		//Save high score, save exp (if we have that), save achievements etc etc
	}
}
