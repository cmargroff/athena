using UnityEngine;

public class ShopBuildingBehavior: BuildingInteractBehaviour
{
    public ShopTypeEnum ShopType;

    public int MinimumCost=5;//todo:set this value for real

    private IndicatorBehavior _indicator;

    private BuildingUsableIndicator _buildingUsableIndicator;
    protected override void Start()
    {
        base.Start();
        SetupIndicator();
        _buildingUsableIndicator = GetComponent<BuildingUsableIndicator>();//todo: add required
    }

    protected override void PlausibleUpdate()//todo: this should be only checked on events
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

