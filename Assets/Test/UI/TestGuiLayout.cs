using UnityEngine;
using System.Collections;

public class TestGuiLayout : MonoBehaviour
{

    // Use this for initialization

    RectTransform root;
    void Start()
    {
        root = uGuiLayout.BeginSubControl();
        root.parent = GetComponent<Canvas>().transform;

        uGuiLayout.BeginVertical();

        uGuiLayout.Label("My Label");

        uGuiLayout.EndVertical();
        uGuiLayout.EndSubControl();
    }

    void Update()
    {
        //Debug.Log(root.anchoredPosition + "   " + root.anchorMin + "    " + root.anchorMin + "   " + root.offsetMin + "    " + root.offsetMax + "    " + root.pivot + "   " + root.sizeDelta + "    " + root.localPosition);
    }

}
