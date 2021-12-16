using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script we can use to test the app in the editor w/o a build.
/// Guide: use the mouse to look around the scene.
/// Enable “Mouse Look Testing” and then hit play. Uncheck when exporting project
/// </summary>
public class Draw : MonoBehaviour
{
    public GameObject spacePenPoint;
    public GameObject stroke;
    public bool mouseLookTesting;
    public static bool drawing = false;
    private float pitch = 0;
    private float yaw = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        lookWithMouse();
    }

    /// <summary>
    /// Mouse can be used to look around the scene
    /// if mouseLookTesting == true
    /// </summary>
    private void lookWithMouse()
    {
        if (mouseLookTesting)
        {
            yaw += 2 * Input.GetAxis("Mouse X");
            pitch -= 2 * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }

    public void StartStroke()
    {
        drawing = true;
        GameObject currentStroke = Instantiate(stroke, spacePenPoint.transform.position, spacePenPoint.transform.rotation);
    }

    public void EndStroke()
    {
        drawing = false;
    }

}
