using UnityEngine;
using System.Collections;

public class panting : MonoBehaviour {

	public AudioClip dogPant;
	AudioSource audio;

	void Start () {

		audio = GetComponent<AudioSource> ();
		audio.clip = dogPant;
	
	}
	
	void Update () {

		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.UpArrow)) {
			audio.Play();

		}
	
	}
}
