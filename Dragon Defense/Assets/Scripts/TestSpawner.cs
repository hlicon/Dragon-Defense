using UnityEngine;
using System.Collections;

public class TestSpawner : MonoBehaviour {
    public float spawnFreq; //used for time between spawns
    public GameObject knight;
    public Vector2 spawnPos;

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn());
	}

    IEnumerator Spawn()
    {
        while(true)
        {
            Instantiate(knight, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnFreq);
        }
    }
}
