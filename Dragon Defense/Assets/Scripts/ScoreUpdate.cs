using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreUpdate : MonoBehaviour {
	
	public GameObject scoreObject;
	private Text scoreText;

    private static int enemiesKilled;
    public static int EnemiesKilled
    {
        get { return enemiesKilled; }
    }
	private static float score;
	public static float Score
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

	void OnLevelWasLoaded(){
		enemiesKilled = 0;
		score = 0;
	}

	void Start(){
		scoreText = scoreObject.GetComponent<Text>();
        scoreText.text = "Score: " + score;
	}


	public void OnDestroyEnemy(float points){
		score += points;
        enemiesKilled++;
		scoreText.text = "Score: " + score ;
	}
}
