using UnityEngine;

public class CameraDrag : MonoBehaviour
{
	[SerializeField]
	private float smoothAmount = 10f;

	private bool isMoving;

	private Vector3 hitPosition;
	private Vector3 cameraPosition;
	private Vector3 targetPosition;

	private void Update()
	{ 
		if (isMoving)
		{
			transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothAmount);
			if (transform.position == targetPosition)
			{
				isMoving = false;
			}
		}
	}

	public void OnPress()
	{
		hitPosition = Input.mousePosition;
		cameraPosition = transform.position;
	}

	public void OnHold()
	{
		Vector3 currentPosition = Input.mousePosition;
		Vector3 direction = Camera.main.ScreenToWorldPoint(currentPosition) - Camera.main.ScreenToWorldPoint(hitPosition);

		isMoving = true;

		direction = -direction;

		targetPosition = cameraPosition + direction;
	}
}