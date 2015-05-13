using UnityEngine;
using System.Collections;

public class digRocks : MonoBehaviour {

	public bool dug = false;
	GameObject[] rocks;
	AudioSource audio;
	public AudioClip rockfall;
	BoxCollider barrier;

	void Start () {

		rocks = GameObject.FindGameObjectsWithTag ("rockBar");
		barrier = gameObject.GetComponent<BoxCollider> ();
		audio = gameObject.GetComponent<AudioSource> ();


	
	}
	
	void OnTriggerStay (Collider other){
		if (Input.GetKeyUp (KeyCode.Mouse1) && dug == false && other.gameObject.CompareTag ("Player")) {
			foreach(GameObject rock in rocks){
				rock.GetComponent<Rigidbody>().isKinematic=false;
			}
			barrier.enabled = false;
			audio.PlayOneShot(rockfall, 1);

			dug = true;
		}
	}
}
