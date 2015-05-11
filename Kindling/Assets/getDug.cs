using UnityEngine;
using System.Collections;

public class getDug : MonoBehaviour {

	public GameObject pendant1;
	public bool dug = false;
	GameObject [] pieces;

	void Start () {

		pieces = GameObject.FindGameObjectsWithTag ("pendant piece");

		foreach(GameObject piece in pieces){
			piece.GetComponent<Renderer>().enabled = false;
		}

	}
	
	void OnTriggerStay (Collider other){
		if (Input.GetKeyUp (KeyCode.Mouse1) && dug == false && other.gameObject.CompareTag ("Player")) {
			foreach(GameObject piece in pieces){
				piece.GetComponent<Renderer>().enabled = true;
			}
			pendant1.SendMessage("goGrabIt");
			dug = true;
		}
	}
}
