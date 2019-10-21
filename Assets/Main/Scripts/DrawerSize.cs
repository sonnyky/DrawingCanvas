using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawerSize : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    DrawerSizeType m_DrawerSize;

    DrawerSizeSelector m_DrawerSizeSelector;

    void Start()
    {
        m_DrawerSizeSelector = FindObjectOfType<DrawerSizeSelector>();
    }

    public DrawerSizeType GetDrawerSize()
    {
        return m_DrawerSize;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_DrawerSizeSelector.SetNewDraweSize(m_DrawerSize);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
