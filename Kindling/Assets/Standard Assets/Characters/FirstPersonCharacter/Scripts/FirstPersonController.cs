using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;

		public bool tilting = false;
		public bool digging = false;
		public GameObject playerCam;
		bool mouseLock = true;
		string state = "neutral";
		bool justListened = false;
		bool justDug = false;
		public GameObject digParticles;
		

        // Use this for initialization
        private void Start()
        {
			//Init mouse lock
			Cursor.lockState = CursorLockMode.Locked;

            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);
        }


        // Update is called once per frame
        private void Update()
        {

			//Mouse lock toggle
			if (Input.GetKeyDown(KeyCode.T)){
				if (mouseLock){
					Cursor.lockState = CursorLockMode.None;
					mouseLock = false;
					Cursor.visible = true;
					Debug.Log("Mouse Lock OFF");
				} else if (mouseLock == false){
					Cursor.lockState = CursorLockMode.Locked;
					mouseLock = true;
					Cursor.visible = false;
					Debug.Log ("Mouse Lock ON");
				}
			}

			speedManager ();

            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }


        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;


            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }
		private Vector3 headChill = Vector3.zero;

        private void RotateView()
		{
			//Define them as current rotation so we don't swing to 0,0,0. Then set tilt/reset amt.
			Quaternion headRot = playerCam.transform.rotation;
			//Vector3 headChill;// = headRot.eulerAngles;
			Vector3 _headTilt = headRot.eulerAngles;
			Vector3 headDig = headRot.eulerAngles;
			//headChill.z = 0;
			//headChill.x = 0;
			_headTilt.z = 20;
			headDig.x = 50;

			Debug.Log (state);

			//CONTROLS PROTOTYPE 2 -- 

			switch (state) {
			case "neutral":
				//change state
				if (Input.GetKey (KeyCode.Q)) {
					headChill = headRot.eulerAngles;
					state = "listening";
				} else if (Input.GetKey (KeyCode.E)) {
					headChill = headRot.eulerAngles;
					state = "digging";
				}
				if (justListened){
					if(headRot.eulerAngles.z > 0.1f){
						playerCam.transform.rotation = Quaternion.Slerp (headRot, Quaternion.Euler (headChill), .2f);
					}
					else {
						justListened = false;
					}
				} else if (justDug){
					if(headRot.eulerAngles.x > 0.1f){
						playerCam.transform.rotation = Quaternion.Slerp (headRot, Quaternion.Euler (headChill), .2f);
					}
					else {
						justDug = false;
					}
				} else {
					m_MouseLook.LookRotation (transform, m_Camera.transform);
					digParticles.SetActive(false);
				}
				break;
			case "listening":
				//cock head
				playerCam.transform.rotation = Quaternion.Slerp (headRot, Quaternion.Euler (_headTilt), .2f);
				//change back to neutral
				if (Input.GetKeyUp (KeyCode.Q)){
					justListened = true;
					state = "neutral";
				}
				break;
			case "digging":
				//look down
				playerCam.transform.rotation = Quaternion.Slerp (headRot, Quaternion.Euler (headDig), .2f);
				//particle effect
				digParticles.SetActive(true);
				//change back to neutral
				if (Input.GetKeyUp (KeyCode.E)){
					digParticles.SetActive(false);
					justDug = true;
					state = "neutral";
				}
				break;
			case "squirrel":
				//look at squirrel
				break;
			}

			//CONTROLS PROTOTYPE 1 --

			//KEY INPUTS, change state
			/*
			if (Input.GetKey (KeyCode.Q)) {
				state = "listening";
			} else if (Input.GetKey (KeyCode.E)) {
				state = "digging";
			} else {
				state = "neutral";
			}
			
			//if (key)
				//state = blah

			//case "blah"
				//do stuff

			//LISTENING

			//First determine if we're pressing tilt
			if (Input.GetKey (KeyCode.Q)) tilting = true;
			else tilting = false;


			//If pressing Q, do the tilt
			if(tilting){
				playerCam.transform.rotation = Quaternion.Slerp (headRot, Quaternion.Euler (_headTilt), .2f);
			}
			//Otherwise, return to neutral before going back to FPS mode.
			else {
				if(headRot.eulerAngles.z > 0.1f){
					playerCam.transform.rotation = Quaternion.Slerp (headRot, Quaternion.Euler (headChill), .2f);
				}
				else {
					m_MouseLook.LookRotation (transform, m_Camera.transform);
				}
			}

			//DIGGING
			
			//First determine if we're pressing dig
			if (Input.GetKey (KeyCode.E)) {
				digging = true;
				playerCam.transform.rotation = Quaternion.Slerp (headRot, Quaternion.Euler (headDig), .2f);
			} else {
				if (digging && headRot.eulerAngles.x > 0.1f) {
					playerCam.transform.rotation = Quaternion.Slerp (headRot, Quaternion.Euler (headChill), .2f);
				} else if (digging){
					digging = false;
					m_MouseLook.Init(transform , m_Camera.transform);
				}
			}

			if (!digging) {
				m_MouseLook.LookRotation (transform, m_Camera.transform);
			}
			*/


        }

		void speedManager(){
			if (Input.GetKey (KeyCode.S)) {
				m_WalkSpeed = 2.5f;
			} else {m_WalkSpeed = 5;}
		}

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
