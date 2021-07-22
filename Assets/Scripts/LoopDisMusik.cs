using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopDisMusik: MonoBehaviour
{
	public AudioSource audioSource;
	public float cuttoff;

	private void Start()
	{
		IEnumerator Coroutine()
		{
		Begin:
			yield return new WaitForSeconds(cuttoff);
			audioSource.time = 0;
			goto Begin;
		}

		StartCoroutine(Coroutine());
	}
}
