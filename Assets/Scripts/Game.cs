using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance { get; private set; }

	[Header("Prefabs")]
	public GameObject misilePrefab;
	public GameObject friendPrefab;
	public GameObject enemyPrefab;

	[Header("Celebrities")]
	public Player player;
	public Camera cam;

	[Header("Settings")]
	public int startEnemies;

	[Header("Data - Do not change!")]
	public bool betraying;
	public List<Enemy> enemies = new List<Enemy>();
	public List<Friend> friends = new List<Friend>();

	private void Awake() =>
		instance = this;

	private void Start()
	{
		for (int i = 0; i < startEnemies; i++)
			Instantiate(enemyPrefab);
	}

	public Vector3 MousePos()
	{
		var pxCoord = Input.mousePosition;
		var pos = cam.ScreenToWorldPoint(pxCoord);
		pos.z = 0;
		return pos;
	}

	public void Betray()
	{
		IEnumerator Coroutine()
		{
			if (betraying)
				yield break;

			betraying = true;

			int fcount = friends.Count;
			var positions = new Vector3[fcount];
			for (int i = 0; i < fcount; i++)
				positions[i] = friends[i].transform.position;

			while (friends.Count > 0)
			{
				Destroy(friends[0].gameObject);
				yield return null;
			}

			yield return new WaitForEndOfFrame();

			for (int i = 0; i < fcount; i++)
			{
				var obj = Instantiate(enemyPrefab);
				obj.transform.position = positions[i];
			}

			Instantiate(enemyPrefab);

			betraying = false;
		}

		StartCoroutine(Coroutine());
	}
}
