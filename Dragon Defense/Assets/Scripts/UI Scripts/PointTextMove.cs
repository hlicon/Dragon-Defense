using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointTextMove : MonoBehaviour {
	
	[HideInInspector]
	public float pointDisplay = 0;
	private float moveUpAmount = 2f;
	private float startMoveUpAmount;
	[HideInInspector]
	public Vector2 movePosition;
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
		startMoveUpAmount = moveUpAmount;
		text = GetComponent<Text>();
	}

	void FixedUpdate(){
		if(!paused)
			Move();
	}

	void Update(){
		text.text = "+ " + pointDisplay;
	}

	private void Move(){
		movePosition.y += moveUpAmount;
		moveUpAmount -= moveUpAmount/50;

		if(moveUpAmount <= .5f){
			timedDestroy += Time.fixedDeltaTime;
			text.CrossFadeAlpha(0f, .35f, false);
			if(timedDestroy >= .5f){
				Pooling.Despawn(gameObject);
				timedDestroy = 0;
				moveUpAmount = startMoveUpAmount;
			}
		}
		transform.position = movePosition;
	}

	void OnPause(){
		paused = !paused;
	}
}
