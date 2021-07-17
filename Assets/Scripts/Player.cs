using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
	[Header("Controller")]
	public Rigidbody rb;
	public float speed;
	public float flipSpeed;
	public float turnSpeed;

	[Header("Cannon")]
	public Transform cannon;
	public GameObject laserPrefab;

	private void FixedUpdate()
	{
		rb.AddForce(speed * transform.forward * Time.deltaTime, ForceMode.Acceleration);

		float turnX = -Input.GetAxis("Vertical");
		float turnY = Input.GetAxis("Horizontal");

		rb.AddRelativeTorque(new Vector3(0, turnY) * turnSpeed * Time.deltaTime, ForceMode.VelocityChange);
		var euler = rb.rotation.eulerAngles;
		rb.rotation = Quaternion.Euler(euler.x + turnX * flipSpeed * Time.deltaTime, euler.y, 0);

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
