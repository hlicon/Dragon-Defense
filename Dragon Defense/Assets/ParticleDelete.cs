using UnityEngine;
using System.Collections;

public class ParticleDelete : MonoBehaviour {

	void Start(){
		StartCoroutine(DeleteMe());
	}

	private IEnumerator DeleteMe(){
		yield return new WaitForSeconds(GetComponent<ParticleSystem>().startLifetime);
		Destroy(gameObject); //Destroys particle system after all particles are gone
	}

}
