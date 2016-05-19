using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public delegate void DestroyEvent();
    public static event DestroyEvent OnDestroyPlayer;

    public GameStateManager gsm;

    [Header("Player Values")]
	public float speed; //Speed of the player

	private Rigidbody2D rb; //Rigidbody component
	private bool paused;
	private bool roundWin;

	[Header("Health Bar Values")]
	public float health;
	private float startHealth;
	public Slider healthBar;
	public Gradient healthColorGradient;
	public Image healthBarColor;
	private float healthPercent;
    private float timer;

	private GameObject damageText;

	#region Event Subscriptions
	void OnEnable(){
		GameStateManager.OnPause += OnPause;
		GameStateManager.OnRoundWin += OnRoundWin;
	}
	void OnDisable(){
		GameStateManager.OnPause -= OnPause;
		GameStateManager.OnRoundWin -= OnRoundWin;
	}
	void OnDestroy(){
		GameStateManager.OnPause -= OnPause;
		GameStateManager.OnRoundWin -= OnRoundWin;
	}
	#endregion

	// Use this for initialization
	void Start () {
		startHealth = health;
		healthPercent = health/startHealth;
		healthBar.maxValue = startHealth;
		UpdateHealthBar();
		rb = GetComponent<Rigidbody2D> ();
		damageText = (GameObject)Resources.Load("Action Texts/DamageText");
		paused = false;
        timer = 0;
	}

	void FixedUpdate () {
		if(!paused){
    		float moveHorizontal = Input.GetAxis ("Horizontal");
	    	float moveVertical = Input.GetAxis ("Vertical");
		    //Getting input

    		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
	    	//Getting a vector of where to move

    		rb.AddForce (movement * speed);
	    	//Actually moving the player by adding force to the rigidbody component
		}
	}

	void Update() {
        timer += Time.deltaTime;

        if (Input.GetKeyDown("p") && !roundWin)
        {
            gsm.PauseGame();
        }

        if (roundWin)
        {
            VictoryAnimation();
        }
	}

	private void UpdateHealthBar(){
		healthBar.value = health;
		healthPercent = health/healthBar.maxValue;
		healthBarColor.color = healthColorGradient.Evaluate(healthPercent);
	}

	public void OnRoundWin(){
		paused = !paused;
		roundWin = !roundWin;
	}

    public void OnPause(){
		paused = !paused;
	}

    public void Damage(float dmg)
    {
        health -= dmg;
		if(health <= 0){
			if(OnDestroyPlayer != null)
				OnDestroyPlayer();
			Destroy(gameObject);
			healthBarColor.enabled = false;
		}
		SpawnDamageText(dmg, transform.Find("Breath Point").position);
		UpdateHealthBar();
    }

	private void SpawnDamageText(float damageDealt, Vector3 shotPosition){
		Vector2 damageTextSpawn = shotPosition;
		damageTextSpawn = Camera.main.WorldToScreenPoint(damageTextSpawn);
		GameObject clone = (GameObject)Pooling.Spawn(damageText, damageTextSpawn, Quaternion.identity);
		clone.GetComponent<Text>().enabled = false;
		clone.transform.SetParent(GameObject.FindGameObjectWithTag("ScreenSpaceCanvas").transform); 
		clone.transform.SetAsFirstSibling();
		clone.GetComponent<Text>().CrossFadeAlpha(1, 0f, false);
		DamageTextMove cloneDTM = clone.GetComponent<DamageTextMove>();
		cloneDTM.movePosition = damageTextSpawn;
		cloneDTM.StartCoroutine(cloneDTM.UpdateTextDisplay(10, damageDealt));
		clone.GetComponent<Text>().enabled = true;

	}

    private void VictoryAnimation()
    {
        int animationInterval = 2;

        if (timer > animationInterval)
        {
            rb.AddForce(new Vector2(0, 20000), ForceMode2D.Force);
            timer = 0;
        }
    }
}