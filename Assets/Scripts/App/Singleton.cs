using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Singleton Class used for components
/// that requires only an Instance
/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    public static T Instance { get; private set; }

    /// <summary>
    /// Checks if an Instance already exists
    /// </summary>
    public static bool isInitialized
    {
        get { return Instance != null; }
    }

    protected virtual void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError($"Trying to instantiate a second Instance of singleton class {GetType().Name}");
        }
        else
        {
            Instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }
}
