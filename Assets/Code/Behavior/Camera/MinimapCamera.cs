using UnityEngine;
using System.Collections;

public class MinimapCamera : MonoBehaviour {
    
    public Camera minimapCamera = null;
    Camera mainCamera = null;
	
    // Use this for initialization
	void Awake () {
        //minimapCamera = gameObject.camera;        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (mainCamera == null)
            mainCamera = SceneManager.m_gameScene.m_mainCamera.GetComponent<Camera>();

        if (Input.GetKey(KeyCode.Alpha1))
        {
            minimapCamera.GetComponent<Camera>().enabled = false;
            mainCamera.GetComponent<Camera>().enabled = true;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            mainCamera.GetComponent<Camera>().enabled = false;
            minimapCamera.GetComponent<Camera>().enabled = true;
        }
	}
}
