using UnityEngine;

public class CameraDrag : MonoBehaviour
{
	[SerializeField]
	float smoothAmount = 10f;

	[HideInInspector]
	public bool isMenuOpen;

	bool isMoving;

	Vector3 hitPosition;
	Vector3 currentPosition;
	Vector3 cameraPosition;
	Vector3 targetPosition;

	void Update()
	{ 
		if (!isMenuOpen)
		{
			if (Input.GetMouseButtonDown(0))
			{
				hitPosition = Input.mousePosition;
				cameraPosition = transform.position;
			}

			if (Input.GetMouseButton(0))
			{
				currentPosition = Input.mousePosition;
				LeftMouseDrag();
				isMoving = true;
			}

			if (isMoving)
			{
				transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothAmount);

				if (transform.position == targetPosition)
				{
					isMoving = false;
				}
			}
		}
	}

	void LeftMouseDrag()
	{
		Vector3 direction = Camera.main.ScreenToWorldPoint(currentPosition) - Camera.main.ScreenToWorldPoint(hitPosition);

		direction = -direction;

		targetPosition = cameraPosition + direction;
	}
	void OnMenuButton()
	{
		isMenuOpen = (isMenuOpen == true ? false : true);
	}
	private void OnEnable()
	{
		Menu.MenuButton += OnMenuButton;
	}
	private void OnDisable()
	{
		Menu.MenuButton -= OnMenuButton;
	}
}