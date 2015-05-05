using UnityEngine;
using System.Collections;

public class SoundtrackTrigger : MonoBehaviour {
	public AudioClip Soundtrack;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")){
			AudioSource audio = GetComponent<AudioSource>();
			audio.PlayOneShot(Soundtrack, 1f);
			Debug.Log("Soundtrack played");
		}
		
	}
}
