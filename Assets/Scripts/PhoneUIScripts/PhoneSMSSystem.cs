using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A simple phone-style SMS / messenger system between the player and an NPC.
///
/// - Configure the conversation in the Inspector.
/// - Both NPC lines and player reply options are editable.
/// - At runtime, the player selects replies; the NPC responds.
/// - All sent messages are persisted (via PlayerPrefs) so they can be viewed again.
///
/// Usage:
/// 1. Create a Canvas-based "phone" UI.
/// 2. Add a vertical layout group (or similar) to a Content area and assign it to messageContainer.
/// 3. Create a message prefab with a Text (or TMP) component and optional styling for player/NPC.
/// 4. Create a button prefab for choices with a Text (or TMP) label.
/// 5. Wire everything up on this component in the Inspector.
/// </summary>
public class PhoneSMSSystem : MonoBehaviour
{
    [Header("Conversation Identity")]
    [Tooltip("Unique ID so this conversation's history can be saved/loaded independently.")]
    public string conversationId = "default_conversation";

    [Header("UI References")]
    [Tooltip("Parent transform where message bubbles will be instantiated.")]
    public Transform messageContainer;

    [Tooltip("Prefab for a single message bubble. Must contain a Text or TMP_Text component.")]
    public GameObject messagePrefab;

    [Tooltip("Parent transform where player choice buttons will be instantiated.")]
    public Transform choiceContainer;

    [Tooltip("Prefab for a single choice button. Must contain a Button and a Text or TMP_Text component.")]
    public GameObject choiceButtonPrefab;

    [Header("Conversation Definition (editable in Inspector)")]
    [Tooltip("Conversation nodes defining NPC lines and player reply options.")]
    public List<PhoneMessageNode> conversationNodes = new List<PhoneMessageNode>();

    [Header("History Settings")]
    [Tooltip("If true, previously saved messages will be shown when the conversation opens again.")]
    public bool loadSavedHistoryOnStart = true;

    [Tooltip("If true, new messages will be auto-saved as the conversation progresses.")]
    public bool autoSaveHistory = true;

    private const string PlayerPrefsKeyPrefix = "PHONE_SMS_HISTORY_";

    private readonly List<PhoneMessage> _history = new List<PhoneMessage>();
    private int _currentNodeIndex = 0;
    private bool _conversationFinished;

    #region Unity Lifecycle

    private void Awake()
    {
        if (loadSavedHistoryOnStart)
        {
            LoadHistory();
        }

        // Rebuild UI from loaded history (if any)
        foreach (var msg in _history)
        {
            SpawnMessageBubble(msg);
        }
    }

    private void Start()
    {
        // Start the conversation from the first node if available.
        if (conversationNodes != null && conversationNodes.Count > 0)
        {
            ShowCurrentNode();
        }
        else
        {
            Debug.LogWarning("PhoneSMSSystem: No conversation nodes defined.");
        }
    }

    #endregion

    #region Public API

    /// <summary>
    /// Clears current UI and replays the saved history only (no new choices).
    /// Can be used if you want a "view mode" separate from active conversation.
    /// </summary>
    public void ShowSavedHistoryOnly()
    {
        ClearMessages();
        foreach (var msg in _history)
        {
            SpawnMessageBubble(msg);
        }

        ClearChoices();
    }

    /// <summary>
    /// Deletes all saved history for this conversation.
    /// </summary>
    public void ClearSavedHistory()
    {
        _history.Clear();
        PlayerPrefs.DeleteKey(GetPrefsKey());
        PlayerPrefs.Save();
        ClearMessages();
        ClearChoices();
        _currentNodeIndex = 0;
        _conversationFinished = false;
        ShowCurrentNode();
    }

    #endregion

    #region Conversation Flow

