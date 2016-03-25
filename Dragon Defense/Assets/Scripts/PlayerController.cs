using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public delegate void DestroyEvent(float score);
    public static event DestroyEvent OnDestroyPlayer;

    [Header("Player Values")]
	public float speed; //Speed of the player
    public float health;

	private Rigidbody2D rb; //Rigidbody component
	private bool paused;

	#region Event Subscriptions
	void OnEnable(){
		GameStateManager.OnPause += OnPause;
	}
	void OnDisable(){
		GameStateManager.OnPause -= OnPause;	
	}
	void OnDestroy(){
		GameStateManager.OnPause -= OnPause;
	}
	#endregion

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		paused = false;
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
        if(health <= 0)
        {
            if(OnDestroyPlayer != null)
            {
                OnDestroyPlayer(0.0f); //come back to, temporary passed value
            }
            Destroy(this.gameObject);
        }
	}

    public void OnPause(){
		paused = !paused;
	}

    public void Damage(float dmg)
    {
        health -= dmg;
    }
}