using UnityEngine;
using System.Collections;

public class listenManager : MonoBehaviour {

	public bool inRange = false;
	public bool audioPlaying = false;
	float listenVol;
	AudioSource audio;
	public AudioClip listeningAudio;

	void Start () {
	
	}
	
	void Update () {
		
		if (inRange) {
			if (audioPlaying == false) {
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					audio = GetComponent<AudioSource> ();
					audio.PlayOneShot (listeningAudio, 1);
					audioPlaying = true;
					Debug.Log ("play audio");
				}
			} else {
				if (Input.GetKey (KeyCode.Mouse0)) {
					audio.volume = 1;
				} else if (Input.GetKeyUp (KeyCode.Mouse0)) {
					audio.volume = 0;
				}
			}
		} else if (inRange == false) {
			audio = GetComponent<AudioSource> ();
			audio.volume = 0;
		}
	
	}

	void OnTriggerEnter (Collider other){
		//listenSource = other.gameObject;
		if (other.gameObject.CompareTag ("Player")) {
			inRange = true;
			//Debug.Log ("Near Church");
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			inRange = false;
		}
	}

}
