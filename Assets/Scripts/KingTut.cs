using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KingTut: MonoBehaviour
{
	public GameObject[] tutorials;
	public GameObject backButton;
	public GameObject nextButton;
	public GameObject playButton;
	public int loc;

	public void Back()
	{
		tutorials[loc].SetActive(false);
		playButton.SetActive(false);
		nextButton.SetActive(true);
		loc--;
		if (loc <= 0)
			backButton.SetActive(false);
		tutorials[loc].SetActive(true);
	}

	public void Next()
	{
		tutorials[loc].SetActive(false);
		loc++;
		backButton.SetActive(true);
		if (loc >= tutorials.Length)
			SceneManager.LoadScene(1);
		else
		{
			if (loc >= tutorials.Length - 1)
			{
				playButton.SetActive(true);
				nextButton.SetActive(false);
			}
			
			tutorials[loc].SetActive(true);
		}
	}
}
