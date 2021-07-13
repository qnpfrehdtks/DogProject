using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{

    private bool m_isInitialize;
    static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject newInstance = GameObject.Find(typeof(T).Name);

                if (!newInstance)
                {
                    newInstance = new GameObject(typeof(T).Name);
                    _instance = newInstance.AddComponent<T>();
                    _instance.InitManager();

                    return _instance;
                }
                else
                {
                    _instance = newInstance.GetComponent<T>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void InitManager()
    {
        if(!m_isInitialize)
        {
            Initialize();
            return;
        }

        m_isInitialize = true;
    }

    protected abstract bool Initialize();
   
}
