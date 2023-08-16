public class ShopBuildingBehavior: BuildingInteractBehaviour
{
    public ShopTypeEnum ShopType;

    public override void Interact()
    {
        base.Interact();
        _gameManager.ShowShop(ShopType);
    }

    public enum ShopTypeEnum
    {
        Weapon,
        Powerup
    }
}

