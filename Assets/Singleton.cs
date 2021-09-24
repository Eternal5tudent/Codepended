using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    [SerializeField] private bool persistBetweenScenes;
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<T>();
            if (persistBetweenScenes)
            {
                if(transform.parent != null)
                {
                    transform.parent = null;
                }
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
