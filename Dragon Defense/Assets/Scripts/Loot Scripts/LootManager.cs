using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootManager : MonoBehaviour {
	static private Queue<GameObject> lootList;
	[SerializeField] private GameObject loot;
	const float LOOT_CHANCE = 60; //will alter later
	static Vector2 lootSpawn = new Vector2(9999, 9999);

	#region Event Subscriptions
	void OnEnable()
	{
		EnemyClass.OnDestroyEnemy += OnDestroyEnemy;
		TestSpawner.OnNextWave += OnNextWave;
	}

	void OnDisable()
	{
		EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;
	}

	void OnDestroy()
	{
		EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;
	}
	#endregion

	void Start () {
		lootList = new Queue<GameObject>();
		Pooling.Preload (loot, 10); //temporary
	}
	
	public void AddLoot (GameObject newLoot) {
		lootList.Enqueue (newLoot);
	}

	public GameObject PeekLoot() {
		GameObject currentLoot = lootList.Peek ();

//		print ("You looted " + currentLoot.GetComponent<Loot> ().LootName);
		return currentLoot;
	}

	public GameObject ReturnLoot() {
		return lootList.Dequeue ();
	}

	public void ClearLoot() {
		lootList.Clear ();
	}

	public void OnDestroyEnemy(float points) { //will make loot conditional later
		GameObject newLoot = new GameObject();
//		newLoot.GetComponent<Loot> ().RandomizeLootName ();
		AddLoot (newLoot);
		PeekLoot ();
	}

	void OnNextWave ()
	{
		lootList.Clear ();
	}
}
