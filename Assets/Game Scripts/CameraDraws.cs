using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script we can use to test the app in the editor w/o a build.
/// Guide: use the mouse to look around the scene.
/// Enable “Mouse Look Testing” and then hit play. Uncheck when exporting project
/// </summary>
public class CameraDraws : MonoBehaviour
{
    
    public bool mouseLookTesting;
    private float pitch = 0;
    private float yaw = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseLookTesting)
        {
            yaw += 2 * Input.GetAxis("Mouse X");
            pitch -= 2 * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }


}
