using UnityEngine;
[RequireComponent(typeof(BuildingUsableIndicator))]
public class ShopBuildingBehavior : BuildingInteractBehaviour
{
    public ShopTypeEnum ShopType;
    public ShopCanvasBehavior Shop;
    public int MinimumCost = 5; // todo:set this value for real
    private IndicatorBehavior _indicator;
    private BuildingUsableIndicator _buildingUsableIndicator;

    //private void Awake()
    //{

    //}

    protected override void Start()
    {
        base.Start();
        SetupIndicator();
        _buildingUsableIndicator = GetComponent<BuildingUsableIndicator>();

        _gameManager.OnInventoryChanged.AddListener((type) =>
        {
            if (type == PickupTypeEnum.Coin)
            {
                CoinsChanged(_gameManager.Pickups[PickupTypeEnum.Coin]);
            }
        });
        Shop = _gameManager.GetShop(ShopType);
        if (Shop == null)
        {
            Debug.LogError($"Shop not found for {ShopType}");
            return;
        }

        Shop.OnMinCostChanged.AddListener((min) =>
        {
            MinimumCost = min;
        });
    }
    private void CoinsChanged(int amount = 0)
    {
        if (amount >= MinimumCost)
        {
            _buildingUsableIndicator.EnableHoverIndicator();
            _indicator.On = true;
        }
        else
        {
            _buildingUsableIndicator.DisableHoverIndicator();
            _indicator.On = false;
        }
    }
    public override void Interact()
    {
        base.Interact();
        _gameManager.ShowShop(ShopType);
    }
    private void SetupIndicator()
    {
        var obj = Instantiate(_indicatorTemplate, Vector3.zero, Quaternion.identity);
        obj.name = $"{name} warning";
        obj.transform.parent = transform;
        _indicator = obj.GetComponent<IndicatorBehavior>();
        _indicator.Target = transform;
        _indicator.gameObject.SetActive(true);
    }
    public enum ShopTypeEnum
    {
        Weapon,
        Powerup,
        Military
    }
}