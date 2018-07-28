using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// like GUILayout, but for uGui. Allows you to construct editors from prefab'd controls quickly


public class uGuiLayout
{
    private static Stack<RectTransform> subControls = new Stack<RectTransform>();

    private static Vector2 currentPosition = new Vector2(0, 0);

    public enum Mode
    {
        Horizontal,
        Vertical
    }

    public static Stack<float> positionCache = new Stack<float>();
    public static Stack<Mode> modeCache = new Stack<Mode>();

    public static void BeginHorizontal()
    {
        positionCache.Push(currentPosition.x);
        modeCache.Push(Mode.Horizontal);
        RectTransform t = BeginSubControl();
        t.gameObject.AddComponent<HorizontalLayoutGroup>();

    }
    public static void EndHorizontal()
    {
        currentPosition.x = positionCache.Pop();
        modeCache.Pop();
        EndSubControl();
    }
    public static void BeginVertical()
    {
        BeginSubControl();
        positionCache.Push(currentPosition.y);
        modeCache.Push(Mode.Vertical);
        RectTransform t = BeginSubControl();
        t.gameObject.AddComponent<VerticalLayoutGroup>();
    }
    public static void EndVertical()
    {
        currentPosition.y = positionCache.Pop();
        modeCache.Pop();
        EndSubControl();
    }

    public static RectTransform BeginSubControl()
    {
        GameObject go = new GameObject();
        RectTransform t = go.AddComponent<RectTransform>();
        if (subControls.Count > 0)
        {
            t.parent = subControls.Peek();
        }
        subControls.Push(t);
        t.anchoredPosition = Vector2.zero;
        t.anchorMin = new Vector2(0, 1);
        t.anchorMax = new Vector2(0, 1);
        t.offsetMax = Vector2.zero;
        t.offsetMin = Vector2.zero;
        t.pivot = currentPosition;

        //t.localPosition = new Vector3(currentPosition.x, currentPosition.y, 0);
        return t;
    }

    public static void EndSubControl()
    {
        subControls.Pop();
    }

    public static void AddChild(RectTransform trans)
    {
        trans.parent = subControls.Peek();
        trans.anchoredPosition = currentPosition;
    }

    public static Text Label(string text, string name = "Text")
    {
        GameObject go = new GameObject(name);
        Text t = go.AddComponent<Text>();
        t.text = text;
        AddChild(t.rectTransform);
        return t;
    }

    //public static void Slider(float value, float min = 0.0f, float max = 0.0f);
}
