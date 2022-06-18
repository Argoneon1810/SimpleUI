using UnityEngine;

public class MovableCanvasObject : MonoBehaviour
{
    Canvas mCanvas;
    Vector3 lastMousePosition = Vector3.negativeInfinity;

    private void Start()
    {
        mCanvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag()
    {
        lastMousePosition = Input.mousePosition.GetMousePositionMatchingScaler(mCanvas);
    }

    public void OnDrag()
    {
        if (lastMousePosition == Vector3.negativeInfinity) return;

        Vector3 mousePositionOnScaledCanvas = Input.mousePosition.GetMousePositionMatchingScaler(mCanvas);

        (transform as RectTransform).anchoredPosition += (mousePositionOnScaledCanvas - lastMousePosition).ToVector2();

        lastMousePosition = mousePositionOnScaledCanvas;
    }

    public void OnEndDrag()
    {
        lastMousePosition = Vector3.negativeInfinity;
    }
}
