using UnityEngine;
using System.Collections;

public class TestSpawner : MonoBehaviour
{
    public delegate void NextWaveEvent();
    public static event NextWaveEvent OnNextWave;

    public float spawnFreq; //used for time between spawns
    public GameObject knight;
    public GameObject ogre;
    public GameObject glider;
    public Vector2 spawnPos;
    public int rareEnemyChance;
    //public int numWaves; //ENDLESS MODE
    public int initialWaveSize;
    private bool paused;
	private bool playerDead;
    private float timer;
    public static int pop;
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
        paused = false;
		playerDead = false;
        timer = 0;
        pop = 0;
        currentWave = 1;
        currentWaveSize = initialWaveSize;
		Pooling.Preload(knight, 5);
		Pooling.Preload(ogre, 5);
		Pooling.Preload(glider, 5);
    }

    void Update()
    {
		if(!paused)
			timer += Time.deltaTime;
		
        if (!paused && !playerDead && pop < currentWaveSize && timer > spawnFreq)
        {
            if(Random.Range(0, rareEnemyChance) == rareEnemyChance - 1) {
                if(Random.Range(0,2) == 0)
                {
                    Spawn(ogre, spawnPos);
                } else {
                    Vector2 gliderSpawn = spawnPos;
                    gliderSpawn.y += 4;
                    Spawn(glider, gliderSpawn);
                }
                
            } else {
                Spawn(knight,spawnPos);
            }
        }
    }

    private void Spawn(GameObject enemy, Vector2 spawnPos)
    {
		GameObject clone = (GameObject)Pooling.Spawn(enemy, spawnPos, Quaternion.identity);
		timer = 0;
        pop++;        
    }

    public void NextWave()
    {
        currentWave++;
        currentWaveSize *= 2;
        pop = 0;
        print("Wave " + currentWave + " starting!");
        if (OnNextWave != null)
            OnNextWave();
    }

	public void OnDestroyPlayer(){
		playerDead = true;
	}

    public void OnPause()
    {
        paused = !paused;
    }
}
