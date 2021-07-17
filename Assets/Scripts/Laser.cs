using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser: MonoBehaviour
{
	public float suicideClock;
	public float speed;

	private void Start() =>
		StartCoroutine(TheWorstIsYetToHappen());

	private void FixedUpdate() =>
		transform.position += transform.forward * speed * Time.deltaTime;

	private IEnumerator TheWorstIsYetToHappen()
	{
		yield return new WaitForSeconds(suicideClock);
		Destroy(gameObject);
	}
}
