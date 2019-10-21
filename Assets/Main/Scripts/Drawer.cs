using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DrawingCanvas;

public class Drawer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    DrawableCanvas m_DrawableCanvas;

    private Color m_DefaultColor = new Color(0f, 0f, 0f, 1f);

    private Color m_ActiveColor;

    private DrawerSizeType m_DrawerSize;
    private int m_Size = 5;

    private Point? m_PreviousPoint;

    private ToolSelector m_ToolSelector;

    private DrawerSizeSelector m_DrawerSizeSelector;

    void Start()
    {
        m_DrawableCanvas.InitCanvas();
        m_ToolSelector = FindObjectOfType<ToolSelector>();
        m_DrawerSizeSelector = FindObjectOfType<DrawerSizeSelector>();
        m_ActiveColor = m_DefaultColor;
        if(m_ToolSelector != null)
        {
            m_ToolSelector.onToolChanged += OnToolChanged;
        }

        if(m_DrawerSizeSelector != null)
        {
            m_DrawerSizeSelector.onDrawerSizeChanged += OnSizeChanged;
        }
    }

   
    public void OnToolChanged()
    {
        m_ActiveColor = m_ToolSelector.m_ActiveColor;
    }

    public void OnSizeChanged()
    {
        m_DrawerSize = m_DrawerSizeSelector.m_ActiveDrawerSize;
        switch (m_DrawerSize)
        {
            case DrawerSizeType.Small:
                m_Size = 5;
                break;

            case DrawerSizeType.Medium:
                m_Size = 14;
                break;

            case DrawerSizeType.Large:
                m_Size = 20;
                break;

            default:
                m_Size = 5;
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Point point = ConvertToPoint(eventData);
        Debug.Log("Active color : " + m_ActiveColor);
        m_DrawableCanvas.DrawPoint(point, m_Size, m_ActiveColor);
        m_PreviousPoint = point;

        //onBeginDraw?.Invoke();
    }

    private Vector2 GetLocalPosition(Vector2 position)
    {
        var canvas = GetComponentInParent<Canvas>();
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            return transform.InverseTransformPoint(position);
        }

        Vector2 localPosition = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, position, canvas.worldCamera, out localPosition);

        return localPosition;
    }

    private Point ConvertToPoint(PointerEventData eventData)
    {
        Vector2 localPosition = GetLocalPosition(eventData.position);

        Vector2 normalizedPoint = Rect.PointToNormalized((transform as RectTransform).rect, localPosition);

        int x = Mathf.RoundToInt(m_DrawableCanvas.Width * normalizedPoint.x);
        int y = Mathf.RoundToInt(m_DrawableCanvas.Height * normalizedPoint.y);

        return new Point { x = x, y = y };
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_PreviousPoint = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        Point point = ConvertToPoint(eventData);

        float length = 0f;

        if (m_PreviousPoint.HasValue)
        {
            m_DrawableCanvas.DrawLine(m_PreviousPoint.Value, point, m_Size, m_ActiveColor);

            length = new Vector2(point.x - m_PreviousPoint.Value.x, point.y - m_PreviousPoint.Value.y).magnitude;
        }
        else
        {
            m_DrawableCanvas.DrawPoint(point, m_Size, m_ActiveColor);
        }

        //if (!m_SoundPlayer.IsPlaying() && length > 30f)
        //{
        //    m_SoundPlayer.Play(m_DrawingToolSelector.SelectedDrawingTool.Type.GetSeType().LoadClip());
        //}

        m_PreviousPoint = point;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_PreviousPoint = null;

        //onEndDraw?.Invoke();
    }
}
