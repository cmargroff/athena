using UnityEngine;

public class AthenaMonoBehavior : BaseMonoBehavior
{
    // Start is called before the first frame update
    protected GameManagerBehavior _gameManager;
    protected BaseStateMachineBehavior _stateMachine;
    private bool _started;

    protected virtual void Awake()
    {
        _gameManager = SafeFindObjectOfType<GameManagerBehavior>();
        _stateMachine = GetComponent<BaseStateMachineBehavior>();
    }

    protected virtual void Start()
    {
       


        OnActive();
        _started = true;
    }

    public virtual void DisabledStart()
    {
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
            PlausibleUpdate();
        }
    }
    public virtual void OnActive()
    {
        if (_stateMachine != null)
        {
            _stateMachine.SetInitialState();
        }
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
            PlausibleFixedUpdate();
        }
    }
    protected virtual void ContinuousUpdate()
    {
    }
    protected virtual void PlausibleUpdate()
    {
    }
    protected virtual void WhenPausedUpdate()
    {
    }
    protected virtual void ContinuousFixedUpdate()
    {
    }
    protected virtual void PlausibleFixedUpdate()
    {
    }
    protected virtual void WhenPausedFixedUpdate()
    {
    }


}