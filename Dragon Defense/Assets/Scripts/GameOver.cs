using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public GameObject gameOverObject;
    private bool gameOver;

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

    void Start () {
        gameOver = false;
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
        gameOverObject.GetComponentInChildren<Text>().text = "You win!";
        gameOverObject.SetActive(true);
        gameOver = true;
    }

	public void ReplayLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    void Update()
    {
        if(ScoreUpdate.EnemiesKilled >= TestSpawner.waveSize)
        {
            EndGameVictory();
        }
    }
}
