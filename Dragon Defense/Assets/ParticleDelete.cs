using UnityEngine;
using System.Collections;

public class ParticleDelete : MonoBehaviour {

	void Start(){
		StartCoroutine(deleteMe());
	}

	private IEnumerator deleteMe(){
		yield return new WaitForSeconds(GetComponent<ParticleSystem>().startLifetime);
		Destroy(gameObject);
	}

}
