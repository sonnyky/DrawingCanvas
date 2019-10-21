using UnityEngine;

namespace DrawingCanvas
{
    public struct Point
    {
        public int x;
        public int y;

        public static Point Lerp(Point a, Point b, float t)
        {
            float x = Mathf.Lerp(a.x, b.x, t);
            float y = Mathf.Lerp(a.y, b.y, t);
            return new Point { x = (int)x, y = (int)y };
        }
    }
}