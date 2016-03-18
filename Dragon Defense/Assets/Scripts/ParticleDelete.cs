using UnityEngine;
using System.Collections;

public class ParticleDelete : MonoBehaviour {

	ParticleSystem system;
	private bool paused;

	private float deleteTimer;

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
		system = GetComponent<ParticleSystem>();
		system.collision.SetPlane(0, GameObject.FindGameObjectWithTag("Ground").transform);
		paused = false;
		deleteTimer = system.startLifetime;
	}
		
	void Update(){
		if(!paused){
			deleteTimer -= Time.deltaTime;
			if(deleteTimer <= 0f)
				Destroy(gameObject);
		}
	}

	public void OnPause(){
		paused = !paused;
		if(system != null){
			if(system.isPlaying){
				system.playbackSpeed = 0;
				system.Pause();
			} else if(system.isPaused){
				system.playbackSpeed = 1;
				system.Play();

			}
		}
	}
}
