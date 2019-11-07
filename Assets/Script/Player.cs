using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	private Scene currentScene;
	private Rigidbody rigid;
	private CharacterController controller;
	public bool hideCursor = false;
	public LayerMask groundMask;
	public Transform groundCheck;
	public Transform respawnPoint;
	public float groundDistance = 0.4f;

	public float moveSpeed = 10f, maxVelocity = 20f;
	public float jumpHeight = 2f;
	public float rotationSpeed = 90f;
	public float gravity = -10f;

	private Vector3 velocity;
	public bool isGrounded;

	void Awake()
	{
		isGrounded = true;
		rigid = GetComponent<Rigidbody>();
		controller = GetComponent<CharacterController>();
	}

	void Start()
	{
		//Should the cursor be hidden
		if (hideCursor)
		{
			//hide it
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	// Update is called once per frame
	void Update()
	{
		//Create a sphere on the groundCheck Position and checking if it collide with the layermask
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		//To check if it is grounded or not AND will at the gravity * deltatime if player is not grounded so the player will start falling
		if (isGrounded && velocity.y < 0)
		{

			velocity.y = -2f;

		}

		float inputH = Input.GetAxis("Horizontal"); // Storing input value x axis
		float inputV = Input.GetAxis("Vertical");   //Storing input value z axis
													// Move the Player base on the local direction for left and up and diagonal
		Vector3 move = transform.right * inputH + transform.forward * inputV;
		//Move the player with Character controller component
		controller.Move(move * moveSpeed * Time.deltaTime);
		//Implement Jumping

		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			Jump();
		}
		float inputR = Input.GetAxis("Mouse X");
		Rotate(inputR);
		//Make the character fall down
		velocity.y += gravity * Time.deltaTime;
		//gravity 2*
		controller.Move(velocity * Time.deltaTime);
		Respawn();
	}
	private void OnTriggerEnter(Collider other)
	{
		//Did we hit an item?
		Item item = other.GetComponent<Item>();
		if (item)
		{
			//collect it!
			item.Collect();
		}
	}

	public void Jump()
	{
		isGrounded = false;
		velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
	}
	public void Rotate(float inputR)
	{
		//Rotating using Rigidbody (using Physics)
		float angle = inputR * rotationSpeed * Time.deltaTime;
		Quaternion rotation = rigid.rotation * Quaternion.AngleAxis(angle, Vector3.up);
		rigid.MoveRotation(rotation);

	}
	public void Respawn()
	{
		currentScene = SceneManager.GetActiveScene();
		if (transform.position.y < -7f)
		{
			SceneManager.LoadScene(currentScene.buildIndex);
		}


	}

}
