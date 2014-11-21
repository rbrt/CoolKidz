using UnityEngine;
using System.Collections;

public class Cigarette : MonoBehaviour {

	[SerializeField] ParticleSystem smokeParticle;
	float smokeRate = .4f;

	void Start(){
		this.StartSafeCoroutine(SpawnSmoke());
	}

	IEnumerator SpawnSmoke(){
		while (true){
			var smoke = GameObject.Instantiate(smokeParticle, transform.position, transform.rotation) as GameObject;
			this.StartSafeCoroutine(KillSmoke(smoke));
			yield return new WaitForSeconds(smokeRate);
		}
	}

	IEnumerator KillSmoke(GameObject smokeParticleInstance){
		yield return new WaitForSeconds(5f);
		DestroyImmediate(smokeParticleInstance, true);
	}
}
