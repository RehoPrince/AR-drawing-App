using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum instructionUI
{
    None = 0, CrossPlatformFindAPlane, FindAFace, FindABody,
    FindAnImage, FindAnObject, ARKitCoachingOverlay, TapToPlace
};
public class AnimatedPrompt : MonoBehaviour
{
    [SerializeField] instructionUI instruction;

    [SerializeField] ARUXAnimationManager arAnimManager;

    bool isStarted;

    // Start is called before the first frame update
    void Start()
    {
        ShowInstructions();
        isStarted = true;
    }

    void OnEnable()
    {
        if (isStarted) ShowInstructions();
    }

    void OnDisable()
    {
        arAnimManager.FadeOffCurrentUI();
    }

    void ShowInstructions()
    {
        switch (instruction)
        {
            case instructionUI.None:
                //Does nothing
                break;
            case instructionUI.CrossPlatformFindAPlane:
                arAnimManager.ShowCrossPlatformFindAPlane();
                break;
            case instructionUI.FindAFace:
                arAnimManager.ShowFindFace();
                break;
            case instructionUI.FindABody:
                arAnimManager.ShowFindBody();
                break;
            case instructionUI.FindAnImage:
                arAnimManager.ShowFindImage();
                break;
            case instructionUI.FindAnObject:
                arAnimManager.ShowFindObject();
                break;
            case instructionUI.ARKitCoachingOverlay:
                //Does nothing
                break;
            case instructionUI.TapToPlace:
                arAnimManager.ShowTapToPlace();
                break;
            default:
                Debug.LogError("Instruction switch missing, please ensure instruction" + instruction + "exists");
                break;
        }
    }
}
