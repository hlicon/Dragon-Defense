using UnityEngine;
using System.Collections;

public class ShotClass : MonoBehaviour {

	public string shotName;
	public float damage;
	public bool canRoll;
	public bool canCollideWithShots;
	public bool lobShot;
	public bool straightShot;
	public int amountToFire = 1;
	public int angle;
	public int power;
}