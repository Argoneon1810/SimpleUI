using UnityEngine;

public class MarginSetter : MonoBehaviour
{
    [SerializeField] private Margin margin;

    private void OnValidate()
    {
        (transform as RectTransform).offsetMin = new Vector2(margin.marginLeft, margin.marginBottom);
        (transform as RectTransform).offsetMax = new Vector2(-margin.marginRight, -margin.marginTop);
    }

    [System.Serializable]
    public class Margin
    {
        [System.Serializable]
        public class OptionalMargin
        {
            public bool useMarginTop = false;
            public float marginTop = -1;
            public bool useMarginBottom = false;
            public float marginBottom = -1;
            public bool useMarginLeft = false;
            public float marginLeft = -1;
            public bool useMarginRight = false;
            public float marginRight = -1;
        }

        public float _margin = 0;
        public bool useOptionalMargin = false;
        public OptionalMargin optionalMargin;

        public float marginTop => (!useOptionalMargin || !optionalMargin.useMarginTop) ? _margin : optionalMargin.marginTop;
        public float marginBottom => (!useOptionalMargin || !optionalMargin.useMarginBottom) ? _margin : optionalMargin.marginBottom;
        public float marginLeft => (!useOptionalMargin || !optionalMargin.useMarginLeft) ? _margin : optionalMargin.marginLeft;
        public float marginRight => (!useOptionalMargin || !optionalMargin.useMarginRight) ? _margin : optionalMargin.marginRight;
    }
}
