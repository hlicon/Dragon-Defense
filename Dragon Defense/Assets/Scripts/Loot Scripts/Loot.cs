using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour {
	private string lootName;
	public string LootName {
		get { return lootName; }
	}
	private static string[] lootTypeList = { "sushi", "Deep Impact on HD-DVD", "broken rake", "underwear", "dakimakura",
	"empty chest", "Betamax Player"};

	// Use this for initialization
	void Start () {
		RandomizeLootName ();
	}

	public void RandomizeLootName() {
		int lootNo = Random.Range (0, lootTypeList.Length);
		lootName = lootTypeList [lootNo];
	}
}
