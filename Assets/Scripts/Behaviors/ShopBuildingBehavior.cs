public class ShopBuildingBehavior: BuildingInteractBehaviour
{
    public ShopTypeEnum ShopType;

    public override void Interact()
    {
        base.Interact();
        _gameManager.Paused=true;
        if (ShopType == ShopTypeEnum.Weapon)
        {
            _gameManager.WeaponShop.gameObject.SetActive(true);
            _gameManager.WeaponShop.BuildShop();
        }
        else
        {
            _gameManager.PowerUpShop.gameObject.SetActive(true);
            _gameManager.PowerUpShop.BuildShop();
        }
    }

    public enum ShopTypeEnum
    {
        Weapon,
        Powerup
    }
}

