using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    public static event System.Action MenuButton;
    public TextMeshProUGUI tileText;

    public GameObject gameMenu;

    public void OnMenuButtonPressed()
	{
        MenuButton?.Invoke();
        gameMenu.SetActive(gameMenu.activeSelf ? false : true);

        if (gameMenu.activeSelf)
		{
            GetTopLeftTile();
		}
	}

    void GetTopLeftTile()
	{
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(0, Screen.height, 0));

		if (Physics.Raycast(ray, out RaycastHit hitInfo))
		{
			tileText.SetText(hitInfo.collider.name);
		}
	}
}
