using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopDisMusik: MonoBehaviour
{
	public AudioSource audioSource;
	public float start;
	public float cuttoff;

	private void Start()
	{
		IEnumerator Coroutine()
		{
		Begin:
			audioSource.time = start;
			yield return new WaitForSeconds(cuttoff - start);
			goto Begin;
		}

		StartCoroutine(Coroutine());
	}
}
