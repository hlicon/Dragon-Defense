using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreUpdate : MonoBehaviour {
	
	public GameObject scoreObject;
	public GameObject goldObject;
	private Text scoreText;
	private GameStateManager gameStateManager;
	private Text goldText;

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
	private static float gold; //debug
	public static float Gold
	{
		get { return gold; }
	}

	public delegate void SellEvent();
	public static event SellEvent OnSellItem;

	#region Event Subscriptions
	void OnEnable()
	{
        TestSpawner.OnNextWave += OnNextWave;
		EnemyClass.OnDestroyEnemy += OnDestroyEnemy;
		//LootManager.OnSellItem += OnSellItem;
	}
	void OnDisable()
	{
        TestSpawner.OnNextWave -= OnNextWave;
        EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;
		//LootManager.OnSellItem -= OnSellItem;

	}
	void OnDestroy()
	{
        TestSpawner.OnNextWave -= OnNextWave;
        EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;
		//LootManager.OnSellItem -= OnSellItem;
	}
	#endregion

	void OnLevelWasLoaded(){
        totalEnemiesKilled = 0; //maybe not? we'll see
		waveEnemiesKilled = 0;
		score = 0;
		gold = 0;
	}

	void Start(){
		scoreText = scoreObject.GetComponent<Text>();
		goldText = goldObject.GetComponent<Text> ();
        scoreText.text = "Score: " + score;
		gameStateManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateManager>();
		goldText.text = "Hoard Worth: " + gold;
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

	public bool BuyItem(int value){
		bool canBuy = false;
		if(value <= gold){
			gold -= value;
			canBuy = true;
		}
		goldText.text = "Hoard Worth: " + gold;
		return canBuy;
	}

	public void SellItem(int value) {
		OnSellItem(); //Loot manager needs this to check for size of list and adjust UI
		gold += value;
		goldText.text = "Hoard Worth: " + gold;
	}
}
