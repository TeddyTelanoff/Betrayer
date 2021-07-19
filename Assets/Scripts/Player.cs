using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
	public static Player main { get; private set; }

	[Header("Controller")]
	public Rigidbody rb;
	public float speed;
	public float dashSpeed;
	public float dashDuration;
	public float dashRefuel;
	public float flipSpeed;
	public float turnSpeed;

	[Header("Cannon")]
	public Transform cannon;
	public GameObject laserPrefab;

	private float rotX;
	private float dashLeftRatio => 1 - dashLeft / dashDuration;
	private float dashLeft;
	private float dashRefuelLeft;

	private void Awake() =>
		main = this;

	private void Start() =>
		dashLeft = dashDuration;

	private void FixedUpdate()
	{
		float accel = speed;
		if (Input.GetKey(KeyCode.LeftShift) && dashLeft > 0)
		{
			accel = dashSpeed;
			dashLeft -= Time.deltaTime;
			dashRefuelLeft = dashLeftRatio * dashRefuel;
		}
		else if (dashRefuelLeft > 0)
		{
			dashRefuelLeft -= Time.deltaTime;
			if (dashRefuelLeft <= 0)
				dashLeft = dashDuration;
		}

		rb.AddForce(accel * transform.forward * Time.deltaTime, ForceMode.Acceleration);

		float turnX = -Input.GetAxis("Vertical");
		float turnY = Input.GetAxis("Horizontal");

		rb.AddRelativeTorque(new Vector3(0, turnY) * turnSpeed * Time.deltaTime, ForceMode.VelocityChange);
		var euler = rb.rotation.eulerAngles;
		rotX += turnX * flipSpeed * Time.deltaTime;
		if (rotX > 360)
			rotX -= 360;
		else if (rotX < -260)
			rotX += 360;

		if (rotX < -80)
			rotX = -80;
		else if (rotX > 80)
			rotX = 80;

		rb.rotation = Quaternion.Euler(rotX, euler.y, 0);

		if (Input.GetKey(KeyCode.Space))
			Shoot();
	}

	private void Shoot()
	{
		var obj = Instantiate(laserPrefab);
		obj.transform.position = cannon.position;
		obj.transform.rotation = cannon.rotation;
	}
}
