using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[System.Serializable]
public class InteractionModesDict: SerializableDictionaryBase<string, GameObject> { }

public class InteractionController : Singleton<InteractionController>
{
    [SerializeField] InteractionModesDict interactionModesDict;

    GameObject currentMode;

    void Awake()
    {
        base.Awake();

        ResetAllModes();
    }

    void Start()
    {
        _EnableMode("Start");
    }

    void ResetAllModes()
    {
        foreach(GameObject modeGO in interactionModesDict.Values)
        {
            modeGO.SetActive(false);
        }
    }

    public static void EnableMode(string name)
    {
        Instance?._EnableMode(name);
    }

    void _EnableMode(string name)
    {
        GameObject mode;

        if (interactionModesDict.TryGetValue(name, out mode))
        {
            StartCoroutine(ChangeMode(mode));
        }
        else
        {
            Debug.LogError("Undefined Mode: " + name);
        }
    }

    IEnumerator ChangeMode(GameObject mode)
    {
        if(mode == currentMode)
        {
            yield break;
        }

        if (currentMode)
        {
            currentMode.SetActive(false);
        }

        currentMode = mode;

        if (mode)
        {
            mode.SetActive(true);
        }
    }
}
