using UnityEngine;
using System.Collections;

public class headTilt : MonoBehaviour {

	Vector3 headChill;
	Vector3 _headTilt;

	public bool tilting;

	void Start () {

		headChill = new Vector3 (0, 0, 0);
		_headTilt = new Vector3 (0, 0, 100);

		tilting = false;

	}
	
	void Update () {

		Vector3 headRot = transform.rotation.eulerAngles;

		if (Input.GetKey (KeyCode.Q)) {
			Debug.Log("bark");
			headRot = Vector3.Lerp (headRot, _headTilt, .5f);
			transform.rotation.eulerAngles.Set(headRot.x, headRot.y, headRot.z);
		} else if (Input.GetKeyUp (KeyCode.Q)){
			headRot = Vector3.Lerp (headRot, headChill, .5f);
			transform.rotation.eulerAngles.Set(headRot.x, headRot.y, headRot.z);
		}

	}
}
