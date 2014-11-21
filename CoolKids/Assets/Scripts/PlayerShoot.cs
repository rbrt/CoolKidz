using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerShoot : MonoBehaviour {

	[SerializeField] protected GameObject muzzleFlash;
	[SerializeField] protected Transform cameraTransform;
	[SerializeField] protected Transform ejectionPort;
	[SerializeField] protected GameObject shellPrefab;

	private SafeCoroutine shootingCoroutine;

	private const float	rateOfFire = .1f,
					    muzzleFlashDuration = .01f;

	IEnumerator Primer(){
		yield break;
	}

	void Awake () {
		shootingCoroutine = this.StartSafeCoroutine(Primer());
	}

	public void SetShooting(bool shootingValue){
		if (shootingCoroutine.IsPaused || shootingCoroutine.IsRunning){
			shootingCoroutine.Stop();
			muzzleFlash.SetActive(false);
		}
		if (shootingValue){
			shootingCoroutine = this.StartSafeCoroutine(ShootCoroutine());
		}
	}

	IEnumerator ShootCoroutine(){
		float startTime = Time.time;

		while (true){
			if (Time.time - startTime >= rateOfFire){
				startTime = Time.time;
				muzzleFlash.SetActive(true);
				muzzleFlash.transform.LookAt(cameraTransform);
				muzzleFlash.transform.Rotate(muzzleFlash.transform.right, Random.Range(0, 360));

				var obj = GameObject.Instantiate(shellPrefab, ejectionPort.position, ejectionPort.rotation) as GameObject;
				obj.GetComponent<Rigidbody>().AddForce(ejectionPort.forward, ForceMode.VelocityChange);
			}

			if (Time.time - startTime >= muzzleFlashDuration){
				muzzleFlash.SetActive(false);
			}

			yield return null;
		}
	}

}
