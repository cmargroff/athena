using UnityEngine;
[RequireComponent(typeof(BaseStateMachineBehavior))]
public class SimpleDeath : AthenaMonoBehavior, IDeath
{
  public override void OnActive()
  {
    base.OnActive();
    gameObject.SetActive(false);
  }
}

