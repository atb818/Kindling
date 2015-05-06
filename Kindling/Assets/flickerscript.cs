using UnityEngine;
using System.Collections;

public class flickerscript : MonoBehaviour {

	float amplitudeX = 0.05f;
	float amplitudeY = 0.2f;
	float omegaX = 0.05f;
	float omegaY = 0.2f;
	float index;
	
	public void Update(){
		index += Time.deltaTime;
		float x = amplitudeX*Mathf.Cos (omegaX*index);
		float y = Mathf.Abs (amplitudeY*Mathf.Sin (omegaY*index));
		transform.localPosition = new Vector3(x+321,y+45,144);
	}
}