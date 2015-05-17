using UnityEngine;
using System.Collections;

public class endAudio : MonoBehaviour {

	public AudioClip audClip;
	AudioSource audio;
	bool playtime = false;

	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();
	}
	
	void Update () {
		if (playtime) {
			audio.PlayOneShot(audClip, .4f);
			StartCoroutine (finalCountdown());
		}
	}

	void itsPlaytime(){
		playtime = true;
	}

	IEnumerator finalCountdown(){
		yield return new WaitForSeconds(8);
		Application.LoadLevel ("EndingScene");
	}
}
