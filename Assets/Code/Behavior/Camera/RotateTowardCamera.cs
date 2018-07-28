using UnityEngine;
using System.Collections;

/// <summary>
/// make this object is always rotate toward camera;;
/// </summary>
public class RotateTowardCamera : MonoBehaviour {

    Camera mainCamera = null;

    // Use this for initialization
	void Start () 
    {
    }
        
	// Update is called once per frame
	void Update () {
        if (mainCamera == null)
            mainCamera = SceneManager.m_gameScene.m_mainCamera.GetComponent<Camera>();

        if(mainCamera)
            OnRotateTowardCamera(mainCamera);
	}

    void OnRotateTowardCamera(Camera camera)
    {
        transform.rotation = Quaternion.LookRotation(camera.transform.forward, camera.transform.up);
    }
}
