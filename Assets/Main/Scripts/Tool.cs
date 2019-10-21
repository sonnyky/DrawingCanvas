using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public bool m_IsActive;

    public System.Action OnToolSelected;

    [SerializeField]
    ToolType m_ToolType;

    public ToolType GetToolType()
    {
        return m_ToolType;
    }
}
