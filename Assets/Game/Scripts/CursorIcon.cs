using UnityEngine;
using UnityEngine.UI;

public class CursorIcon : MonoBehaviour
{
    private RectTransform rectTransform => GetComponent<RectTransform>();

    void Update()
    {
        rectTransform.position = Input.mousePosition;
    }

    public void ChangeIcon(Sprite icon)
    {
        GetComponent<Image>().sprite = icon;
    }
}