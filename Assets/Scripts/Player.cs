using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
	[Header("Rocket")]
	public float baseSpeed;

	[Header("Data - Do not change!")]
	public Vector3 dest;
	public float speed => baseSpeed * Time.deltaTime;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			Fire();
	}

	private void FixedUpdate()
	{
		dest = Game.instance.MousePos();
		var dist = dest - transform.position;
		if (dist.magnitude < speed)
			transform.position = dest;
		else
			transform.position += dist.normalized * speed;
		if (dist.magnitude > .1)
			transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg - 90);
	}

	public void Fire()
	{
		IEnumerator Coroutine()
		{
			yield return new WaitForEndOfFrame();

			var obj = Instantiate(Game.instance.misilePrefab);
			obj.transform.position = transform.position;
			var misile = obj.GetComponent<Misile>();
			misile.baseRot = transform.rotation.eulerAngles.z;
		}

		StartCoroutine(Coroutine());
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
			Game.instance.Lose();
	}
}
