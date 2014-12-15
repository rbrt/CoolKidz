using UnityEngine;
using System.Collections;

public class BabySpawner : MonoBehaviour {

	[SerializeField] protected GameObject babyPrefab;
	[SerializeField] protected Transform spawnLocation;
	[SerializeField] protected UnityEngine.UI.Text countdownText;

	int[] difficulties = {10, 20, 40, 60, 80, 100, 150};
	int levelCount = 0;

	public int LevelCount{
		get { return levelCount; }
		set { levelCount = value; }
	}

	void Start () {
		StartCoroutine(SpawnBabies(levelCount));
	}

	IEnumerator SpawnBabies(int level){
		yield return StartCoroutine(GUICountDown());

		Vector3 position = spawnLocation.position;
		for (int i = 0; i < difficulties[level]; i++){
			GameObject.Instantiate(babyPrefab, position, spawnLocation.rotation);
			yield return new WaitForSeconds(.1f);
			position.x += Random.Range(-5,5);
			position.z += Random.Range(-5,5);

			if (Vector3.Distance(position, spawnLocation.position) > 20){
				position = spawnLocation.position;
			}
		}
	}

	IEnumerator GUICountDown(){
		countdownText.text = "3";
		float scale = 1;
		while (scale > .2f){
			countdownText.transform.localScale = Vector3.one * scale;
			scale -= .1f;
			yield return null;
		}

		countdownText.text = "2";
		scale = 1;
		while (scale > .2f){
			scale -= .1f;
			countdownText.transform.localScale = Vector3.one * scale;
			yield return null;
		}

		countdownText.text = "1";
		scale = 1;
		while (scale > .2f){
			scale -= .1f;
			countdownText.transform.localScale = Vector3.one * scale;
			yield return null;
		}

		countdownText.text = "GO";
		countdownText.transform.localScale = Vector3.one;

		yield return new WaitForSeconds(.5f);

		countdownText.text = "";
	}
}
