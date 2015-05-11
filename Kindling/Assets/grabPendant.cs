using UnityEngine;
using System.Collections;

public class grabPendant : MonoBehaviour {

	bool grabbable = false;

	void Start () {
	
	}
	
	void Update () {
		if (grabbable) {
			
		}
	}

	void metWolf(){
		grabbable = true;
	}
}
