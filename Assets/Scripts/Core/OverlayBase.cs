using UnityEngine;

public class OverlayBase : MonoBehaviour
{
    [SerializeField] private string overlayId;

    void Start()
    {
        if (string.IsNullOrEmpty(overlayId))
            overlayId = gameObject.name;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.RegisterOverlay(overlayId, gameObject);
        }
        else
        {
            Debug.LogError("UIManager not found!");
        }
    }
}