using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerShoot : MonoBehaviour {

	private SafeCoroutine shootingCoroutine;

	private const float	rateOfFire = .1f;

	IEnumerator Primer(){
		yield break;
	}

	void Awake () {
		shootingCoroutine = this.StartSafeCoroutine(Primer());
	}

	public void SetShooting(bool shootingValue){
		if (shootingCoroutine.IsPaused || shootingCoroutine.IsRunning){
			shootingCoroutine.Stop();
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
				
			}

			yield return null;
		}
	}

}
