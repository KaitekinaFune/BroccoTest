using UnityEngine;

[RequireComponent(typeof(CameraClamp))]
[RequireComponent(typeof(CameraDrag))]
[RequireComponent(typeof(CameraClamp))]
public class CameraController : MonoBehaviour
{
    private CameraClamp clamp;
    private CameraDrag drag;
    private CameraZoom zoom;

	private bool canMove = true;

    private void Start()
    {
        clamp = GetComponent<CameraClamp>();
        drag = GetComponent<CameraDrag>();
        zoom = GetComponent<CameraZoom>();
    }

	private void Update()
	{
		if (canMove)
		{
			GetInput();
		}
	}
	private void GetInput()
	{
		if (MouseInGameZone())
		{
			if (Input.GetMouseButtonDown(0))
			{
				drag.OnPress();
			}

			if (Input.GetMouseButton(0))
			{
				drag.OnHold();
			}

			if (Input.GetAxis("Mouse ScrollWheel") != 0f)
			{
				zoom.Zoom(Input.GetAxis("Mouse ScrollWheel"));
				clamp.CalculateScreenSize();
			}
		}
	}

	private bool MouseInGameZone()
	{
		return
			(Input.mousePosition.x >= 0 && Input.mousePosition.y >= 0) &&
			(Input.mousePosition.x <= Screen.width && Input.mousePosition.y <= Screen.height);
	}
	private void OnMenuButton()
	{
		canMove = (canMove != true);
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
