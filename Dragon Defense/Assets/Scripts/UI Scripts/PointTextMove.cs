using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointTextMove : MonoBehaviour {
	
	[HideInInspector]
	public float pointDisplay = 0;
	private float moveUpAmount = 2f;
	private Vector3 movePosition;
	private float timedDestroy;
	private bool paused;

	private Text text;

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
		movePosition = transform.position;
		text = GetComponent<Text>();
		text.text = "+ " + pointDisplay;
	}

	void FixedUpdate(){
		if(!paused)
			Move();
	}

	private void Move(){
		movePosition.y += moveUpAmount;
		moveUpAmount -= moveUpAmount/50;

		if(moveUpAmount <= .5f){
			timedDestroy += Time.fixedDeltaTime;
			text.CrossFadeAlpha(0f, .35f, false);
			if(timedDestroy >= .5f){
				Destroy(gameObject);
			}
		}
		transform.position = movePosition;
	}

	void OnPause(){
		paused = !paused;
	}
}
