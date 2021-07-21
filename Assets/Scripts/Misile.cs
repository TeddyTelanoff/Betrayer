using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misile: MonoBehaviour
{
	[Header("Rocket")]
	public float sinStrength;
	public float sinStep;
	public float sinOff;
	public float baseSpeed;
	public float baseRot;

	public float sin => Mathf.Sin((Time.time + sinOff) * sinStep) * sinStrength;
	public float speed => baseSpeed * Time.deltaTime;

	private void FixedUpdate()
	{
		float rot = sin + baseRot;
		transform.rotation = Quaternion.Euler(0, 0, rot);
		var dir = new Vector3(-Mathf.Sin(rot * Mathf.Deg2Rad), Mathf.Cos(rot * Mathf.Deg2Rad));
		transform.position += dir * speed;
	}
}
