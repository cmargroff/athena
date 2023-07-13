using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehavior : AthenaMonoBehavior
{
    private Rigidbody _rb;
    protected override void Start()
    {
        base.Start();
        _rb=GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    protected  override void PausibleUpdate()
    {
        _rb.MovePosition(_rb.position + transform.forward * 10*Time.deltaTime);
        base.PausibleUpdate();
    }
}
