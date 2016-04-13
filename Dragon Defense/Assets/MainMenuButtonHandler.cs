using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour {

	public GameObject menuPanel;
	public GameObject optionsPanel;

	public void LoadMain(){
		//Fade out
		//play dragon wake up sound(s)
		SceneManager.LoadScene("Main");
	}

	public void ToggleOptions(){
		if(menuPanel.activeSelf){//if menu panel is active, turn it off and turn options panel on
			menuPanel.SetActive(false);
			optionsPanel.SetActive(true);
		} else {//Else if menu is NOT active, we turn it on, and turn options off
			menuPanel.SetActive(true);
			optionsPanel.SetActive(false); 
		}
	}

	public void ExitGame(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
