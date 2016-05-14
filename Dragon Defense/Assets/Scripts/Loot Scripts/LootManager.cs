using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LootManager : MonoBehaviour {
	public RectTransform panel;

	static private Stack<GameObject> lootList;
	static private GameObject[] lootTypeList;

	public delegate void Event(float value);
	public static event Event OnSellItem;

	[SerializeField] private GameObject loot;
	[SerializeField] private GameObject contentPanel;
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
		TestSpawner.OnNextWave -= OnNextWave;
	}

	void OnDestroy()
	{
		EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;
		TestSpawner.OnNextWave -= OnNextWave;
	}
	#endregion

	void Start () {
		lootList = new Stack<GameObject>();
		lootTypeList = new GameObject[Loot.LootTypeList.Length];

		for (int i = 0; i < Loot.LootTypeList.Length; ++i) {
			lootTypeList [i] = Instantiate (loot, new Vector2 (99999, 99999), Quaternion.identity) as GameObject;
			lootTypeList [i].GetComponent<Loot> ().SetLootType (i);
			lootTypeList [i].GetComponent<Loot> ().GetComponent<Text> ().text = lootTypeList [i].GetComponent<Loot> ().LootName;
		}
	}
	
	public void AddLoot (GameObject newLoot) {
		lootList.Push (newLoot);
	}

	public void ClearLoot() {
		lootList.Clear ();
	}

	public void OnDestroyEnemy(float points) { //will make loot conditional later
		lootList.Push(lootTypeList[Random.Range((int)0, (int)lootTypeList.Length)]);
		print (lootList.Peek ().GetComponent<Loot> ().LootName);
	}

	public void DisplayLoot() {
		while (lootList.Count > 0) {
			Loot temp = lootList.Peek ().GetComponent<Loot> ();
			string lootName = temp.LootName;
			int lootVal = temp.LootVal;

			GameObject nextItem = Instantiate(lootList.Pop ()) as GameObject;

			temp = nextItem.GetComponent<Loot> ();

			temp.SetLootName (lootName);
			temp.SetLootVal (lootVal);

			nextItem.transform.SetParent (panel, false);
		}
	}

	public void OnNextWave() {
		lootList.Clear ();

		foreach (Transform child in contentPanel.transform) {
			GameObject.Destroy (child.gameObject);
		}
	}
}