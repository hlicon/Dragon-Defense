using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreUpdate : MonoBehaviour {
	
	public GameObject scoreObject;
	private Text scoreText;

	private static float score;
	public static float Score
	{
		get { return score; }
	}

	#region Event Subscriptions
	void OnEnable()
	{
		EnemyClass.OnDestroyEnemy += OnDestroyEnemy;
	}
	void OnDisable()
	{
		EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;

	}
	void OnDestroy()
	{
		EnemyClass.OnDestroyEnemy -= OnDestroyEnemy;

	}
	#endregion

	void Start(){
		scoreText = scoreObject.GetComponent<Text>();
        scoreText.text = "Score: " + score;
	}


	public void OnDestroyEnemy(float points){
		score += points;
		scoreText.text = "Score: " + score ;
	}
}
