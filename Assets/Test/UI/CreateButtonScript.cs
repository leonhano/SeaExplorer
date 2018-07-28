
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreateButtonScript : MonoBehaviour
{
    private void Start()
    {
        if (FindObjectOfType<EventSystem>() == null)
        {
            var es = new GameObject("EventSystem", typeof(EventSystem));
            es.AddComponent<StandaloneInputModule>();
        }

        var canvasObject = new GameObject("Canvas");
        var canvas = canvasObject.AddComponent<Canvas>();
        canvasObject.AddComponent<GraphicRaycaster>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        var buttonObject = new GameObject("Button");
        var image = buttonObject.AddComponent<Image>();
        image.transform.parent = canvas.transform;
        image.rectTransform.sizeDelta = new Vector2(180, 50);
        image.rectTransform.anchoredPosition = Vector3.zero;
        image.color = new Color(1f, .3f, .3f, .5f);

        var button = buttonObject.AddComponent<Button>();
        button.targetGraphic = image;
        button.onClick.AddListener(() => Debug.Log(Time.time));

        var textObject = new GameObject("Text");
        textObject.transform.parent = buttonObject.transform;
        var text = textObject.AddComponent<Text>();
        text.rectTransform.sizeDelta = Vector2.zero;
        text.rectTransform.anchorMin = Vector2.zero;
        text.rectTransform.anchorMax = Vector2.one;
        text.rectTransform.anchoredPosition = new Vector2(.5f, .5f);
        text.text = "dynamic create!";
        text.font = Resources.FindObjectsOfTypeAll<Font>()[0];
        text.fontSize = 20;
        text.color = Color.yellow;
        text.alignment = TextAnchor.MiddleCenter;

        button.onClick.AddListener(onBtnClicked);
    }

    public void onBtnClicked()
    {
        Debug.Log("btn clicked!");
    }
}