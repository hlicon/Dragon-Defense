using UnityEngine;
using System.Collections;

public class ClickShoot : MonoBehaviour {

	private Vector2 mouseClickPosition;
	private bool canShoot;
	private int selection = 0;
	private float power;
	private float angle;

	public Transform shotSpawn;
	private Vector2 shotSpawnPos;

	[Header("Weapons")]
	public GameObject[] Shots;

	void Start(){
		canShoot = true;
		shotSpawnPos = shotSpawn.transform.position;
	}

	void Update(){
		GetSelection();
		if(Input.GetMouseButton(0)){
			mouseClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if(canShoot && selection >= 0){
				CalculateShot();
			}
		} 
	}
		
	private void CalculateShot(){
		shotSpawnPos = shotSpawn.transform.position;
		Debug.DrawLine(shotSpawnPos, mouseClickPosition, Color.red);

		Vector2 Dir = mouseClickPosition - shotSpawnPos;

		if(Dir.x > 4f)
			Dir.x = 4f;
		if(Dir.y > 4f)
			Dir.y = 4f;

		FireWeapon(Dir);
		print(Dir);
	}

	private void FireWeapon(Vector2 Dir){
		canShoot = false;
		Vector2 shotSpawnPos = shotSpawn.position;
		GameObject clone = (GameObject)Instantiate(Shots[selection], shotSpawnPos, Quaternion.identity);
		ShotClass cloneShotClass = clone.GetComponent<ShotClass>();
		clone.GetComponent<Rigidbody2D>().AddForce(Dir + Dir, ForceMode2D.Impulse);

		StartCoroutine(ShotWait());
	}

	private IEnumerator ShotWait(){
		yield return new WaitForSeconds(.5f);
		canShoot = true;
	}

	private void GetSelection(){
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			selection = 0;
		}
	}

}
