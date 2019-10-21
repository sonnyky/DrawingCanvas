using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawingTool : Tool, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    Color m_DrawToolColor;

    ToolSelector m_ToolSelector;

    [SerializeField]
    DrawingToolType m_Type;

    [SerializeField]
    DrawerSize m_Size;

    private Vector3 m_CurrentPosition;
    private Vector3 m_SelectedPosition;

    private void Start()
    {
        m_ToolSelector = FindObjectOfType<ToolSelector>();
        m_CurrentPosition = GetComponent<RectTransform>().localPosition;
        m_SelectedPosition = m_CurrentPosition + (Vector3.up * 100f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_ToolSelector.SetActiveTool(gameObject.GetComponent<DrawingTool>());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public Color GetToolColor()
    {
        return m_DrawToolColor;
    }
}
