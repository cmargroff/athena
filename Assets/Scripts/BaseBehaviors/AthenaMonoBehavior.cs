using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AthenaMonoBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected GameManagerBehavior _gameManager;
    private bool _started;
    protected virtual void Start()
    {
        _gameManager = SafeFindObjectOfType<GameManagerBehavior>();
        //todo:add safe find
        OnActive();
        _started=true;
    }
    protected virtual void OnEnable()
    {
        if (_started)
        {
            OnActive();
        }
    }
    // Update is called once per frame

    private bool _lastPauseState;
    void Update()
    {
        if (_lastPauseState && !_gameManager.Paused)
        {
            OnUnPaused();
            
        }
        else if (!_lastPauseState && _gameManager.Paused)
        {
            OnPaused();
        }
        _lastPauseState = _gameManager.Paused;
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
    protected virtual void OnActive()
    { 
    
    }
    protected virtual void OnPaused()
    {
    }

    protected virtual void OnUnPaused()
    {
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


    protected T SafeFindObjectOfType<T>() where T : UnityEngine.Object
    {
        var obj = FindObjectOfType<T>();
        if (obj == null || obj.ToString() == "null")
        {
            throw new System.Exception($"{typeof(T).Name} not found in world");
        }
        return obj;
    }

}
