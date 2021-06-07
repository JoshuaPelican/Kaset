using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasChanger : MonoBehaviour
{
    public Canvas mainCanvas;

    public void CanvasSwapWorld()
    {
        mainCanvas.renderMode = RenderMode.WorldSpace;
    }

    public void CanvasSwapCamera()
    {
        mainCanvas.renderMode = RenderMode.ScreenSpaceCamera;
    }
}
