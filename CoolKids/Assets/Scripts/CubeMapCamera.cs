using UnityEngine;
using System.Collections;

public class CubeMapCamera : MonoBehaviour {

	Camera cam;

	[SerializeField] protected Renderer cubeMapRenderer;
	[SerializeField] protected Transform orientationTransform;

	static bool instance = false;

	int cubemapSize = 128;
	bool oneFacePerFrame = false;
	private RenderTexture rtex;

	void Start () {
		instance = true;

		if (instance){
			StartCoroutine(UpdateCubemap( 63 ));
		}

	}

	void LateUpdate () {
		if (oneFacePerFrame) {
			var faceToRender = Time.frameCount % 6;
			var faceMask = 1 << faceToRender;
			StartCoroutine(UpdateCubemap (faceMask));
		}
		else {
			StartCoroutine(UpdateCubemap (63));
		}
	}

	IEnumerator UpdateCubemap (int faceMask) {
		if (!cam) {
			GameObject go = new GameObject("CubemapCamera", typeof(Camera));
			go.transform.position = orientationTransform.position;
			go.transform.rotation = orientationTransform.rotation;
			cam = go.GetComponent<Camera>();
			cam.farClipPlane = 100;
		}

		if (!rtex) {
			rtex = new RenderTexture (cubemapSize, cubemapSize, 16);
			rtex.isCubemap = true;
			rtex.hideFlags = HideFlags.HideAndDontSave;
			while(cubeMapRenderer == null){
				yield return null;
			}
			cubeMapRenderer.sharedMaterial.SetTexture ("_Cube", rtex);
		}

		cam.transform.position = orientationTransform.position;
		cam.RenderToCubemap (rtex, faceMask);
	}

	void OnDisable () {
		DestroyImmediate (cam);
		DestroyImmediate (rtex);
	}
}
