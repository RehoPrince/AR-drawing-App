using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A Controller class that switches between Pen points
/// based on Drawing Modes: Surface (AR) or Space (Non-AR)
/// </summary>
public class PenPointController : Singleton<PenPointController>
{
    public static bool surfaceDrawingMode =  true;

    public static void SurfaceDraw()
    {
        surfaceDrawingMode = true;
    }

    public static void SpaceDraw()
    {
        surfaceDrawingMode = false;
    }
}
