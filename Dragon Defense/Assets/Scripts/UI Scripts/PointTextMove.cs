using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointTextMove : MonoBehaviour {
	[HideInInspector]
	public float pointDisplay;
	private float moveUpAmount = 2f;
	private Vector3 movePosition;
	private float timedDestroy;
	private Text text;

	void Start(){
		movePosition = transform.position;
		text = GetComponent<Text>();
		text.text = "+ " + pointDisplay;
	}

	void FixedUpdate(){
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
}
