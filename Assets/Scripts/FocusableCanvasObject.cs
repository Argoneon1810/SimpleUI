using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusableCanvasObject : MonoBehaviour
{
    public void OnClick(Transform transformToReorder)
    {
        List<FocusableCanvasObject> focusables = new List<FocusableCanvasObject>(FindObjectsOfType<FocusableCanvasObject>());
        focusables.Sort(delegate (FocusableCanvasObject f1, FocusableCanvasObject f2) {
            return f1.transform.GetSiblingIndex() < f2.transform.GetSiblingIndex() ? -1 : 1;
        });
        transformToReorder.SetSiblingIndex(focusables[focusables.Count-1].transform.GetSiblingIndex() + 1);
    }
}
