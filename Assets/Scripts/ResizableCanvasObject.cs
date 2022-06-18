using UnityEngine;

public class ResizableCanvasObject : MonoBehaviour
{
    [SerializeField] int allowancePx = 5;
    [SerializeField] float minimumSize = 64;

    RectTransform mRectTransform;
    Canvas mCanvas;

    bool resizeXMin = false;
    bool resizeXMax = false;
    bool resizeYMin = false;
    bool resizeYMax = false;
    public bool resizing => resizeXMin || resizeXMax || resizeYMin || resizeYMax;

    private void Start()
    {
        mRectTransform = transform as RectTransform;
        mCanvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag()
    {
        Vector3 startMousePos = Input.mousePosition.GetMousePositionMatchingScaler(mCanvas).ToCoordinateZeroCenter(mCanvas);
        Rect rect = mRectTransform.rect.ApplyPosition(mRectTransform.anchoredPosition);
        resizeXMin = startMousePos.x.InRange(rect.x - allowancePx, rect.x + allowancePx, true);
        resizeYMin = startMousePos.y.InRange(rect.y - allowancePx, rect.y + allowancePx, true);
        resizeXMax = startMousePos.x.InRange(rect.x + rect.width - allowancePx, rect.x + rect.width + allowancePx, true);
        resizeYMax = startMousePos.y.InRange(rect.y + rect.height - allowancePx, rect.y + rect.height + allowancePx, true);
    }

    public void OnDrag()
    {
        if (!resizing) return;

        Vector3 currentMousePos = Input.mousePosition.GetMousePositionMatchingScaler(mCanvas).ToCoordinateZeroCenter(mCanvas);
        Rect rect = mRectTransform.rect.ApplyPosition(mRectTransform.anchoredPosition);

        if (resizeXMin)
        {
            float oldRectX = rect.x;
            rect.x = currentMousePos.x;
            rect.width += oldRectX - rect.x;
            if (rect.width < minimumSize) rect.width = minimumSize;
        }
        if (resizeXMax)
        {
            rect.width += currentMousePos.x - (rect.x + rect.width);
            if (rect.width < minimumSize) rect.width = minimumSize;
        }
        if (resizeYMin)
        {
            float oldRectY = rect.y;
            rect.y = currentMousePos.y;
            rect.height += oldRectY - rect.y;
            if (rect.height < minimumSize) rect.height = minimumSize;
        }
        if (resizeYMax)
        {
            rect.height += currentMousePos.y - (rect.y + rect.height);
            if (rect.height < minimumSize) rect.height = minimumSize;
        }

        mRectTransform.anchoredPosition = new Vector2(
            Useful.Math.Median(rect.x, rect.x + rect.width),
            Useful.Math.Median(rect.y, rect.y + rect.height)
        );
        mRectTransform.sizeDelta = new Vector2(rect.width, rect.height);
    }

    public void OnEndDrag()
    {
        resizeXMax = false;
        resizeXMin = false;
        resizeYMax = false;
        resizeYMin = false;
    }
}
