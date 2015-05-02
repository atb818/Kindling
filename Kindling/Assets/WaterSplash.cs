using UnityEngine;
using System.Collections;

public class WaterSplash : MonoBehaviour {
	public AudioClip Splash;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
		AudioSource audio = GetComponent<AudioSource>();
		audio.PlayOneShot(Splash, 1f);
		Debug.Log("audio hit");
	
	}
}
