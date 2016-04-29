using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootManager : MonoBehaviour {
	private Queue<GameObject> lootList;

	// Use this for initialization
	void Start () {
		lootList = new Queue<GameObject>();
	}
	
	public void AddLoot (GameObject loot) {
		lootList.Enqueue (loot);
	}

	public GameObject PeekLoot() {
		return lootList.Peek ();
	}

	public GameObject ReturnLoot() {
		return lootList.Dequeue ();
	}

	public void ClearLoot() {
		lootList.Clear ();
	}
}
