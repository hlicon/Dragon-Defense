using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeHandler : MonoBehaviour {

	public AudioMixer masterMixer;

	public void SetMusicVolume(float musicLevel){
		masterMixer.SetFloat("musicVol", musicLevel);
	}

	public void SetSFXVolume(float SFXLevel){
		masterMixer.SetFloat("SFXVol", SFXLevel);
	}


}
