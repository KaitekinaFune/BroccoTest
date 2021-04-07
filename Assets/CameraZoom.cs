using UnityEngine;

[RequireComponent(typeof(CameraClamp))]
[RequireComponent(typeof(CameraDrag))]
public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    private float minCamSize, maxCamSize;

    CameraClamp clamp;
    CameraDrag drag;

	private void Start()
	{
        clamp = GetComponent<CameraClamp>();
        drag = GetComponent<CameraDrag>();
	}

	void Update()
    {
        if (!drag.isMenuOpen)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                Zoom(Input.GetAxis("Mouse ScrollWheel"));
            }
        }
    }

    void Zoom(float zoomAmount)
    {
        float newSize = Camera.main.orthographicSize - zoomAmount;

        Camera.main.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
        clamp.CalculateScreenSize();
    }

}
