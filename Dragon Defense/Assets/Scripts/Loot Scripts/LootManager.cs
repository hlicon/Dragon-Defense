using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LootManager : MonoBehaviour {
	public RectTransform panel;

	static private Stack<GameObject> lootList;
	static private GameObject[] lootTypeList;

	[SerializeField] private GameObject loot;
	[SerializeField] private GameObject contentPanel;

	[SerializeField] private ContentSizeFitter menuFitter;
	[SerializeField] private GameObject menuScrollbar;

	const float LOOT_CHANCE = 60; //will alter later
	static Vector2 lootSpawn = new Vector2(9999, 9999);

	#region Event Subscriptions
	void OnEnable()
	{
		EnemyClass.OnDestroyEnemy += OnDestroyEnemy;
		TestSpawner.OnNextWave += OnNextWave;
		ScoreUpdate.OnSellItem += OnSellItem;
	}

	void OnDisable()
	{
		EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;
		TestSpawner.OnNextWave -= OnNextWave;
		ScoreUpdate.OnSellItem -= OnSellItem;
	}

	void OnDestroy()
	{
		EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;
		TestSpawner.OnNextWave -= OnNextWave;
		ScoreUpdate.OnSellItem -= OnSellItem;
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

	public void OnSellItem(){
		if(menuScrollbar.activeSelf == false){
			menuFitter.enabled = false;
		}
	}

	public void DisplayLoot() {
		if(lootList.Count > 6)
			menuFitter.enabled = true;
		else 
			menuFitter.enabled = false;
		
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
			Destroy(child.gameObject);
		}
	}
}