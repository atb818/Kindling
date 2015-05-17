using UnityEngine;
using System.Collections;

public class ENDIT : MonoBehaviour {

	bool gameOver = false;

	public GameObject player;

	void Start () {
	
	}

	void Update(){
		if (gameOver) {
			if (Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel ("Proto2");
			}
		}
	}
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			rollCredits();
		}
	}

	void rollCredits(){
		gameOver = true;
		player.SetActive (false);
	}
}
