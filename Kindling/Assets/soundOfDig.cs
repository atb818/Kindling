using UnityEngine;
using System.Collections;

public class soundOfDig : MonoBehaviour {

	AudioSource audio;
	public AudioClip diggin;

	void Start () {
	
		audio = gameObject.GetComponent<AudioSource> ();

	}
	
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			audio.Play (); 
		} else if (Input.GetKeyUp (KeyCode.Mouse1)) {
			audio.Stop ();
		}

	}
}
