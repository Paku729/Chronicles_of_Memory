using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleLabel;
    [SerializeField] private Image coverImage;
    [SerializeField] private GameObject soonTag;


    public event System.Action<StoryData> OnCardClicked;

    private StoryData currentStory;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (currentStory == null)
            {
                Debug.LogError("StoryCard: currentStory = null! Не вызывался SetData?");
                return;
            }
            OnCardClicked?.Invoke(currentStory);
        });
    }

    public void SetData(StoryData story)
    {
        if (story == null)
        {
            Debug.LogError("StoryCard: SetData получил null!");
            return;
        }

        currentStory = story;

        // Название
        if (titleLabel != null)
            titleLabel.text = story.title;

        // Тег "Скоро"
        if (soonTag != null)
            soonTag.SetActive(!story.isActive);

        // Цвет фона
        if (coverImage != null)
        {
            switch (story.coverColor)
            {
                case "cover-1":
                    coverImage.color = new Color(0.15f, 0.29f, 0.29f);
                    break;
                case "cover-2":
                    coverImage.color = new Color(0.23f, 0.18f, 0.32f);
                    break;
                case "cover-3":
                    coverImage.color = new Color(0.17f, 0.23f, 0.32f);
                    break;
                default:
                    coverImage.color = new Color(0.2f, 0.2f, 0.3f);
                    break;
            }
        }
    }
}