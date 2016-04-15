using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeScenes : MonoBehaviour {

	public delegate void SceneChangeEvent();
	public static event SceneChangeEvent OnSceneChange;

	private bool fadeOut;
	private string moveToScene;
	private AsyncOperation op;

	public void FadeToNewScene(string sceneName){
		fadeOut = true;
		moveToScene = sceneName;
		StartCoroutine(FadeWait());
	}

	private IEnumerator FadeWait(){
		op = SceneManager.LoadSceneAsync(moveToScene);
		op.allowSceneActivation = false;
		yield return new WaitForSeconds(2f);
		op.allowSceneActivation = true;
	}

	void Update(){
		if(fadeOut){
			OnSceneChange();
		}
	}
}
