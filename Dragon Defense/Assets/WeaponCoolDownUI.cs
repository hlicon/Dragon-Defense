using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponCoolDownUI : MonoBehaviour {

	private Image CDImage;
	public float cooldown;
	public float startCooldown;
	private float fillAmount;

	void Start(){
		CDImage = transform.Find("Image - Cooldown").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		fillAmount = cooldown/startCooldown;
		CDImage.fillAmount = fillAmount;
	}
}
