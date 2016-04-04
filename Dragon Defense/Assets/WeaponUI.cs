using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour {

	public static int currentSelection = 0;

	public GameObject[] weaponSlots;
	#region Event Subscriptions
	void OnEnable(){
		ClickShoot.OnSelectionChanged += OnSelectionChanged;
	}
	void OnDisable(){
		ClickShoot.OnSelectionChanged -= OnSelectionChanged;
	}
	void OnDestroy(){
		ClickShoot.OnSelectionChanged -= OnSelectionChanged;
	}
	#endregion



	void OnSelectionChanged(int selection){
		currentSelection = selection;

		for(int i = 0; i < weaponSlots.Length; i++){
			if(currentSelection == i)
				weaponSlots[currentSelection].GetComponent<Button>().interactable = false;
			else
				weaponSlots[i].GetComponent<Button>().interactable = true;
		}
	}
}
