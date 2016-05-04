using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponCoolDownUI : MonoBehaviour {

	private Image CDImage;
	public float cooldown;
	public float startCooldown;
	private float fillAmount;
	public bool hasWeapon;

	void Start(){
		CDImage = transform.Find("Image - Cooldown").GetComponent<Image>();

		if (transform.Find ("Image - Weapon").GetComponent<Image>().sprite == null) {
			hasWeapon = false;
		} else {
			hasWeapon = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (hasWeapon) {
			fillAmount = cooldown / startCooldown;
			CDImage.fillAmount = fillAmount;
		}
	}
}
