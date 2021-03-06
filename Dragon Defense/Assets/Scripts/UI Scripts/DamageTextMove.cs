﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageTextMove : MonoBehaviour {

	public Color[] colors;
	public float damageDealt;
	[HideInInspector]
	public int colorNumber = 0;
	[HideInInspector]
	public Vector2 movePosition;
	private Vector3 rotation;
	private float moveUpAmount = 1.5f;
	private float startMoveUpAmount;
	private float moveLeftRightAmount;
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
		rotation = Vector3.zero;
		movePosition = transform.position;
		randXMove = Random.Range(0, 2);
		randZRotate = Random.Range(0, 2);
		rotateAmount = Random.Range(5f, 7.5f) + Random.value;
		moveLeftRightAmount += Random.value + Random.value;

		startMoveUpAmount = moveUpAmount;
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
					Pooling.Despawn(gameObject);
					timedDestroy = 0;
					moveUpAmount = startMoveUpAmount;
					moveLeftRightAmount = Random.value + Random.value;
					rotation.z = 0;
				}
			}
		}
	}

	public IEnumerator UpdateTextDisplay(int number, float damage){
		yield return new WaitForEndOfFrame();
		text.color = colors[number];
		text.text = damage.ToString();
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
