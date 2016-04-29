using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyClass : MonoBehaviour {

    public delegate void DestroyEvent(float points);
    public static event DestroyEvent OnDestroyEnemy;

	[Header("Health Bar")]
    [SerializeField] protected float health;
	protected float startHealth;
	public Slider healthBar;
	public Gradient healthColorGradient;
	public Image healthBarColor;
	private float healthPercent;
	[Header("")]
    public float damage;	
    public float moveSpeed;
	public float startMoveSpeed;
	public float StartMoveSpeed{
		get{return startMoveSpeed;}
	}
	public Vector2 velocity;
    public float points;
    public bool isMelee;

    protected bool paused;
    protected bool wasPaused;
	protected Vector2 startSpawn;

	[Header("Action Texts")]
	public GameObject damageText;
	public GameObject pointText;

	#region Event Subscriptions
	void OnEnable(){
		GameStateManager.OnPause += OnPause;
		ShotClass.OnDamage += OnDamage;

	}
	void OnDisable(){
		GameStateManager.OnPause -= OnPause;
		ShotClass.OnDamage -= OnDamage;
	}
	void OnDestroy(){
		GameStateManager.OnPause -= OnPause;
		ShotClass.OnDamage -= OnDamage;
	}
	#endregion

    public void OnPause() 
    {
        paused = !paused;
        wasPaused = true;
    }

	public void OnDamage(float damageDealt, GameObject col, int weaponNumber, Vector3 shotPosition) //Used for taking damage
    {
		if(col == gameObject){
			health -= damageDealt;
			SpawnDamageText(damageDealt, col, weaponNumber, shotPosition);
			UpdateHealthBar();
		}
		if(health <= 0) {
            if(OnDestroyEnemy != null)
                OnDestroyEnemy(points);
			SpawnPointText(points);
			DespawnThis();
		}
    }

	void OnTriggerEnter2D(Collider2D col){
		if(col.name.Contains("Despawn")){
			TestSpawner.pop--;
			DespawnThis();
		}
	}

	private void DespawnThis(){
		ResetValues();
		UpdateHealthBar();
		Pooling.Despawn(gameObject);
	}

	public void ResetValues(){
		print("ResetValues() method successfully called."); //debug
		moveSpeed = startMoveSpeed; //FUCKING WORK YOU ASS
		print (moveSpeed); //debug
		health = startHealth;
		transform.position = startSpawn;
	}

	protected void UpdateHealthBar(){
		healthBar.value = health;
		healthPercent = health/healthBar.maxValue;
		healthBarColor.color = healthColorGradient.Evaluate(healthPercent);
	}

	private void SpawnPointText(float pointValue){
		Vector2 pointTextSpawn = transform.position;
		pointTextSpawn.y += GetComponent<SpriteRenderer>().sprite.bounds.size.y/2;
		pointTextSpawn = Camera.main.WorldToScreenPoint(pointTextSpawn);
		GameObject clone = (GameObject)Pooling.Spawn(pointText, pointTextSpawn, Quaternion.identity);
		clone.transform.SetParent(GameObject.FindGameObjectWithTag("ScreenSpaceCanvas").transform);
		clone.transform.SetAsFirstSibling();
		clone.GetComponent<Text>().CrossFadeAlpha(1, 0f, false);
		PointTextMove clonePTM = clone.GetComponent<PointTextMove>();
		clonePTM.pointDisplay = pointValue;
		clonePTM.movePosition = pointTextSpawn;
	}

	private void SpawnDamageText(float damageDealt, GameObject col, int weaponNumber, Vector3 shotPosition){
		Vector2 damageTextSpawn = shotPosition;
		damageTextSpawn = Camera.main.WorldToScreenPoint(damageTextSpawn);
		GameObject clone = (GameObject)Pooling.Spawn(damageText, damageTextSpawn, Quaternion.identity);
		clone.transform.SetParent(GameObject.FindGameObjectWithTag("ScreenSpaceCanvas").transform); 
		clone.transform.SetAsFirstSibling();
		clone.GetComponent<Text>().CrossFadeAlpha(1, 0f, false);
		DamageTextMove cloneDTM = clone.GetComponent<DamageTextMove>();
		cloneDTM.colorNumber = weaponNumber;
		cloneDTM.damageDealt = damageDealt;
		cloneDTM.movePosition = damageTextSpawn;
	}

}
