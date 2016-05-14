using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Loot : MonoBehaviour {
	private ScoreUpdate scoreUpdateObject;
	private string lootName;
	public string LootName {
		get { return lootName; }
	}
	private int lootVal;
	public int LootVal {
		get { return lootVal; }
	}
	private static string[] lootTypeList = { "sushi", "Deep Impact on HD-DVD", "weird underwear", "dakimakura",
		"empty chest", "liquid swords (weapon)", "Liquid Swords (album)" };
	public static string[] LootTypeList {
		get { return lootTypeList; }
	}
	private static int[] lootValueList = { 5, 4, 2, 3, 4, 2, 10 };


	void Start () {
		//RandomizeLootName ();
		// = GetComponent<GameStateManager>().GetComponent<ScoreUpdate>();
		scoreUpdateObject = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreUpdate>();
	}

	public void RandomizeLootName() {
		int lootNo = Random.Range (0, lootTypeList.Length - 1);
		SetLootType (lootNo);
	}

	public void SetLootType(int lootNo) { 
		if (lootNo >= 0 && lootNo < lootTypeList.Length) {
			lootName = lootTypeList [lootNo];
			lootVal = lootValueList [lootNo];
			GetComponent<Text> ().text = lootName;
		}
	}

	public void PrintLootName() { //debug
		print(lootName);
	}

	public void SetLootName(string name) {
		lootName = name;
	}

	public void SetLootVal(int val) {
		lootVal = val;
	}

	public void SellItem() {
		scoreUpdateObject.SellItem (lootVal);
		Destroy (this.gameObject);
	}
}
