using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Budump: MonoBehaviour
{
	public float beet;
	public float kapow;
	public float off;
	public float sin => Mathf.Sin(Time.time * beet) * kapow;

	private void Start() =>
		off = transform.position.y;

	private void FixedUpdate()
	{
		var pos = transform.position;
		transform.position = new Vector3(pos.x, off + sin, pos.z);
	}
}
