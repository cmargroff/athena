using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ShopBuildingBehavior:BuildingInteractBehaviour
{
    public override void Interact()
    {
        base.Interact();
        _gameManager.Paused=true;
        _gameManager.Shop.gameObject.SetActive(true);
    }
}

