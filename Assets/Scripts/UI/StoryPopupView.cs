using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoryPopupView : MonoBehaviour
{
    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI genreText;
    [SerializeField] private TextMeshProUGUI seasonText;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button closeButton;

    private StoryData currentStory;

    void Start()
    {
        // Подписываемся на кнопки
        if (closeButton != null)
            closeButton.onClick.AddListener(Close);

        if (playButton != null)
            playButton.onClick.AddListener(OnPlayClicked);
    }

    public void SetData(StoryData story)
    {
        currentStory = story;

        if (titleText != null)
            titleText.text = story.title;

        if (descriptionText != null)
            descriptionText.text = story.description;

        if (genreText != null)
            genreText.text = string.Join(", ", story.genres);

        if (seasonText != null)
            seasonText.text = $"Сезон {story.season}";
    }

    private void Close()
    {
        UIManager.Instance.HideOverlay("storyPopup");
    }

    private void OnPlayClicked()
    {
        if (currentStory == null)
        {
            Debug.LogWarning("StoryPopup: нет данных для запуска!");
            return;
        }

        Debug.Log($"Запуск новеллы: {currentStory.title}");
        Close();

        // TODO: открыть экран чтения
    }
}
