using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
	[Header("Rocket")]
	public float sinStrength;
	public float sinStep;
	public float sinOff;
	public float baseSpeed;

	public float sin => Mathf.Sin((Time.time + sinOff) * sinStep) * sinStrength;
	public Vector3 dest => Game.instance.player.transform.position;
	public float speed => baseSpeed * Time.deltaTime;

	private void Awake() =>
		sinOff = Random.value;

	private void FixedUpdate()
	{
		var dist = dest - transform.position;
		float baseRot = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg - 90;
		float rot = sin + baseRot;
		transform.rotation = Quaternion.Euler(0, 0, rot);
		var dir = new Vector3(-Mathf.Sin(rot * Mathf.Deg2Rad), Mathf.Cos(rot * Mathf.Deg2Rad));
		transform.position += dir * speed;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Kabloowi"))
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}
