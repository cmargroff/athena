public  class ItemShopCanvasBehavior : ShopCanvasBehavior<PowerUpSO>
{
    protected override string GetTitle()
    {
        return "Items";
    }
  public override void Buy(PowerUpSO item)
  {
    throw new System.NotImplementedException();
  }
}