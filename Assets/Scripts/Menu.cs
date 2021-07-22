using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu: MonoBehaviour
{
	public void WhatsOnTheMenu() =>
		SceneManager.LoadScene(1);
	public void Noob() =>
		SceneManager.LoadScene(2);
}
