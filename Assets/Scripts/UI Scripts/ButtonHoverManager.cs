using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonHoverManager : MonoBehaviour, IPointerEnterHandler
{
    public Button [] _Buttons;

    public void OnPointerEnter(PointerEventData eventData) 
    {
        foreach (Button btn in _Buttons)
        {
            if (btn.gameObject.activeInHierarchy) 
            {
                Image img = btn.GetComponent<Image>();

                if (img != null) 
                {
                    Color c = img.color;
                    c.a = 1f;
                    img.color = c;
                }
            }
        }
        Debug.Log("Hovering over button: " + gameObject.name);
    }
}
