using UnityEngine;

/// <summary>
/// Plays a notebook UI transition using an Animator (preferred over manual RectTransform tweening).
/// Assign/modify the Animator Controller and its animations in the Inspector.
/// </summary>
public class Notebook_TransitionScript : MonoBehaviour
{
    [Header("Animator")]
    [Tooltip("Animator that controls the notebook panel transitions. If not assigned, will try GetComponent<Animator>().")]
    [SerializeField] private Animator notebookAnimator;

    [Header("Animator Controls")]
    [Tooltip("Trigger parameter that plays the 'show/slide in' transition. Leave empty to use Play(showStateName).")]
    [SerializeField] private string showTrigger = "Show";
    [Tooltip("Trigger parameter that plays the 'hide/slide out' transition. Leave empty if you don't support hiding.")]
    [SerializeField] private string hideTrigger = "Hide";

    [Tooltip("Optional: state name to play for show when showTrigger is empty. Example: 'Notebook_Show'.")]
    [SerializeField] private string showStateName = "";
    [Tooltip("Optional: state name to play for hide when hideTrigger is empty. Example: 'Notebook_Hide'.")]
    [SerializeField] private string hideStateName = "";

    [Header("Auto Play")]
    [Tooltip("If true, calls Hide() in Awake (uses hideTrigger/hideStateName).")]
    [SerializeField] private bool startHidden = true;
    [SerializeField] private bool playShowOnStart = true;
    [SerializeField] private float showDelay = 0f;

    [Header("Toggle Tracking (Optional)")]
    [Tooltip("Used only for Toggle(). If you don't need Toggle, you can ignore this.")]
    [SerializeField] private bool startsShown = false;
    private bool isShown;

    private void Awake()
    {
        if (notebookAnimator == null)
            notebookAnimator = GetComponent<Animator>();

        isShown = startsShown;

        if (startHidden)
        {
            isShown = false;
            Hide();
        }
    }

    private void Start()
    {
        if (playShowOnStart)
        {
            if (showDelay > 0f)
                Invoke(nameof(Show), showDelay);
            else
                Show();
        }
    }

    public void Show()
    {
        if (notebookAnimator == null) return;

        isShown = true;

        if (!string.IsNullOrWhiteSpace(showTrigger))
        {
            notebookAnimator.ResetTrigger(hideTrigger);
            notebookAnimator.SetTrigger(showTrigger);
            return;
        }

        if (!string.IsNullOrWhiteSpace(showStateName))
        {
            notebookAnimator.Play(showStateName, 0, 0f);
        }
    }

    public void Hide()
    {
        if (notebookAnimator == null) return;

        isShown = false;

        if (!string.IsNullOrWhiteSpace(hideTrigger))
        {
            notebookAnimator.ResetTrigger(showTrigger);
            notebookAnimator.SetTrigger(hideTrigger);
            return;
        }

        if (!string.IsNullOrWhiteSpace(hideStateName))
        {
            notebookAnimator.Play(hideStateName, 0, 0f);
        }
    }

    public void Toggle()
    {
        if (isShown) Hide();
        else Show();
    }
}

