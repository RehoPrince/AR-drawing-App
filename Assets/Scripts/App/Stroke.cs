using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Some logic that will make the stroke track 
/// with the transform of our pen point
/// </summary>
public class Stroke : MonoBehaviour
{
    /*TODO: 
            - connect pen point so that it matches the size of our paint stroke
            instead of controlling through the editor (Pt. 2 of directions)
            - find alt solution to getting pen point variable
    */

    private Transform penPoint; //calling in the pen point from the Draw script

    // Start is called before the first frame update
    void Start()
    {
        penPoint = GameObject.FindObjectOfType<Draw>().penPoint;

    }
    // Update is called once per frame
    void Update()
    {
        // if (!Draw.drawing) return;

        // if (PenPointController.surfaceDrawingMode)
        // {
        //     enableSurfaceStroke();
        // }
        // else
        // {
        //     enableSpaceStroke();
        // }

        penPoint = GameObject.FindObjectOfType<Draw>().penPoint;
        
        if (Draw.drawing)
        {
            this.transform.position = penPoint.transform.position;
            this.transform.rotation = penPoint.transform.rotation;
        }
        else
        {
            this.enabled = false;
        }
    }

    // private void enableSurfaceStroke()
    // {
    //     if (Draw.drawing)
    //     {
    //         this.transform.position = surfacePenPoint.transform.position;
    //         this.transform.rotation = surfacePenPoint.transform.rotation;
    //     }
    //     else
    //     {
    //         this.enabled = false;
    //     }
    // }

    // private void enableSpaceStroke()
    // {
    //     if (Draw.drawing)
    //     {
    //         this.transform.position = spacePenPoint.transform.position;
    //         this.transform.rotation = spacePenPoint.transform.rotation;
    //     }
    //     else
    //     {
    //         this.enabled = false;
    //     }
    // }


}
