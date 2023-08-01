public class ShopBuildingBehavior: BuildingInteractBehaviour
{
    public override void Interact()
    {
        base.Interact();
        _gameManager.Paused=true;
        _gameManager.Shop.gameObject.SetActive(true);
    }
}

