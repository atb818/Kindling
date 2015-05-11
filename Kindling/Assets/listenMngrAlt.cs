using UnityEngine;
using System.Collections;

public class listenMngrAlt : MonoBehaviour {

	public bool inRange = false;
	public bool audioPlaying = false;
	float listenVol;
	AudioSource audio;
	AudioSource mAudio;
	public AudioClip listeningAudio;
	//public GameObject muffledAudio;
	
	void Start () {
		
		audio = GetComponent<AudioSource> ();
		audio.volume = 1;
		
		//mAudio = muffledAudio.GetComponent<AudioSource> ();
		//mAudio.volume = 0;
		
	}
	
	void Update () {
		
		if (inRange) {
			if (audioPlaying == false) {
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					audio = GetComponent<AudioSource> ();
					audio.PlayOneShot (listeningAudio, 1);
					audioPlaying = true;

					//Debug.Log ("play audio");
				}
			} else {
				if (Input.GetKey (KeyCode.Mouse0)) {
					audio.volume = 1;
					//mAudio.volume = 0;
				} else if (Input.GetKeyUp (KeyCode.Mouse0)) {
					audio.volume = .1f;
					//mAudio.volume = 1;
				}
			} 
			/*if (audioPlaying == false){
				mAudio.volume = .3f;
			}
			
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				audioPlaying = true;
				audio.volume = 1;
				mAudio.volume = 0;
			} else if (Input.GetKeyUp (KeyCode.Mouse0)) {
				audio.volume = 0;
				mAudio.volume = .3f;
			}
			
		} else if (inRange == false) {
			audio.volume = 0;
			mAudio.volume = 0;
		}*/
		
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
