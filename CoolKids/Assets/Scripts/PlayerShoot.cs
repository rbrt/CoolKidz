using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerShoot : MonoBehaviour {

	[SerializeField] protected GameObject muzzleFlash;
	[SerializeField] protected Transform cameraTransform;
	[SerializeField] protected Transform ejectionPort;
	[SerializeField] protected GameObject shellPrefab;
	[SerializeField] protected GameObject muzzle;
	[SerializeField] protected GameObject mainCamera;
	[SerializeField] protected GameObject tracerPrefab;
	[SerializeField] protected GameObject bulletImpact;
	[SerializeField] protected GameObject bulletImpactBody;

	float gunDamage = 10;

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

				var ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

				RaycastHit hit;
				if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit)){
					Vector3 targetPoint = hit.point;
					// baby hit
					if (hit.collider.GetComponent<Enemy>()){
						GameObject.Instantiate(bulletImpactBody, targetPoint, Quaternion.Euler(Vector3.zero));

						if (hit.collider.GetComponent<BabyController>()){
							hit.collider.GetComponent<BabyController>().GetShot(gunDamage);
						}
					}
					else{
						GameObject.Instantiate(bulletImpact, targetPoint, Quaternion.Euler(Vector3.zero));
					}

					var tracer = GameObject.Instantiate(tracerPrefab,
														muzzle.transform.position,
														Quaternion.LookRotation(targetPoint - muzzle.transform.position));
				}

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
