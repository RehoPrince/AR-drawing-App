using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using DG.Tweening;

/// <summary>
/// A Class that makes a dictionary in Unity Editor
/// </summary>
[System.Serializable]
public class UIPanelDictionary: SerializableDictionaryBase<string, CanvasGroup> { }

/// <summary>
/// A Singleton UI Controller class
/// </summary>
public class UIController : Singleton<UIController>
{
    [SerializeField] UIPanelDictionary uiPanels;

    CanvasGroup currentPanel;

    void Awake()
    {
        base.Awake();
        ResetAllUI();
    }



    void ResetAllUI()
    {
        foreach (CanvasGroup panel in uiPanels.Values)
        {
            panel.gameObject.SetActive(false);
        }
    }

    public static void ShowUI(string name)
    {
        Instance?._ShowUI(name);
    }

    void _ShowUI(string name)
    {
        CanvasGroup panel;

        if (uiPanels.TryGetValue(name, out panel))
        {
            ChangeUI(uiPanels[name]);
        }
        else
        {
            Debug.LogError("Undefined UI Panel: " + name);
        }
    }

    void ChangeUI(CanvasGroup panel)
    {
        if (panel == currentPanel) return;

        if (currentPanel) FadeOut(currentPanel);

        currentPanel = panel;
        if (panel) FadeIn(panel);
    }

    void FadeOut(CanvasGroup panel)
    {
        panel.DOFade(0f, .5f).OnComplete(()=> panel.gameObject.SetActive(false));
       
    }

    void FadeIn(CanvasGroup panel)
    {
        panel.gameObject.SetActive(true);
        panel.DOFade(1, .5f);
    }
}
