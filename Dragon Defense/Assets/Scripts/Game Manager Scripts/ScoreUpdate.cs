using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreUpdate : MonoBehaviour {
	
	public GameObject scoreObject;
	private Text scoreText;
	private GameStateManager gameStateManager;

    private static int totalEnemiesKilled;
    public static int TotalEnemiesKilled
    {
        get { return totalEnemiesKilled; }
    }
    private static int waveEnemiesKilled;
    public static int WaveEnemiesKilled
    {
        get { return waveEnemiesKilled; }
    }
	private static float score;
	public static float Score
	{
		get { return score; }
	}

	#region Event Subscriptions
	void OnEnable()
	{
        TestSpawner.OnNextWave += OnNextWave;
		EnemyClass.OnDestroyEnemy += OnDestroyEnemy;
	}
	void OnDisable()
	{
        TestSpawner.OnNextWave -= OnNextWave;
        EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;

	}
	void OnDestroy()
	{
        TestSpawner.OnNextWave -= OnNextWave;
        EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;
	}
	#endregion

	void OnLevelWasLoaded(){
        totalEnemiesKilled = 0; //maybe not? we'll see
		waveEnemiesKilled = 0;
		score = 0;
	}

	void Start(){
		scoreText = scoreObject.GetComponent<Text>();
        scoreText.text = "Score: " + score;
		gameStateManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateManager>();
	}


	public void OnDestroyEnemy(float points){
		score += points;
        waveEnemiesKilled++;
        totalEnemiesKilled++;
		scoreText.text = "Score: " + score ;
	}

    public void OnNextWave()
    {
        waveEnemiesKilled = 0;
    }
}
