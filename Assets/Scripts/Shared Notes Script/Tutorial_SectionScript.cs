using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Controls a simple tutorial section that appears before the Shared Notes game starts.
/// - While the tutorial is visible, the Shared_Notes_Manager is disabled so it cannot run.
/// - Shows a background image and explanatory text about the rules.
/// - Title/body text are editable in the Inspector.
/// Hook a UI button to CloseTutorial() to dismiss the tutorial and start the game.
/// </summary>
public class Tutorial_SectionScript : MonoBehaviour
{
    [Header("Shared Notes Game Reference")]
    [Tooltip("Reference to the Shared_Notes_Manager that runs the keyword game.")]
    [SerializeField] private Shared_Notes_Manager sharedNotesManager;

    [Tooltip("Notebook transition script that should be disabled during the tutorial and only enabled once the game starts.")]
    [SerializeField] private Notebook_TransitionScript notebookTransition;

    [Header("Tutorial UI")]
    [Tooltip("CanvasGroup that contains the tutorial UI (background + texts).")]
    [SerializeField] private CanvasGroup tutorialCanvasGroup;
    [Tooltip("Optional: Image used as the tutorial background.")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text bodyText;

    [Header("Editable Tutorial Content")]
    [SerializeField] private string tutorialTitle = "How to Play";
    [TextArea(3, 6)]
    [SerializeField] private string tutorialBody =
        "Listen carefully to the lecture.\n\n" +
        "Afterwards, select the 5 keywords that best summarize what you heard. " +
        "Place them in your notebook, then press Submit.\n\n" +
        "Picking even one wrong keyword will cause you to lose, " +
        "so choose wisely!";

    private bool tutorialActive = true;

    private void Awake()
    {
        // Prevent the Shared Notes game from starting while the tutorial is active.
        if (sharedNotesManager != null)
        {
            sharedNotesManager.enabled = false;
        }

        // Also prevent notebook transitions from playing until after the tutorial.
        if (notebookTransition != null)
        {
            notebookTransition.enabled = false;
        }
    }

    private void Start()
    {
        // Initialize the tutorial texts from the editable fields.
        if (titleText != null)
        {
            titleText.text = tutorialTitle;
        }

        if (bodyText != null)
        {
            bodyText.text = tutorialBody;
        }

        SetTutorialVisible(true);
    }

    /// <summary>
    /// Called by a UI button when the player is done reading the tutorial.
    /// Hides the tutorial and enables the Shared Notes game.
    /// </summary>
    public void CloseTutorial()
    {
        if (!tutorialActive)
            return;

        tutorialActive = false;
        SetTutorialVisible(false);

        if (sharedNotesManager != null)
        {
            sharedNotesManager.enabled = true;
        }

        // Now that the game manager is running, allow the notebook transition script to run.
        if (notebookTransition != null)
        {
            notebookTransition.enabled = true;
        }
    }

    private void SetTutorialVisible(bool visible)
    {
        if (tutorialCanvasGroup != null)
        {
            tutorialCanvasGroup.alpha = visible ? 1f : 0f;
            tutorialCanvasGroup.interactable = visible;
            tutorialCanvasGroup.blocksRaycasts = visible;
        }
        else
        {
            // Fallback if no CanvasGroup is assigned: toggle this object instead.
            gameObject.SetActive(visible);
        }
    }
}

