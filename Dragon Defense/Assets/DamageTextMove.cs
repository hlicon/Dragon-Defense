using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageTextMove : MonoBehaviour {

	public Color[] colors;
	public float damageDealt;
	private Vector2 movePosition;
	private float moveAmount = 2f;
	private Text text;
	private float timedDestroy = 0;
	private bool paused;

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
		text.color = colors[WeaponUI.currentSelection];
		movePosition = transform.position;
		text.text = damageDealt.ToString();
	}

	void FixedUpdate(){
		if(!paused){
		if(moveAmount >= -.1f){
		movePosition.y += moveAmount;
		transform.position = movePosition;
		moveAmount -= .025f;
		}
		if(moveAmount <= .5f){
			timedDestroy += Time.fixedDeltaTime;
			text.CrossFadeAlpha(0f, .25f, false);
			if(timedDestroy >= .4f){
				Destroy(gameObject);
			}
		}
		}
	}

	void OnPause(){
		paused = !paused;
	}
}
