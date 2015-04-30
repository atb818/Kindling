using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public float speed;
	public float walkSpeed = 6f;
	public float sprintSpeed = 10f;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;

	public Transform cam;

	void Start(){
		speed = walkSpeed;

	}

	void Update() {

		if (Input.GetKey (KeyCode.W)) {
			transform.position = transform.forward * speed;
		}

		/*
		CharacterController controller = GetComponent<CharacterController>();

		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			//sprint
			if (Input.GetKeyDown(KeyCode.LeftShift)){
				speed = sprintSpeed;
			} else speed = walkSpeed;
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		*/

		//transform.rotation = Quaternion.Euler (new Vector3 (this.transform.rotation.x, cam.transform.rotation.y, this.transform.rotation.z));
		                           

	}

}
