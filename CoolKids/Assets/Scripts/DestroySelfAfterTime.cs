using UnityEngine;
using System.Collections;

public class DestroySelfAfterTime : MonoBehaviour {

	[SerializeField] float timeToDie;
	private float startTime;

	void Start () {
		startTime = Time.time;
	}

	void Update () {
		if (Time.time - startTime > timeToDie){
			Destroy(this.gameObject);
		}
	}
}
