using UnityEngine;
using System.Collections;

public class TestSpawner : MonoBehaviour {
    public float spawnFreq; //used for time between spawns
    public GameObject enemy;
    public Vector2 spawnPos;
	private bool pause;
	private float timer;

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

	// Use this for initialization
	void Start () {
		pause = false;
		timer = 0;
		Spawn();
	}

	void Update(){
		if(!pause){
		timer += Time.deltaTime;
		Spawn();
		}
	}

	private void Spawn()
    {
			if(timer > spawnFreq){
			Instantiate(enemy, spawnPos, Quaternion.identity);
				timer = 0;
			}
    }

	public void OnPause(){
		pause = !pause;
	}
}
