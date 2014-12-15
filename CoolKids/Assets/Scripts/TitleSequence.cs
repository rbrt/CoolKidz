using UnityEngine;
using System.Collections;


public class TitleSequence : MonoBehaviour {

	[SerializeField] protected UnityEngine.UI.Text rabiesText,
					    						   babiesText,
												   promptText;

	[SerializeField] protected GameObject[] explosions;

	bool finished;

	// Use this for initialization
	void Start () {
		StartCoroutine(Sequence());
		StartCoroutine(StartExploding());
	}

	// Update is called once per frame
	void Update () {
		if (finished){
			if (Input.GetKeyDown(KeyCode.Space)){
				Application.LoadLevel("MainScene");
			}
		}
	}

	IEnumerator StartExploding(){
		for (int i = 0; i < explosions.Length; i++){
			StartCoroutine(HandleExplosions(explosions[i]));
			yield return new WaitForSeconds(.2f);
		}
	}

	IEnumerator HandleExplosions(GameObject explosion){
		while (true){
			explosion.SetActive(true);
			yield return new WaitForSeconds(2);
			explosion.SetActive(false);
		}
	}

	IEnumerator Sequence(){

		rabiesText.gameObject.SetActive(true);

		yield return new WaitForSeconds(1);
		babiesText.gameObject.SetActive(true);

		yield return new WaitForSeconds(1);
		promptText.gameObject.SetActive(true);

		finished = true;
		StartCoroutine(Flash());
	}

	IEnumerator Flash(){
		while (true){
			yield return new WaitForSeconds(1);
			promptText.enabled = !promptText.enabled;
		}
	}
}
