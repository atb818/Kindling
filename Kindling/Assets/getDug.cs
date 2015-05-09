using UnityEngine;
using System.Collections;

public class getDug : MonoBehaviour {

	public GameObject pendant;
	public bool dug = false;

	void Start () {
		pendant.SetActive (false);
	}
	
	void OnTriggerStay (Collider other){
		if (Input.GetKeyUp (KeyCode.Mouse1) && dug == false && other.gameObject.CompareTag ("Player")) {
			pendant.SetActive (true);
			dug = true;
		}
	}
}
