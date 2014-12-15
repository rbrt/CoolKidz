using UnityEngine;
using System.Collections;

public class BabyController : MonoBehaviour {

	GameObject player;
	Animator animator;

	float attackDistance = 6f,
		  moveSpeed = 15,
		  health = 100;

	bool dead;

	// Use this for initialization
	void Start () {
		player = PlayerController.Instance;
		animator = GetComponent<Animator>();

		animator.SetBool("seesPlayer", true);

		dead = false;
	}

	// Update is called once per frame
	void Update () {
		if (!dead){
			transform.LookAt(player.transform, Vector3.up);
			var rotation = transform.rotation;
			rotation.x = 0;
			rotation.z = 0;
			transform.rotation = rotation;

			// Run at player
			if (Vector3.Distance(transform.position, player.transform.position) > attackDistance){
				animator.SetBool("attacking", false);

				var pos = transform.position;
				pos += transform.forward * Time.deltaTime * moveSpeed;

				transform.position = pos;
			}
			// Attack player
			else {
				animator.SetBool("attacking", true);
			}
		}
	}

	public void GetShot(float damage){
		health -= damage;

		if (health <= 0){
			dead = true;
			animator.SetBool("dead", true);
		}
	}
}
