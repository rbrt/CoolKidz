using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField] protected Animator animator;

	private bool holdingUp,
				 holdingDown,
				 holdingLeft,
				 holdingRight,
				 holdingShoot,
				 holdingPanLeft,
				 holdingTurnLeft,
				 holdingPanRight,
				 holdingTurnRight,
				 shooting,
				 walking;

	private const float sideSpeed = 10f,
						forwardSpeed = 10f;

	private bool Walking{
		get{
			return walking;
		}
		set{
			if (value != walking){
				// update animator
				if (value){
					animator.SetTrigger("StartRun");
				}
				else{
					animator.SetTrigger("StopRun");
				}
			}
			walking = value;
		}
	}

	private bool Shooting {
		get{
			return shooting;
		}
		set{
			if (value != shooting){
				animator.SetBool("ShootBool", value);
			}
			shooting = value;
		}
	}

	void Start () {

	}

	void Update () {
		HandleInput();
	}

	void HandleInput(){
		var pos = transform.localPosition;

		if (Input.GetKeyDown(KeyCode.UpArrow)){
			holdingUp = true;
			holdingDown = false;
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow)){
			holdingRight = true;
			holdingLeft= false;
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow)){
			holdingLeft = true;
			holdingRight = false;
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow)){
			holdingDown = true;
			holdingUp = false;
		}
		else if (Input.GetKeyDown(KeyCode.Q)){
			holdingPanLeft = true;
			holdingPanRight = false;
		}
		else if (Input.GetKeyDown(KeyCode.E)){
			holdingPanRight = true;
			holdingPanLeft = false;
		}
		else if (Input.GetKeyDown(KeyCode.A)){
			holdingTurnLeft = true;
			holdingTurnRight = false;
		}
		else if (Input.GetKeyDown(KeyCode.D)){
			holdingTurnRight = true;
			holdingTurnLeft = false;
		}
		else if (Input.GetKeyDown(KeyCode.Space)){
			holdingShoot = true;
		}

		if (Input.GetKeyUp(KeyCode.UpArrow)){
			holdingUp = false;
		}
		else if (Input.GetKeyUp(KeyCode.RightArrow)){
			holdingRight = false;
		}
		else if (Input.GetKeyUp(KeyCode.LeftArrow)){
			holdingLeft = false;
		}
		else if (Input.GetKeyUp(KeyCode.DownArrow)){
			holdingDown = false;
		}
		else if (Input.GetKeyUp(KeyCode.Q)){
			holdingPanLeft = false;
		}
		else if (Input.GetKeyUp(KeyCode.A)){
			holdingTurnLeft = false;
		}
		else if (Input.GetKeyUp(KeyCode.E)){
			holdingPanRight = false;
		}
		else if (Input.GetKeyUp(KeyCode.D)){
			holdingTurnRight = false;
		}
		else if (Input.GetKeyUp(KeyCode.Space)){
			holdingShoot = false;
		}

		// Update animator
		Walking = (holdingUp || holdingLeft || holdingRight || holdingDown);
		Shooting = holdingShoot;

		if (holdingLeft){
			pos.z += Time.deltaTime * sideSpeed;
		}
		else if (holdingRight){
			pos.z -= Time.deltaTime * sideSpeed;
		}

		if (holdingUp){
			pos.x += Time.deltaTime * forwardSpeed;
		}
		else if (holdingDown){
			pos.x -= Time.deltaTime * forwardSpeed;
		}

		transform.localPosition = pos;
	}
}
