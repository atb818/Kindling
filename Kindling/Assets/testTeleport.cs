using UnityEngine;
using System.Collections;

public class testTeleport : MonoBehaviour {

	Vector3 start;
	Vector3 town;
	Vector3 farm; 
	Vector3 cabin;

	void Start(){
		start = new Vector3 (231, 11, 435);
		town = new Vector3 (220, 15, 374);
		farm = new Vector3 (189, 25, 285);
		cabin = new Vector3 (300, 46, 175);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			transform.position = start;
		}		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			transform.position = town;
		}		else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			transform.position = farm;
		}		else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			transform.position = cabin;
		}
	}
}
