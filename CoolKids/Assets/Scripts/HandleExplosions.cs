using UnityEngine;
using System.Collections;

public class HandleExplosions : MonoBehaviour {

	[SerializeField] protected GameObject[] explosions;

	// Use this for initialization
	void Start () {
		StartCoroutine(StartExploding());
	}

	IEnumerator StartExploding(){
		for (int i = 0; i < explosions.Length; i++){
			StartCoroutine(HandleExplosion(explosions[i]));
			yield return new WaitForSeconds(.2f);
		}
	}

	IEnumerator HandleExplosion(GameObject explosion){
		while (true){
			explosion.SetActive(true);
			yield return new WaitForSeconds(2);
			explosion.SetActive(false);
		}
	}
}
