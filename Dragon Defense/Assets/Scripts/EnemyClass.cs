using UnityEngine;
using System.Collections;

public class EnemyClass : MonoBehaviour {

    public float health;
    public float damage; //Currently unused
    public float velocity;

    protected bool paused;
    protected bool wasPaused;

    public void DeleteObject() //Used for deleting the object upon death
    {
        Destroy(this.gameObject);
    }

    /*public void OnPause() //Currently nonfunctional for enemies
    {
        paused = !paused;
        wasPaused = true;
    }*/

    public void DamageEnemy(float dmg) //Used for taking damage
    {
        this.health -= dmg;
    }

}
