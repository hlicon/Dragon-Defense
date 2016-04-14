using UnityEngine;
using System.Collections;

public class Despawner : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
		if (col.GetComponent<EnemyClass>() != null) { //come back to, probably a better way to do this        
            Destroy(col.gameObject);
            TestSpawner.pop--;
        }
    }
}