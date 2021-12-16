using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Some logic that will make the stroke track 
/// with the transform of our pen point
/// </summary>
public class Stroke : MonoBehaviour
{
    /*TODO: -find an alternative to GameObject.Find("PenPoint")
            -connect pen point so that it matches the size of our paint stroke
            instead of controlling through the editor (Pt. 2 of directions)
            -TBD
    */

    private GameObject penPoint;

    // Start is called before the first frame update
    void Start()
    {
        penPoint = GameObject.Find("PenPoint");
    }
    // Update is called once per frame
    void Update()
    {
        enableStroke();
    }

    private void enableStroke()
    {
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


}
