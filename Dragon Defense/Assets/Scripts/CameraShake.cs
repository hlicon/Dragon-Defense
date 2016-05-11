using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	[Header("Magnitudes")]
	public AnimationCurve positionMagnitude;
	public AnimationCurve rotationMagnitude;

	[Header("Shake Duration")]
	public float duration;

	private float startDuration;
	private float percentComplete;
	private bool canShake;
	private Vector3 originalPosition;
	private Quaternion originalRotation;

	void Start(){
		originalPosition = transform.position;
		originalRotation = transform.localRotation;
	}

	void Update(){
		if(canShake && duration > 0f){
			percentComplete = 1- duration/startDuration;
			duration -= Time.deltaTime;
			transform.localPosition = originalPosition + Random.insideUnitSphere * positionMagnitude.Evaluate(percentComplete);
			transform.localRotation = Quaternion.Euler(Random.insideUnitSphere * rotationMagnitude.Evaluate(percentComplete));

			if(duration < 0){
				duration = startDuration;
				canShake = false;
			}
		}

		if(!canShake && transform.localPosition != originalPosition){
			transform.localPosition = originalPosition;
			transform.rotation = originalRotation;
		}
	}

	public void StartShake(){
		startDuration = duration;
		canShake = true;
	}

}
