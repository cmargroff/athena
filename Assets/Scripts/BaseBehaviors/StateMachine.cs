using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IStateMachine
{
    void SetInitialState(GameObject gameObject);
    void SetState(GameObject gameObject, Type stateType);
}
public abstract class BaseStateMachine: IStateMachine
{
    protected Type _currentState;

    public void SetInitialState(GameObject gameObject)
    {
        SetState(gameObject,GetInitialState());
    }

    public void SetState(GameObject gameObject, Type stateType)
    {
        if (_currentState != stateType)
        {
            var components = gameObject.GetComponentsInChildren<MonoBehaviour>();
            foreach (var component in components)
            {
                if (component is IState)
                {
                    component.enabled = component.GetType().GetInterface(stateType.Name) != null;
                }
            }
        }
        _currentState = stateType;
    }
    protected abstract Type GetInitialState();
}

public class LifeAndDeathStateMachine : BaseStateMachine
{
    protected override Type GetInitialState() {
        return typeof(ISpawn);
    }
}

