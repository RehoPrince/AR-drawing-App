using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class StartMode : MonoBehaviour
{

    [SerializeField] string nextMode = "Scan";

    // On Enable is called when the Gameobject becomes enabled and active
    void OnEnable()
    {
        UIController.ShowUI("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (ARSession.state <= ARSessionState.None) return; //Guardian code

        if(ARSession.state == ARSessionState.Unsupported)
        {
            InteractionController.EnableMode("NonAR");
        }
        else if(ARSession.state >= ARSessionState.Ready)
        {
            InteractionController.EnableMode("Scan");
        }


    }

    
}
