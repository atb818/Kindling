using UnityEngine;
using System.Collections;

public class lightManager : MonoBehaviour {

	Light lt;
	public float ltGradient = 0.2f;
	public float ltLimit = 2f;

	void Start () {
		lt = GetComponent<Light>();
		lt.intensity = ltLimit;
	}
	
	void Update () {
	
		if (Input.GetKey (KeyCode.Mouse0)) {
			/*if (RenderSettings.ambientIntensity > 0){
				RenderSettings.ambientIntensity = RenderSettings.ambientIntensity - ltGradient;
			}*/
			if (this.gameObject.CompareTag ("dirLight") && lt.intensity > 0) {
				lt.intensity = lt.intensity - ltGradient;
			} else if (this.gameObject.CompareTag ("listLight") && lt.intensity < 8) {
				lt.intensity = lt.intensity + ltGradient;
			}
		} else {
			/*if (RenderSettings.ambientIntensity < ltLimit){
				RenderSettings.ambientIntensity = RenderSettings.ambientIntensity + ltGradient;
			}*/
			if (this.gameObject.CompareTag ("dirLight") && lt.intensity < ltLimit) {
				lt.intensity = lt.intensity + (ltGradient * 3);
			} else if (lt.intensity > ltLimit){
				lt.intensity = ltLimit;
			}
			else if (this.gameObject.CompareTag ("listLight") && lt.intensity > ltLimit) {
				lt.intensity = lt.intensity - (ltGradient * 3);
			}

		}

	}
}
