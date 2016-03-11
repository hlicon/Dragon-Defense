using UnityEngine;
using System.Collections;

public class ParticleDelete : MonoBehaviour {

	ParticleSystem system;

	void Start(){
		system = GetComponent<ParticleSystem>();
		system.collision.SetPlane(0, GameObject.FindGameObjectWithTag("Ground").transform);
		StartCoroutine(DeleteMe());
	}

	private IEnumerator DeleteMe(){
		yield return new WaitForSeconds(GetComponent<ParticleSystem>().startLifetime);
		Destroy(gameObject); //Destroys particle system after all particles are gone
	}

}
