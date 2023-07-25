using System;

public class LifeAndDeathStateMachine : BaseStateMachineBehavior
{
    protected override Type GetInitialState() {
        return typeof(ISpawn);
    }
}

