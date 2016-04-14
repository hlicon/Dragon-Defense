using UnityEngine;
using System.Collections;

public class TestSpawner : MonoBehaviour
{
    public delegate void Event();
    public static event Event OnNextWave;

    public float spawnFreq; //used for time between spawns
    public GameObject knight;
    public GameObject ogre;
    public GameObject glider;
    public Vector2 spawnPos;
    public int rareEnemyChance;
    //public int numWaves; //ENDLESS MODE
    public int initialWaveSize;
    private bool pause;
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
        pause = false;
		playerDead = false;
        timer = 0;
        pop = 0;
        currentWave = 1;
        currentWaveSize = initialWaveSize;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!pause && !playerDead && pop < currentWaveSize && timer > spawnFreq)
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
        Instantiate(enemy, spawnPos, Quaternion.identity);
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
        pause = !pause;
    }
}
