using System;
using UnityEngine;

public abstract class BaseStateMachineBehavior : MonoBehaviour
{
    protected Type _currentState;

    public void SetInitialState()
    {
        if (_currentState == null)
        {
            SetState(GetInitialState());
        }
    }
    public void ResetInitialState()
    {

        SetState(GetInitialState());

    }
    public void SetState(Type stateType)
    {
        if (_currentState != stateType)
        {
            var components = gameObject.GetComponentsInChildren<MonoBehaviour>(true);
            foreach (var component in components)
            {
                _currentState = stateType;
                if (component is IState)
                {
                    component.enabled = component.GetType().GetInterface(stateType.Name) != null;
                }
            }

        }

    }
    protected abstract Type GetInitialState();
}

