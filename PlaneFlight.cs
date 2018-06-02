using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFlight : MonoBehaviour {

	public float lift;
	public float acceleration;
	public float rotationvert;
	public float rotationhori;
	public float elevator;
	public float leftright;
	GameObject ElevatorLeft;
	GameObject ElevatorRight;
	GameObject ElevatorMiddle;
	float speed;
	Rigidbody rb;
	public float clampAngle = 90.0f;
	private float rotX = 0.0f; // rotation around the up/y axis
	private float rotY = 0.0f; // rotation around the right/x axis
	public float mouseSensitivity = 100.0f;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		ElevatorLeft = GameObject.Find ("ElevatorLeft");
		ElevatorRight = GameObject.Find ("ElevatorRight");
		ElevatorMiddle = GameObject.Find ("ElevatorMiddle");
		Vector3 rot = transform.localRotation.eulerAngles;
		rotX = rot.x;
		rotY = rot.y;
	}


	void FixedUpdate () {
		
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = -Input.GetAxis("Mouse Y");

		rotX += mouseX * mouseSensitivity * Time.deltaTime;
		rotY += mouseY * mouseSensitivity * Time.deltaTime;

		speed = Vector3.Project(rb.velocity, new Vector3(0,0,1)).magnitude;
		if (Input.GetKey (KeyCode.W)) {
			rb.AddRelativeForce (transform.up * acceleration);
			Debug.Log ("W");
		}

		if (Input.GetKey (KeyCode.S) && Vector3.Project(rb.velocity, new Vector3(0,0,1)).magnitude > 10) {
			rb.AddRelativeForce (-1 * acceleration, 0, 0);
		}



		rb.AddRelativeForce ( 0,0,speed * speed * lift);





		rb.AddForceAtPosition(
			-transform.forward * leftright * Input.GetAxis("Horizontal") * speed,
			ElevatorRight.transform.position
		);

		rb.AddForceAtPosition(
			-transform.forward * leftright * -Input.GetAxis("Horizontal") * speed,
			ElevatorLeft.transform.position
		);

		rb.AddForceAtPosition(
			-transform.right * elevator * -mouseY * speed,
			ElevatorMiddle.transform.position
		);
	}
}
