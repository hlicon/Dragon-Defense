using UnityEngine;
using System.Collections;

public class ShotClass : MonoBehaviour {

	public string shotName; //Name of the weapon
	public float damage; //Damage the weapon deals
	public float timeAlive;
	public bool canRoll; //Can it roll on the ground? (not sure what this will be fure quite yet)
	public bool canCollideWithShots; //Can it collide with other shots?
	public bool lobShot; //Will it lob, or be a straight shot?
	public int amountToFire = 1; //Amount of shots to fire (increased with upgrade system etc)
	public int angle; //Angle to be shot at
	public int power; //How fast/far the shot will be

	public void deleteObject(){
		Destroy(this.gameObject);
	}
}