    private void ShowCurrentNode()
    {
        if (_conversationFinished)
        {
            return;
        }

        if (_currentNodeIndex < 0 || _currentNodeIndex >= conversationNodes.Count)
        {
            Debug.LogWarning("PhoneSMSSystem: Current node index is out of range. Ending conversation.");
            EndConversation();
            return;
        }

        var node = conversationNodes[_currentNodeIndex];

        // NPC message
        if (!string.IsNullOrWhiteSpace(node.npcText))
        {
            var npcMessage = new PhoneMessage
            {
                sender = MessageSender.NPC,
                text = node.npcText
            };
            AddMessageToHistory(npcMessage);
            SpawnMessageBubble(npcMessage);
        }

        // Player choices
        ClearChoices();
        if (node.playerChoices != null && node.playerChoices.Count > 0)
        {
            foreach (var choice in node.playerChoices)
            {
                SpawnChoiceButton(choice);
            }
        }
        else
        {
            // No choices means conversation ends after NPC line.
            EndConversation();
        }
    }

    private void OnPlayerChoiceSelected(PhoneMessageChoice choice)
    {
        // Player message
        var playerMessage = new PhoneMessage
        {
            sender = MessageSender.Player,
            text = choice.playerText
        };
        AddMessageToHistory(playerMessage);
        SpawnMessageBubble(playerMessage);

        ClearChoices();

        // Go to the next node
        if (choice.nextNodeIndex >= 0 && choice.nextNodeIndex < conversationNodes.Count)
        {
            _currentNodeIndex = choice.nextNodeIndex;
            ShowCurrentNode();
        }
        else
        {
            // Invalid next index means the conversation ends here.
            EndConversation();
        }
    }

    private void EndConversation()
    {
        _conversationFinished = true;
        ClearChoices();
    }

    #endregion

    #region History Management

    private void AddMessageToHistory(PhoneMessage message)
    {
        _history.Add(message);

        if (autoSaveHistory)
        {
            SaveHistory();
        }
    }

    private void SaveHistory()
    {
        try
        {
            var wrapper = new PhoneMessageHistoryWrapper { messages = _history };
            string json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(GetPrefsKey(), json);
            PlayerPrefs.Save();
        }
        catch (Exception e)
        {
            Debug.LogError($"PhoneSMSSystem: Failed to save history. {e}");
        }
    }

