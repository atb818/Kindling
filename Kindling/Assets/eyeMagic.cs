using UnityEngine;
using System.Collections;

public class eyeMagic : MonoBehaviour {

	public Renderer rend;
	
	void Start () {
		
		rend = GetComponent<Renderer> ();
		rend.enabled = false;
		
	}
	
	void Update () {
		
		if (Input.GetKey (KeyCode.Mouse0)) {
			rend.enabled = true;
		} else {
			rend.enabled = false;
		}
		
	}
}
