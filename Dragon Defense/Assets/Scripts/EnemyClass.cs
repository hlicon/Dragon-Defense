using UnityEngine;
using System.Collections;

public class EnemyClass : MonoBehaviour {

    public delegate void DestroyEvent(float points);
    public static event DestroyEvent OnDestroyEnemy;

    [SerializeField] protected float health;
    public float damage; 
	public Vector2 velocity;
    public float points;
    public bool isMelee;

    protected bool paused;
    protected bool wasPaused;

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

    public void DeleteObject() //Used for deleting the object upon death
    {
        Destroy(this.gameObject);
    }

    public void OnPause() 
    {
        paused = !paused;
        wasPaused = true;
    }

	public void OnDamage(float damageDealt, GameObject col) //Used for taking damage
    {
		if(col == this.gameObject){
			health -= damageDealt;
			SpawnDamageText(damageDealt);
		}
		if(health <= 0) {
            if(OnDestroyEnemy != null)
                OnDestroyEnemy(points);
			DeleteObject();

		}
    }

	private void SpawnDamageText(float damageDealt){
		Vector2 damageTextSpawn = transform.position;
		damageTextSpawn.y += GetComponent<SpriteRenderer>().sprite.bounds.size.y/2;
		damageTextSpawn = Camera.main.WorldToScreenPoint(damageTextSpawn);
		GameObject clone = (GameObject)Instantiate(Resources.Load("DamageText"), damageTextSpawn, Quaternion.identity);
		clone.transform.SetParent(GameObject.FindGameObjectWithTag("ScreenSpaceCanvas").transform); 
		clone.transform.SetAsFirstSibling();
		clone.GetComponent<DamageTextMove>().damageDealt = damageDealt;
		print(damageDealt);
	}

}
