using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Shared_Notes_Manager : MonoBehaviour
{
   // Title and Description Content
    [SerializeField] private TMP_Text lectureTitleText;
    [SerializeField] private CanvasGroup lectureCanvasGroup;
    [SerializeField] private float lectureDisplaySeconds = 5f;
    [SerializeField] private string lectureTitle = "The Role of Media in Modern Society";
    [SerializeField] private string lectureSentence = "Media shapes culture, politics, and public opinion through information and influence.";

    // Title and Description Transition Timings
    [SerializeField] private float titleFadeInSeconds = 0.5f;
    [SerializeField] private float titleFadeOutSeconds = 0.5f;
    [SerializeField] private float lectureSentenceDisplaySeconds = 3f;
    [SerializeField] private float sentenceFadeInSeconds = 0.5f;
    [SerializeField] private float sentenceFadeOutSeconds = 0.5f;

    // Text Fragment Content
    [SerializeField] private string[] correctKeywords = new string[5];
    [SerializeField] private string[] wrongKeywords = new string[10];

    // Text Fragment Layout and Prefab
    [SerializeField] private GameObject textFragmentPrefab;
    [SerializeField] private RectTransform fragmentsParent;
    [SerializeField] private int layoutRows = 5;
    [SerializeField] private int layoutCols = 2;
    [SerializeField] private float layoutXStart = -150f;
    [SerializeField] private float layoutYStart = 400f;
    [SerializeField] private float layoutXSpacing = 400f;
    [SerializeField] private float layoutYSpacing = 150f;

    // Notebook Slots (must be exactly 5)
    [SerializeField] private RectTransform[] notebookSlots = new RectTransform[5];


    [Header("Feedback UI (Optional)")]
    [SerializeField] private TMP_Text statusText;

    [Header("Result Screens")]
    [Tooltip("Shown when the player wins.")]
    [SerializeField] private GameObject winScreen;
    [Tooltip("Shown when the player loses.")]
    [SerializeField] private GameObject loseScreen;

    [Header("Selection Animation")]
    [SerializeField] private float moveToSlotSeconds = 0.35f;

    private readonly List<TextFragment> spawnedFragments = new();
    private readonly List<TextFragment> placedFragments = new();

    // Tracks original layout info and whether a fragment is currently on the notebook.
    private class FragmentState
    {
        public RectTransform originalParent;
        public Vector2 originalPosition;
        public bool isOnNotebook;
    }

    private readonly Dictionary<TextFragment, FragmentState> fragmentStates = new();
    private int selectedCount = 0;
    private bool isLocked = false;

    private void Start()
    {
        // Reset runtime state in case this object is reused.
        isLocked = false;
        selectedCount = 0;
        placedFragments.Clear();

        SetLectureVisible(true);
        SetStatus("");

        // Ensure result screens are hidden at the start.
        if (winScreen != null) winScreen.SetActive(false);
        if (loseScreen != null) loseScreen.SetActive(false);

        SetFragmentsInteractable(false);
        SetFragmentsVisible(false);

        SpawnFragments();

        StartCoroutine(LectureThenPlay());
    }

    private IEnumerator LectureThenPlay()
    {
        // Sequence (single TMP reused):
        // fade in -> TITLE -> fade out -> fade in -> SENTENCE -> fade out -> then reveal fragments.
        if (lectureTitleText != null)
        {
            // TITLE
            lectureTitleText.text = lectureTitle;
            lectureTitleText.alpha = 0f;

            float titleFadeIn = Mathf.Max(0f, titleFadeInSeconds);
            float titleHold = Mathf.Max(0f, lectureDisplaySeconds);
            float titleFadeOut = Mathf.Max(0f, titleFadeOutSeconds);

            if (titleFadeIn > 0f) yield return FadeTMPAlpha(lectureTitleText, 0f, 1f, titleFadeIn);
            else lectureTitleText.alpha = 1f;

            if (titleHold > 0f) yield return new WaitForSeconds(titleHold);

            if (titleFadeOut > 0f) yield return FadeTMPAlpha(lectureTitleText, 1f, 0f, titleFadeOut);
            else lectureTitleText.alpha = 0f;

            // SENTENCE (same TMP object)
            lectureTitleText.text = lectureSentence;
            lectureTitleText.alpha = 0f;

            float sentenceFadeIn = Mathf.Max(0f, sentenceFadeInSeconds);
            float sentenceHold = Mathf.Max(0f, lectureSentenceDisplaySeconds);
            float sentenceFadeOut = Mathf.Max(0f, sentenceFadeOutSeconds);

            if (sentenceFadeIn > 0f) yield return FadeTMPAlpha(lectureTitleText, 0f, 1f, sentenceFadeIn);
            else lectureTitleText.alpha = 1f;

            if (sentenceHold > 0f) yield return new WaitForSeconds(sentenceHold);

            if (sentenceFadeOut > 0f) yield return FadeTMPAlpha(lectureTitleText, 1f, 0f, sentenceFadeOut);
            else lectureTitleText.alpha = 0f;
        }

        SetLectureVisible(false);
        SetFragmentsVisible(true);
        SetFragmentsInteractable(true);
    }

    private void SpawnFragments()
    {
        if (textFragmentPrefab == null || fragmentsParent == null)
        {
            Debug.LogError("SharedNotesGameController: Assign textFragmentPrefab and fragmentsParent.");
            return;
        }

        if (notebookSlots == null || notebookSlots.Length != 5)
        {
            Debug.LogError("SharedNotesGameController: notebookSlots must have exactly 5 slots.");
            return;
        }

        if (correctKeywords == null || correctKeywords.Length != 5)
        {
            Debug.LogError("SharedNotesGameController: correctKeywords must have exactly 5 entries.");
            return;
        }

        if (wrongKeywords == null || wrongKeywords.Length < 5)
        {
            Debug.LogError("SharedNotesGameController: redHerringKeywords must have at least 5 entries.");
            return;
        }

        // Clean up any previous run (if this object is reused).
        foreach (var frag in spawnedFragments)
        {
            if (frag != null) Destroy(frag.gameObject);
        }
        spawnedFragments.Clear();
        fragmentStates.Clear();

        var words = new List<(string word, bool isCorrect)>(10);
        for (int i = 0; i < 5; i++)
            words.Add((correctKeywords[i], true));

        var sampledWrong = SampleWithoutReplacement(wrongKeywords, 5);
        for (int i = 0; i < sampledWrong.Count; i++)
            words.Add((sampledWrong[i], false));

        Shuffle(words);

        for (int i = 0; i < words.Count; i++)
        {
            // Orderly layout (rows x cols)
            int rows = Mathf.Max(1, layoutRows);
            int cols = Mathf.Max(1, layoutCols);
            int max = Mathf.Min(rows * cols, words.Count);
            if (i >= max) break;

            int col = i / rows;
            int row = i % rows;
            if (col >= cols) break;

            Vector2 pos = new Vector2(
                layoutXStart + (col * layoutXSpacing),
                layoutYStart - (row * layoutYSpacing)
            );

            GameObject go = Instantiate(textFragmentPrefab, fragmentsParent);
            // Important: keywords must stay invisible until the title+sentence sequence is finished.
            go.SetActive(false);

            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt != null) rt.anchoredPosition = pos;

            TextFragment fragment = go.GetComponent<TextFragment>();
            if (fragment == null)
            {
                Debug.LogError("SharedNotesGameController: textFragmentPrefab must have a TextFragment component.");
                Destroy(go);
                continue;
            }

            fragment.Initialize(words[i].word, words[i].isCorrect, OnFragmentClicked);
            fragment.SetInteractable(false);
            spawnedFragments.Add(fragment);

            // Remember the original parent/position so we can return it from the notebook.
            if (rt != null)
            {
                fragmentStates[fragment] = new FragmentState
                {
                    originalParent = rt.parent as RectTransform,
                    originalPosition = rt.anchoredPosition,
                    isOnNotebook = false
                };
            }
        }
    }

    private void OnFragmentClicked(TextFragment fragment)
    {
        if (isLocked || fragment == null)
            return;

        // If this fragment is already on the notebook, send it back to the blackboard.
        if (fragmentStates.TryGetValue(fragment, out FragmentState state) && state.isOnNotebook)
        {
            fragment.SetInteractable(false);
            StartCoroutine(MoveFragmentBackToBlackboard(fragment));
            return;
        }

        // Otherwise, move it from the blackboard into the next available notebook slot.
        fragment.SetInteractable(false);

        if (selectedCount >= 5)
            return;

        RectTransform slot = notebookSlots[selectedCount];
        selectedCount++;
        placedFragments.Add(fragment);

        StartCoroutine(MoveFragmentToSlot(fragment, slot));

    }

    /// <summary>
    /// Called by the Submit button. Evaluates the 5 selected fragments:
    /// - If any selected fragment is incorrect -> game over.
    /// - If all 5 are correct -> win.
    /// If fewer than 5 fragments are selected, shows a hint and does nothing.
    /// </summary>
    public void SubmitSelection()
    {
        if (isLocked)
            return;

        if (selectedCount < 5)
        {
            SetStatus("Select 5 keywords before submitting.");
            return;
        }

        bool allCorrect = true;
        for (int i = 0; i < placedFragments.Count; i++)
        {
            if (placedFragments[i] == null)
                continue;

            if (!placedFragments[i].isCorrect)
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
            Win();
        else
            Fail();
    }

    private IEnumerator MoveFragmentToSlot(TextFragment fragment, RectTransform slot)
    {
        if (fragment == null || slot == null)
            yield break;

        RectTransform fragRT = fragment.GetComponent<RectTransform>();
        if (fragRT == null)
            yield break;

        // Compute slot position in fragment parent's local space.
        Canvas rootCanvas = fragRT.GetComponentInParent<Canvas>();
        Camera cam = rootCanvas != null ? rootCanvas.worldCamera : null;

        Vector2 slotScreen = RectTransformUtility.WorldToScreenPoint(cam, slot.TransformPoint(Vector3.zero));
        RectTransform fragParentRT = fragRT.parent as RectTransform;

        if (fragParentRT == null)
            yield break;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            fragParentRT,
            slotScreen,
            cam,
            out Vector2 endPos
        );

        Vector2 start = fragRT.anchoredPosition;
        float duration = Mathf.Max(0.01f, moveToSlotSeconds);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
            fragRT.anchoredPosition = Vector2.Lerp(start, endPos, t);
            yield return null;
        }

        // Snap and re-parent into the notebook so it stays aligned.
        fragRT.SetParent(slot.parent, false);
        fragRT.anchorMin = slot.anchorMin;
        fragRT.anchorMax = slot.anchorMax;
        fragRT.pivot = slot.pivot;
        fragRT.anchoredPosition = slot.anchoredPosition;
        fragRT.sizeDelta = slot.sizeDelta;
        fragRT.localRotation = Quaternion.identity;
        fragRT.localScale = Vector3.one;
        fragRT.SetAsLastSibling();

        // Once the fragment is on the notebook, force its text color to black.
        TMP_Text fragmentText = fragment.GetComponentInChildren<TMP_Text>();
        if (fragmentText != null)
        {
            fragmentText.color = Color.black;
        }

        // Mark this fragment as being on the notebook and allow it to be clicked again.
        if (fragmentStates.TryGetValue(fragment, out FragmentState state))
        {
            state.isOnNotebook = true;
        }
        fragment.SetInteractable(true);
    }

    /// <summary>
    /// Animates a fragment from the notebook back to its original position on the blackboard.
    /// </summary>
    private IEnumerator MoveFragmentBackToBlackboard(TextFragment fragment)
    {
        if (fragment == null)
            yield break;

        if (!fragmentStates.TryGetValue(fragment, out FragmentState state) ||
            state.originalParent == null)
            yield break;

        RectTransform fragRT = fragment.GetComponent<RectTransform>();
        if (fragRT == null)
            yield break;

        // Work in the original parent's local space.
        RectTransform targetParent = state.originalParent;

        // Convert current position into the target parent's local space.
        Vector3 worldPos = fragRT.TransformPoint(Vector3.zero);
        Vector2 startLocal;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            targetParent,
            RectTransformUtility.WorldToScreenPoint(null, worldPos),
            null,
            out startLocal
        );

        Vector2 start = startLocal;
        Vector2 end = state.originalPosition;

        // Temporarily parent to the original parent so we animate in the correct space.
        fragRT.SetParent(targetParent, false);
        fragRT.anchoredPosition = start;

        float duration = Mathf.Max(0.01f, moveToSlotSeconds);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
            fragRT.anchoredPosition = Vector2.Lerp(start, end, t);
            yield return null;
        }

        // Snap back to original position.
        fragRT.anchoredPosition = end;

        // Mark as back on the blackboard.
        state.isOnNotebook = false;

        // Remove from placed list and reduce selected count.
        placedFragments.Remove(fragment);
        selectedCount = Mathf.Max(0, selectedCount - 1);

        // Restore its default text color if desired (optional).
        TMP_Text fragmentText = fragment.GetComponentInChildren<TMP_Text>();
        if (fragmentText != null)
        {
            fragmentText.color = Color.white;
        }

        // Allow interaction again.
        fragment.SetInteractable(true);
    }

    private void Win()
    {
        if (isLocked) return;
        isLocked = true;

        SetFragmentsInteractable(false);

        // Show win screen, hide lose screen (if assigned).
        if (winScreen != null) winScreen.SetActive(true);
        if (loseScreen != null) loseScreen.SetActive(false);
    }

    private void Fail()
    {
        if (isLocked) return;
        isLocked = true;

        SetFragmentsInteractable(false);

        // Show lose screen, hide win screen (if assigned).
        if (loseScreen != null) loseScreen.SetActive(true);
        if (winScreen != null) winScreen.SetActive(false);
    }

    private void SetLectureVisible(bool visible)
    {
        if (lectureCanvasGroup == null) return;
        lectureCanvasGroup.alpha = visible ? 1f : 0f;
        lectureCanvasGroup.interactable = visible;
        lectureCanvasGroup.blocksRaycasts = visible;
    }

    private void SetFragmentsVisible(bool visible)
    {
        // We only toggle the GameObject active state; FloatingEffect (if present) will naturally stop updating.
        for (int i = 0; i < spawnedFragments.Count; i++)
        {
            if (spawnedFragments[i] != null)
                spawnedFragments[i].gameObject.SetActive(visible);
        }
    }

    private void SetFragmentsInteractable(bool interactable)
    {
        for (int i = 0; i < spawnedFragments.Count; i++)
        {
            spawnedFragments[i]?.SetInteractable(interactable && !isLocked);
        }
    }

    private void SetStatus(string message)
    {
        if (statusText != null)
            statusText.text = message ?? "";
    }

    private static IEnumerator FadeTMPAlpha(TMP_Text tmp, float from, float to, float duration)
    {
        if (tmp == null)
            yield break;

        duration = Mathf.Max(0.01f, duration);
        float elapsed = 0f;

        tmp.alpha = from;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            tmp.alpha = Mathf.Lerp(from, to, t);
            yield return null;
        }
        tmp.alpha = to;
    }

    private static void Shuffle<T>(IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int j = Random.Range(i, list.Count);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    private static List<string> SampleWithoutReplacement(string[] pool, int count)
    {
        var indices = new List<int>(pool.Length);
        for (int i = 0; i < pool.Length; i++)
            indices.Add(i);

        Shuffle(indices);

        int take = Mathf.Min(count, indices.Count);
        var result = new List<string>(take);
        for (int i = 0; i < take; i++)
            result.Add(pool[indices[i]]);

        return result;
    }
}
