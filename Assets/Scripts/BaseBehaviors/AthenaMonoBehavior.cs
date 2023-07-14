using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AthenaMonoBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    protected GameManagerBehavior _gameManager;
    protected virtual void Start()
    {
        _gameManager = FindObjectOfType<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        ContinuousUpdate();
        if (_gameManager.Paused)
        {
            WhenPausedUpdate();
        }
        else
        {
            PausibleUpdate();
        }
    }
    void FixedUpdate()
    {
        ContinuousFixedUpdate();
        if (_gameManager.Paused)
        {
            WhenPausedFixedUpdate();
        }
        else
        {
            PausibleFixedUpdate();
        }
    }

    protected virtual void ContinuousUpdate()
    { 
    }
    protected virtual void PausibleUpdate()
    {
    }
    protected virtual void WhenPausedUpdate()
    {
    }
    protected virtual void ContinuousFixedUpdate()
    {
    }
    protected virtual void PausibleFixedUpdate()
    {
    }
    protected virtual void WhenPausedFixedUpdate()
    {
    }


    //protected T SafeGetComponent<T>()
    //{
    //    T obj = this.GetComponent<T>();
    //    if (obj == null|| obj.ToString()=="null")
    //    {
    //        throw new System.Exception($"{typeof(T).Name} not found");
    //    }
    //    return obj;
    //}

    protected void SafeAssigned<T>(T obj)
    {
        if (obj == null|| obj.ToString()=="null")
        {
            throw new System.Exception($"{typeof(T).Name} not assigned in editor");
        }
    }

}
