using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 0.3f;
    [SerializeField] private bool enableTransitions = true;

    [Header("Startup Screen")]
    [SerializeField] private string startScreenId = "home";

    private Dictionary<string, GameObject> screenRegistry = new Dictionary<string, GameObject>();
    private Stack<GameObject> screenStack = new Stack<GameObject>();

    public GameObject CurrentScreen { get; private set; }
    public event Action<string> OnScreenChanged;

    private bool isTransitioning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(ShowStartScreenAfterRegistration());
    }

    private IEnumerator ShowStartScreenAfterRegistration()
    {
        yield return null;

        if (screenRegistry.ContainsKey(startScreenId))
        {
            ShowScreen(startScreenId, false);
        }
        else
        {
            Debug.LogWarning($"Start screen '{startScreenId}' not registered!");
        }
    }

    public void RegisterScreen(string id, GameObject screen)
    {
        if (!screenRegistry.ContainsKey(id))
        {
            screenRegistry.Add(id, screen);
            screen.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Screen '{id}' already registered!");
        }
    }

    public void ShowScreen(string id, bool addToStack = true)
    {
        if (isTransitioning)
        {
            Debug.Log("Transition in progress, ignoring click.");
            return;
        }

        if (!screenRegistry.TryGetValue(id, out var newScreen))
        {
            Debug.LogError($"Screen '{id}' not registered!");
            return;
        }

        if (CurrentScreen == newScreen && newScreen.activeSelf)
            return;

        if (CurrentScreen != null && addToStack)
            screenStack.Push(CurrentScreen);

        if (enableTransitions)
        {
            StartCoroutine(SwitchScreenCoroutine(CurrentScreen, newScreen));
        }
        else
        {
            if (CurrentScreen != null)
                CurrentScreen.SetActive(false);

            newScreen.SetActive(true);
            CurrentScreen = newScreen;
            OnScreenChanged?.Invoke(id);
        }
    }

    private IEnumerator SwitchScreenCoroutine(GameObject oldScreen, GameObject newScreen)
    {
        isTransitioning = true;

        if (oldScreen != null)
            yield return StartCoroutine(FadeOut(oldScreen));

        yield return StartCoroutine(FadeIn(newScreen));

        CurrentScreen = newScreen;
        OnScreenChanged?.Invoke(GetScreenId(newScreen));

        isTransitioning = false;
    }

    private IEnumerator FadeIn(GameObject screen)
    {
        var canvasGroup = screen.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = screen.AddComponent<CanvasGroup>();

        screen.SetActive(true);
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;

        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / transitionDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    private IEnumerator FadeOut(GameObject screen)
    {
        var canvasGroup = screen.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            screen.SetActive(false);
            yield break;
        }

        canvasGroup.blocksRaycasts = false;

        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / transitionDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        screen.SetActive(false);
    }

    public void GoBack()
    {
        if (screenStack.Count == 0)
        {
            Debug.Log("No screens in stack to go back to.");
            return;
        }

        var previousScreen = screenStack.Pop();
        ShowScreen(GetScreenId(previousScreen), false);
    }

    private string GetScreenId(GameObject screen)
    {
        foreach (var kvp in screenRegistry)
        {
            if (kvp.Value == screen)
                return kvp.Key;
        }
        return null;
    }

    public void ClearStack()
    {
        screenStack.Clear();
    }
}