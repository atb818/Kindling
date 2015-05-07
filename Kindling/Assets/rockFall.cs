using UnityEngine;
using System.Collections;

public class rockFall : MonoBehaviour {

	bool falling = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (gameObject.GetComponent<Rigidbody> ().isKinematic == false && transform.localScale.x > .1f) {
			transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
		}
		if (transform.localScale.x <= .1f) {
			gameObject.SetActive (false);
		}

	}
}
