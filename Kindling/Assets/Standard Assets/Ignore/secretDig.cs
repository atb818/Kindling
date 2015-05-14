using UnityEngine;
using System.Collections;

public class secretDig : MonoBehaviour {

	bool secretFound = false;

	GameObject [] hinges;


	void Start () {
		secretFound = false;

		hinges = GameObject.FindGameObjectsWithTag ("oriensSecret");
		/*foreach(GameObject hinge in hinges){
			hinge.SetActive (false);
		}*/

	}
	
	void OnTriggerStay (Collider other){
		if (Input.GetKeyUp (KeyCode.Mouse1) && secretFound == false && other.gameObject.CompareTag ("Player")) {
			foreach(GameObject hinge in hinges){
				hinge.SetActive (true);
			}
			secretFound = true;
		}
	}
}
