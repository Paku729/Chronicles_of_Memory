using UnityEngine;

public class CarouselController : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform contentContainer;
    [SerializeField] private StoryPopupView popupView;
    private void Awake()
    {
        LoadStories();
    }

    private void LoadStories()
    {
        var stories = StoryRepository.GetStories();
        foreach (var story in stories)
        {
            var cardGO = Instantiate(cardPrefab, contentContainer);
            var card = cardGO.GetComponent<StoryCard>();

            if (card == null)
            {
                Debug.LogError($"На объекте {cardGO.name} нет компонента StoryCard!");
                continue;
            }

            card.SetData(story);
            card.OnCardClicked += OnStoryCardClicked;
        }
    }

    private void OnStoryCardClicked(StoryData story)
    {
        Debug.Log($"Выбрана новелла: {story.title}");

        // Передаём данные в попап
        if (popupView != null)
        {
            popupView.SetData(story);
        }

        // Открываем оверлей через UIManager
        UIManager.Instance.ShowOverlay("storyPopup");
    }
}