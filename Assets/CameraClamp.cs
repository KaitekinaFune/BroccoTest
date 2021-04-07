using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
	Vector2 screenSizeInWorldUnits;
	float initialCameraHeight = 10f;

	void Start()
	{
		CalculateScreenSize();
	}

	void LateUpdate()
	{
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, MapBuilder.min.x + screenSizeInWorldUnits.x, MapBuilder.max.x - screenSizeInWorldUnits.x),
			initialCameraHeight,
			Mathf.Clamp(transform.position.z, MapBuilder.min.y + screenSizeInWorldUnits.y, MapBuilder.max.y - screenSizeInWorldUnits.y));
	}
	public void CalculateScreenSize()
	{
		screenSizeInWorldUnits.x = (Camera.main.aspect * Camera.main.orthographicSize);
		screenSizeInWorldUnits.y = Camera.main.orthographicSize;
	}
}
