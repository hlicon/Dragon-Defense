using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {

    public GameObject gameOverObject;
    private Text gameOverText;
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
        gameOverText = gameOverObject.GetComponent<Text>();
        gameOverText.text = "";
        gameOver = false;
	}

    public void OnDestroyPlayer(float score)
    {
        EndGame();
    }

    public void EndGame()
    {
        gameOverText.text = "You Died";
        gameOver = true;
    }
}
