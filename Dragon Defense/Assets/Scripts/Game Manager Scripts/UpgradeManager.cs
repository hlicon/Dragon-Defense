using UnityEngine;
using System.Collections;

public class UpgradeManager : MonoBehaviour {

	public GameObject weapon;
	public GameObject subweapon;

	private ShotClass weaponShotClass;
	private ShotClass subweaponShotClass;

	int damageLevel;
	int splitLevel;
	int affectLevel;

	private float startDamageMultiplier;
	private float startAffectDamageMultiplier;
	private int startSplitAmount;
	private float startTimeMultiplier;
	private float startAffectHitRate;

	void Start(){
		damageLevel = 0;
		splitLevel = 0;
		affectLevel = 0;
		weaponShotClass = weapon.GetComponent<ShotClass>(); //Getting weapon shot class
		if(subweapon != null)
			subweaponShotClass = subweapon.GetComponent<ShotClass>(); //Getting subweapon
		//shot class if there is one. Like stalagmites/lava
		SetStartValues();
	}

	private void SetStartValues(){ //Setting start values to reset to when game is turned off
		startDamageMultiplier = weaponShotClass.damageMultiplier;
		startAffectDamageMultiplier = weaponShotClass.affectDamageMultiplier;
		startSplitAmount = weaponShotClass.amountToFire;
		startTimeMultiplier = weaponShotClass.affectTimeMultiplier;
		startAffectHitRate = weaponShotClass.affectHitRate;
		if(subweapon != null){
			startAffectDamageMultiplier = subweaponShotClass.affectDamageMultiplier;
			startDamageMultiplier = subweaponShotClass.damageMultiplier;
			startTimeMultiplier = subweaponShotClass.affectTimeMultiplier;
			startAffectHitRate = subweaponShotClass.affectHitRate;
		}
	}

	public void UpgradeWeapon(string type, int upgradeNumber){ //Not sure if we need parameters like this.. Might use later
		if(subweapon == null){
		switch(type){ //Single weapon damages
		case "damage": weaponShotClass.affectDamageMultiplier += startAffectDamageMultiplier * .5f;
				weaponShotClass.damageMultiplier += startDamageMultiplier * .5f; damageLevel+=1; break;
		case "split": weaponShotClass.amountToFire += 1; weaponShotClass.damageMultiplier -= startDamageMultiplier * .25f;
				splitLevel+=1; break;
		case "affect": weaponShotClass.affectTimeMultiplier += startTimeMultiplier *.5f;
				weaponShotClass.affectHitRate -= startAffectHitRate * .25f; affectLevel+=1; break;
		}
		} else {
			switch(type){ //Subweapon damages
			case "damage": subweaponShotClass.affectDamageMultiplier += startAffectDamageMultiplier * .5f;
				subweaponShotClass.damageMultiplier += startDamageMultiplier * .5f; damageLevel+=1; break;
			case "split": weaponShotClass.amountToFire += 1; subweaponShotClass.damageMultiplier -= startDamageMultiplier * .25f; 
				splitLevel+=1; break;
			case "affect": subweaponShotClass.affectTimeMultiplier += startTimeMultiplier *.5f;
				subweaponShotClass.affectHitRate += startAffectHitRate * .5f;  affectLevel+=1; break;
			}
		}
	}

	public bool UpgradeCheck(string type, int upgradeNumber){ //Checking if we can buy upgrade based on previous purchase
		bool canUpgrade = false;
		switch(type){
		case "damage": if(upgradeNumber-1 == damageLevel){canUpgrade = true;} break;
		case "split": if(upgradeNumber-1 == splitLevel){canUpgrade = true;} break;
		case "affect": if(upgradeNumber-1 == affectLevel){canUpgrade = true;} break;
		}
		return canUpgrade;
	}

	public void ResetPrefab(){ //Resetting prefab to "before" upgrades
		if(subweapon == null && weaponShotClass !=null){
			weaponShotClass.affectDamageMultiplier = startAffectDamageMultiplier;
			weaponShotClass.damageMultiplier = startDamageMultiplier;
			weaponShotClass.affectHitRate = startAffectHitRate;
			weaponShotClass.affectTimeMultiplier = startTimeMultiplier;
			weaponShotClass.amountToFire = startSplitAmount;
		} else if(subweapon != null && weaponShotClass != null) {
			weaponShotClass.amountToFire = startSplitAmount;
			subweaponShotClass.affectDamageMultiplier = startAffectDamageMultiplier;
			subweaponShotClass.damageMultiplier = startDamageMultiplier;
			subweaponShotClass.affectHitRate = startAffectHitRate;
			subweaponShotClass.affectTimeMultiplier = startTimeMultiplier;
		}
	}

}
