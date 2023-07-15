using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehavior : AthenaMonoBehavior
{
    public bool Paused;

    public Collider Bounds;
    public GameObject Player;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SafeAssigned(Bounds);
    }

    // Update is called once per frame
    
}
