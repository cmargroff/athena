using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    if (component.GetType().GetInterface(stateType.Name) != null)
                    {
                        component.enabled = true;



                    }
                    else
                    {
                        component.enabled = false;
                    }
                }
            }

        }

    }
    protected abstract Type GetInitialState();
}

