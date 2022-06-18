using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class StretchHorizontallyWithZeroAnchor : MonoBehaviour
{
    private void Awake()
    {
        CanvasScaler canvasScaler = GetComponentInParent<Canvas>().GetComponent<CanvasScaler>();
        RectTransform rt = transform as RectTransform;
        Vector2 sizeDelta = rt.sizeDelta;
        sizeDelta.x = Useful.CanvasScale.GetWidthMatchingScaler(canvasScaler);
        rt.sizeDelta = sizeDelta;
        rt.anchoredPosition = Vector2.zero;
    }
}
