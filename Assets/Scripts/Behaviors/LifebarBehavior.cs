using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer))]
public class LifebarBehavior : AthenaMonoBehavior
{
    private Material _material;

    // Start is called before the first frame update
    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }
    protected override void Start()
    {
        base.Start();
    
    }

    public void SetHealthPercent(float percent)
    {
        _material.SetFloat("_LifePercentage", percent);
    }

    protected override void  ContinuousUpdate()
    {

        transform.forward=Camera.main.transform.forward;
        transform.Rotate(90, 180, 0);


    }

}
