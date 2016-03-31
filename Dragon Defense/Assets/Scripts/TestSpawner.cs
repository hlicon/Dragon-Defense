using UnityEngine;
using System.Collections;

public class TestSpawner : MonoBehaviour
{
    public delegate void Event();
    public static event Event OnNextWave;

    public float spawnFreq; //used for time between spawns
    public GameObject knight;
    public GameObject ogre;
    public Vector2 spawnPos;
    public int rareEnemyChance;
    public int numWaves;
    public int initialWaveSize;
    private bool pause;
	private bool playerDead;
    private float timer;
    private int pop;
    private int currentWave;
    public int CurrentWave
    {
        get { return currentWave; }
    }
    private int currentWaveSize;
    public int CurrentWaveSize
    {
        get { return currentWaveSize; }
    }
    

    #region Event Subscriptions
    void OnEnable()
    {
        GameStateManager.OnPause += OnPause;
		PlayerController.OnDestroyPlayer += OnDestroyPlayer;
    }
    void OnDisable()
    {
        GameStateManager.OnPause -= OnPause;
		PlayerController.OnDestroyPlayer -= OnDestroyPlayer;
    }
    void OnDestroy()
    {
        GameStateManager.OnPause -= OnPause;
		PlayerController.OnDestroyPlayer -= OnDestroyPlayer;
    }
    #endregion

    // Use this for initialization
    void Start()
    {
        pause = false;
		playerDead = false;
        timer = 0;
        pop = 0;
        currentWave = 1;
        currentWaveSize = initialWaveSize;
    }

    void Update()
    {
		if (!pause && !playerDead && pop < currentWaveSize)
        {
            timer += Time.deltaTime;
            if(Random.Range(0, rareEnemyChance) == rareEnemyChance - 1) {
                Spawn(ogre);
            } else {
                Spawn(knight);
            }
        }
    }

    private void Spawn(GameObject enemy)
    {
        if (timer > spawnFreq)
        {
            Instantiate(enemy, spawnPos, Quaternion.identity);
            timer = 0;
            pop++;
        }
    }

    public void NextWave()
    {
        currentWave++;
        currentWaveSize *= 2;
        pop = 0;
        print("Wave " + currentWave + " of " + numWaves + " starting!");
        if (OnNextWave != null)
            OnNextWave();
    }

	public void OnDestroyPlayer(){
		playerDead = true;
	}

    public void OnPause()
    {
        pause = !pause;
    }
}
