using UnityEngine;
using System.Collections;

public class withWolf : MonoBehaviour {

	bool grabbed = false;
	bool grabbable = false;
	bool _metWolf = false;
	bool onPed = false;
	bool endInteraction = false;
	public Transform holder;
	//public Transform pedestal;
	Transform whereItAt;	

	public GameObject pendant2;
	public GameObject pedestal;

	void Start () {

		pendant2.SetActive (false);

	}
	
	void Update () {

	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.CompareTag ("Player") && _metWolf && grabbable) {
			pendant2.SetActive (true);
			gameObject.SetActive (false);
			pedestal.SendMessage ("takeIt");
		}
	}

	void metWolf(){
		if (_metWolf == false) {
			_metWolf = true;
		}
	}

	void goGrabIt(){
		if (grabbable == false) {
			grabbable = true;
		}
	}

	/*void inCave(){
		grabbed = false;
		grabbable = false;
		endInteraction = true;
		//transform.position = pedestal.position;
	}*/
	
}
