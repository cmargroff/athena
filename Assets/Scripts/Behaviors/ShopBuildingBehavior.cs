public class ShopBuildingBehavior: BuildingInteractBehaviour
{
    public ShopTypeEnum ShopType;

    public override void Interact()
    {
        base.Interact();
        _gameManager.ShowShop(ShopTypeEnum.Weapon);
    }

    public enum ShopTypeEnum
    {
        Weapon,
        Powerup
    }
}

