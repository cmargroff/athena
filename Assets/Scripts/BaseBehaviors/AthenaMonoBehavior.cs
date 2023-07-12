using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AthenaMonoBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager _gameManager;
    protected virtual void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
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

    public virtual void ContinuousUpdate()
    { 
    }
    public virtual void PausibleUpdate()
    {
    }
    public virtual void WhenPausedUpdate()
    {
    }
    public virtual void ContinuousFixedUpdate()
    {
    }
    public virtual void PausibleFixedUpdate()
    {
    }
    public virtual void WhenPausedFixedUpdate()
    {
    }
     
    
        
    

}
