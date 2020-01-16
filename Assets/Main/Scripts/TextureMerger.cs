using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureMerger
{
    public static Texture2D MergeTextures(Texture2D guideBottom, Texture2D doodleTop)
    {
        Texture2D merged = new Texture2D(guideBottom.width, guideBottom.height);

        Texture2D topResized = Resize(doodleTop, guideBottom.width, guideBottom.height);

        for (int x = 0; x < guideBottom.width; x++)
        {
            for (int y = 0; y < guideBottom.height; y++)
            {
                Color guideColor = guideBottom.GetPixel(x, y);
                Color doodleColor = topResized.GetPixel(x, y);
                doodleColor = (doodleColor.a == 0f) ? guideColor : doodleColor;
                merged.SetPixel(x, y, doodleColor);
            }
        }

        merged.Apply();
        return merged;
    }

    public static Texture2D Resize(Texture2D source, int newWidth, int newHeight)
    {
        source.filterMode = FilterMode.Point;
        RenderTexture rt = RenderTexture.GetTemporary(newWidth, newHeight);
        rt.filterMode = FilterMode.Point;
        RenderTexture.active = rt;
        Graphics.Blit(source, rt);
        Texture2D nTex = new Texture2D(newWidth, newHeight);
        nTex.ReadPixels(new Rect(0, 0, newWidth, newWidth), 0, 0);
        nTex.Apply();
        RenderTexture.active = null;
        return nTex;

    }
}
