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

	protected bool slowed;
	private float slowedValue;
	protected bool fired;
	private Vector4 firedValues;
	protected bool poisoned;
	private Vector4 poisonedValues;
	protected bool stunned;
	private float stunnedValue;
	protected SpriteRenderer spriteRend;


	[Header("Action Texts")]
	public GameObject damageText;
	public GameObject pointText;

	[Header("Affect Particles")]
	public ParticleSystem slowedParticles;
	public ParticleSystem firedParticles;
	public ParticleSystem stunnedParticles;
	public ParticleSystem poisonedParticles;

	#region Event Subscriptions
	void OnEnable(){
		GameStateManager.OnPause += OnPause;
		ShotClass.OnDamage += OnDamage;
		ShotClass.OnAffect += OnAffect;
	}
	void OnDisable(){
		GameStateManager.OnPause -= OnPause;
		ShotClass.OnDamage -= OnDamage;
		ShotClass.OnAffect -= OnAffect;
	}
	void OnDestroy(){
		GameStateManager.OnPause -= OnPause;
		ShotClass.OnDamage -= OnDamage;
		ShotClass.OnAffect -= OnAffect;
	}
	#endregion

    public void OnPause() 
    {
        paused = !paused;
        wasPaused = true;
		ParticleToggle();
    }

	public void OnDamage(float damageDealt, GameObject col, int weaponNumber, Vector3 shotPosition) //Used for taking damage
    {
		if(col == gameObject){
			health -= damageDealt;
			SpawnDamageText(damageDealt, weaponNumber, shotPosition);
			UpdateHealthBar();
		}
		if(health <= 0) {
            if(OnDestroyEnemy != null)
                OnDestroyEnemy(points);
			SpawnPointText(points);
			DespawnThis();
		}
    }

	public void OnAffect(GameObject col, string affectType, float affectDamage, float affectTime, float affectHitRate){
		if(col.gameObject == gameObject){
			if(affectType.Contains("Fire")){ firedValues = FireDamage(affectDamage, affectTime, 0, affectHitRate);
				fired = true; firedParticles.Play();}
			
			if(affectType.Contains("Slow")){ slowedValue = SlowDown(affectTime);
				slowed = true; slowedParticles.Play();}
			
			if(affectType.Contains("Stun")){ stunnedValue = DoStun(affectTime);
				stunned = true; stunnedParticles.Play(); }
			
			if(affectType.Contains("Poison")) { poisonedValues = PoisonDamage(affectDamage, affectTime, 0, affectHitRate);
				poisoned = true; poisonedParticles.Play();}
		}
	}

	void LateUpdate(){
		if(!paused){
		CheckAffects();
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.name.Contains("Despawn")){
			TestSpawner.pop--;
			DespawnThis();
		}
	}

	private void ParticleToggle(){
		if(!paused){
			if(slowedParticles.isPaused && slowed)
				slowedParticles.Play();
			if(firedParticles.isPaused && fired)
				firedParticles.Play();
			if(stunnedParticles.isPaused && stunned)
				stunnedParticles.Play();
			if(poisonedParticles.isPaused && poisoned)
				poisonedParticles.Play();
		}

		if(paused){
			slowedParticles.Pause();
			firedParticles.Pause();
			stunnedParticles.Pause();
			poisonedParticles.Pause();
		}
	}

	private void DespawnThis(){
		ResetValues();
		UpdateHealthBar();
		Pooling.Despawn(gameObject);
	}

	public void ResetValues(){
		moveSpeed = startMoveSpeed;
		health = startHealth;
		transform.position = startSpawn;
		slowed = false;
		fired = false;
		poisoned = false;
		stunned = false;
		spriteRend.color = Color.white;
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

	private void SpawnDamageText(float damageDealt, int weaponNumber, Vector3 shotPosition){
		Vector2 damageTextSpawn = shotPosition;
		damageTextSpawn = Camera.main.WorldToScreenPoint(damageTextSpawn);
		GameObject clone = (GameObject)Pooling.Spawn(damageText, damageTextSpawn, Quaternion.identity);
		clone.GetComponent<Text>().enabled = false;
		clone.transform.SetParent(GameObject.FindGameObjectWithTag("ScreenSpaceCanvas").transform); 
		clone.transform.SetAsFirstSibling();
		clone.GetComponent<Text>().CrossFadeAlpha(1, 0f, false);
		DamageTextMove cloneDTM = clone.GetComponent<DamageTextMove>();
		cloneDTM.movePosition = damageTextSpawn;
		cloneDTM.StartCoroutine(cloneDTM.UpdateTextDisplay(weaponNumber, damageDealt));
		clone.GetComponent<Text>().enabled = true;

	}

	private void CheckAffects(){
		if(slowed){
			slowedValue = SlowDown(slowedValue);
		}
		if(fired){
			firedValues = FireDamage(firedValues.x, firedValues.y, firedValues.z, firedValues.w);
		}
		if(stunned){
			stunnedValue = DoStun(stunnedValue);
		}
		if(poisoned){
			poisonedValues = PoisonDamage(poisonedValues.x, poisonedValues.y, poisonedValues.z, poisonedValues.w);
		}
	}

	private float SlowDown(float slowTime){
		if(spriteRend.color == Color.white){
			spriteRend.color = Color.cyan;
		}
		if(slowTime >= 0 && !stunned){
			moveSpeed = startMoveSpeed/2;
		}
		slowTime -= Time.deltaTime;
		if(slowTime < 0){
			slowed = false;
			spriteRend.color = Color.white;
			moveSpeed = startMoveSpeed;
			slowedParticles.Stop();
		}
		return slowTime;
	}

	private Vector4 FireDamage(float affectDamage, float affectTime, float attackRate, float affectHitRate){
		attackRate += Time.deltaTime;
		if(spriteRend.color == Color.white){
			spriteRend.color = Color.red;
		}
		if(attackRate >= affectHitRate){
			health -= affectDamage;
			UpdateHealthBar();
			Vector2 damageTextSpawn = transform.position;
			damageTextSpawn.y += GetComponent<SpriteRenderer>().sprite.bounds.size.y/2;
			SpawnDamageText(affectDamage, 0, damageTextSpawn);
			attackRate = 0;
			if(health <= 0){
				if(OnDestroyEnemy != null){
					OnDestroyEnemy(points);
					SpawnPointText(points);
					DespawnThis();
				}
			}
		}

		affectTime -= Time.deltaTime;
		if(affectTime <= 0){
			fired = false;
			spriteRend.color = Color.white;
			firedParticles.Stop();
		}
		return new Vector4(affectDamage, affectTime, attackRate, affectHitRate);
	}

	private Vector4 PoisonDamage(float affectDamage, float affectTime, float attackRate, float affectHitRate){
		attackRate += Time.deltaTime;
		if(spriteRend.color == Color.white){
			spriteRend.color = Color.green;
		}
		if(attackRate >= affectHitRate){
			health -= affectDamage;
			UpdateHealthBar();
			Vector2 damageTextSpawn = transform.position;
			damageTextSpawn.y += GetComponent<SpriteRenderer>().sprite.bounds.size.y/2;
			SpawnDamageText(affectDamage, 2, damageTextSpawn);
			attackRate = 0;
			if(health <= 0){
				if(OnDestroyEnemy != null){
					OnDestroyEnemy(points);
					SpawnPointText(points);
					DespawnThis();
				}
			}
		}

		affectTime -= Time.deltaTime;
		if(affectTime <= 0){
			poisoned = false;
			spriteRend.color = Color.white;
			poisonedParticles.Stop();
		}
		return new Vector4(affectDamage, affectTime, attackRate, affectHitRate);
	}

	private float DoStun(float stunTime){
		if(spriteRend.color == Color.white){
			spriteRend.color = Color.yellow;
		}
		if(stunTime >= 0){
			moveSpeed -= Time.deltaTime * 5;
			if(moveSpeed <= 0)
				moveSpeed = 0;
		}
		stunTime -=Time.deltaTime;
		if(stunTime < 0){
			stunned = false;
			spriteRend.color = Color.white;
			moveSpeed = startMoveSpeed;
			stunnedParticles.Stop();
		}
		return stunTime;
	}
}