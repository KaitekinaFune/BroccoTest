using UnityEngine;


public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    private float minCamSize, maxCamSize;

    public void Zoom(float zoomAmount)
    {
        float newSize = Camera.main.orthographicSize - zoomAmount;

        Camera.main.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }

}
