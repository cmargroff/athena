using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseStateMachineBehavior:MonoBehaviour
{
    protected Type _currentState;

    public void SetInitialState(GameObject gameObject)
    {
        if (_currentState == null)
        {
            SetState(gameObject, GetInitialState());
        }
    }

    public void SetState(GameObject gameObject, Type stateType)
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

