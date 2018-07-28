using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{
    public Transform[] raycastPoints;
    public LayerMask raycastMask;
    public float m_seaGauge = 0.1f; //do raycast to detect shallowWater

    public float mSpeed = 0f;    
    public ShipUnit mShipUnit = null;
    
    void Start()
    {
        mShipUnit = ShipUnit.Find<ShipUnit>(gameObject);

        if (mShipUnit == null)
            Object.Destroy(this);

        raycastMask |= (1 << LayerMask.NameToLayer(LayerDef.Water));
        raycastMask = ~raycastMask;
    }
 
	
	// Update is called once per frame
	void Update () {
	
	}

    protected bool IsShallowWater()
    {
        // Determine if the ship has hit shallow water
        if (raycastPoints != null)
        {
            foreach (Transform point in raycastPoints)
            {
                if (Physics.Raycast(point.position, Vector3.down, m_seaGauge, raycastMask))
                {
                    Debug.Log("Shallow waterr, can't move!");
                    return true;
                }
            }
        }

        return false;
    }
}
