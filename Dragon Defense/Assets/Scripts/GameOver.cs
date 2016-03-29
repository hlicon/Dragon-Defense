using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public GameObject gameOverObject;
	public GameObject gameWinObject;
    private TestSpawner testSpawner;
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
	}

    public void OnDestroyPlayer()
    {
        EndGameDeath();
    }

    public void EndGameDeath()
    {
		gameOverObject.SetActive(true);
        gameOver = true;
    }

    public void EndGameVictory()
    {
		gameWinObject.SetActive(true);
        gameOver = true;
    }

	public void ReplayLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void OnDestroyEnemy(float dam){
		if(ScoreUpdate.EnemiesKilled >= testSpawner.waveSize) {
			EndGameVictory();
		}
	}
}
