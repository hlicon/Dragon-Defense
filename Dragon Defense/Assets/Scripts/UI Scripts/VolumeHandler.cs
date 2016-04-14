using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class VolumeHandler : MonoBehaviour {

	public AudioMixer masterMixer;

	public AudioSource mainMenuMusic;

	private AudioSource currentMusic;
	private string currentScene;

	#region Event Subscriptions
	void OnEnable()
	{
		FadeScenes.OnSceneChange += OnSceneChange;
	}
	void OnDisable()
	{
		FadeScenes.OnSceneChange -= OnSceneChange;
	}
	void OnDestroy()
	{
		FadeScenes.OnSceneChange -= OnSceneChange;
	}
	#endregion

	void Start(){
		DontDestroyOnLoad(gameObject);
		currentScene = SceneManager.GetActiveScene().name;
		print(currentScene);
		if(currentScene.Equals("MainMenu")){
			print(mainMenuMusic.gameObject.name);
			currentMusic = mainMenuMusic;
		}
	}

	void OnSceneChange(){
		currentMusic.volume -= .01f;
	}

	public void SetMusicVolume(float musicLevel){
		masterMixer.SetFloat("musicVol", musicLevel);
	}

	public void SetSFXVolume(float SFXLevel){
		masterMixer.SetFloat("SFXVol", SFXLevel);
	}


}
