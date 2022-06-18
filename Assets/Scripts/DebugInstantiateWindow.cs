using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInstantiateWindow : MonoBehaviour
{
    [SerializeField] GameObject WindowPrefab;

    public void OnClick()
    {
        GameObject window = Instantiate(WindowPrefab);
        window.transform.SetParent(GetComponentInParent<Canvas>().transform, false);
    }
}
