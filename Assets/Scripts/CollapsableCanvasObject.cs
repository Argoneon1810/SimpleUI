using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CollapsableCanvasObject : MonoBehaviour
{
    [SerializeField] Transform collapsableItemHolder;
    [SerializeField] float transformLength = 1;
    
    RectTransform mRectTransform;
    Image mImage;

    bool collapsed = false;
    Vector2 preCollapsePosition;
    Vector2 preCollapseSize;

    AnimationCurve transformCurve = AnimationCurve.EaseInOut(0,0,1,1);

    Task resizeTask;
    Task rotateTask;
    Task changeCornerRoundnessTask;

    private void Start()
    {
        if (!collapsableItemHolder) collapsableItemHolder = FindObjectsOfType<CollapsedItemHolder>()[0].transform;
        mRectTransform = transform as RectTransform;
        mImage = GetComponent<Image>();
    }

    public void OnClick(RectTransform rectTransform) {
        if(collapsed)
        {
            collapsed = false;
            StartCoroutine(TransformCoroutine(preCollapsePosition, preCollapseSize));

            if (rotateTask != null && rotateTask.Running)
                rotateTask.Stop();
            rotateTask = new Task(RotateButton(rectTransform, rectTransform.localRotation, Quaternion.identity));

            if (changeCornerRoundnessTask != null && changeCornerRoundnessTask.Running)
                changeCornerRoundnessTask.Stop();
            changeCornerRoundnessTask = new Task(ChangeCornerRoundness(5));
        }
        else
        {
            collapsed = true;
            StartCoroutine(Collapse(rectTransform));

            if (rotateTask != null && rotateTask.Running)
                rotateTask.Stop();
            rotateTask = new Task(RotateButton(rectTransform, rectTransform.localRotation, Quaternion.Euler(0, 0, 180)));

            if (changeCornerRoundnessTask != null && changeCornerRoundnessTask.Running)
                changeCornerRoundnessTask.Stop();
            changeCornerRoundnessTask = new Task(ChangeCornerRoundness(15));
        }
    }

    IEnumerator Collapse(RectTransform rectTransform)
    {

        preCollapsePosition = mRectTransform.localPosition;
        preCollapseSize = mRectTransform.sizeDelta;

        GameObject collapsedItem = new GameObject("PlaceHolder", typeof(RectTransform));
        collapsedItem.transform.SetParent(collapsableItemHolder);

        yield return new WaitForEndOfFrame();

        collapsedItem.transform.SetParent(collapsableItemHolder.parent);
        (collapsedItem.transform as RectTransform).ChangeAnchorKeepPosition(new Vector2(.5f, .5f), new Vector2(.5f, .5f));
        collapsedItem.transform.localScale = Vector3.one;

        yield return new WaitForEndOfFrame();

        resizeTask = new Task(
            TransformCoroutine(
                collapsedItem.transform.localPosition,
                new Vector2(64, 64)
            )
        );
        
        StartCoroutine(DestroyAfterResizeCoroutine(collapsedItem));
    }

    IEnumerator TransformCoroutine(Vector3 targetPos, Vector2 targetSize)
    {
        RectTransform mRectTransform = transform as RectTransform;
        float t = 0;
        float posT;
        float sizeT;
        Vector3 oldPos = mRectTransform.localPosition;
        Vector2 oldSize = mRectTransform.sizeDelta;
        while(true)
        {
            if (t > 1) break;
            t += Time.deltaTime / transformLength;
            posT = transformCurve.Evaluate(t);
            sizeT = transformCurve.Evaluate(t);
            mRectTransform.localPosition = Vector3.Lerp(oldPos, targetPos, posT);
            mRectTransform.sizeDelta = Vector2.Lerp(oldSize, targetSize, sizeT);
            yield return null;
        }
    }

    IEnumerator DestroyAfterResizeCoroutine(GameObject itemToDestroy)
    {
        while(resizeTask.Running) yield return null;
        Destroy(itemToDestroy);
    }

    IEnumerator RotateButton(RectTransform buttonToRotate, Quaternion from, Quaternion to)
    {
        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime / transformLength;
            buttonToRotate.localRotation = Quaternion.Lerp(from, to, t);
            yield return null;
        }
    }

    IEnumerator ChangeCornerRoundness(float target)
    {
        float original = mImage.pixelsPerUnitMultiplier;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / transformLength;
            mImage.pixelsPerUnitMultiplier = Mathf.Lerp(original, target, t);
            yield return null;
        }
    }
}
