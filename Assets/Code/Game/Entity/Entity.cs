using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour, IEntity
{
    

    public int ID { get { return gameObject.GetHashCode(); } }    

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static T Find<T>(GameObject go) where T : UnityEngine.MonoBehaviour
    {
        return go.GetComponent<T>();
    }
}
