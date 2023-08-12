public class ShopBuildingBehavior: BuildingInteractBehaviour
{
    public ShopTypeEnum ShopType;

    public override void Interact()
    {
        base.Interact();
        _gameManager.Paused=true;
        switch (ShopType)
        {
            case ShopTypeEnum.Weapon:
                _gameManager.WeaponShop.gameObject.SetActive(true);
                _gameManager.WeaponShop.BuildShop();
                break;
            case ShopTypeEnum.Military:
                _gameManager.MilitaryShop.gameObject.SetActive(true);
                _gameManager.MilitaryShop.BuildShop();
                break;
            default:
                _gameManager.PowerUpShop.gameObject.SetActive(true);
                _gameManager.PowerUpShop.BuildShop();
                break;
        }

        
    }

    public enum ShopTypeEnum
    {
        Weapon,
        Powerup,
        Military
    }
}

