using UnityEngine;

public class ScreenBase : MonoBehaviour
{
    [SerializeField] private string screenId;

    void Start()
    {
        if (string.IsNullOrEmpty(screenId))
            screenId = gameObject.name;

        // Регистрируем экран в UIManager
        if (UIManager.Instance != null)
        {
            UIManager.Instance.RegisterScreen(screenId, gameObject);
        }
        else
        {
            Debug.LogError("UIManager not found!");
        }

        if (screenId == "home")
            gameObject.SetActive(true);

    }
}