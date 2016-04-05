using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public GameObject gameOverObject;
	public GameObject gameWinObject;
    private TestSpawner testSpawner;
	private GameStateManager gameStateManager;
	private bool gameOver;

    #region Event Subscriptions
    void OnEnable()
    {
        PlayerController.OnDestroyPlayer += OnDestroyPlayer;
		EnemyClass.OnDestroyEnemy += OnDestroyEnemy;
    }

    void OnDisable()
    {
        PlayerController.OnDestroyPlayer -= OnDestroyPlayer;
		EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;
    }

    void OnDestroy()
    {
        PlayerController.OnDestroyPlayer -= OnDestroyPlayer;
		EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;
    }
    #endregion

    void Start () {
        gameOver = false;
        testSpawner = GameObject.FindGameObjectWithTag("Respawn").GetComponentInParent<TestSpawner>();
		//gameStateManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateManager>();
	}

    public void OnDestroyPlayer()
    {
        EndGameDeath();
    }

    public void EndGameDeath()
    {
		gameOverObject.SetActive(true);
        gameOver = true;
		GameStateManager.PauseGameEnd();
    }

    public void EndGameVictory()
    {
		gameWinObject.SetActive(true);
        gameOver = true;
		GameStateManager.PauseGameEnd();
    }

	public void ReplayLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void OnDestroyEnemy(float dam){
		if(ScoreUpdate.WaveEnemiesKilled >= testSpawner.CurrentWaveSize) {
            if (testSpawner.CurrentWave >= testSpawner.numWaves)
            {
                EndGameVictory();
            } else
            {
                testSpawner.NextWave();
            }
		}
	}
}
