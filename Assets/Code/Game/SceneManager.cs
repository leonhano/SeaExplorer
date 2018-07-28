using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public static GameScene m_gameScene;
	// Use this for initialization
	void Start () {

        //add fps tool
        gameObject.AddComponent<HUDFPS>();

        InitScene();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitScene()
    {
        m_gameScene = new GameScene();
    }
}

public class GameScene
{
    public readonly GameObject m_waterGO = null;
    public readonly GameObject m_terrainGO = null;
    public readonly GameObject m_mainCamera = null;
    public readonly GameObject m_player = null;

    public GameScene()
    {
        m_terrainGO = GameObject.FindWithTag(TagDef.Terrain);
        m_waterGO = GameObject.FindWithTag(TagDef.Water);
        m_mainCamera = GameObject.FindGameObjectWithTag(TagDef.MainCamera);
        m_player = GameObject.FindGameObjectWithTag(TagDef.Player);
    }
}