using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
	[Header("Controller")]
	public Rigidbody rb;
	public float speed;

	[Header("Cannon")]
	public float fireDist;
	public Transform cannon;
	public GameObject laserPrefab;

	private void FixedUpdate()
	{
		rb.AddForce(speed * transform.forward * Time.deltaTime, ForceMode.Acceleration);

		var dir = Player.main.transform.position - transform.position;
		transform.forward = dir;
		if (dir.magnitude < fireDist)
			Shoot();
	}

	private void Shoot()
	{
		var obj = Instantiate(laserPrefab);
		obj.transform.position = cannon.position;
		obj.transform.rotation = cannon.rotation;
	}
}
