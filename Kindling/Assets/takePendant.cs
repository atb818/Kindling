using UnityEngine;
using System.Collections;

public class takePendant : MonoBehaviour {

	public GameObject pendant2;
	public GameObject pendant3;

	public GameObject wolfGod;
	public GameObject wolfSick;

	public GameObject endAudio1;
	public GameObject endAudio2;

	public GameObject whiteWall;

	bool canTake = false;

	// Use this for initialization
	void Start () {

		pendant3.SetActive (false);
	
	}
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Player") && canTake) {
			pendant2.SetActive (false);
			pendant3.SetActive (true);
			wolfGod.SetActive (true);
			wolfSick.SetActive (false); 
			whiteWall.GetComponent<UnityEngine.UI.Image>().CrossFadeAlpha(255, 30, false);
			endAudio1.SendMessage("itsPlaytime");
			endAudio2.SendMessage("itsPlaytime");
		}
	}

	void takeIt(){
		canTake = true;
	}
}
