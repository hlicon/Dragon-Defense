using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

	public delegate void PauseEvent();
	public static event PauseEvent OnPause;

	public GameObject pausePanel;
    public GameObject scorePanel;

    private float score;

    public float Score
    {
        get { return score; }
    }

    #region Event Subscriptions
    void OnEnable()
    {
        EnemyClass.OnDestroyEnemy += OnDestroyEnemy;
    }
    void OnDisable()
    {
        EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;

    }
    void OnDestroy()
    {
        EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;

    }
    #endregion

    public void PauseGame(){
		if(OnPause != null){
			OnPause();
			pausePanel.SetActive(!pausePanel.activeSelf); 
		}
	}



    public void OnDestroyEnemy(float points){
        score += points;
        scorePanel.GetComponent<Text>().text = "Score: " + score;
    }

}
