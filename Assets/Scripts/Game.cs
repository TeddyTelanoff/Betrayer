using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance { get; private set; }

	public Camera cam;

	private void Awake() =>
		instance = this;

	public Vector3 MousePos()
	{
		var pxCoord = Input.mousePosition;
		var pos = cam.ScreenToWorldPoint(pxCoord);
		pos.z = 0;
		return pos;
	}
}
