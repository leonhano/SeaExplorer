using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldCanvasUI : MonoBehaviour
{
    public Canvas worldCanvas = null;   //worldCanvas;
    public RenderMode renderMode = RenderMode.WorldSpace;

    protected GameObject canvasGO = null;
    public GameObject CanvasGO { get { return canvasGO; } }

	// Use this for initialization
	void Awake () {        
        //set worldCanvas;
        InitWorldCanvasUI();
	}
	
	// Update is called once per frame
	void Update () {	
	}

    /// <summary>
    /// init world canvas;
    /// </summary>
    void InitWorldCanvasUI()
    {        
        string name = gameObject.name + "_WorldCanvas";

        //check whether this canvas has been existed?
        Canvas tempCanvas = gameObject.GetComponentInChildren<Canvas>();
        if ((tempCanvas != null) && (tempCanvas.name == name))
           worldCanvas = tempCanvas;
        
        if (worldCanvas == null)
        {
            canvasGO = new GameObject(name);
            canvasGO.transform.SetParent(gameObject.transform);
            worldCanvas = canvasGO.AddComponent<Canvas>();
        }

        GraphicRaycaster graphicRaycaster = canvasGO.GetComponent<GraphicRaycaster>();
        if (graphicRaycaster != null)
        {
            graphicRaycaster = canvasGO.AddComponent<GraphicRaycaster>();
        }

        worldCanvas.renderMode = renderMode;
    }
    
    
}