    private void LoadHistory()
    {
        string key = GetPrefsKey();
        if (!PlayerPrefs.HasKey(key))
        {
            return;
        }

        try
        {
            string json = PlayerPrefs.GetString(key);
            if (!string.IsNullOrEmpty(json))
            {
                var wrapper = JsonUtility.FromJson<PhoneMessageHistoryWrapper>(json);
                if (wrapper != null && wrapper.messages != null)
                {
                    _history.Clear();
                    _history.AddRange(wrapper.messages);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"PhoneSMSSystem: Failed to load history. {e}");
        }
    }

    private string GetPrefsKey()
    {
        return PlayerPrefsKeyPrefix + conversationId;
    }

    #endregion

    #region UI Helpers

    private void SpawnMessageBubble(PhoneMessage message)
    {
        if (messageContainer == null || messagePrefab == null)
        {
            Debug.LogWarning("PhoneSMSSystem: Message container or prefab not assigned.");

            return;
        }

        GameObject instance = Instantiate(messagePrefab, messageContainer);

        // Try UnityEngine.UI.Text first
        var uiText = instance.GetComponentInChildren<Text>();
        if (uiText != null)
        {
            uiText.text = message.text;
        }
        else
        {
            // Try TextMeshPro if available (safe reflection-like check)
            var tmpType = Type.GetType("TMPro.TMP_Text, Unity.TextMeshPro");
            if (tmpType != null)
            {
                var tmp = instance.GetComponentInChildren(tmpType) as Component;
                if (tmp != null)
                {
                    var textProp = tmpType.GetProperty("text");
                    if (textProp != null && textProp.CanWrite)
                    {
                        textProp.SetValue(tmp, message.text, null);
                    }
                }
            }
        }

        // Optional: apply different styling based on sender (if the prefab supports it).
        var messageStyler = instance.GetComponent<PhoneSMSMessageStyler>();
        if (messageStyler != null)
        {
            messageStyler.ApplyStyle(message.sender);
        }
    }

    private void SpawnChoiceButton(PhoneMessageChoice choice)
    {
        if (choiceContainer == null || choiceButtonPrefab == null)
        {
            Debug.LogWarning("PhoneSMSSystem: Choice container or prefab not assigned.");
            return;
        }

        GameObject instance = Instantiate(choiceButtonPrefab, choiceContainer);

        // Set button label (Text or TMP)
        var uiText = instance.GetComponentInChildren<Text>();
        if (uiText != null)
        {
            uiText.text = choice.playerText;
        }
        else
        {
            var tmpType = Type.GetType("TMPro.TMP_Text, Unity.TextMeshPro");
            if (tmpType != null)
            {
                var tmp = instance.GetComponentInChildren(tmpType) as Component;
                if (tmp != null)
                {
                    var textProp = tmpType.GetProperty("text");
                    if (textProp != null && textProp.CanWrite)
                    {
                        textProp.SetValue(tmp, choice.playerText, null);
                    }
                }
            }
        }

        var button = instance.GetComponent<Button>();
        if (button != null)
        {
            var capturedChoice = choice;
            button.onClick.AddListener(() => OnPlayerChoiceSelected(capturedChoice));
        }
        else
        {
            Debug.LogWarning("PhoneSMSSystem: Choice button prefab does not have a Button component.");
        }
    }

    private void ClearMessages()
    {
        if (messageContainer == null) return;

        for (int i = messageContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(messageContainer.GetChild(i).gameObject);
        }
    }

    private void ClearChoices()
    {
        if (choiceContainer == null) return;

        for (int i = choiceContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(choiceContainer.GetChild(i).gameObject);
        }
    }

    #endregion
}

/// <summary>
/// Who sent the message.
/// </summary>
public enum MessageSender
{
    Player,
    NPC
}

/// <summary>
/// One message in the conversation history.
/// </summary>
[Serializable]
public class PhoneMessage
{
    public MessageSender sender;

    [TextArea]
    public string text;
}

/// <summary>
/// A single NPC line and the possible player replies from that point.
/// All fields are editable in the Inspector.
/// </summary>
[Serializable]
public class PhoneMessageNode
{
    [Header("NPC Line")]
    [TextArea]
    public string npcText;

    [Header("Player Reply Options")]
    public List<PhoneMessageChoice> playerChoices = new List<PhoneMessageChoice>();
}

/// <summary>
/// A single selectable player reply that points to the next node.
/// </summary>
[Serializable]
public class PhoneMessageChoice
{
    [TextArea]
    public string playerText;

    [Tooltip("Index of the next node in the conversationNodes list. Use -1 to end the conversation.")]
    public int nextNodeIndex = -1;
}

/// <summary>
/// Wrapper class so we can serialize a List&lt;PhoneMessage&gt; using JsonUtility.
/// </summary>
[Serializable]
public class PhoneMessageHistoryWrapper
{
    public List<PhoneMessage> messages = new List<PhoneMessage>();
}

/// <summary>
/// Optional helper to style message bubbles differently for player vs NPC.
/// Attach to your message prefab and implement ApplyStyle as needed.
/// </summary>
public class PhoneSMSMessageStyler : MonoBehaviour
{
    [Header("Optional Styling")]
    public Color playerColor = Color.green;
    public Color npcColor = Color.white;

    public Graphic backgroundGraphic;

    public void ApplyStyle(MessageSender sender)
    {
        if (backgroundGraphic == null) return;

        backgroundGraphic.color = sender == MessageSender.Player ? playerColor : npcColor;
    }
}

