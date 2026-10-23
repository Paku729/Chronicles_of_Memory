using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Кнопка нижнего навигационного бара. Переключает экраны через UIManager
/// и сама держит активное состояние: при смене экрана подменяет
/// иконку на tab_*_active / tab_*_inactive.
/// </summary>
public class NavButton : MonoBehaviour
{
    [Header("Навигация")]
    [SerializeField] private string screenId; // home, echo, shop, profile, ads

    [Header("Активное состояние")]
    [SerializeField] private Image iconImage; // иконка вкладки (обычно тот же Image, что и у кнопки)
    [SerializeField] private Sprite spriteActive;   // tab_*_active
    [SerializeField] private Sprite spriteInactive; // tab_*_inactive

    private void Start()
    {
        var btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(OnClick);
        else
            Debug.LogError($"NavButton на объекте '{name}' без Button — клики не сработают!");

        // Если иконку не назначили в инспекторе — берём Image с самой кнопки
        if (iconImage == null)
            iconImage = GetComponent<Image>();

        if (UIManager.Instance != null)
        {
            // Событие могло пройти до подписки — подтягиваем текущий экран сразу
            UIManager.Instance.OnScreenChanged += HandleScreenChanged;
            ApplyState(UIManager.Instance.CurrentScreenId == screenId);
        }
    }

    private void OnDestroy()
    {
        if (UIManager.Instance != null)
            UIManager.Instance.OnScreenChanged -= HandleScreenChanged;
    }

    private void OnClick()
    {
        if (UIManager.Instance != null)
            UIManager.Instance.ShowScreen(screenId);
        else
            Debug.LogError("UIManager.Instance is null!");
    }

    private void HandleScreenChanged(string changedScreenId)
    {
        ApplyState(changedScreenId == screenId);
    }

    private void ApplyState(bool isActive)
    {
        // У центральной кнопки (самоцвет) активная/неактивная иконка одна и та же
        if (iconImage == null || spriteActive == null || spriteInactive == null)
            return;

        iconImage.sprite = isActive ? spriteActive : spriteInactive;
    }
}
