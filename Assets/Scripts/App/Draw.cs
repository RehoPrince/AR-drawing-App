using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;


/// <summary>
/// A script we can use to test the app in the editor w/o a build.
/// Guide: use the mouse to look around the scene.
/// Enable “Mouse Look Testing” and then hit play. Uncheck when exporting project
/// </summary>
public class Draw : MonoBehaviour
{
    /*TODO: 
            - find better alt to FindObjectOfType in the Start method
            - TBD
    */

    public TrackableType surfaceToDetect;
    private ARRaycastManager arOrigin;

    public GameObject spacePenPoint;
    public GameObject surfacePenPoint;
    public GameObject stroke;
    public static bool drawing = false;

    [HideInInspector]
    public Transform penPoint;

    public bool mouseLookTesting;
    private float pitch = 0;
    private float yaw = 0;

    #region MonoBehaviour Functions
    // Start is called before the first frame update
    void Start()
    {
        arOrigin = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        LookWithMouse();
        PenModeSelect();
        RaycastDetection();

    }


    #endregion
    #region Stroke Functions
    /// <summary>
    /// Mouse can be used to look around the scene
    /// if mouseLookTesting == true
    /// </summary>
    private void LookWithMouse()
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
        GameObject currentStroke = Instantiate(stroke, penPoint.transform.position, penPoint.transform.rotation);
    }

    public void EndStroke()
    {
        drawing = false;
    }

    /// <summary>
    /// Determines the Pen Point to use based on current Drawing Mode
    /// </summary>
    public void PenModeSelect()
    {
        if (PenPointController.surfaceDrawingMode)
        {
            penPoint = surfacePenPoint.transform;

            surfacePenPoint.SetActive(true);
            spacePenPoint.SetActive(false);
            // Debug.LogError("Surface drawing has not been setup yet!");
        }
        else
        {
            penPoint = spacePenPoint.transform;

            surfacePenPoint.SetActive(false);
            spacePenPoint.SetActive(true);
            // Debug.Log("Drawing in 3D space now");
        }
    }

    /*Explanation from tutorial:
        we send out a ray from the center of the camera into AR detected space.
        Then we use the raycast method on the ARRaycastManager to see if we’ve hit
        the surface we wanted to detect. And finally, we update the position of our
        pen point to be the where the ray collides with our detected surface.
    */
    private void RaycastDetection()
    {
        Vector3 centerPoint = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));

        List<ARRaycastHit> validHits = new List<ARRaycastHit>();
        arOrigin.Raycast(centerPoint, validHits, surfaceToDetect);

        gameObject.transform.position = validHits[0].pose.position;
        gameObject.transform.rotation = validHits[0].pose.rotation;
    }

    #endregion

}
