using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public  class BuildingHoverBehaviour:AthenaMonoBehavior
{
    [SerializeField]
    private GameObject _hoverIndicator;

    protected override void Start()
    {
        base.Start();
        SafeAssigned(_hoverIndicator);
    }

    public void EnableHoverIndicator()
    {
        _hoverIndicator.SetActive(true);
    }
    public void DisableHoverIndicator()
    {
        _hoverIndicator.SetActive(false);
    }

}
