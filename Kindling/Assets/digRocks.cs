using UnityEngine;
using System.Collections;

public class digRocks : MonoBehaviour {

	public bool dug = false;
	GameObject[] rocks;

	void Start () {

		rocks = GameObject.FindGameObjectsWithTag ("rockBar");


	
	}
	
	void OnTriggerStay (Collider other){
		if (Input.GetKeyUp (KeyCode.Mouse1) && dug == false && other.gameObject.CompareTag ("Player")) {
			foreach(GameObject rock in rocks){
				rock.GetComponent<Rigidbody>().isKinematic=false;
			}
			dug = true;
		}
	}
}
