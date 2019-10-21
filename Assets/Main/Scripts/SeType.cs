using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeType
{
    ClickedSendButton = 1,
    ClickedBackButton = 2,
    ClickedSmallChalk = 3,
    ClickedMiddleChalk = 4,
    ClieckedLargeChalk = 5,
    Chalk = 6,
    Eraser = 7,
    SuccessFlick = 8,
    FailedFlick = 9,
}

public static class SeTypeExtension
{
    public static AudioClip LoadClip(this SeType seType)
    {
        return Resources.Load<AudioClip>(string.Format("Sounds/SE/se_{0:D3}", (int)seType));
    }
}