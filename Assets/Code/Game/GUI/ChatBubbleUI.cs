using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChatBubbleUI : MonoBehaviour {

    WorldCanvasUI worldCanvasUI = null;
    public Vector2 sizeDelta = Vector2.zero;
    public bool autoCanvasUIs = true;

    public Sprite bgSprite = null;                            //background image;
    Image bgImage = null;

    public Texture bgTexture = null;                        //background raw image
    RawImage bgRawImage = null;

    Text textUI = null;    //text ui
    public int fontSize = 10;    //text font size;
    public Font font = null;
    public List<string> chatStrList = new List<string>();   //chat string list;

    //time interval to pop chat string; if it is 0, then means tap and pop next; otherwise, auto pop (timeInterval)
    public float timeInterval = 0;
    bool autoPop = true;
    float curDeltaTime = 0f;
    public bool HideAfterAllChatPoped = false;

	// Use this for initialization
    void Awake()
    {
        //get world canvas;
        worldCanvasUI = gameObject.GetComponent<WorldCanvasUI>();
        if (worldCanvasUI == null)
        {
            worldCanvasUI = gameObject.AddComponent<WorldCanvasUI>();
        }
        GameObject canvasGO = worldCanvasUI.CanvasGO;
        canvasGO.AddComponent<RotateTowardCamera>();
        
        //create chat bubble ui;

        //build background image if have;
        if(bgSprite != null)
        {
            var bgImageGO = new GameObject("Background_Image");
            bgImageGO.transform.SetParent(canvasGO.transform);
            bgImage = bgImageGO.AddComponent<Image>();
            bgImage.fillCenter = true;
            bgImage.sprite = bgSprite;
            if(sizeDelta == Vector2.zero)
                sizeDelta = new Vector2(bgSprite.rect.width, bgSprite.rect.height);
            bgImage.rectTransform.sizeDelta = sizeDelta;
        }
        else if (bgTexture != null)
        {
            var bgRawImageGO = new GameObject("Background_RawImage");
            bgRawImageGO.transform.SetParent(canvasGO.transform);
            bgRawImage = bgRawImageGO.AddComponent<RawImage>();
            //bgRawImage.fillCenter = true;
            bgRawImage.texture = bgTexture;
            if (sizeDelta == Vector2.zero)
                sizeDelta = new Vector2(bgTexture.width, bgTexture.height);
            bgRawImage.rectTransform.sizeDelta = sizeDelta;
        }

        //create text ui
        var textGO = new GameObject("ChatText");
        textGO.transform.SetParent(canvasGO.transform);
        textUI = textGO.AddComponent<Text>();
        textUI.rectTransform.sizeDelta = Vector2.zero;
        textUI.rectTransform.anchorMin = Vector2.zero;
        textUI.rectTransform.anchorMax = Vector2.one;
        textUI.rectTransform.anchoredPosition = new Vector2(.5f, .5f);
        if (font)
            textUI.font = font;
        else
            textUI.font = Resources.FindObjectsOfTypeAll<Font>()[0];
        textUI.fontSize = fontSize;
        textUI.color = Color.black;
        textUI.alignment = TextAnchor.MiddleCenter;

        ResetCanvasUIs();

        //if time interval is less than 0.01 sec, then auto pop
        autoPop = (timeInterval >= (0.01f)) ? true : false;
	}
	
	// Update is called once per frame
	void Update () {
        PopChatUI();
	}

    /// <summary>
    /// limit canvas uis ,based on size delta
    /// </summary>
    void LimitCanvasUIsBasedOnSizeDelta()
    {
        if (sizeDelta == Vector2.zero)
        {
            //reset canvas and position;
            float width = Mathf.Max(sizeDelta.x, textUI.preferredWidth);
            float height = Mathf.Max(sizeDelta.y, textUI.preferredHeight);
            //Debug.Log("chat bubble ui: width = " + width + "  height = " + height);
            sizeDelta = new Vector2(width, height);
        }
        ResetCanvasUIs(sizeDelta);
    }

    /// <summary>
    /// Auto canvas uis
    /// </summary>
    void AutoCanvasUIs()
    {
        //reset canvas and position;
        float width = Mathf.Max(sizeDelta.x, textUI.preferredWidth);
        float height = Mathf.Max(sizeDelta.y, textUI.preferredHeight);
        ResetCanvasUIs(new Vector2(width, height));
    }
    
    /// <summary>
    /// based on image and text to set right size and move canvas;
    /// </summary>
    void ResetCanvasUIs()
    {
        if (autoCanvasUIs)
            AutoCanvasUIs();
        else
            LimitCanvasUIsBasedOnSizeDelta();
    }
    void ResetCanvasUIs(Vector2 size)
    {
        if (size == Vector2.zero)
            return;

        sizeDelta = size;
        //reset canvas
        RectTransform rectTrans = worldCanvasUI.CanvasGO.GetComponent<RectTransform>();
        if (rectTrans)
        {
            rectTrans.sizeDelta = sizeDelta;    //reset canvas size;

            rectTrans.transform.localPosition = new Vector3(0, Mathf.FloorToInt(sizeDelta.y / 2), 0);  //move bottom of canvas is local object;
            //Vector3 pos = rectTrans.transform.localPosition;
            //rectTrans.transform.position = new Vector3(pos.x, pos.y + (width / 2), pos.z);  //move bottom of canvas is local object;
        }

        //reset image
        if (bgImage)
            bgImage.rectTransform.sizeDelta = sizeDelta;

        if (bgRawImage)
            bgRawImage.rectTransform.sizeDelta = sizeDelta;

        if (textUI)
            textUI.rectTransform.sizeDelta = sizeDelta;
    }

    /// <summary>
    /// pop chat ui
    /// </summary>
    void PopChatUI()
    {
        if(textUI == null)
            return;

        if (autoPop)
        {
            //auto pop btw time interval;
            curDeltaTime += Time.deltaTime;
            if (curDeltaTime >= timeInterval)
            {
                PopChat();                
            }
        }
        else
        {
            //pop next chat based on touch, mouse down, space pressed;
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || (Input.touchCount > 0))
            {
                PopChat();
            }
        }
    }

    //pop one chat
    void PopChat()
    {
        if(bgImage != null)
            sizeDelta = bgImage.rectTransform.sizeDelta;
        else if(bgRawImage != null)
            sizeDelta = bgRawImage.rectTransform.sizeDelta;
        else
            sizeDelta = Vector2.zero;

        if (chatStrList.Count > 0)
        {
            EnableChatBubbleUI(true);
            string chat = chatStrList[0];
            textUI.text = chat;
            chatStrList.RemoveAt(0);

            ResetCanvasUIs();
        }
        else if (HideAfterAllChatPoped)
            EnableChatBubbleUI(false);

        curDeltaTime = 0;
    }

    /// <summary>
    /// hide chat bubble ui;
    /// </summary>
    public void EnableChatBubbleUI(bool enable)
    {
        if (textUI)
            textUI.enabled = enable;

        if (bgImage)
            bgImage.enabled = enable;

        if (bgRawImage)
            bgRawImage.enabled = enable;
    }

    /// <summary>
    /// destory chat bubble ui;
    /// </summary>
    public void DestoryChatBubbleUI()
    {
        if (textUI)
        {            
            Object.DestroyImmediate(textUI);
            var textGO = textUI.gameObject;
            if(textGO)
                Object.DestroyImmediate(textGO);
        }

        if (bgImage)
        {            
            Object.DestroyImmediate(bgImage);
            var bgImageGO = bgImage.gameObject;
            if (bgImageGO)
                Object.DestroyImmediate(bgImageGO);
        }

        if (bgRawImage)
        {
            Object.DestroyImmediate(bgRawImage);
            var bgRawImageGO = bgRawImage.gameObject;
            if (bgRawImageGO)
                Object.DestroyImmediate(bgRawImageGO);
        }

        Object.Destroy(this);
    }

    /// <summary>
    /// add chat string to ready for pop
    /// </summary>
    /// <param name="chat"></param>
    public void AddChatString(string chat)
    {
        if (chat.Length <= 0)
            return;
        chatStrList.Add(chat);
    }
}
