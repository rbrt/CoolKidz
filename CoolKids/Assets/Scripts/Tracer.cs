using UnityEngine;
using System.Collections;

public class Tracer : MonoBehaviour {

	float startTime;
	float lifeTime = 4;
	float moveSpeed = 100;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}

	void Update(){
		if (Time.time - startTime > lifeTime){
			Destroy(this.gameObject);
		}

		var pos = transform.position;
		pos += transform.forward * Time.deltaTime * moveSpeed;
		transform.position = pos;
	}

}
