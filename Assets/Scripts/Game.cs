using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
	public Text lvlTxt;
	public GameObject losePanel;

	[Header("Settings")]
	public int startEnemies;
	public float bleepDuration;

	[Header("Data - Do not change!")]
	public int iteration;
	public bool betraying;
	public Vector3 playerStartPos;
	public List<Enemy> enemies = new List<Enemy>();
	public List<Friend> friends = new List<Friend>();

	private void Awake() =>
		instance = this;

	private void Start()
	{
		Time.timeScale = 1;
		if (player)
			playerStartPos = player.transform.position;

		iteration++;
		if (lvlTxt)
			lvlTxt.text = $"Level {iteration}";

		IEnumerator Coroutine()
		{
			yield return new WaitForSeconds(1.69f);
			for (int i = 0; i < startEnemies; i++)
				Instantiate(enemyPrefab);
		}

		StartCoroutine(Coroutine());
	}

	public void Exit() =>
		Application.Quit();

	public void Lose()
	{
		Time.timeScale = 0;
		losePanel.SetActive(true);
	}

	public void Menu()
	{
		Time.timeScale = 1;
		Debug.Log("menu");
		SceneManager.LoadSceneAsync(0);
	}

	public void ResetTimeline()
	{
		IEnumerator Coroutine()
		{
			iteration = 1;
			foreach (var friend in friends)
				Destroy(friend.gameObject);
			yield return null;
			foreach (var enemy in enemies)
				Destroy(enemy.gameObject);
			yield return null;

			lvlTxt.text = $"Level {iteration}";
			for (int i = 0; i < startEnemies; i++)
				Instantiate(enemyPrefab);
			yield return null;

			losePanel.SetActive(false);
			player.transform.position = playerStartPos;

			Time.timeScale = 1;
		}

		StartCoroutine(Coroutine());
	}

	public void Betray()
	{
		IEnumerator Coroutine()
		{
			if (betraying)
				yield break;

			betraying = true;
			iteration++;
			lvlTxt.text = $"Level {iteration}";

			int fcount = friends.Count;
			var positions = new Vector3[fcount];
			for (int i = 0; i < fcount; i++)
				positions[i] = friends[i].transform.position;

			while (friends.Count > 0)
			{
				Destroy(friends[0].gameObject);
				yield return new WaitForSeconds(bleepDuration);
			}

			for (int i = 0; i < fcount; i++)
			{
				var obj = Instantiate(enemyPrefab);
				obj.transform.position = positions[i];
				yield return new WaitForSeconds(bleepDuration);
			}

			Instantiate(enemyPrefab);

			betraying = false;
		}

		StartCoroutine(Coroutine());
	}

	public Vector3 MousePos()
	{
		var pxCoord = Input.mousePosition;
		var pos = cam.ScreenToWorldPoint(pxCoord);
		pos.z = 0;
		return pos;
	}
}
