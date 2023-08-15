
using UnityEngine;
[RequireComponent(typeof(BaseStateMachineBehavior))]
internal class SimpleSpawn : AthenaMonoBehavior, ISpawn
{
    public override void OnActive()
    {
        base.OnActive();
        _stateMachine.SetState(typeof(IAlive));
    }
}