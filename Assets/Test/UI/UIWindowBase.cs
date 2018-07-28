using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// UIWindowBase by Riolis - create draggable windows in 1 click - just drag this script to a panel with your window. Approved by Tim C :cool:
/// </summary>
public class UIWindowBase : MonoBehaviour, IDragHandler
{
	RectTransform m_transform = null;
	
	// Use this for initialization
	void Start () {
		m_transform = GetComponent<RectTransform>();
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		m_transform.position += new Vector3(eventData.delta.x, eventData.delta.y);
		
		// magic : add zone clamping if's here.
	}
}