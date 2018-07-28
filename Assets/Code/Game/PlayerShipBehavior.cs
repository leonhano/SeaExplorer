using UnityEngine;
using System.Collections;

public class PlayerShipBehavior : MonoBehaviour {

    bool bIsInteractiving = false;

	// Use this for initialization
	void Start () {
        //Physics.IgnoreCollision(this.collider, SceneManager.m_gameScene.m_waterGO.collider);
        //Physics.IgnoreCollision(this.collider, SceneManager.m_gameScene.m_terrainGO.collider);
        bIsInteractiving = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//     void OnCollisionEnter(Collision collision)
//     {
//         Debug.Log("OnCollisionEnter ");
//     }

    void OnTriggerEnter(Collider other)
    {
        GameObject colliderGO = other.gameObject;
        if (colliderGO.tag == TagDef.Water || colliderGO.tag == TagDef.Terrain)
            return;


        if (colliderGO.tag == TagDef.Port)
            InteractiveWithPort(colliderGO);
        else if (colliderGO.tag == TagDef.NPC)
            InteractiveWithNPC(colliderGO);
        else
        {
            Debug.Log("Unknown behavior. collider = " + colliderGO.name);
        }
    }

//     void OnCollisionExit(Collision collisionInfo)
//     {
//         Debug.Log("OnCollisionExit ");
//     }
    void OnTriggerExit(Collider other) 
    {
        GameObject colliderGO = other.gameObject;
        if (colliderGO.tag == TagDef.Water || colliderGO.tag == TagDef.Terrain)
            return;


        if (colliderGO.tag == TagDef.Port)
            FinishInteractiveWithPort(colliderGO);
        else if (colliderGO.tag == TagDef.NPC)
            FinishInteractiveWithNPC(colliderGO);
        else
        {
            Debug.Log("Unknown behavior. collider = " + colliderGO.name);
        }
    }

    void HighlightCollider(GameObject colliderGO, bool highlight)
    {
        Highlightable highlightScript = GetComponent<Highlightable>();
        if (highlightScript == null)
            highlightScript = colliderGO.AddComponent<Highlightable>();
        highlightScript.SetAllChildrenHighlightable(highlight);
    }

    /// <summary>
    /// Interactive with port;
    /// </summary>    
    void InteractiveWithPort(GameObject port)
    {
        bIsInteractiving = true;
        Debug.Log("InteractiveWithPort -->  port:" + port.name);

        HighlightCollider(port, true);

        port.SendMessage(Command.InteractiveWithPlayer, this, SendMessageOptions.DontRequireReceiver);
    }

    void FinishInteractiveWithPort(GameObject port)
    {
        bIsInteractiving = false;
        Debug.Log("FinishInteractiveWithPort -->  port:" + port.name);

        HighlightCollider(port, false);
        port.SendMessage(Command.FinishInteractiveWithPlayer, this, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Interactive with NPC;
    /// </summary>    
    void InteractiveWithNPC(GameObject npc)
    {
        bIsInteractiving = true;
        Debug.Log("InteractiveWithNPC -->  NPC:" + npc.name);

        HighlightCollider(npc, true);

        npc.SendMessage(Command.InteractiveWithPlayer, gameObject, SendMessageOptions.DontRequireReceiver);
    }

    void FinishInteractiveWithNPC(GameObject npc)
    {
        bIsInteractiving = false;
        HighlightCollider(npc, false);
        Debug.Log("FinishInteractiveWithNPC -->  NPC:" + npc.name);

        npc.SendMessage(Command.FinishInteractiveWithPlayer, this, SendMessageOptions.DontRequireReceiver);
    }
}
