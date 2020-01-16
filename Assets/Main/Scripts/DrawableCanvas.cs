using UnityEngine;
using UnityEngine.EventSystems;
using DrawingCanvas;
using UnityEngine.UI;

public class DrawableCanvas : MonoBehaviour
{
    private Texture2D m_CanvasImage;

    private Color m_CanvasColor = new Color(0f, 0f, 0f, 0f);

    public int Width { get { return m_CanvasImage == null ? 0 : m_CanvasImage.width; } }

    public int Height { get { return m_CanvasImage == null ? 0 : m_CanvasImage.height; } }

    public void InitCanvas()
    {
        if(m_CanvasImage == null)
        {
            m_CanvasImage = new Texture2D(1024, 1024);
        }
        m_CanvasImage.filterMode = FilterMode.Point;
        GetComponent<RawImage>().texture = m_CanvasImage;
        ClearCanvas();
    }

    public void DrawPoint(Point point, int size, Color color)
    {
        WritePoint(point, size, color);

        m_CanvasImage.Apply();
    }

    private void WritePoint(Point point, int size, Color color)
    {
        for (int x = -size; x < size; x++)
        {
            for (int y = -size; y < size; y++)
            {
                if (new Vector2(x, y).magnitude < size)
                {
                    m_CanvasImage.SetPixel(point.x + x, point.y + y, color);
                }
            }
        }
    }

    public void ClearCanvas()
    {
        for (int i = 0; i < m_CanvasImage.width; i++)
        {
            for (int j = 0; j < m_CanvasImage.height; j++)
            {
                m_CanvasImage.SetPixel(i, j, m_CanvasColor);
            }
        }
        m_CanvasImage.Apply();
    }

    public void DrawLine(Point beginPoint, Point endPoint, int size, Color color)
    {
        int diffY = beginPoint.y - endPoint.y;
        int diffX = beginPoint.x - endPoint.x;
        int stepx, stepy;

        if (diffY < 0)
        {
            diffY = -diffY;
            stepy = -1;
        }
        else
        {
            stepy = 1;
        }
        if (diffX < 0)
        {
            diffX = -diffX;
            stepx = -1;
        }
        else
        {
            stepx = 1;
        }

        diffY <<= 1;
        diffX <<= 1;

        float fraction = 0;

        if (diffX > diffY)
        {
            fraction = diffY - (diffX >> 1);
            while (Mathf.Abs(endPoint.x - beginPoint.x) > 1)
            {
                if (fraction >= 0)
                {
                    endPoint.y += stepy;
                    fraction -= diffX;
                }
                endPoint.x += stepx;
                fraction += diffY;

                WritePoint(endPoint, size, color);
            }
        }
        else
        {
            fraction = diffX - (diffY >> 1);
            while (Mathf.Abs(endPoint.y - beginPoint.y) > 1)
            {
                if (fraction >= 0)
                {
                    endPoint.x += stepx;
                    fraction -= diffY;
                }
                endPoint.y += stepy;
                fraction += diffX;

                WritePoint(endPoint, size, color);
            }
        }

        m_CanvasImage.Apply();
    }

}
