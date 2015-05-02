using UnityEngine;
using System.Collections;

public class deathWhine : MonoBehaviour {

	bool hasApproached = false;
	AudioSource audio;
	public AudioClip whine;

	void OnTriggerEnter (Collider other){
		//listenSource = other.gameObject;
		if (other.gameObject.CompareTag ("Player") && hasApproached == false) {
			hasApproached = true;
			audio = GetComponent<AudioSource>();
			audio.PlayOneShot(whine, 1);
			//Debug.Log ("Near Church");
		}
	}
	
}
