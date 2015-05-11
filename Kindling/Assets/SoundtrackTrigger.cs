using UnityEngine;
using System.Collections;

public class SoundtrackTrigger : MonoBehaviour {
	public AudioClip Soundtrack;
	AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource>();
	}

	void Update () {
		if (Input.GetKey (KeyCode.Mouse0)) {
			//audio.volume = audio.volume - .05f;
			//if (audio.volume <= 0.1f){
				audio.volume = 0.15f;
			//}
		} else {
			//audio.volume = audio.volume + .05f;
			//if  (audio.volume >= .45f){
				audio.volume = .45f;
			//}
		}
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")){
			audio.PlayOneShot(Soundtrack, 1f);
			Debug.Log("Soundtrack played");
		}
		
	}
}
