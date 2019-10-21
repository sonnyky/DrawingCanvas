using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSelector : MonoBehaviour
{

    private Tool m_ActiveTool;

    [SerializeField]
    Tool[] m_Tools;

    public System.Action onToolChanged;

    public Color m_ActiveColor { get; set; }

    public int m_ActiveSize { get; set; }

    public void SetActiveTool(Tool newActiveTool)
    {
        m_ActiveTool = newActiveTool;
        if(newActiveTool.GetToolType().Equals( ToolType.DrawingTool))
        {
            m_ActiveColor = newActiveTool.gameObject.GetComponent<DrawingTool>().GetToolColor();
        }
        if (onToolChanged != null)
        {
            onToolChanged.Invoke();
        }
    }
}
