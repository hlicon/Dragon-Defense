﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

	public delegate void PauseEvent();
	public static event PauseEvent OnPause;

	public GameObject pausePanel;

    public void PauseGame(){
		if(OnPause != null){
			OnPause();
			pausePanel.SetActive(!pausePanel.activeSelf); 
		}
	}

}
