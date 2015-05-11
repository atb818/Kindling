using UnityEngine;
using System.Collections;

public class laughter : MonoBehaviour {

	AudioSource audio;
	public AudioClip laugh;
	bool enteredCave = false;

	void Start () {
	
		audio = GetComponent<AudioSource> ();	

	}
	
	void Update () {
	
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.CompareTag ("Player") && enteredCave == false) {
			audio.PlayOneShot (laugh, 1);
			enteredCave = true;
		}
	}
}
