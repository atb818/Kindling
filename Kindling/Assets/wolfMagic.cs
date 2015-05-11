using UnityEngine;
using System.Collections;

public class wolfMagic : MonoBehaviour {

	public Renderer rend;
	public GameObject sLight;
	//Light _sLight;
	public GameObject pendant;

	bool metDog = false;

	void Start () {

		rend = GetComponent<Renderer> ();
		rend.enabled = false;
		sLight.SetActive (false);
		//_sLight = sLight.GetComponent<Light> ();
		//_sLight.intensity = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Mouse0)) {
			rend.enabled = true;
			sLight.SetActive (true);
			/*_sLight.intensity = _sLight.intensity +.2f;
			if (_sLight.intensity > 1){
				_sLight.intensity = 1;
			}*/
			if (metDog){
				pendant.SendMessage("metWolf");		
			}
		} else {
			rend.enabled = false;
			sLight.SetActive (false);
			//_sLight.intensity = 0;
		}

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			metDog = true;
		}
		/*if (other.gameObject.CompareTag ("pendant")) {
			pendant.SendMessage ("inCave");
		}*/
	}
}
