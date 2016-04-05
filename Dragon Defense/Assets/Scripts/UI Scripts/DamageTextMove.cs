using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageTextMove : MonoBehaviour {

	public Color[] colors;
	public float damageDealt;
	[HideInInspector]
	public int colorNumber;
	private Vector2 movePosition;
	private Vector3 rotation;
	private float moveUpAmount = 1.5f;
	private float moveLeftRightAmount = Random.value + Random.value;
	private float rotateAmount;
	private Text text;
	private float timedDestroy = 0;
	private bool paused;
	private int randXMove, randZRotate;

	#region Event Subscriptions
	void OnEnable(){
		GameStateManager.OnPause += OnPause;
	}
	void OnDisable(){
		GameStateManager.OnPause -= OnPause;
	}
	void OnDestroy(){
		GameStateManager.OnPause -= OnPause;
	}
	#endregion

	void Start(){
		text = GetComponent<Text>();
		text.color = colors[colorNumber];
		rotation = Vector3.zero;
		movePosition = transform.position;
		text.text = damageDealt.ToString();
		randXMove = Random.Range(0, 2);
		randZRotate = Random.Range(0, 2);
		rotateAmount = Random.Range(5f, 7.5f) + Random.value;
	}

	void FixedUpdate(){
		if(!paused){
			if(moveUpAmount >= .35f)
				MoveLeftRight();
				RotateLeftRight();
			if(moveUpAmount >= -.1f){
				movePosition.y += moveUpAmount;
				transform.position = movePosition;
				moveUpAmount -= moveUpAmount/50;
			}
			if(moveUpAmount <= .5f){
				timedDestroy += Time.fixedDeltaTime;
				text.CrossFadeAlpha(0f, .25f, false);
				if(timedDestroy >= .4f){
					Destroy(gameObject);
				}
			}
		}
	}

	private void RotateLeftRight(){
		if(Mathf.Abs(rotation.z) < 360f){
		if(randZRotate == 0){
				rotation.z += rotateAmount;
		} else {
				rotation.z -= rotateAmount;
		}
		}
		transform.rotation = Quaternion.Euler(rotation);
	}

	private void MoveLeftRight(){
		if(randXMove == 0){
			movePosition.x -= moveLeftRightAmount;
		} else {
			movePosition.x += moveLeftRightAmount;
		}
		moveLeftRightAmount -= moveLeftRightAmount/50;
	}

	void OnPause(){
		paused = !paused;
	}
}
