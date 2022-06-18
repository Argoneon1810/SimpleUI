using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class Useful
{
    public class Debug
    {
        public static void ClearLog()
        {
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }
    }

    public class CanvasScale
    {
        public static float GetWidthMatchingScaler(CanvasScaler scaler) => Mathf.Lerp(
            scaler.referenceResolution.x,
            scaler.referenceResolution.y * Camera.main.aspect,
            scaler.matchWidthOrHeight
        );

        public static float GetHeightMatchingScaler(CanvasScaler scaler) => Mathf.Lerp(
            scaler.referenceResolution.x / Camera.main.aspect,
            scaler.referenceResolution.y,
            scaler.matchWidthOrHeight
        );
    }

    public class Math
    {
        public static float Median(float small, float big) => ((big - small) / 2f) + small;
    }
}