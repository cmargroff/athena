using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[RequireComponent(typeof(BuildingHoverBehaviour))]
public  class BuildingInteractBehaviour:AthenaMonoBehavior
{
    [HideInInspector]
    public BuildingHoverBehaviour BuildingHover;
    protected override void Start()
    {
        BuildingHover=GetComponent<BuildingHoverBehaviour>();
    }

    public virtual void Interact()
    {
        Debug.Log("Building interaction");
    }


}
