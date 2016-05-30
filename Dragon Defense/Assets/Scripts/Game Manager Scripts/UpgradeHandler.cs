using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour {

	// Buttons that have the "UpgradeHandler" script work via putting the button
	// itself in it's own "OnClick()" method. In the 'function' dropdown, select
	// 'UpgradeHandler' and look for 'CheckUpgrade()'

	public int upgradeCost;
	public int upgradeLevel; //Level of the specific upgrade.. 1, 2, or 3.
	public string upgradeType; //Type of the specific upgrade.. damage, split, or affect.
	[Header("Images")]
	public Image[] upgradeLines; //Which lines are connected to this upgrade slot?
	public Image upgradeImage; //The background of the upgrade image
	[Header("New UI")]
	public Sprite newUpgrade; //blue (pressed) button image to change 'upgradeImage' to
	private Color lineColor; // What color the upgrade line images will change to
	//Upon successful purchase

	private UpgradeManager upgradeManager;
	private ScoreUpdate scoreUpdate;

	void Start(){
		scoreUpdate = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreUpdate>();
		upgradeManager = transform.parent.parent.parent.GetComponent<UpgradeManager>();
		//Getting UpgradeManager from 'weapon name Upgrades panel' lol parent.parent.parent
		SetLineColor();
	}

	private void SetLineColor(){
		lineColor = new Color();
		ColorUtility.TryParseHtmlString("#35738AFF", out lineColor);
		//Setting line color to the same color as the blue (pressed) button color
	}

	public void CheckUpgrade(){
		if(upgradeManager.UpgradeCheck(upgradeType, upgradeLevel)){ //Is our current upgrade level high enough?
			if(scoreUpdate.BuyItem(upgradeCost)) { //If so, do we have enough money?
			for(int i = 0; i < upgradeLines.Length; i++){
				upgradeLines[i].color = lineColor; //Changing line colors
			}
			upgradeManager.UpgradeWeapon(upgradeType, upgradeLevel);
			upgradeImage.overrideSprite = newUpgrade; //Changing upgrade image to blue (pressed) button image
			gameObject.SetActive(false); //Turning lock image/cost text off
			}
		}
	}

}
