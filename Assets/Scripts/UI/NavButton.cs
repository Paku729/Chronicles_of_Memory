using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NavButton : MonoBehaviour
{
    [SerializeField] private string screenId; // home, echo, shop, profile, ads

    private void Start()
    {
        var btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (UIManager.Instance != null)
            UIManager.Instance.ShowScreen(screenId);
        else
            Debug.LogError("UIManager.Instance is null!");
    }
}