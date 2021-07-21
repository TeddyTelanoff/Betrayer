using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
	[Header("Rocket")]
	public float baseSpeed;

	public Vector3 dest => Game.instance.player.transform.position;
	public float speed => baseSpeed * Time.deltaTime;

	private void Awake() =>
		Game.instance.enemies.Add(this);

	private void OnDestroy() =>
		Game.instance.enemies.Remove(this);

	private void FixedUpdate()
	{
		var dist = dest - transform.position;
		if (dist.magnitude < speed)
			transform.position = dest;
		else
			transform.position += dist.normalized * speed;
		if (dist.magnitude > .1)
			transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg - 90);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Kabloowi"))
			TurnGood();
		else if (other.gameObject.layer == LayerMask.NameToLayer("Friend"))
			TurnGood();
	}

	public void TurnGood()
	{
		Debug.Log("Well, maybe I don't want to be the badguy anymore...");

		IEnumerator Coroutine()
		{
			yield return new WaitForEndOfFrame();

			var obj = Instantiate(Game.instance.friendPrefab);
			obj.transform.position = transform.position;

			Destroy(gameObject);
		}

		StartCoroutine(Coroutine());
	}
}
