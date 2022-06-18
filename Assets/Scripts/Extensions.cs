using UnityEngine;
using UnityEngine.UI;

public static class Extensions
{
    public static Vector2 ToVector2(this Vector3 orig)
    {
        return new Vector2(orig.x, orig.y);
    }

    public static Vector3 GetMousePositionMatchingScaler(this Vector3 mousePos, Canvas targetCanvas)
    {
        CanvasScaler scaler = targetCanvas.GetComponent<CanvasScaler>();
        
        float scaledCanvasWidth = Useful.CanvasScale.GetWidthMatchingScaler(scaler);
        float scaledCanvasHeight = Useful.CanvasScale.GetHeightMatchingScaler(scaler);

        mousePos.x = mousePos.x / Screen.width * scaledCanvasWidth;
        mousePos.y = mousePos.y / Screen.height * scaledCanvasHeight;

        return mousePos;
    }

    public static Vector3 ToCoordinateZeroCenter(this Vector3 mousePos, Canvas targetCanvas)
    {
        CanvasScaler scaler = targetCanvas.GetComponent<CanvasScaler>();

        float scaledCanvasWidth = Useful.CanvasScale.GetWidthMatchingScaler(scaler);
        float scaledCanvasHeight = Useful.CanvasScale.GetHeightMatchingScaler(scaler);

        mousePos.x -= scaledCanvasWidth / 2;
        mousePos.y -= scaledCanvasHeight / 2;

        return mousePos;
    }

    public static void ChangeAnchorKeepPosition(this RectTransform rt, Vector2 anchorMin, Vector2 anchorMax)
    {
        Vector3 localPosition = rt.localPosition;
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.localPosition = localPosition;
    }

    public static bool InRange(this float val, float from, float to, bool inclusive)
    {
        return inclusive ? from <= val && val <= to : from < val && val < to;
    }

    public static Rect ApplyPosition(this Rect rect, Vector2 anchoredPos)
    {
        rect.x += anchoredPos.x;
        rect.y += anchoredPos.y;
        return rect;
    }

}