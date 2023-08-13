using UnityEngine;
[RequireComponent(typeof(BuildingUsableIndicator))]
public class ShopBuildingBehavior: BuildingInteractBehaviour
{
    public ShopTypeEnum ShopType;
    public ShopBehavior Shop;


    public int MinimumCost=5;//todo:set this value for real

    private IndicatorBehavior _indicator;

    private BuildingUsableIndicator _buildingUsableIndicator;
    protected override void Start()
    {
        base.Start();
        SetupIndicator();
        _buildingUsableIndicator = GetComponent<BuildingUsableIndicator>();

        _gameManager.OnCoinsChanged.AddListener(CoinsChanged);

        switch (ShopType)
        {
            case ShopTypeEnum.Weapon:
                Shop= _gameManager.WeaponShop;
                break;
            case ShopTypeEnum.Military:
                Shop = _gameManager.MilitaryShop;
                break;
            default:
                Shop = _gameManager.PowerUpShop;
                break;
        }

        Shop.OnMinCostChanged.AddListener(() => MinimumCost=Shop.MinCost);
    }

    private void  CoinsChanged()
    {
        if (_gameManager.Pickups.GetValueOrDefault("Coin") >= MinimumCost)
        {
            _buildingUsableIndicator.EnableHoverIndicator();
            _indicator.On=true;
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
        _gameManager.Paused=true;
        switch (ShopType)
        {
            case ShopTypeEnum.Weapon:
                Shop.gameObject.SetActive(true);
            
                break;
            case ShopTypeEnum.Military:
                Shop.gameObject.SetActive(true);

                break;
            default:
                Shop.gameObject.SetActive(true);
                break;
        }
        Shop.BuildShop();
    }

    private void SetupIndicator()
    {
        var obj = Instantiate(_indicatorTemplate, Vector3.zero, Quaternion.identity);
        obj.name = $"{name} warning";
        obj.transform.parent=transform;
        _indicator = obj.GetComponent<IndicatorBehavior>();
        _indicator.Target = transform;

        //Warning.transform.parent = gameObject.transform;


        _indicator.gameObject.SetActive(true);
    }

    public enum ShopTypeEnum
    {
        Weapon,
        Powerup,
        Military
    }



}

