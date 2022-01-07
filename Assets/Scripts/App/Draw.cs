using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;
using UnityEngine.UI;


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

    [Header("Trackable Type Vars")]
    [Space(5)]
    public TrackableType surfaceToDetect;

    [Header("Pens and Stroke Vars")]
    [Space(5)]
    public GameObject spacePenPoint;
    public GameObject surfacePenPoint;
    public GameObject stroke;

    [Header("Surface to Track")]
    [Space(5)]
    public Slider[] colorSliders; //Should be 3 or 4 in RGB or RGBA format in Order

    [Header("Color Spectrum Var")]
    [Space(10)]
    [Range(1, 255)]
    [Tooltip("This values determines how many color combinations would be available. " +
        "The higher the number the wider the spectrum")]
    public int spectrumIntensity = 10;

    [HideInInspector]
    public Transform penPoint;

    [Header("Draw State Vars")]
    [Space(5)]
    public static bool drawing = false;
    public bool mouseLookTesting;

    private float pitch = 0;
    private float yaw = 0;

    private ARRaycastManager arOrigin;

    private Color colorFromUI;

    #region MonoBehaviour Functions
    // Start is called before the first frame update
    void Start()
    {
        //arOrigin = FindObjectOfType<ARRaycastManager>();
        arOrigin = gameObject.GetComponentInParent<ARRaycastManager>();
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
        ColorFromUI();
        drawing = true;
        GameObject currentStroke = Instantiate(stroke, penPoint.transform.position, penPoint.transform.rotation);
        currentStroke.GetComponent<Stroke>().strokeColor = colorFromUI;
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
            

            surfacePenPoint.SetActive(true);
            spacePenPoint.SetActive(false);

            penPoint = surfacePenPoint.transform;
            penPoint.GetComponent<Renderer>().material.color = colorFromUI;
            // Debug.LogError("Surface drawing has not been setup yet!");
        }
        else
        {
            

            surfacePenPoint.SetActive(false);
            spacePenPoint.SetActive(true);

            penPoint = spacePenPoint.transform;
            penPoint.GetComponent<Renderer>().material.color = colorFromUI;
            // Debug.Log("Drawing in 3D space now");
        }
    }


    /// <summary>
    /// Convert values from slider UI to Color (RGB)
    /// </summary>
    private void ColorFromUI()
    {
        colorFromUI = new Color(colorSliders[0].value * spectrumIntensity, colorSliders[1].value * spectrumIntensity, colorSliders[2].value * spectrumIntensity);
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
