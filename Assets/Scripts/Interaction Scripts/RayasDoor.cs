using TMPro;
using UnityEngine;

public class RayasDoor : MonoBehaviour
{
    [Header("UI")]
    public GameObject _InteractIndicator;
    public TextMeshProUGUI _InteractText;

    public bool _IsInRange;
    public bool _HasInteracted;

    public PackedBagInteractionRayaDorm _Cutscene6;

    // Update is called once per frame
    void Update()
    {
        if (_IsInRange && !_HasInteracted && Input.GetKeyDown(KeyCode.F))
        {
            _HasInteracted = true;
            _Cutscene6.enabled = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raya'sDoor") && !_HasInteracted)
        {
            _IsInRange = true;
            _InteractIndicator.SetActive(true);

            if (_HasInteracted)
                _InteractText.text = "Interacted!";
            else
                _InteractText.text = "Press F to Interact";
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Raya'sDoor"))
        {
            _IsInRange = false;
            _InteractIndicator.SetActive(false);
        }
    }
}
