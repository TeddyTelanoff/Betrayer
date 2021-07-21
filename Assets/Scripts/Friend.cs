using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend: MonoBehaviour
{
	[Header("Rocket")]
	public float baseSpeed;

	public Vector3 dest => Game.instance.player.transform.position;
	public float speed => baseSpeed * Time.deltaTime;

	private void Awake() =>
		Game.instance.friends.Add(this);

	private void OnDestroy() =>
		Game.instance.friends.Remove(this);

	private void FixedUpdate()
	{
		var dist = dest - transform.position;
		if (dist.magnitude < speed)
			transform.position = dest;
		else
			transform.position += dist.normalized * speed;
		if (dist.magnitude > .1)
			transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg - 90);


		if (Game.instance.enemies.Count == 0)
			Game.instance.Betray();
	}
}
