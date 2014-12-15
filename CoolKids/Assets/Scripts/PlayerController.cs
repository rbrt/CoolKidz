﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField] protected Animator animator;
	[SerializeField] protected PlayerShoot playerShoot;
	[SerializeField] protected GameObject coolCamera;
	[SerializeField] protected Transform playerTorso;

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

	public float XSensitivity = 1f;
	public float YSensitivity = 1f;
	public bool clampVerticalRotation = true;
	public float MinimumX = -90F;
	public float MaximumX = 90F;
	public bool smooth;
	public float smoothTime = 5f;

	private Quaternion characterTargetRot;

	void Start(){
		characterTargetRot = transform.localRotation;
	}

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
				playerShoot.SetShooting(value);
			}
			shooting = value;
		}
	}

	void Update () {
		HandleInput();
	}

	void HandleInput(){

		MouseInput();

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
		else if (Input.GetKeyDown(KeyCode.C)){
			coolCamera.SetActive(!coolCamera.activeSelf);
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
			pos += Time.deltaTime * forwardSpeed * transform.forward;
		}
		else if (holdingRight){
			pos -= Time.deltaTime * forwardSpeed * transform.forward;
		}

		if (holdingUp){
			pos += Time.deltaTime * sideSpeed * transform.right;
		}
		else if (holdingDown){
			pos -= Time.deltaTime * sideSpeed * transform.right;
		}

		transform.localPosition = pos;
	}

	void MouseInput(){
		float yRot = Input.GetAxis("Mouse X") * XSensitivity;
		float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

		characterTargetRot *= Quaternion.Euler (0f, yRot, 0f);

		if(clampVerticalRotation)
		//    m_CameraTargetRot = ClampXRotation (m_CameraTargetRot);

		if(smooth) {
			transform.localRotation = Quaternion.Slerp (transform.localRotation, characterTargetRot,
			smoothTime * Time.deltaTime);
			//camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,smoothTime * Time.deltaTime);
		}
		else {
			transform.localRotation = characterTargetRot;
			//    camera.localRotation = m_CameraTargetRot;
		}
	}

	Quaternion ClampXRotation(Quaternion q){
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

		angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}

}
