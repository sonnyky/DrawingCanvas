using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerSizeSelector : MonoBehaviour
{

    public DrawerSizeType m_ActiveDrawerSize;

    public System.Action onDrawerSizeChanged;

    public void SetNewDraweSize(DrawerSizeType newSize)
    {
        m_ActiveDrawerSize = newSize;
        if(onDrawerSizeChanged != null)
        {
            onDrawerSizeChanged.Invoke();
        }
    }
   
}
